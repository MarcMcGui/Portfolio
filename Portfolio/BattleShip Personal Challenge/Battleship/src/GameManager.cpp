#include "GameManager.h"
#include <iostream>
#include <cstdlib>
#include <string>
#include <map>
#include "ship.h"
#include<map>
#include<ctime>
#include <vector>    
#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>
#include <sstream>       

GameManager::GameManager() {
    cheat = false;
	running = true;

    // Initialize ships using std::vector
    ships = new ship[5];

	ships[0] = ship("battleship", 4, 'b');
    ships[1] = ship("cruiser", 3, 'c');
    ships[2] = ship("submarine", 3, 's');
    ships[3] = ship("destroyer", 2, 'd');
    ships[4] = ship("carrier", 5, 'a');

    cursorX = 0;
    cursorY = 0;
    hits = 0;
    rounds = 35;

    // Initialize grid
    for (int i = 0; i < 10; i++) {
        for (int j = 0; j < 10; j++) {
            grid[i][j] = '.';
        }
    }

	// Initialize SDL
	if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_EVENTS) != 0) {
		std::cerr << "SDL_Init Error: " << SDL_GetError() << std::endl;
		exit(1);
	}

	window = nullptr;      // Declare the window pointer
	renderer = nullptr; 
	
		// Create an SDL window
	window = SDL_CreateWindow("Battleship Game", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 800, 600, SDL_WINDOW_SHOWN);
	if (!window) {
		std::cerr << "SDL_CreateWindow Error: " << SDL_GetError() << std::endl;
		SDL_Quit();
		exit(1);
	}
	
		// Create a renderer for the window
	renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
	if (!renderer) {
		std::cerr << "SDL_CreateRenderer Error: " << SDL_GetError() << std::endl;
		SDL_DestroyWindow(window);
		SDL_Quit();
		exit(1);
	}
	

    placeShips();
}

void GameManager::restart() {
	cheat = false;
	rounds = 35;
	hits = 0;
	for (int i = 0; i < 10; i++) {
		for (int j = 0; j < 10; j++) {
			grid[i][j] = '.';
		}
	}
	delete[] ships;
	ships = new ship[5];
	ships[0] = ship("battleship", 4, 'b');
    ships[1] = ship("cruiser", 3, 'c');
    ships[2] = ship("submarine", 3, 's');
    ships[3] = ship("destroyer", 2, 'd');
    ships[4] = ship("carrier", 5, 'a');

	placeShips();
}

GameManager::~GameManager() {
	SDL_DestroyRenderer(renderer);
    SDL_DestroyWindow(window);
    SDL_Quit();
}

void GameManager::drawShip(ship batShip) {
	int pos = 0;
	unsigned seed = time(0);
	srand(seed);
	int startX = rand() % 10;
	int startY = rand() % 10;

	int direction = rand() % 4;
	int endX = startX;
	int endY = startY;
	switch (direction) {
	case 0:
		endY = startY + batShip.length;
		break;
	case 1:
		endX = startX + batShip.length;
		break;
	case 2:
		endY = startY - batShip.length;
		break;
	case 3:
		endX = startX - batShip.length;
		break;
	default:
		break;
	}

	coords* newCoords = batShip.isShipBlocked(*(new coords(startX, startY)), *(new coords(endX, endY)), grid);
	string xVal = grid[startX];
	char startingPoint = xVal.at(startY);

	while (newCoords[0].x != startX || newCoords[0].y != startY) {
		startX = rand() % 10;
		startY = rand() % 10;
		endX = startX;
		endY = startY;
		direction = rand() % 4;
		switch (direction) {
		case 0:
			endY = startY + batShip.length;
			break;
		case 1:
			endX = startX + batShip.length;
			break;
		case 2:
			endY = startY - batShip.length;
			break;
		case 3:
			endX = startX - batShip.length;
			break;
		default:
			break;
		}
		newCoords = batShip.isShipBlocked(*new coords(startX, startY), *new coords(endX, endY), grid);
	}

	if (newCoords != NULL) {
		for (int i = 0; i < batShip.length; i++) {
			grid[newCoords[i].x][newCoords[i].y] = batShip.symbol;
		}
	}



	delete newCoords;  

	
}



void GameManager::placeShips() {
	for (int i = 0; i < 5; i++) {
		drawShip(ships[i]);
	}
}

void GameManager::attackShip(char symbol) {
	for (int i=0; i < 5; i++) {
		if (ships[i].symbol == symbol) {
			ships[i].length -= 1;
		}
	}
}


void GameManager::drawMap() {
    // Clear the screen
    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255); // Black background
    SDL_RenderClear(renderer);

    // Draw the grid
    for (int i = 0; i < 10; i++) {
        for (int j = 0; j < 10; j++) {
            int x = j * 60; // Cell width (60px per cell)
            int y = i * 60; // Cell height (60px per cell)

            // Default to white (for water and empty spaces)
			SDL_SetRenderDrawBlendMode(renderer, SDL_BLENDMODE_NONE);
            SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

            if (grid[j][i] == '.' || (grid[j][i] != 'O' && grid[j][i] != 'X')) {
                // If the cell is empty (water), just show the background
                if (cursorY == i && cursorX == j) {
                    // Draw cursor on empty space in red
                    SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255); // Red for cursor
                }
            }
            else if (grid[j][i] == 'O') {
                // 'O' marks a miss, so we show it
                
				if (cursorY == i && cursorX == j) {
                    // Draw cursor on empty space in red
                    SDL_SetRenderDrawColor(renderer, 0, 0, 200, 100); // Red for cursor
                }
				else{
					SDL_SetRenderDrawColor(renderer, 0, 0, 255, 255); // Blue for misses
				}
            }
            else if (grid[j][i] == 'X') {
                // 'X' marks a hit, so we show it
                
				if (cursorY == i && cursorX == j) {
                    // Draw cursor on empty space in red
                    SDL_SetRenderDrawColor(renderer, 200, 0, 0, 100); // Red for cursor
                }
				else{
					SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255); // Red for hits
				}
            }

            // Draw each grid cell
            SDL_Rect cell = { x, y, 60, 60 }; // Each cell is a 60x60 square
            SDL_RenderFillRect(renderer, &cell);

        }
    }

    // Present the rendered content to the screen
    SDL_RenderPresent(renderer);
}

void GameManager::drawMapCheat() {
	cout << " ";
	for (int i = 0; i < 10; i++) {
		for (int j = 0; j < 10; j++) {
				if (cursorY == i && cursorX == j) cout << '[' << grid[j][i] << ']';
				else cout << grid[j][i];
			if (j == 9) cout << '\n';
			cout << " ";
		}
	}
}

void GameManager::lose() {

	std::cout << " YOU LOSE, FAILURE!!!";


	std::cout << "press any key to restart ...\n";

	char input;
	
	restart();
}

void GameManager::win() {
	std::cout << " YOU WIN!!!";
		
	std::cout << "press any key to restart ...\n";

	char input;
	std::cin >> input;


	restart();
		
}

//redraws map for every movement/action
void GameManager::update(SDL_Event &e) {
    // Event handling
    while (SDL_PollEvent(&e) != 0) {
        if (e.type == SDL_QUIT) {
            running = false;
        }

		drawMap();

        if (e.type == SDL_KEYDOWN) {
            switch (e.key.keysym.sym) {
                case SDLK_a: // Move left
                    if (cursorX > 0) cursorX -= 1;
                    break;
                case SDLK_s: // Move down
                    if (cursorY < 9) cursorY += 1;
                    break;
                case SDLK_d: // Move right
                    if (cursorX < 9) cursorX += 1;
                    break;
                case SDLK_w: // Move up
                    if (cursorY > 0) cursorY -= 1;
                    break;
                case SDLK_e: // Fire
                    if (grid[cursorX][cursorY] != '.' && rounds > 0 && grid[cursorX][cursorY] != 'O') {
                        attackShip(grid[cursorX][cursorY]);
                        grid[cursorX][cursorY] = 'X';
                        rounds -= 1;
                        hits += 1;
                    } else if (grid[cursorX][cursorY] == '.' && rounds > 0) {
                        grid[cursorX][cursorY] = 'O';
                        rounds -= 1;
                    }
                    break;
                case SDLK_r: // Toggle cheat mode
                    cheat = !cheat;
                    break;
                default:
                    break;
            }
        }
		if (rounds <= 0 && hits < 17) {
			lose();
		}
		if (hits >= 17 && rounds > 0) {
			win();
		}
		std::stringstream info;
		info << "Location: (" << cursorX << ", " << cursorY << ")\n";
		info << "Rounds left: " << rounds << "\n";
		info << "Ships destroyed:\n";
		for (int i = 0; i < 5; i++) {
			if (ships[i].length <= 0) {
				info << ships[i].shipName << "\n";
			}
			else {
				info << "_______\n";
			}
		}
	
		// Render text on the screen (e.g., on the right side)
		renderText(renderer, info.str(), 600, 50);  // Adjust the (600, 50) for position on screen
	
		// Present the screen
		SDL_RenderPresent(renderer);
    }

	// Function to render text to the screen

}

void GameManager::renderText(SDL_Renderer* renderer, const std::string& text, int x, int y) {
    TTF_Font* font = TTF_OpenFont("resources/arial.ttf", 24); // Load font, replace path if necessary
    if (!font) {
        std::cerr << "Failed to load font: " << TTF_GetError() << std::endl;
        return;
    }

    SDL_Color color = {255, 255, 255}; // White color
    SDL_Surface* textSurface = TTF_RenderText_Solid(font, text.c_str(), color);
    SDL_Texture* textTexture = SDL_CreateTextureFromSurface(renderer, textSurface);

    int textWidth = 0, textHeight = 0;
    SDL_QueryTexture(textTexture, NULL, NULL, &textWidth, &textHeight);

    // Create a rectangle to position the text
    SDL_Rect renderQuad = {x, y, textWidth, textHeight};

    // Render the text
    SDL_RenderCopy(renderer, textTexture, NULL, &renderQuad);

    // Clean up
    SDL_DestroyTexture(textTexture);
    SDL_FreeSurface(textSurface);
    TTF_CloseFont(font);
}


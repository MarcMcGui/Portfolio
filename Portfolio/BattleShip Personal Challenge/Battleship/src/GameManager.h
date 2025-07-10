#pragma once
#include <map>
#include <iostream>
#include <string>
#include "ship.h"
#include <map>
#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>      
using namespace::std;

class GameManager
{
public:
    bool cheat;
    bool running;
    ship *ships;
    char grid[10][10];
    int rounds;
    int hits;
    int cursorX;
    int cursorY;
    SDL_Window* window;
    SDL_Renderer* renderer;
    GameManager();
    ~GameManager();
    void attackShip(char symbol);
    void restart();
    void drawMap();
    void drawMapCheat();
    void drawShip(ship batShip);
    void placeShips();
    void win();
    void lose();
    void update(SDL_Event &e);
    void renderText(SDL_Renderer* renderer, const std::string& text, int x, int y);
    
private:
    
    
};


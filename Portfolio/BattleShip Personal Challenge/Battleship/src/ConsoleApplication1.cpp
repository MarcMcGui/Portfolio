// ConsoleApplication1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <cstdlib>
#include <string>
#include <map>
#include "GameManager.h"
#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>

using namespace::std;

int main()
{
    int num = 0;
    GameManager manager = *new GameManager();
    SDL_Event e;
    manager.running = true;
    while (manager.running)  
    {
        manager.update(e);
        SDL_Delay(16);
    }
    return 0;
   
}

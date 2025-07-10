#pragma once
#include <map>
#include <iostream>
#include <string>
#include <map>
#include "coords.h"

using namespace::std;
#ifndef _TOOLS_H_
#define _TOOLS_H_
class ship
{
public:
	string shipName;
	coords* isShipBlocked(coords start, coords end, char grid[10][10]);
	char symbol;
	int length;

	ship(string shipName, int length, char symbol);
	ship();
	
private:

};

#endif

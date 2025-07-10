#include "ship.h"
#include <string>
#include <iostream>
#include <cstdlib>
#include <string>
#include <map>
#include <map>
using namespace::std;


ship::ship() {
	this->shipName = "";
	this->length = 0;
}

ship::ship(string shipName, int length, char symbol) {

	this->symbol = symbol;
	this->shipName = shipName;
	this->length = length;
}

coords* ship::isShipBlocked(coords start, coords end, char grid[10][10]) {

	coords* finalCoords = new coords[this->length];
	if (end.x > 9 || end.y > 9 || end.x < 0 || end.y < 0) {
		finalCoords[0] = *new coords(-10, -10);
		return finalCoords;
	}


	finalCoords[0] = start;
	finalCoords[this->length - 1] = end;

	int coordValue = 1;

	int pointRangeX;
	int pointRangeY;
	int xDirection = 1;
	int yDirection = 1;
	if (end.x - start.x != 0) {
		pointRangeX = end.x - start.x;
		xDirection = (end.x - start.x) / abs(end.x - start.x);
	}
	else pointRangeX = 0;
	if (end.y - start.y != 0) {
		pointRangeY = end.y - start.y;
		yDirection = (end.y - start.y) / abs(end.y - start.y);
	}
	else pointRangeY = 0;
	while (coordValue < length) {
		int x = (abs(pointRangeX) > 0) ? start.x + (xDirection * coordValue) : start.x;
		int y = (abs(pointRangeY) > 0) ? start.y + (yDirection * coordValue) : start.y;
		char currentPoint = grid[x][y];
		string point(1, currentPoint);
		if (point.compare(".") != 0) {
			finalCoords[0] = *new coords(-10, -10);
			return finalCoords;
		}
		else {
			finalCoords[coordValue] = *new coords(x, y);
		}
		coordValue++;

	}
	return finalCoords;

}


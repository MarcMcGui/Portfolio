cmake_minimum_required(VERSION 3.10)
project(BattleShip)

# Find SDL2 package
find_package(SDL2 REQUIRED)
find_package(SDL2_ttf REQUIRED)

# Set the C++ standard
set(CMAKE_CXX_STANDARD 17)

# Include SDL2 headers
include_directories(${SDL2_INCLUDE_DIRS} ${SDL2_TTF_INCLUDE_DIRS})

# Add your source files
add_executable(BattleShip src/ConsoleApplication1.cpp src/GameManager.cpp src/ship.cpp src/coords.cpp)

# Link the SDL2 library
target_link_libraries(BattleShip ${SDL2_LIBRARIES} ${SDL2_TTF_LIBRARIES})


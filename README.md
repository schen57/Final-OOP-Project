# 3-19-2023 OOP Principal Mission Check Point Project 
 Meant to showcase Abstraction/Inheritance/Polymorphism/Encapsulation


Project Design Document
Project Goal: Create a project that highlights the following:
    a. Abstraction
    b. Inheirtance
    c. Polymorphism
    d. Encapsulation

Project title - Magic simulation 

Project Overview

The application:
Scenes in the project
1. Menu
2. Main Scene
3. Game over overlay


User Interactions:
Menu:
1. Able to start the game 
2. Menu has a background music to it
3. Shows the longest time survived


Main Scene:
1. Some background image 
2. Players are bounded within a movement range
3. Top down view
4. Shows the total enemy killed
5. Shows the player health on screen
6. Shows the time elapsed

Game over Overlays:
1. Displayes the game over text
2. Takes the user back to title screen
3. Summarize the total enemy killed
4. Summarize the total time elapsed 

User Interactions:
Player:
1. Control a shape to move horizontally (WASD keys)
2. A random power up is spawned with the player
3. The power up will fire in predefined patterns automatically
4. Player has a health amount and when health is depleted, the game is over

Power ups:
1. Power ups stack on each other
2. Power up are spawn randomly on the scene every x amount of time
3. Different power up would shoot different types of fireballs automatically (Fire, earth, water, wind, lightning)
4. Power up will have different cool downs 

Enemy:
5. Enemy will spawn from different directions and close in at the player
6. Enemy will have 3 types (Easy, Medium, Hard). They differentiate by their speed, size, life
7. Spawn density reduces as survived time goes on


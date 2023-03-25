Immediate Project Task List
3/25/2023
~~1. Add a weapon power pick up that increases the number of objects spawned~~
2. Spawn the power up randomly on the map for the player to pick up
3. Add a pause screen to display:
    - Tutorial Information (enemy information)
    - Player's current stat (max health, current health, number of projectiles, attack power)
2. Add another weapon system with a different movement pattern - rock - shot gun pattern - player position determine where the object is moving to 
3. The weappon when powered up, the size of the objects increase
4. Store the player's best score and display in the menu screen
5. Add a player leveling system based on how many enemies killed 


Past Tasks that are done
1. ~~Add a gameover screen that allows (Restart the game, quit, go back to the main menu)~~
2. ~~Add logic to trigger game over when the game is over~~
    - ~~Logic to show the game over screen~~
    - ~~Add functionality to the menu button~~
    - ~~Add functionality to the restart button~~
    - Pause everything that's on the screen
~~3. Abstract and refactor the enemy movement and controller code (abstraction of methods, variables)~~
4. ~~Add spawn and speed difficulty scaling that depends on the time elapsed (spawn more as time goes on, spawn faster as time goes on, move in closer to the player faster). ~~
5. ~~Introduce the second type of enemy - median difficulty (larger, moves slower, spawns slower)~~
6. ~~Think about the logic to initialize different types of enemy:~~
    - ~~My enemy spawner is in the GameManager script~~
    ~~- My enemy health is defined in the Enemy Controller script ~~
    ~~- I want to create a enemy of certain prefab type and based on that prefab type create enemy of different attributes~~
    ~~- The enemies should look different (prefab)~~
    ~~- The enemies should have different health, movement speed, damage on the player~~
    ~~- EnemySpawner should decide on which enemy to spawn~~
        ~~- Create an array to store different enemy prefabs~~
        ~~- Randomly spawn different enemy class with different probabilities ~~
7. ~~Add a spawn swarm of enemies option~~
8. ~~Prevent enemy from overlapping with each other~~

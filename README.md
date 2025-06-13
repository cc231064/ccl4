**Tectonic Pets**

Storyline: Blorbo is taking care of her animals by changing tectonic plates. You are helping Blorbo in achieving her Goal by adding animals onto the planet and changing Biomes.   

Features: The Player takes the animals, biomes and trees out of their inventory and adds them to the planet. 

System design: Blorbo will mostly fly around the tectonic plates and observe them. If there was to be a bigger change, Blorbo will fly up the screen and talk to the player to explain the changes. Depending on the changes, Blorbo will have different reactions and expressions.  

Passive animations such as sheep, chickens, goats etc. will react scared to aggressive animals such as foxes and wolves.  

System infrastructure:  
Spawner.cs is the start of code.
Spawner.cs instanciates, and stores, the prefab tiles.
Each tile has PlateData.cs, Selector.cs, Animation.cs, and Snapper.cs.
Snapper.cs is responsible for generating the diagetic coords. (though this will likely be ported to the PlateData.cs)
PlateData.cs handels the data needed for collisions (not implemented yet)
Selector.cs handles the selector system (this is currently just the mouse)
Animation.cs stores all the animations and a copy of all the data needed for said animation.
Controller.cs and Techtonic Controller.cs both work togther to take user input (keyboard) and manipulate the plates to (in the future) make geologic features.

![plates](https://github.com/user-attachments/assets/9a0d1ab8-dca9-4e82-a831-27290abf9fab)

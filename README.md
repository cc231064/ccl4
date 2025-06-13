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
![foxwolf](https://github.com/user-attachments/assets/60c14957-d651-4fe3-a0d5-a62397e56418)
![cow](https://github.com/user-attachments/assets/53bb8606-27b1-4cb4-9dd4-f1c47c7bd1a2)
![chicken](https://github.com/user-attachments/assets/73637039-4c0c-4afd-8ce4-5f6d7ef30396)
![blorbo](https://github.com/user-attachments/assets/02254768-1aea-4aa3-8550-98fec0eab9d1)
![sheep](https://github.com/user-attachments/assets/1fb78504-b40e-4468-999c-90cbecadeca8)
![goat](https://github.com/user-attachments/assets/d325c4be-07f0-4e24-95d5-6e67caadd7a0)

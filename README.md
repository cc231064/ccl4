Tectonic Pets

Storyline: Blorbo is taking care of her animals by changing tectonic plates. You are helping Blorbo in achieving her Goal by changing Biomes.   

User Guideline:
Press the Start Game Button to play our game. Click on a plate and press A, W, E, D to create a mountain, ocean, island or volcanoe. If you clicked on a plate and press the S button the animals on the screen will jump and the terretory of the aggressive animals, which includes the fox and wolf, will light up red. The goal of the game is to make sure that the not aggressive animals, chicken, cow, sheep, goats are safe from the predatory animals and that the predatory animals still have enough space for their territory.
Over the herbavoures animals is a explanation mark If they are in the territory of the predatory animal, to update this you need to click the S button. To check If you completed all tasks to win you have to click on Blorbo, who will let you know If you won or If you have to try again.

Features: The Player chooses a biome to change and creates depending on the thype of biome mountains volcanoes, islnads or coeans.

System design: Blorbo will mostly fly around the tectonic plates and observe them. If there was to be a bigger change, Blorbo will fly up the screen and talk to the player to explain the changes. Depending on the changes, Blorbo will have different reactions and expressions.  

Passive animations such as sheep, chickens, goats etc. will react scared to aggressive animals such as foxes and wolves.  

System infrastructure:  
The Design phylosophy of this project was very Onject oriented. The scripts were all written to be placed on individual items, drawing data from linked "Data-Scripts". 
The main script that runs on Start up Is the Spawner script, that calls various functions in other scripts (mostly instantiatior scripts, which do not run on their own as to prevent them running in the wrong order), as well ad pulling data from its DataScript and setting up the two way data links. This, in hindsight, could have benefitted from being extracted into a json file and json reader, but that was not done.
Each plate uses its spawned position to set its ID and place itself in the correctly scalled world position. It then instantiates the model that shows the visual for the plate. This makes it easy to then refference the plate Data (Tiles are reffered to as Plates), and change the plate model for rendering. 
Collisions are handeled in a Controller script, and The creature Territory and animations are controlled in a Creature script. The Animations are stored in a library however, meaning every simple animated object refers to this.
Animals also have an animation controller that calls a trigger event, which is then called in the built in Unity pipline.

![chicken](https://github.com/user-attachments/assets/3c4384c2-05a3-4efb-82d8-417109fafd26)
![blorbo](https://github.com/user-attachments/assets/b9b26d6b-fb17-4af9-9c73-1034d8887685)
![sheep](https://github.com/user-attachments/assets/3bc41344-9bc2-4492-ac74-8cd1e3b1b514)
![goat](https://github.com/user-attachments/assets/26a4c85e-058a-4992-8866-de901dc9b9cf)
![foxwolf](https://github.com/user-attachments/assets/61e29154-c920-4cc5-b95e-ad02d07e9302)
![cow](https://github.com/user-attachments/assets/9a1e7cd9-e0ba-47a9-b3be-2e0682e9b706)
![plates](https://github.com/user-attachments/assets/cc8c0335-fbae-4df5-b3ba-45f8d3c0f1d1)


UI_Manager - Does what it says
Game Manager - Sort of the root. It holds all the other managers, does save/load of preferences, and is the ONLY object receiving an OnUpdate() - which it then sends to the others
LevelBehavior - Or whatever. You might call this 'thegameplay' or all kinds of other things. It does the brunt of the top-level work for the actual gameplay.
Player - If the player has a physical representation (such as a ship, character, or icon), then you almost always want a single object for the player.
AudioManager - something for later.
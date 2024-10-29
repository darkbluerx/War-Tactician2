# **War-Tactician2 - War Tactician**
- Last Updated: 29.10.2024
- Status: Completed
- Unity Version: 2022.3.6f1

Summary: A three-dimensional tower defense and real-time action strategy game. Follow-up project from the previous project The Power of Iron.
 
 ## Table of Contents
   - [Applications Used](#applications-used))
   - [Credits](#credits)
   - [Sources](#sources)

## Applications Used
- AI Navigation: Unity Navmesh, used for AI pathfinding.

## Credits

- Game Designer: Antti Sironen
- Level Making: Antti Sironen
- Project Manager: Antti Sironen
- UI Artist: Min Fei Kultainen

- **Programming:**
- Access to data: Unit/castle information, recruitment unit information and unit spawn button information. The information is obtained from own scriptable objects
- Main Menu: Start Menu
- Managers: 
  Game Manager, controls the playback of sounds. Shows Gameplay Canvas and Shop Canvas
  Unit manager, Retrieves prefab models and stats of units/castles and spawns units. Controls the unit's movement and attack. 
  UI Manager, handles unit's health bar, display win/loss images.
  Cash System, manages the player's cash and updates the UI and buttons.
- Recruit System: Handles the recruitment of units. Updates money count.
- Units: Troop movement, using pathfinding (NavMesh). Playback of animation, troop actions (target detection/attack)

## Sources
- [Unity Royale](https://github.com/ciro-unity/UnityRoyale-Public)
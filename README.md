# **War-Tactician2 - War Tactician**

- Last updated: 2024-10-29
- Status: Completed
- Unity version: 2022.3.6f1

Summary: A three-dimensional tower defense and real-time action strategy game, developed in collaboration with two artists. Follow-up project to the previous project The Power of Iron.

## Table of Contents

- [Applications Used](#applications-used)
- [Credits](#credits)
- [Sources](#sources)

## Applications Used

- AI Navigation: Unity NavMesh, used for AI pathfinding.

## Credits

- Game Designer: Antti Sironen
- Level Making: Antti Sironen
- Project Manager: Antti Sironen
- 3D Artist (models & animations: castles, soldiers, fences, spectator stands): name withheld at their request
- UI Artist: Min Fei Kultainen

### Programming: Antti Sironen

Based on code from Unity's UnityRoyale sample project, adapted and modified for this game.

- Access to data: Unit/castle information, recruitment unit information and unit spawn button information. The information is obtained from custom ScriptableObjects.
- Main Menu: Start menu.
- Managers:
  - Game Manager: controls the playback of sounds. Shows Gameplay Canvas and Shop Canvas.
  - Unit Manager: retrieves prefab models and stats of units/castles and spawns units. Controls the unit's movement and attack.
  - UI Manager: handles unit's health bar, displays win/loss images.
  - Cash System: manages the player's cash and updates the UI and buttons.
- Recruit System: Handles the recruitment of units. Updates money count.
- Units: Troop movement using pathfinding (NavMesh). Playback of animation, troop actions (target detection/attack).

## Sources

- [Unity Royale](https://github.com/ciro-unity/UnityRoyale-Public): Unity sample project; its code was used as a base and modified for this project.
- Terrain, sounds and other props: ready-made third-party asset packs.

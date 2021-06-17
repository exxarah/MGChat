## TODO.md
<details>
<summary>v0.2 - A Skeletal Game</summary>

- [X] Setup Readme Stuff
- [ ] Setup Documentation/Comments
- [X] Add Game States/Scene Manager
- [X] Make v simple Main Screen
- [ ] Netwoking?
  - [X] Render second player
  - [X] Jsonify input data for second player
  - [X] Read jsonified input data from thread to game
  - [X] Fake network input better (have local character export to thread, and remote player read that)
  - [X] Add new players on receive/etc. Spawn through the network
  - [X] Set netID to player name
  - [X] Actually network it
  - [X] Tidy up Code/Move to own namespace
  - [ ] Remove Players on disconnect
  - [ ] Neater/more efficient data
    - [ ] Replace Json with binary (or similar)
  - [ ] Only send/receive data when new data
  - [ ] Login/Auth
- [X] Nametag over local player
  - [X] Rewrite UI system stuff into non-ECS
- [X] Enter name for tag at main menu
  - [X] Draw a textbox
  - [X] Write in the textbox
  - [X] Use as character name
- [X] Setup Camera
  - [X] Camera object that gets optionally passed to draw functions, and operates on positions (non destructively)
  - [X] Can have multiple cameras (for minimaps etc)
- [X] Debug Menu
  - [X] FPS Counter
  - [X] Each System's time to complete loop
  - [X] How many entities each System operates on per loop
  - [X] Entities Count
  - [X] Render the Debug Info as an overlaid screen
  - [X] ScreenManager Update has code to toggle Debug Info -- F3
- [X] Physics \o/
  - [X] Rewrite movement to be in PhysicsSystem
  - [X] ~~Rewrite movement to use physics~~ <- Kinematic seems like it's better. Can revisit if needed
  - [X] Discover Collisions
  - [X] Resolve Collisions
  - [X] !Make them work with offsets properly!
- [ ] Environment
  - [X] Setup TileMap class
  - [X] Render TileMaps
  - [X] Json for tiles and map
      - [X] Json to define types of tiles, and give them an identifier
      - [X] File to define a map, 2d array of tile identifiers
  - [X] Actually load/use json
  - [X] Add some bush entities with colliders and sprites
  - [ ] Set up big tilemap (tilemap of chunk tilemaps)
  - [ ] Only load active chunks/maps into memory (papa tilemap handles this)
  - [ ] Only draw currently on screen tiles (each chunk can handle this itself using camera?)
- [ ] Ability Bar
  - [ ] Ability (Probably external to ECS)
    - [ ] Abstract class for abilities to inherit from
    - [ ] Execute()
  - [ ] AbilityUserComponent
    - [ ] Array of Len(abilityBarAmount), can be an Ability, or null
  - [ ] InputSystem sends Commands to CommandComponent/InputComponent/AbilityUserComponent, with the skill button pressed
  - [ ] Something similar for RemoteSystem
  - [ ] AbilitySystem takes CommandComponent/AbilityUserComponent, executes the correct ability
  - [ ] Make Emote Abilities
  - [ ] UI hookups for ability bar
- [ ] Rewrite GameStateManager, to be a better FSM
  - [X] Rewrite
  - [ ] Add transitions and stuff
- [ ] Choose between Char_1, Char_2
- [X] Streamline PlayerFactory, single Player.json, then Factory adds specifics?
- [ ] Write Json Importer for the Content Pipeline (http://rbwhitaker.wikidot.com/content-pipeline-extension-1)
- [X] Render order on sprites, sort by Position.Y for draw order
- [ ] Only render entities that are in camera view
- [X] Fix the Framerate averaging, it currently throws an error if it's still infinity. Might just be a spritefont issue, with no default set?
- [ ] Split up Physics system so that collision is separate(?)
</details>

<details>
<summary>v0.3 - An Organised Game</summary>

- [ ] Tooling
  - [ ] Entity Designer Tool
  - [ ] SpriteSheet Packer
  - [ ] Map Designer Tool
</details>

<details>
<summary>v?.? - A Dumping Ground For Premature Optimisations/Overachievement</summary>

- [ ] Optimise ECS.Manager, consider bitmasking
- [ ] Chunking for environment
- [ ] Quadtrees/Collision Detection optimisation
- [ ] Revisit Shaders (http://rbwhitaker.wikidot.com/hlsl-tutorials)
</details>
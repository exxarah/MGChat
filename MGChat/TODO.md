## TODO.md
<details>
<summary>v0.2 - A Skeletal Game</summary>

- [X] Setup Readme Stuff
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
  - [X] Only send/receive data when new data
      - [X] Serverside
      - [X] Clientside
  - [ ] Login/Auth
    - [ ] Server stores playerName (used as NetID), and player customised json/data to be loaded.
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
- [X] Environment
  - [X] Setup TileMap class
  - [X] Render TileMaps
  - [X] Json for tiles and map
      - [X] Json to define types of tiles, and give them an identifier
      - [X] File to define a map, 2d array of tile identifiers
  - [X] Actually load/use json
  - [X] Add some bush entities with colliders and sprites
  - [X] Only load active chunks/maps into memory (papa tilemap handles this)
  - [X] Only draw currently on screen tiles (each chunk can handle this itself using camera?)
  - [X] OPTIMISE!!!!!
- [ ] Ability Bar
  - [X] Ability (Probably external to ECS)
    - [X] Abstract class for abilities to inherit from
    - [X] Execute()
  - [X] AbilityUserComponent
    - [X] Array of Len(abilityBarAmount), can be an Ability, or null
  - [X] InputSystem sends Commands to CommandComponent/AbilityUserComponent, with the skill button pressed
  - [ ] Something similar for RemoteSystem
  - [X] AbilitySystem takes CommandComponent/AbilityUserComponent, executes the correct ability
  - [X] EmoteAbility automatically disappears
  - [X] Make all Emote Abilities
  - [ ] UI hookups for ability bar
  - [ ] Make it work in multiplayer
- [ ] Rewrite GameStateManager, to be a better FSM
  - [X] Rewrite
  - [ ] Add transitions and stuff
- [ ] Choose between Char_1, Char_2
- [X] Streamline PlayerFactory, single Player.json, then Factory adds specifics?
- [ ] Write Json Importer for the Content Pipeline (http://rbwhitaker.wikidot.com/content-pipeline-extension-1)
- [X] Render order on sprites, sort by Position.Y for draw order
- [X] Only render entities that are in camera view
- [X] Fix the Framerate averaging, it currently throws an error if it's still infinity. Might just be a spritefont issue, with no default set?
- [ ] Split up Physics system so that collision is separate(?)
- [ ] Refactor InputSystem to use Commands for movement, consolidate MoveKeys and AbilityKeys, need to figure out how ot handle multi-dir movement then
</details>

<details>
<summary>v0.3 - An Organised Game</summary>

- [ ] Tooling
  - [ ] Entity Designer Tool
    - [ ] Dynamically fetch all possible components https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
    - [ ] Dynamically generate form from components
      - [ ] https://actualrandy.wordpress.com/2014/10/01/auto-generate-forms-and-class-by-reverse-engineering-your-class-or-web-reference/
      - [ ] Backup is that I create a form for each component
    - [ ] List of components on one side, with representation of the entity in the main view
      - [ ] Click on component, blind opens with variables, can edit them there
      - [ ] Button to add new, from dropdown
    - [ ] Figure out how to use mgchat systems?
      - [ ] If there's no easy way, have a button that will instead launch a monogame instance that CAN use them, even if it's in a separate window
    - [ ] Automatically add it into MGCB (is that even possible lmfao)
    - [ ] Load and edit existing entities
  - [ ] SpriteSheet Packer
    - [ ] Align spritesheets for animation appropriately. Probably requires some reworking of the existing animation system, variable size etc
    - [ ] Similar to Entity Designer, include visual representation, to check that it's looking good, fps, etc
  - [ ] Map Designer Tool
    - [ ] Build levelmaps, like Tiled
    - [ ] Place entities, based on json
    - [ ] Grid/not-grid
    - [ ] Rework existing chunking/map loading to use binary for larger maps
</details>

<details>
<summary>v?.? - A Dumping Ground For Premature Optimisations/Overachievement</summary>

- [ ] Optimise ECS.Manager, rewrite to use bitmasking for components
- [X] Chunking for environment
- [ ] Thread new chunk loading/unloading
- [ ] Quadtrees/Collision Detection optimisation
- [ ] Revisit Shaders (http://rbwhitaker.wikidot.com/hlsl-tutorials)
- [ ] Rewrite network code to generate normal commands, rather than having separate ones (maybe)
    - [ ] Lookup correct entity by netId, and add the command to them instead?
</details>
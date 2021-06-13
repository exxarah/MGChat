## TODO.md
<details>
<summary>v1.0 - A Playable Game</summary>

- [X] Setup Readme Stuff
- [X] Setup Documentation/Comments
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
- [ ] Setup Camera
  - [ ] Camera object that gets optionally passed to draw functions, and operates on positions (non destructively)
  - [ ] Can have multiple cameras (for minimaps etc)
- [ ] Debug Menu
  - [ ] FPS Counter etc
- [ ] Physics \o/
  - [X] Rewrite movement to be in PhysicsSystem
  - [ ] Rewrite movement to use physics
  - [ ] Discover Collisions
  - [ ] Resolve Collisions
- [ ] Environment
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
- [ ] Choose between Char_1, Char_2
- [ ] Streamline PlayerFactory, single Player.json, then Factory adds specifics?
</details>

<details>
<summary>v2.0 - An Enjoyable Game</summary>

- [ ] Tooling
  - [ ] Entity Designer Tool
</details>
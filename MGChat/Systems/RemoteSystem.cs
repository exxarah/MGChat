using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.Systems
{
    public class RemoteSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            #region Remote In

            List<Network.NetInput> netInputs = Network.Receive();
            List<string> existingPlayers = new List<string>();
            
            // Update Existing Remote Players
            var components = ECS.Manager.Instance.Query<RemoteInputComponent, CommandComponent>();
            if (components != null && netInputs != null)
            {
                EntitiesPerFrame = components.Count;
                foreach (var entity in components)
                {
                    var _remote = (RemoteInputComponent) entity[0];
                    var _command = (CommandComponent) entity[1];
                    Network.NetInput _netInput = default;
                    existingPlayers.Add(_remote.NetId);

                    // Get new positional data, update component
                    foreach (var netInput in netInputs)
                    {
                        if (netInput.NetId == _remote.NetId)
                        {
                            _netInput = netInput;
                            break;
                        }
                    }
                    if (Equals(_netInput, default(Network.NetInput))) { continue; }
                    
                    #region Remote Input

                    _remote.LastDirection = _remote.NewDirection;
                    var oldNew = _remote.NewPosition;
                    _remote.NewPosition = _netInput.Position;
                    _remote.LastPosition = oldNew;

                    // Send Appropriate Commands
                    Vector2 newDir = _remote.NewDirection;
                    if (_remote.NewDirection != Vector2.Zero)
                    {
                        _command.AddCommand(new SetPositionCommand(_remote.NewPosition));
                        _command.AddCommand(new ChangeDirectionCommand(Conversion.VectorToDirection(newDir)));
                    }

                    if (newDir == Vector2.Zero && _remote.LastDirection != Vector2.Zero)
                    {
                        _command.AddCommand(new ChangeStateCommand("Idle"));
                    }

                    if (newDir != Vector2.Zero && _remote.LastDirection == Vector2.Zero)
                    {
                        _command.AddCommand(new ChangeStateCommand("Walk"));
                    }

                    #endregion
                }
            }
            
            // Spawn new Remote Players
            if (netInputs != null)
            {
                foreach (var input in netInputs)
                {
                    if (existingPlayers.Contains(input.NetId)) { continue; }
                
                    var remotePlayer = Factories.PlayerFactory.CreateRemotePlayer("RemotePlayer.json", input);
                    Debug.WriteLine(input.Position);
                }
            }

            #endregion

            #region Remote Out

            List<Network.NetInput> exports = new List<Network.NetInput>();
            components = ECS.Manager.Instance.Query<RemoteExportComponent, TransformComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _remote = (RemoteExportComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                
                // Make a NetInput object, and add it to the list
                var export = new Network.NetInput(_remote.NetId, _transform.Position);
                exports.Add(export);
            }

            Network.Send(exports);
            #endregion
            
            base.Update(gameTime);
        }
    }
}
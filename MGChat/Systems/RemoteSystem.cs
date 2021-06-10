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
            #region Remote In

            List<Network.NetInput> netInputs = Network.Receive();
            var components = ECS.Manager.Instance.Query<RemoteInputComponent, CommandComponent>();
            if (components != null && netInputs != null)
            {
                foreach (var entity in components)
                {
                    var _remote = (RemoteInputComponent) entity[0];
                    var _command = (CommandComponent) entity[1];
                    Network.NetInput _netInput = default;

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

                    var oldNew = _remote.NewPosition;
                    _remote.NewPosition = _netInput.Position;
                    _remote.LastPosition = oldNew;

                    // Send Appropriate Commands
                    Vector2 newDir = Vector2.Normalize(_remote.NewDirection);
                    if (_remote.NewDirection != Vector2.Zero)
                    {
                        //_command.AddCommand(new MoveCommand(newDir));
                        _command.AddCommand(new SetPositionCommand(_remote.NewPosition));
                        _command.AddCommand(new ChangeDirectionCommand(Util.Conversion.VectorToDirection(newDir)));
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
        }
    }
}
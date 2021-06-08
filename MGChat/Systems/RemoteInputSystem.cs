using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using MGChat.Commands;
using MGChat.Components;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.Systems
{
    public struct NetInput
    {
        public string NetId;
        public Vector2 Position;
    }
    
    public class RemoteInputSystem : ECS.System
    {
        private int timeCount = 0;
        private int timeBetween = 200;
        public void Update(GameTime gameTime, string netData="")
        {
            List<NetInput> netInputs = JsonConvert.DeserializeObject<List<NetInput>>(netData);
            var components = ECS.Manager.Instance.Query<RemoteInputComponent, CommandComponent>();
            if (components == null || netInputs == null) { return; }

            foreach (var entity in components)
            {
                var _remote = (RemoteInputComponent) entity[0];
                var _command = (CommandComponent) entity[1];
                NetInput _netInput = default;

                // Get new positional data, update component
                foreach (var netInput in netInputs)
                {
                    if (netInput.NetId == _remote.NetId)
                    {
                        _netInput = netInput;
                        break;
                    }
                }
                if (Equals(_netInput, default(NetInput))) { continue; }

                var oldNew = _remote.NewPosition;
                _remote.NewPosition = _netInput.Position;
                _remote.LastPosition = oldNew;
                /**
                if (timeCount >= timeBetween)
                {
                    var oldNew = _remote.NewPosition;
                    _remote.NewPosition = _remote.LastPosition + new Vector2(0, 1);
                    _remote.LastPosition = oldNew;
                    timeCount = 0;
                }
                timeCount++;
                **/

                // Send Appropriate Commands
                Vector2 newDir = _remote.NewDirection;
                if (_remote.NewDirection != Vector2.Zero)
                {
                    _command.AddCommand(new MoveCommand(newDir));
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
            }
        }
    }
}
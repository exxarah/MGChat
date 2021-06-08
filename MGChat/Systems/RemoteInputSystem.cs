using System.Diagnostics;
using System.Numerics;
using MGChat.Commands;
using MGChat.Components;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.Systems
{
    public class RemoteInputSystem : ECS.System
    {
        private int timeCount = 0;
        private int timeBetween = 200;
        public override void Update(GameTime gameTime)
        {
            var components = ECS.Manager.Instance.Query<RemoteInputComponent, CommandComponent, MovableComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _remote = (RemoteInputComponent) entity[0];
                var _command = (CommandComponent) entity[1];
                // var _movable = (MovableComponent) entity[2];

                if (timeCount >= timeBetween)
                {
                    Debug.WriteLine("DOWN");
                    _remote.FakeDown();
                    timeCount = 0;
                }

                timeCount++;

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
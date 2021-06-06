using MGChat.Commands;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class MovementSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            var components = ECS.Manager.Instance.Query<CommandComponent, MovableComponent, TransformComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _movable = (MovableComponent) entity[1];
                var _transform = (TransformComponent) entity[2];

                MoveCommand currCommand = _command.GetCommand<MoveCommand>();
                if (currCommand is null) { continue; }

                float delta = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                _transform.Position.X += currCommand.Direction.X * _movable.Speed * delta;
                _transform.Position.Y += currCommand.Direction.Y * _movable.Speed * delta;
            }
            
            base.Update(gameTime);
        }
    }
}
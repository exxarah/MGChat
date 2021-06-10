using System.Diagnostics;
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

                MoveCommand _move = _command.GetCommand<MoveCommand>();
                if (_move is not null)
                {
                    float delta = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    _transform.Position.X += _move.Direction.X * _movable.Speed * delta;
                    _transform.Position.Y += _move.Direction.Y * _movable.Speed * delta;
                }

                SetPositionCommand _setPos = _command.GetCommand<SetPositionCommand>();
                if (_setPos is not null)
                {
                    _transform.Position.X = _setPos.Position.X;
                    _transform.Position.Y = _setPos.Position.Y;
                }
            }
            
            base.Update(gameTime);
        }
    }
}
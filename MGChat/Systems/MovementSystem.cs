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
            var components = ECS.Manager.Instance.Query<CommandComponent, TransformComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];

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
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
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = ECS.Manager.Instance.Query<CommandComponent, TransformComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];

                SetPositionCommand _setPos = _command.GetCommand<SetPositionCommand>();
                if (_setPos is not null)
                {
                    _transform.Position = new Vector2(_setPos.Position.X, _setPos.Position.Y);
                }
            }
            base.Update(gameTime);
        }
    }
}
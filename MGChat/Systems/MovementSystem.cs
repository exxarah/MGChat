using System.Diagnostics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Util;
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

                var _setPositions = _command.GetAllCommands<SetPositionCommand>();
                if (_setPositions.Count < 1)
                {
                    continue;
                }

                var _setPos = _setPositions[^1];
                _transform.Position = new Vector2(_setPos.Position.X, _setPos.Position.Y);
                if (ECS.Manager.Instance.Fetch<RemoteExportComponent>(_transform.Parent).Count != 0)
                {
                    Network.SendCommand(_setPos);
                }
            }
            base.Update(gameTime);
        }
    }
}
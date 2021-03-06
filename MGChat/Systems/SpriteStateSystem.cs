using System.Diagnostics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class SpriteStateSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = ECS.Manager.Instance.Query<SpriteComponent, SpriteStateComponent, CommandComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                var _sprite = (SpriteComponent) entity[0];
                var _spriteState = (SpriteStateComponent) entity[1];
                var _command = (CommandComponent) entity[2];

                bool changed = false;
                bool remoteExport = (ECS.Manager.Instance.Fetch<RemoteExportComponent>(_sprite.Parent).Count != 0);

                    ChangeDirectionCommand dirCommand = _command.GetCommand<ChangeDirectionCommand>();
                if (dirCommand != null)
                {
                    if (_spriteState.Direction != dirCommand.Direction && remoteExport)
                    {
                        Network.SendCommand(dirCommand);
                    }
                    _spriteState.ChangeState(newDir: dirCommand.Direction);
                    changed = true;
                }
                
                ChangeStateCommand stateCommand = _command.GetCommand<ChangeStateCommand>();
                if (stateCommand != null)
                {
                    if (_spriteState.State != stateCommand.State && remoteExport)
                    {
                        Network.SendCommand(stateCommand);
                    }
                    _spriteState.ChangeState(newState: stateCommand.State);
                    changed = true;
                }

                if (changed)
                {
                    _sprite.SpriteY = _spriteState.SpriteY;
                    _sprite.SpriteHeight = _spriteState.SpriteHeight;
                    _sprite.SpriteWidth = _spriteState.SpriteWidth;
                }
            }
            base.Update(gameTime);
        }
    }
}
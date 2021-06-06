using MGChat.Commands;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class SpriteStateSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            var components = ECS.Manager.Instance.Query<SpriteComponent, SpriteStateComponent, CommandComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _sprite = (SpriteComponent) entity[0];
                var _spriteState = (SpriteStateComponent) entity[1];
                var _command = (CommandComponent) entity[2];

                SpriteCommand currCommand = _command.GetCommand<SpriteCommand>();
                if (currCommand is null) { continue; }

                string newState = $"{(currCommand.State != "" ? currCommand.State : _spriteState.State)}_{(currCommand.Direction != "" ? currCommand.Direction : _spriteState.Direction)}";
                _spriteState.ChangeState(newState);

                _sprite.SpriteY = _spriteState.SpriteY;
            }
            
            base.Update(gameTime);
        }
    }
}
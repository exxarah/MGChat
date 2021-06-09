using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class EntityLabel : Label
    {
        private int _trackedEntity;

        public EntityLabel(int entity, string fontPath="Fonts/Arcade_In_12") : base(fontPath, "", Vector2.Zero, true)
        {
            _trackedEntity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            var entity = ECS.Manager.Instance.Fetch<TransformComponent, InformationComponent>(_trackedEntity);
            _position = ((TransformComponent) entity[0]).Position;
            _text = ((InformationComponent) entity[1]).Name;

            
            // Realign EntityLabel
            if (_centered)
            {
                var _sprite = (SpriteComponent) ECS.Manager.Instance.Fetch<SpriteComponent>(_trackedEntity)[0];

                Vector2 labelPos = _position;
                labelPos.Y -= _sprite.SpriteHeight;
                Vector2 stringWidth = _font.MeasureString(_text);
                float stringMiddle = stringWidth.X / 2;
                float spriteMiddle = _position.X + _sprite.SpriteWidth;
                labelPos.X = spriteMiddle - stringMiddle;
                _position = labelPos;
            }

            base.Update(gameTime);
        }
    }
}
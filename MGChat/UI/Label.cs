using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class Label : UiElement
    {
        private int _trackedEntity;
        private string _text = "";
        private Vector2 _position = Vector2.Zero;
        private bool _centered = true;

        public Label(int entity)
        {
            _trackedEntity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            var entity = ECS.Manager.Instance.Fetch<TransformComponent, InformationComponent>(_trackedEntity);
            _position = ((TransformComponent) entity[0]).Position;
            _text = ((InformationComponent) entity[1]).Name;

            
            // Realign Label
            if (_centered)
            {
                var _sprite = (SpriteComponent) ECS.Manager.Instance.Fetch<SpriteComponent>(_trackedEntity)[0];

                Vector2 labelPos = _position;
                labelPos.Y -= _sprite.SpriteHeight;
                Vector2 stringWidth = Parent.Font.MeasureString(_text);
                float stringMiddle = stringWidth.X / 2;
                float spriteMiddle = _position.X + _sprite.SpriteWidth;
                labelPos.X = spriteMiddle - stringMiddle;
                _position = labelPos;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(Parent.Font, _text, _position, Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
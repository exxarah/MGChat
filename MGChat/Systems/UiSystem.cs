using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class UiSystem : ECS.System
    {
        private SpriteFont _font;
        
        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Fonts/Arcade_In_12");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var components = ECS.Manager.Instance.Query<LabelComponent, SpriteComponent, TransformComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _label = (LabelComponent) entity[0];
                var _sprite = (SpriteComponent) entity[1];
                var _transform = (TransformComponent) entity[2];

                // Align Text
                Vector2 labelPos = _transform.Position;
                labelPos.Y -= _sprite.SpriteHeight;
                if (_label.Centered)
                {
                    Vector2 stringWidth = _font.MeasureString(_label.Text);
                    float stringMiddle = stringWidth.X / 2;
                    float spriteMiddle = _transform.Position.X + _sprite.SpriteWidth;
                    labelPos.X = spriteMiddle - stringMiddle;
                }
                
                spriteBatch.Begin();
                spriteBatch.DrawString(_font, _label.Text, labelPos, Color.White);
                spriteBatch.End();
            }
            
            base.Draw(spriteBatch);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class Label : UiElement
    {
        protected string _text;
        protected Vector2 _position;
        protected bool _centered;
        private string _fontPath;
        protected SpriteFont _font;

        public Label(string fontPath, string text, Vector2 position, bool centered)
        {
            _fontPath = fontPath;
            _text = text;
            _position = position;
            _centered = centered;
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>(_fontPath);
            
            // Need Font to Center
            if (_centered) { _position = Util.Text.CenterString(_position, _text, _font); }

            base.LoadContent(content);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _text, _position, Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class Label : UiElement
    {
        public string Text;
        private string _fontPath;
        protected SpriteFont _font;
        
        public Label(string fontPath, string text, Vector2 position, Util.UI.ObjAlign xAlign=Util.UI.ObjAlign.Center, Util.UI.ObjAlign yAlign=Util.UI.ObjAlign.Center) : base(position, xAlign, yAlign)
        {
            _fontPath = fontPath;
            Text = text ?? " ";
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>(_fontPath);
            
            // Need Font to Center
            if (_xAlign == Util.UI.ObjAlign.Center) { _position = Util.UI.CenterXAlign(_position, Text, _font); }
            else if (_xAlign == Util.UI.ObjAlign.Right) { _position = Util.UI.RightXAlign(_position, Text, _font); }

            if (_yAlign == Util.UI.ObjAlign.Center) { _position = Util.UI.CenterYAlign(_position, Text, _font); }
            else if (_yAlign == Util.UI.ObjAlign.Top) { _position = Util.UI.TopYAlign(_position, Text, _font); }

            base.LoadContent(content);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, Text, _position, Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
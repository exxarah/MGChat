using MGChat.Util;
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

        public Label(string fontPath, string labelText, Vector2 position, Util.UI.ObjAlign xAlign, Util.UI.ObjAlign yAlign) : base(position, xAlign, yAlign)
        {
            _fontPath = fontPath;
            Text = labelText ?? " ";
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>(_fontPath);

            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera=null)
        {
            spriteBatch.Begin(transformMatrix: camera?.ViewMatrix);
            spriteBatch.DrawString(_font, Text, AlignedPosition, Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        public override void Align()
        {
            Size = _font.MeasureString(Text);
            base.Align();
        }
    }
}
using Microsoft.Xna.Framework;

namespace MGChat.UI
{
    public class ValueLabel : Label
    {
        private string _labelText;
        public string ValueText;

        public ValueLabel(string fontPath, string labelText, string valueText, Vector2 position, Util.UI.ObjAlign xAlign, Util.UI.ObjAlign yAlign) : base(fontPath, labelText, position, xAlign, yAlign)
        {
            _labelText = labelText;
            ValueText = valueText;
        }

        public override void Update(GameTime gameTime)
        {
            Text = _labelText + ValueText;
            base.Update(gameTime);
        }
    }
}
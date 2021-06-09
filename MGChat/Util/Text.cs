using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Util
{
    public class Text
    {
        public static Vector2 CenterString(Vector2 centerPos, string text, SpriteFont font)
        {
            Vector2 stringSize = font.MeasureString(text);
            Vector2 textMiddle = new Vector2(stringSize.X / 2, stringSize.Y / 2);
            Vector2 textPos = new Vector2((int) (centerPos.X - textMiddle.X), (int) (centerPos.Y - textMiddle.Y));

            return textPos;
        }
    }
}
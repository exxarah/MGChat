using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Util
{
    public static class UI
    {
        public enum ObjAlign
        {
            Right, Left, Center, Top, Bottom
        }
        public static Vector2 CenterXAlign(Vector2 centerPos, string text, SpriteFont font)
        {
            Vector2 stringSize = font.MeasureString(text);
            Vector2 textMiddle = new Vector2(stringSize.X / 2, stringSize.Y);
            Vector2 textPos = new Vector2((int) (centerPos.X - textMiddle.X), centerPos.Y);

            return textPos;
        }

        public static Vector2 CenterXAlign(Vector2 centerPos, int objWidth)
        {
            Vector2 objMiddle = new Vector2(objWidth / 2, centerPos.Y);
            Vector2 objPos = new Vector2((int) (centerPos.X - objMiddle.X), centerPos.Y);

            return objPos;
        }

        public static Vector2 RightXAlign(Vector2 rightPos, string text, SpriteFont font)
        {
            Vector2 stringSize = font.MeasureString(text);
            Vector2 textPos = new Vector2((int) (rightPos.X - stringSize.X), (int) (rightPos.Y));

            return textPos;
        }
        
        public static Vector2 RightXAlign(Vector2 rightPos, int objWidth)
        {
            Vector2 objPos = new Vector2((int) (rightPos.X - objWidth), (int)(rightPos.Y));

            return objPos;
        }
        
        public static Vector2 CenterYAlign(Vector2 centerPos, string text, SpriteFont font)
        {
            Vector2 stringSize = font.MeasureString(text);
            Vector2 textMiddle = new Vector2(stringSize.X, stringSize.Y / 2);
            Vector2 textPos = new Vector2(centerPos.X, (int)(centerPos.Y - textMiddle.Y));

            return textPos;
        }

        public static Vector2 CenterYAlign(Vector2 centerPos, int objHeight)
        {
            Vector2 objMiddle = new Vector2(centerPos.X, objHeight / 2);
            Vector2 objPos = new Vector2(centerPos.X, (int)(centerPos.Y - objMiddle.Y));

            return objPos;
        }
        
        public static Vector2 TopYAlign(Vector2 rightPos, int objHeight)
        {
            Vector2 objPos = new Vector2(rightPos.X, (int)(rightPos.Y - objHeight));

            return objPos;
        }
        
        public static Vector2 TopYAlign(Vector2 rightPos, string text, SpriteFont font)
        {
            Vector2 stringSize = font.MeasureString(text);
            Vector2 textPos = new Vector2((int) (rightPos.X), (int) (rightPos.Y - stringSize.Y));

            return textPos;
        }

        public static Texture2D BuildTexture(SpriteBatch spriteBatch, int width, int height, Color colour)
        {
            var texture = new Texture2D(spriteBatch.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = colour;
            }
            texture.SetData(data);

            return texture;
        }
    }
}
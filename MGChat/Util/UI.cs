using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.Util
{
    public static class UI
    {
        public enum ObjAlign
        {
            Right, Left, Center, Above, Below
        }

        public static Vector2 Align(Vector2 position, Vector2 size, ObjAlign xAlign, ObjAlign yAlign)
        {
            Vector2 finalPosition = Vector2.Zero;
            Vector2 objMiddle = new Vector2(size.X / 2, size.Y / 2);

            // Align X
            if (xAlign == ObjAlign.Left) { finalPosition.X = position.X - size.X; }
            else if (xAlign == ObjAlign.Center) { finalPosition.X = position.X - objMiddle.X; }
            else if (xAlign == ObjAlign.Right) { finalPosition.X = position.X; }

            // Align Y
            if (yAlign == ObjAlign.Above) { finalPosition.Y = position.Y - size.Y; }
            else if (yAlign == ObjAlign.Center) { finalPosition.Y = position.Y - objMiddle.Y; }
            else if (yAlign == ObjAlign.Below) { finalPosition.Y = position.Y; }

            return finalPosition;
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
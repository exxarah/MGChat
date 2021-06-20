using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Util
{
    public static class Shape
    {
        public struct DrawShape
        {
            public Texture2D Texture;
            public Rectangle Rect;
            public Color Colour;

            public DrawShape(Texture2D texture, Rectangle rect, Color colour)
            {
                Texture = texture;
                Rect = rect;
                Colour = colour;
            }
        }
        
        public static Texture2D GenerateSquareShape(GraphicsDevice graphicsDevice, int width, int height)
        {
            Texture2D rectangle = new Texture2D(graphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            for (int i = 0; i < width * height; i++)
            {
                colorData[i] = Color.White;
            }
            
            rectangle.SetData(colorData);

            return rectangle;
        }

        public static Texture2D GenerateSquareOutlineShape(GraphicsDevice graphicsDevice, int width, int height,
            int borderWidth, int borderHeight)
        {
            Texture2D rectangle = new Texture2D(graphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = x * width + y;
                    if (x > borderWidth && x < width - borderWidth &&
                        y > borderHeight && y < height - borderHeight)
                    {
                        colorData[index] = Color.Transparent;
                    }
                    else
                    {
                        colorData[index] = Color.White;
                    }
                }
            }
            
            rectangle.SetData(colorData);

            return rectangle;
        }

        public static Texture2D GenerateCircleShape(GraphicsDevice graphicsDevice, int diameter)
        {
            Texture2D circle = new Texture2D(graphicsDevice, diameter, diameter);
            Color[] colorData = new Color[diameter * diameter];

            float radius = diameter / 2f;
            float radiusSq = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiusSq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }
            
            circle.SetData(colorData);

            return circle;
        }
    }
}
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Components
{
    // Stores information for RenderingSystem. Is changed by systems (ie, changing what part of the texture to draw)
    public class SpriteComponent : ECS.Component
    {
        public Texture2D Texture;
        public int Rows, Columns;
        public int SpriteWidth, SpriteHeight;
        public int SpriteX, SpriteY;

        public SpriteComponent(int parent, Texture2D texture, int rows=1, int columns=1) : base(parent)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            SpriteWidth = Texture.Width / Columns;
            SpriteHeight = Texture.Height / Rows;
            SpriteX = 0;
            SpriteY = 0;
        }
    }
}
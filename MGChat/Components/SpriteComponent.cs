using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MGChat.Components
{
    // Stores information for SpriteRenderingSystem. Is changed by systems (ie, changing what part of the texture to draw)
    public class SpriteComponent : ECS.Component
    {
        [JsonIgnore]
        public Texture2D Texture;
        public string TexturePath;
        public int Rows, Columns;
        public int SpriteWidth, SpriteHeight;
        public int SpriteX, SpriteY;
        public int RenderOrder;
        [JsonIgnore] public bool ContentLoaded = false;

        public SpriteComponent(int parent, string texturePath, int rows=1, int columns=1) : base(parent)
        {
            TexturePath = texturePath;
            Rows = rows;
            Columns = columns;
            SpriteWidth = 16;
            SpriteHeight = 16;
            SpriteX = 0;
            SpriteY = 0;
            RenderOrder = 10;
        }
    }
}
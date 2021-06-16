using System.Numerics;
using MGChat.Util;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.TileMap
{
    public class Tile : Cell
    {
        public Vector2 TextureCoordinates;
        
        public Tile(int gridX, int gridY, Vector2 textureCoordinates) : base(gridX, gridY)
        {
            TextureCoordinates = textureCoordinates;
        }
    }
}
using System.Numerics;
using MGChat.Util;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.TileMap
{
    public class Tile : Cell
    {
        public string TileType;
        public int TileTypeId;
        public Vector2 TextureCoordinates;
        
        public Tile(Vector2 textureCoordinates, int gridX=0, int gridY=0) : base(gridX, gridY)
        {
            TextureCoordinates = textureCoordinates;
        }
    }
}
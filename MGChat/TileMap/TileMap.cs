using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.TileMap
{
    public class TileMap : Grid
    {
        private string _texturePath;
        private Texture2D _texture2D;
        private Vector2 _position;
        public Vector2 _cellSize;
        
        public TileMap(string texturePath, int width, int height, Vector2 cellSize, int worldX, int worldY) : base(width, height)
        {
            _texturePath = texturePath;
            _position = new Vector2(worldX, worldY);
            _cellSize = cellSize;
        }

        public void LoadContent(ContentManager content)
        {
            // TODO: Replace with actually loading a texture lol
            _texture2D = Util.Shape.GenerateSquareShape(ScreenManager.Sprites.GraphicsDevice, 32, 32);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera=null)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera?.ViewMatrix);
            for (int x = 0; x < GridActual.GetLength(0); x++)
            {
                for (int y = 0; y < GridActual.GetLength(1); y++)
                {
                    var cell = (Tile)GetTile(x, y);
                    var sourceRectangle = new Rectangle(
                        (int)cell.TextureCoordinates.X, (int)cell.TextureCoordinates.Y,
                        (int)_cellSize.X, (int)_cellSize.Y
                        );
                    var destinationRectangle = new Rectangle(
                        (int)(_position.X + (x * _cellSize.X)), (int)(_position.Y + (y * _cellSize.Y)),
                        (int)_cellSize.X, (int)_cellSize.Y
                        );
                    // TODO: Uncomment the Color.White one, when I'm loading an actual texture
                    spriteBatch.Draw(_texture2D, destinationRectangle, sourceRectangle, Color.ForestGreen);
                    // spriteBatch.Draw(_texture2D, destinationRectangle, sourceRectangle, Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
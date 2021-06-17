using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MGChat.TileMap
{
    public class TileMap : Grid
    {
        private string _texturePath;
        [JsonIgnore] private Texture2D _texture2D;
        private Vector2 _position;
        public Vector2 CellTextureSize;
        public Vector2 CellWorldSize;
        
        public TileMap(int width, int height, Vector2 cellWorldSize, int worldX, int worldY, string texturePath) : base(width, height)
        {
            _texturePath = texturePath;
            _position = new Vector2(worldX, worldY);
            CellTextureSize = new Vector2(16, 16);
            CellWorldSize = cellWorldSize;
        }

        public void LoadContent(ContentManager content)
        {
            if (_texturePath == "")
            {
                _texture2D = Util.Shape.GenerateSquareShape(ScreenManager.Sprites.GraphicsDevice, 32, 32);
            }
            else
            {
                _texture2D = content.Load<Texture2D>(_texturePath);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera=null)
        {
            for (int x = 0; x < GridActual.GetLength(0); x++)
            {
                for (int y = 0; y < GridActual.GetLength(1); y++)
                {
                    var cell = GetTile(x, y);
                    if (cell is TileMap map)
                    {
                        map.Draw(spriteBatch, camera);
                        continue;
                    }

                    var tile = (Tile) cell;
                    var sourceRectangle = new Rectangle(
                        (int)tile.TextureCoordinates.X, (int)tile.TextureCoordinates.Y,
                        (int)CellTextureSize.X, (int)CellTextureSize.Y
                        );
                    var destinationRectangle = new Rectangle(
                        (int)(_position.X + (x * CellWorldSize.X)), (int)(_position.Y + (y * CellWorldSize.Y)),
                        (int)CellWorldSize.X, (int)CellWorldSize.Y
                        );
                    // TODO: Uncomment the Color.White one, when I'm loading an actual texture
                    spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera?.ViewMatrix);
                    // spriteBatch.Draw(_texture2D, destinationRectangle, sourceRectangle, Color.ForestGreen);
                    spriteBatch.Draw(_texture2D, destinationRectangle, sourceRectangle, Color.White);
                    spriteBatch.End();
                }
            }
        }
    }
}
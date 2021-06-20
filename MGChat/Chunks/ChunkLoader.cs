using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Chunks
{
    // Only for static entities. Dynamic entities will need to be handled separately. This is meant to represent the world
    public class ChunkLoader : ECS.System
    {
        public int Player;
        public static int activeChunkWidthHeight = 3;
        public int activeChunkHalfIndex = (activeChunkWidthHeight - 1) / 2;
        public Chunk[,] ActiveChunks = new Chunk[activeChunkWidthHeight,activeChunkWidthHeight];
        
        private Texture2D _outlineShape;
        private Queue<Chunk> _inactiveChunks;

        public ChunkLoader(int player, int chunkX, int chunkY, Vector2 position)
        {
            Player = player;
            
            var chunk = new Chunk(chunkX, chunkY, position);
            chunk.Load();
            ActiveChunks[activeChunkHalfIndex, activeChunkHalfIndex] = chunk;
            ActiveChunks = PopulateNullChunks(ActiveChunks);
            _inactiveChunks = new Queue<Chunk>();
        }

        public override void LoadContent(ContentManager content)
        {
            _outlineShape = Util.Shape.GenerateSquareOutlineShape(ScreenManager.Sprites.GraphicsDevice, 128, 128, 1, 1);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            var newChunks = ShiftChunksOver();
            if (newChunks != null)
            {
                newChunks = PopulateNullChunks(newChunks);
                ActiveChunks = newChunks;
                ScreenManager.CurrentChunk = new Vector2(
                    ActiveChunks[activeChunkHalfIndex, activeChunkHalfIndex].ChunkX,
                    ActiveChunks[activeChunkHalfIndex, activeChunkHalfIndex].ChunkY
                );
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera = null)
        {
            // Draw Debug ChunkLines
            spriteBatch.Begin(transformMatrix: camera?.ViewMatrix);
            foreach (var chunk in ActiveChunks)
            {
                Rectangle textureRect = new Rectangle(
                    (int)chunk.WorldPosition.X, (int)chunk.WorldPosition.Y,
                    (int)chunk.ChunkWidth * chunk.TileWidth, (int)chunk.ChunkHeight * chunk.TileHeight);
                var chunkShape = new Shape.DrawShape(_outlineShape, textureRect, Color.Aqua * 0.5f);
                
                spriteBatch.Draw(chunkShape.Texture, chunkShape.Rect, chunkShape.Colour);
            }
            spriteBatch.End();
            base.Draw(spriteBatch, camera);
        }

        private Chunk[,] ShiftChunksOver()
        {
            // Ensure Player transform.Position is inside ActiveChunks[2,2]
            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(Player)[0];
            if (ActiveChunks[2, 2].Contains(transform.Position)) { return null; }
            
            Chunk[,] newChunks = null;
            int relativeX = 0;
            int relativeY = 0;
            
            // Get shift direction
            for (int x = 0; x < activeChunkWidthHeight; x++)
            {
                for (int y = 0; y < activeChunkWidthHeight; y++)
                {
                    if (ActiveChunks[x, y].Contains(transform.Position))
                    {
                        // Get shift direction
                        relativeX = x - activeChunkHalfIndex;
                        relativeY = y - activeChunkHalfIndex;
                    }
                }
            }
            
            // If relX and relY are still 0, return null, because no changes
            if (relativeX == 0 && relativeY == 0) { return null; }
            newChunks = new Chunk[activeChunkWidthHeight, activeChunkWidthHeight];
            
            // Shift Chunks over
            for (int x = 0; x < activeChunkWidthHeight; x++)
            {
                for (int y = 0; y < activeChunkWidthHeight; y++)
                {
                    if (x + relativeX < 0 || x + relativeX >= activeChunkWidthHeight || y + relativeY < 0 || y + relativeY >= activeChunkWidthHeight)
                    {
                        newChunks[x, y] = null;
                        
                        // Unload removed chunks
                        //ActiveChunks[activeChunkWidthHeight - 1 - x, activeChunkWidthHeight - 1 - y].Unload();
                        
                        // Add newly inactive chunk to _inactiveChunks, to be passed into new Chunks
                        _inactiveChunks.Enqueue(ActiveChunks[activeChunkWidthHeight - 1 - x, activeChunkWidthHeight - 1 - y]);
                    }
                    else
                    {
                        newChunks[x, y] = ActiveChunks[x + relativeX, y + relativeY];
                    }
                }
            }

            return newChunks;
        }

        private Chunk[,] PopulateNullChunks(Chunk[,] newChunks)
        {
            // Replace null with new Chunks
            for (int x = 0; x < activeChunkWidthHeight; x++)
            {
                for (int y = 0; y < activeChunkWidthHeight; y++)
                {
                    // Get replacement Chunk's ChunkX and ChunkY by comparing to newChunks[1, 1]
                    if (newChunks[x, y] is null)
                    {
                        var centerChunk = newChunks[activeChunkHalfIndex, activeChunkHalfIndex];
                        // Now center is 0, 0. We can use these for EZMath with 1,1
                        int relativeX = x - activeChunkHalfIndex;
                        int relativeY = y - activeChunkHalfIndex;

                        int chunkX = centerChunk.ChunkX + relativeX;
                        int chunkY = centerChunk.ChunkY + relativeY;
                        float posX = centerChunk.WorldPosition.X + relativeX * (centerChunk.ChunkWidth * centerChunk.TileWidth);
                        float posY = centerChunk.WorldPosition.Y + relativeY * (centerChunk.ChunkHeight * centerChunk.TileHeight);


                        newChunks[x, y] = new Chunk(chunkX, chunkY, new Vector2(posX, posY));
                        if (_inactiveChunks != null)
                        {
                            Chunk prev;
                            bool existing = _inactiveChunks.TryDequeue(out prev);
                            newChunks[x, y].Load(existing ? prev : null);
                        }
                        else
                        {
                            newChunks[x, y].Load();
                        }
                    }
                }
            }

            return newChunks;
        }
    }
}
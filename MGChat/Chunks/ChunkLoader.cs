using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Chunks
{
    // Only for static entities. Dynamic entities will need to be handled separately. This is meant to represent the world
    public class ChunkLoader : ECS.System
    {
        public int Player;
        public Chunk[,] ActiveChunks = new Chunk[3,3];

        public ChunkLoader(int player, int chunkX, int chunkY, Vector2 position)
        {
            Player = player;
            
            var chunk = new Chunk(chunkX, chunkY, position);
            ActiveChunks[1, 1] = chunk;
            ActiveChunks = PopulateNullChunks(ActiveChunks);
        }
        public override void Update(GameTime gameTime)
        {
            var newChunks = ShiftChunksOver();
            if (newChunks != null)
            {
                newChunks = PopulateNullChunks(newChunks);
                ActiveChunks = newChunks;
            }
            ScreenManager.CurrentChunk = new Vector2(ActiveChunks[1, 1].ChunkX, ActiveChunks[1, 1].ChunkY);

            base.Update(gameTime);
        }

        private Chunk[,] ShiftChunksOver()
        {
            // Ensure Player transform.Position is inside ActiveChunks[1,1]
            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(Player)[0];
            if (ActiveChunks[1, 1].Contains(transform.Position)) { return null; }
            
            Chunk[,] newChunks = null;
            int relativeX = 0;
            int relativeY = 0;
            
            // Get shift direction
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (ActiveChunks[x, y].Contains(transform.Position))
                    {
                        // Get shift direction
                        relativeX = x - 1;
                        relativeY = y - 1;
                    }
                }
            }
            
            // If relX and relY are still 0, return null, because no changes
            if (relativeX == 0 && relativeY == 0) { return null; }
            newChunks = new Chunk[3, 3];
            
            // Shift Chunks over
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (x + relativeX < 0 || x + relativeX > 2 || y + relativeY < 0 || y + relativeY > 2)
                    {
                        newChunks[x, y] = null;
                        // Unload removed chunks
                        // Debug.WriteLine($"Removing {2 - x}, {2 - y}");
                        ActiveChunks[2 - x, 2 - y].Unload();
                    }
                    else
                    {
                        newChunks[x, y] = ActiveChunks?[x + relativeX, y + relativeY];
                    }
                }
            }

            return newChunks;
        }

        private Chunk[,] PopulateNullChunks(Chunk[,] newChunks)
        {
            // Replace null with new Chunks
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    // Get replacement Chunk's ChunkX and ChunkY by comparing to newChunks[1, 1]
                    if (newChunks[x, y] is null)
                    {
                        var centerChunk = newChunks[1, 1];
                        // Now 1, 1 is 0, 0. We can use these for EZMath with 1,1
                        int relativeX = x - 1;
                        int relativeY = y - 1;

                        int chunkX = centerChunk.ChunkX + relativeX;
                        int chunkY = centerChunk.ChunkY + relativeY;
                        float posX = centerChunk.WorldPosition.X + relativeX * (centerChunk.ChunkWidth * centerChunk.TileWidth);
                        float posY = centerChunk.WorldPosition.Y + relativeY * (centerChunk.ChunkHeight * centerChunk.TileHeight);

                        newChunks[x, y] = new Chunk(chunkX, chunkY, new Vector2(posX, posY));
                        newChunks[x, y].Load();
                    }
                }
            }

            return newChunks;
        }
    }
}
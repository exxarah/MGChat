using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGChat.Chunks
{
    public class Chunk
    {
        public int ChunkX, ChunkY;          // For identifying what chunk it is
        public int TileWidth = 32;
        public int TileHeight = 32;         // How big are the tiles
        public int ChunkWidth = 16;
        public int ChunkHeight = 16;        // How many tiles

        private Vector2 _worldPosition;     // TopLeft, used to position any children
        private List<int> _childEntities;   // Entities I'm responsible for instantiating/removing

        public Vector2 WorldPosition => _worldPosition;

        public Chunk(int chunkX, int chunkY, Vector2 worldPosition)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
            _worldPosition = worldPosition;
            _childEntities = new List<int>();
        }

        public void Load()
        {
            
        }

        public void Unload()
        {
            
        }
        
        public bool Contains(Vector2 position)
        {
            if (position.X < _worldPosition.X || _worldPosition.X + (ChunkWidth * TileWidth) < position.X ||
                position.Y < _worldPosition.Y || _worldPosition.Y + (ChunkHeight * TileHeight) < position.Y)
            {
                return false;
            }

            return true;
        }
    }
}
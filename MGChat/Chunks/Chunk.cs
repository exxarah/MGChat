using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Components;
using MGChat.Factories;
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
        private List<int> _tileEntities;    // For easier loading in future

        public Vector2 WorldPosition => _worldPosition;

        public Chunk(int chunkX, int chunkY, Vector2 worldPosition)
        {
            ChunkX = chunkX;
            ChunkY = chunkY;
            _worldPosition = worldPosition;
            _childEntities = new List<int>();
            _tileEntities = new List<int>();
        }

        public void Load(Chunk previousChunk = null)
        {
            int entity = DecorationFactory.CreateBush();
            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(entity)[0];
            transform.Position = _worldPosition;
            _childEntities.Add(entity);
            
            if (previousChunk != null)
            {
                LoadExistingTiles(previousChunk);
            }
            else
            {
                LoadNewTiles();
            }
        }

        public void Unload()
        {
            foreach (var entity in _childEntities)
            {
                ECS.Manager.Instance.DestroyEntity(entity);
            }
            _childEntities.Clear();
            _tileEntities.Clear();
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

        private void LoadNewTiles()
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int y = 0; y < ChunkHeight; y++)
                {
                    var entity = TileFactory.LoadTile(0); // Load a grass tile
                    var transform = (TransformComponent) ECS.Manager.Instance.Fetch<TransformComponent>(entity)[0];
                    transform.Position = _worldPosition + new Vector2(x * TileWidth, y * TileHeight);
                    _tileEntities.Add(entity);
                }
            }
        }

        private void LoadExistingTiles(Chunk previousChunk)
        {
            _tileEntities = previousChunk._tileEntities;
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int y = 0; y < ChunkHeight; y++)
                {
                    var transform = (TransformComponent) ECS.Manager.Instance.Fetch<TransformComponent>(_tileEntities[y * ChunkHeight + x])[0];
                    transform.Position = _worldPosition + new Vector2(x * TileWidth, y * TileHeight);
                }
            }
            previousChunk.Unload();
        }
    }
}
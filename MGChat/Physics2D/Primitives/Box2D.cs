using System;
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class Box2D : Collider2D
    {
        private Vector2 _size = Vector2.Zero;
        private Vector2 _halfSize = Vector2.Zero;
        
        public float Rotation = 0f;
        
        // I don't think this Center works lmfao
        public Vector2 Center => Position + Offset;
        public Vector2 Min => Center - _halfSize;
        public Vector2 Max => Center + _halfSize;

        public Box2D(int parent) : base(parent) { }

        public Box2D(int parent, Vector2 min, Vector2 max) : base(parent)
        {
            _size = max - min;
            _halfSize = _size * 0.5f;
        }

        public Vector2[] GetVertices()
        {
            Vector2[] vertices = new Vector2[]
            {
                new Vector2(Min.X, Min.Y), new Vector2(Min.X, Max.Y),
                new Vector2(Max.X, Min.Y), new Vector2(Max.X, Max.Y)
            };

            if (Rotation != 0.0f)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    var vertex = MGChat.Util.Math.Rotate(vertices[i], Position, Rotation);
                    vertices[i] = vertex;
                }
            }

            return vertices;
        }
    }
}
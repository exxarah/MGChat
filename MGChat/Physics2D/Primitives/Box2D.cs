using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public class Box2D : Collider2D
    {
        public float Width, Height;
        
        [JsonIgnore]
        private Vector2 _halfSize = Vector2.Zero;
        [JsonIgnore]
        private Vector2 _size = Vector2.Zero;
        [JsonIgnore]
        public float Rotation = 0f;
        
        // I don't think this Center works lmfao
        public Vector2 Center => Position + _halfSize;
        public Vector2 Min => Center - _halfSize;
        public Vector2 Max => Center + _halfSize;
        public Vector2 HalfSize => _halfSize;

        public Box2D(int parent) : base(parent) { }

        [JsonConstructor]
        public Box2D(int parent, float width, float height) : base(parent)
        {
            Width = width;
            Height = height;
            
            Vector2 min = Vector2.Zero;
            Vector2 max = new Vector2(width, height);
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

            if (!Util.Math.Compare(Rotation, 0f))
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    var vertex = MGChat.Util.Math.Rotate(vertices[i], Center, Rotation);
                    vertices[i] = vertex;
                }
            }

            return vertices;
        }

        public override bool Contains(Vector2 point)
        {
            // Translate point into local space
            Vector2 localPoint = Util.Math.Rotate(point, Center, Rotation);
            
            return (localPoint.X <= Max.X && Min.X <= localPoint.X) &&
                   (localPoint.Y <= Max.Y && Min.Y <= localPoint.Y);
        }

        public override bool Intersects(Line2D line)
        {
            var localLine = new Line2D(Util.Math.Rotate(line.Start, Center, Rotation),
                Util.Math.Rotate(line.End, Center, Rotation));
            
            if (Contains(localLine.Start) || Contains(localLine.End))
            {
                return true;
            }

            Vector2 unitVector = localLine.End - localLine.Start;
            unitVector.Normalize();
            
            // Dividing by 0 is bad
            unitVector.X = (unitVector.X != 0) ? 1.0f / unitVector.X : 0f;
            unitVector.Y = (unitVector.Y != 0) ? 1.0f / unitVector.Y : 0f;

            Vector2 min = (Min - localLine.Start) * unitVector;
            Vector2 max = (Max - localLine.Start) * unitVector;

            float tmin = Math.Max(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y));
            float tmax = Math.Min(Math.Max(min.X, max.X), Math.Max(min.Y, max.Y));
            if (tmax < 0 || tmin > tmax)
            {
                return false;
            }

            float t = (tmin < 0f) ? tmax : tmin;
            return t > 0f && t * t < localLine.LengthSquared();
        }
    }
}
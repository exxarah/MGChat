using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public class AABB : Collider2D
    {
        private float _width, _height;

        [JsonIgnore] private Vector2 _halfSize = Vector2.Zero;
        [JsonIgnore] private Vector2 _size = Vector2.Zero;

        [JsonIgnore] public float Width => _width * Scale.X;
        [JsonIgnore] public float Height => _height * Scale.Y;
        [JsonIgnore] public Vector2 Center => Position + _halfSize * Scale.X;
        [JsonIgnore] public Vector2 Min => Center - _halfSize * Scale.X;
        [JsonIgnore] public Vector2 Max => Center + _halfSize * Scale.X;

        public AABB(int parent) : base(parent) { }

        [JsonConstructor]
        public AABB(int parent, float width, float height) : base(parent)
        {
            _width = width;
            _height = height;
            
            Vector2 min = Vector2.Zero;
            Vector2 max = new Vector2(width, height);
            _size = max - min;
            _halfSize = _size * 0.5f;
        }

        public override bool Contains(Vector2 point)
        {
            return (point.X <= Max.X && Min.X <= point.X) &&
                   (point.Y <= Max.Y && Min.Y <= point.Y);
        }

        public override bool Intersects(Line2D localLine)
        {
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
        
        public override bool Collides(Collider2D other)
        {
            if (other is AABB aabb)
            {
                return ShapeIntersection.AABBAABB(this, aabb);
            } else if (other is Box2D box2D)
            {
                return ShapeIntersection.AABBBox2D(this, box2D);
            } else if (other is Circle circle)
            {
                return ShapeIntersection.AABBCircle(this, circle);
            }

            return false;
        }
    }
}
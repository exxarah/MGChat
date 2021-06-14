using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public class Circle : Collider2D
    {
        [JsonProperty] private float _radius;
        public float Radius => _radius * Scale.X;
        public Vector2 Center => Position + new Vector2(Radius, Radius);

        public Circle(int parent, float radius=1.0f) : base(parent)
        {
            _radius = radius;
        }

        public override bool Contains(Vector2 point)
        {
            Vector2 centerToPoint = point - Center;
            
            return centerToPoint.LengthSquared() < Radius * Radius;
        }

        public override bool Intersects(Line2D localLine)
        {
            if (Contains(localLine.Start) || Contains(localLine.End))
            {
                return true;
            }

            Vector2 ab = localLine.End - localLine.Start;
            
            // Project point (this.Position) onto ab (line)
            Vector2 centerToLineStart = Center - localLine.Start;
            float t = Vector2.Dot(centerToLineStart, ab) / Vector2.Dot(ab, ab);

            if (t is < 0.0f or > 1.0f)
            {
                return false;
            }
            
            // Find closest point to line segment
            Vector2 closestPoint = localLine.Start + (ab * t);

            return Contains(closestPoint);
        }
        
        public override bool Collides(Collider2D other)
        {
            if (other is AABB aabb)
            {
                return ShapeIntersection.CircleAABB(this, aabb);
            } else if (other is Box2D box2D)
            {
                return ShapeIntersection.CircleBox2D(this, box2D);
            } else if (other is Circle circle)
            {
                return ShapeIntersection.CircleCircle(this, circle);
            }

            return false;
        }
    }
}
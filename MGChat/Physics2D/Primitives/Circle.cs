using System;
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class Circle : Collider2D
    {
        public float Radius;
        public Vector2 Center => Position + new Vector2(Radius, Radius);

        public Circle(int parent, float radius=1.0f) : base(parent)
        {
            Radius = radius;
        }

        public override bool Contains(Vector2 point)
        {
            Vector2 centerToPoint = point - Center;
            
            return centerToPoint.LengthSquared() < Radius * Radius;
        }

        public override bool Intersects(Line2D line)
        {
            if (Contains(line.Start) || Contains(line.End))
            {
                return true;
            }

            Vector2 ab = line.End - line.Start;
            
            // Project point (this.Position) onto ab (line)
            Vector2 centerToLineStart = Center - line.Start;
            float t = Vector2.Dot(centerToLineStart, ab) / Vector2.Dot(ab, ab);

            if (t is < 0.0f or > 1.0f)
            {
                return false;
            }
            
            // Find closest point to line segment
            Vector2 closestPoint = line.Start + (ab * t);

            return Contains(closestPoint);
        }
    }
}
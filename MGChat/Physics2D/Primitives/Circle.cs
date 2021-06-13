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
    }
}
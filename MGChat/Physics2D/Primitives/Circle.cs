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
            throw new System.NotImplementedException();
        }
    }
}
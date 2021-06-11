using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public abstract class Collider2D : ECS.Component
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Offset = Vector2.Zero;
        
        //public abstract float getInertiaTensor(float mass);
        public Collider2D(int parent) : base(parent) { }

        public abstract bool Contains(Vector2 point);

        public abstract bool Intersects(Line2D line);
    }
}
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class Collider2D : ECS.Component
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Offset = Vector2.Zero;
        
        //public abstract float getInertiaTensor(float mass);
        public Collider2D(int parent) : base(parent)
        {
            
        }
    }
}
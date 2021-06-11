using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    public class PhysicsComponent : ECS.Component
    {
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;
        public PhysicsComponent(int parent) : base(parent)
        {
        }
    }
}
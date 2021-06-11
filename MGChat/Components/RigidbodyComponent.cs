using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    public class RigidbodyComponent : ECS.Component
    {
        public Vector2 Velocity;
        public Vector2 Force;
        public float Mass;

        public RigidbodyComponent(int parent) : base(parent)
        {
        }
    }
}
using System.Text.Json.Serialization;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    public class PhysicsComponent : ECS.Component
    {
        public float MaxSpeed;
        public bool Static;

        public PhysicsComponent(int parent) : base(parent)
        {
        }
    }
}
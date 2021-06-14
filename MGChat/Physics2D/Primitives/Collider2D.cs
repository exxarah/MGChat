using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public abstract class Collider2D : ECS.Component
    {
        [JsonIgnore] public Vector2 Position = Vector2.Zero;
        [JsonIgnore] public Vector2 Scale = Vector2.One;
        [JsonIgnore] public float Rotation = 0f;
        
        public Vector2 Offset = Vector2.Zero;
        public bool Solid = true;

        //public abstract float getInertiaTensor(float mass);
        public Collider2D(int parent) : base(parent) { }

        public abstract bool Contains(Vector2 point);

        public abstract bool Intersects(Line2D localLine);

        public abstract bool Collides(Collider2D other);
    }
}
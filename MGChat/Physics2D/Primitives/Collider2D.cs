using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public abstract class Collider2D : ECS.Component
    {
        private Vector2 _position;

        [JsonIgnore]
        public Vector2 Position
        {
            get => _position;
            set => _position = value + (Offset * Scale);
        }
        [JsonIgnore] public Vector2 Scale = Vector2.One;
        [JsonIgnore] public float Rotation = 0f;
        [JsonIgnore] public bool Colliding = false;
        
        public Vector2 Offset;
        public bool Solid = true;

        //public abstract float getInertiaTensor(float mass);
        public Collider2D(int parent, Vector2 offset = default) : base(parent)
        {
            Offset = offset;
        }

        public abstract bool Contains(Vector2 point);

        public abstract bool Intersects(Line2D localLine);

        public abstract bool Collides(Collider2D other);
    }
}
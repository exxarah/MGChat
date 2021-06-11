using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class AABB : Collider2D
    {
        private Vector2 _halfSize = Vector2.Zero;
        private Vector2 _size = Vector2.Zero;

        // Assumes center is in the middle?
        public Vector2 Min => Position - _halfSize;
        public Vector2 Max => Position + _halfSize;

        public AABB(int parent) : base(parent) { }

        public AABB(int parent, Vector2 min, Vector2 max) : base(parent)
        {
            _size = max - min;
            _halfSize = _size * 0.5f;
        }
    }
}
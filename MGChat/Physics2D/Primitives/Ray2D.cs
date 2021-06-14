using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class Ray2D
    {
        private Vector2 _origin;
        private Vector2 _direction;

        public Vector2 Origin => _origin;
        public Vector2 Direction => _direction;

        public Ray2D(Vector2 origin, Vector2 direction)
        {
            this._origin = origin;
            this._direction = direction;
            _direction.Normalize();
        }
    }
}
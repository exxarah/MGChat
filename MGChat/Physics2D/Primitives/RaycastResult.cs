using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class RaycastResult
    {
        public Vector2 Point
        {
            get => _point;
            set => _point = value;
        }

        public Vector2 Normal
        {
            get => _normal;
            set => _normal = value;
        }

        public float T
        {
            get => _t;
            set => _t = value;
        }

        public bool Hit
        {
            get => _hit;
            set => _hit = value;
        }

        private Vector2 _point;
        private Vector2 _normal;
        private float _t;
        private bool _hit;

        public RaycastResult(Vector2 point=default, Vector2 normal=default, float t = -1, bool hit = false)
        {
            _point = point;
            _normal = normal;
            _t = t;
            _hit = hit;
        }
    }
}
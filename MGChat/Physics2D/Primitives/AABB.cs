using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Physics2D.Primitives
{
    public class AABB : Collider2D
    {
        private float _width, _height;
        
        [JsonIgnore]
        private Vector2 _halfSize = Vector2.Zero;
        [JsonIgnore]
        private Vector2 _size = Vector2.Zero;
        
        [JsonIgnore]
        public Vector2 Center => Position + _halfSize;
        [JsonIgnore]
        public Vector2 Min => Center - _halfSize;
        [JsonIgnore]
        public Vector2 Max => Center + _halfSize;

        public AABB(int parent) : base(parent) { }

        [JsonConstructor]
        public AABB(int parent, float width, float height) : base(parent)
        {
            _width = width;
            _height = height;
            
            Vector2 min = Vector2.Zero;
            Vector2 max = new Vector2(width, height);
            _size = max - min;
            _halfSize = _size * 0.5f;
        }

        public override bool Contains(Vector2 point)
        {
            return (point.X <= Max.X && Min.X <= point.X) &&
                   (point.Y <= Max.Y && Min.Y <= point.Y);
        }

        public override bool Intersects(Line2D line)
        {
            throw new System.NotImplementedException();
        }
    }
}
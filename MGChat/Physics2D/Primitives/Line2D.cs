using System;
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D.Primitives
{
    public class Line2D
    {
        private Vector2 _from;
        private Vector2 _to;
        
        public Vector2 Start => _from;
        public Vector2 End => _to;

        public Line2D(Vector2 from, Vector2 to)
        {
            _from = from;
            _to = to;
        }

        public bool Contains(Vector2 point)
        {
            float dy = End.Y - Start.Y;
            float dx = End.X - Start.X;
            
            // Prevent infinite slope on vertical lines
            if (dx == 0)
            {
                return Util.Math.Compare(point.X, Start.X);
            }

            float slope = dy / dx;
            float yIntercept = End.Y - (slope * End.X);

            // Check line equation
            return Util.Math.Compare(point.Y, slope * point.X + yIntercept);
        }

        public bool Intersects(Line2D line)
        {
            throw new NotImplementedException();
        }
    }
}
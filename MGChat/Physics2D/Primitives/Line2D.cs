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

        public float LengthSquared()
        {
            return (End - Start).LengthSquared();
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
            // https://www.habrador.com/tutorials/math/5-line-line-intersection/
            bool isIntersecting = false;

            float denominator = (line.End.Y - line.Start.Y) * (End.X - Start.X) -
                                (line.End.X - line.Start.X) * (End.Y - Start.Y);

            if (denominator != 0)
            {
                float u_a = ((line.End.X - line.Start.X) * (Start.Y - line.Start.Y) - 
                             (line.End.Y - line.Start.Y) * (Start.X - line.Start.X)) / denominator;
                float u_b = ((End.X - Start.X) * (Start.Y - line.Start.Y) - 
                             (End.Y - Start.Y) * (Start.X - line.Start.X)) / denominator;
                
                // Is intersecting if u_a and u_b are between 0 and 1
                float e = float.Epsilon;
                if (u_a >= 0f + e && u_a <= 1f + e && u_b >= 0f + e && u_b <= 1f + e)
                {
                    isIntersecting = true;
                }
            }

            return isIntersecting;
        }
    }
}
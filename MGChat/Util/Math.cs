using Microsoft.Xna.Framework;

namespace MGChat.Util
{
    public static class Math
    {
        /// <summary>
        /// Rotates vertex around center origin, by angleDeg
        /// </summary>
        /// <param name="vertex">Vector2 vertex to rotate</param>
        /// <param name="origin">Vector2 center origin to rotate around</param>
        /// <param name="angleDeg">float amount to rotate by, in degrees</param>
        /// <returns></returns>
        public static Vector2 Rotate(Vector2 vertex, Vector2 origin, float angleDeg)
        {
            float x = vertex.X - origin.X;
            float y = vertex.Y - origin.Y;

            double angleInRadians = ToRadians(angleDeg);
            double cosTheta = System.Math.Cos(angleInRadians);
            double sinTheta = System.Math.Sin(angleInRadians);

            double newX = (cosTheta * (vertex.X - origin.X) - sinTheta * (vertex.Y - origin.Y) + origin.X);
            double newY = (sinTheta * (vertex.X - origin.X) + cosTheta * (vertex.Y - origin.Y) + origin.X);

            return new Vector2((float)newX, (float)newY);
        }

        public static bool Compare(float x, float y, float epsilon=float.Epsilon)
        {
            return System.Math.Abs(x - y) <=
                   epsilon * System.Math.Max(1.0f, System.Math.Max(System.Math.Abs(x), System.Math.Abs(y)));
        }

        public static bool Compare(Vector2 vec1, Vector2 vec2, float epsilon=float.Epsilon)
        {
            return Compare(vec1.X, vec2.X, epsilon) && Compare(vec1.Y, vec2.Y, epsilon);
        }

        public static double ToRadians(double angle)
        {
            return (System.Math.PI / 180) * angle;
        }

        public static Vector2 Lerp(Vector2 pos, Vector2 target)
        {
            
        }
    }
}
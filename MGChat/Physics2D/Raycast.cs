using System;
using System.Diagnostics;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D
{
    public class Raycast
    {
        #region Circle Raycasts

        public static bool RaycastCircle(Circle circle, Ray2D ray)
        {
            RaycastResult result;
            return RaycastCircle(circle, ray, out result);
        }
        
        public static bool RaycastCircle(Circle circle, Ray2D ray, out RaycastResult result)
        {
            result = new RaycastResult();
            
            Vector2 originToCircle = circle.Center - ray.Origin;
            float radiusSquared = circle.Radius * circle.Radius;
            
            // Project vector from ray origin onto direction of ray
            float a = Vector2.Dot(originToCircle, ray.Direction);
            float bSq = originToCircle.LengthSquared() - (a * a);
            if (radiusSquared - bSq < 0.0f)
            {
                return false;
            }

            float f = (float) System.Math.Sqrt(radiusSquared - bSq);
            float t = 0;
            if (originToCircle.LengthSquared() < radiusSquared)
            {
                t = a + f;
            }
            else
            {
                t = a - f;
            }

            if (t < 0)
            {
                return false;
            }

            result.Point = (ray.Origin + ray.Direction) * result.T;
            result.Normal = Vector2.Normalize(result.Point - circle.Center);
            result.T = t;
            result.Hit = true;

            return true;
        }

        #endregion

        #region Box2D Raycasts

        public static bool RaycastBox2D(Box2D box, Ray2D ray)
        {
            RaycastResult result;
            return RaycastBox2D(box, ray, out result);
        }

        public static bool RaycastBox2D(Box2D box, Ray2D ray, out RaycastResult result)
        {
            result = new RaycastResult();
            
            Vector2 xAxis = Util.Math.Rotate(Vector2.UnitX, Vector2.Zero, -box.Rotation);
            Vector2 yAxis = Util.Math.Rotate(Vector2.UnitY, Vector2.Zero, -box.Rotation);

            Vector2 p = box.Position - ray.Origin;
            // Project direction of ray onto axis of box
            Vector2 f = new Vector2(
                Vector2.Dot(ray.Direction, p),
                Vector2.Dot(ray.Direction, p)
            );

            // Project p onto every axis of box
            Vector2 e = new Vector2(
                Vector2.Dot(xAxis, p),
                Vector2.Dot(yAxis, p)
            );

            float[] tArr = new[] {0f, 0f, 0f, 0f};
            for (int i = 0; i < 2; i++)
            {
                var fVal = (i == 0) ? f.X : f.Y;
                var eVal = (i == 0) ? e.X : e.Y;
                var sVal = (i == 0) ? box.HalfSize.X : box.HalfSize.Y;
                
                if (Util.Math.Compare(fVal, 0))
                {
                    // If ray is parallel to the current axis and the origin of the ray is not inside, we have no hit
                    if (-eVal - sVal > 0 || -eVal + sVal < 0) { return false; }

                    // Can't divide by 0
                    switch (i)
                    {
                        case 0:
                            f.X += 0.00001f;
                            break;
                        case 1:
                            f.Y += 0.00001f;
                            break;
                    }
                }

                tArr[i * 2 + 0] = (eVal + sVal) / fVal;    // tmax for axis
                tArr[i * 2 + 1] = (eVal - sVal) / fVal;    // tmin for axis
            }

            float tmin = Math.Max(Math.Min(tArr[0], tArr[1]), Math.Min(tArr[2], tArr[3]));
            float tmax = Math.Min(Math.Max(tArr[0], tArr[1]), Math.Max(tArr[2], tArr[3]));

            float t = (tmin < 0f) ? tmax : tmin;
            bool hit = t > 0f; // && t * t < ray.LocalMax;
            if (!hit)
            {
                return false;
            }

            result.Point = (ray.Origin + ray.Direction) * t;
            result.Normal = Vector2.Normalize(result.Point - box.Center);
            result.T = t;
            result.Hit = hit;

            return true;
        }

        #endregion
        
        #region AABB Raycasts

        public static bool RaycastAABB(AABB box, Ray2D ray)
        {
            RaycastResult result;
            return RaycastAABB(box, ray, out result);
        }

        public static bool RaycastAABB(AABB box, Ray2D ray, out RaycastResult result)
        {
            result = new RaycastResult();

            Vector2 unitVector = ray.Direction;
            unitVector.Normalize();
            
            // Dividing by 0 is bad
            unitVector.X = (unitVector.X != 0) ? 1.0f / unitVector.X : 0f;
            unitVector.Y = (unitVector.Y != 0) ? 1.0f / unitVector.Y : 0f;

            Vector2 min = (box.Min - ray.Origin) * unitVector;
            Vector2 max = (box.Max - ray.Origin) * unitVector;

            float tmin = Math.Max(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y));
            float tmax = Math.Min(Math.Max(min.X, max.X), Math.Max(min.Y, max.Y));
            if (tmax < 0 || tmin > tmax)
            {
                return false;
            }

            float t = (tmin < 0f) ? tmax : tmin;
            bool hit = t > 0f; // && t * t < ray.LocalMax;
            if (!hit)
            {
                return false;
            }

            result.Point = (ray.Origin + ray.Direction) * t;
            result.Normal = Vector2.Normalize(result.Point - box.Center);
            result.T = t;
            result.Hit = hit;

            return true;
        }

        #endregion
    }
}
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Physics2D
{
    public class ShapeIntersection
    {
        #region Circle
        
        public static bool CircleCircle(Circle c1, Circle c2)
        {
            Vector2 vecBetween = c1.Center - c2.Center;
            float radiiSum = c1.Radius + c2.Radius;
            return vecBetween.LengthSquared() <= radiiSum * radiiSum;
        }

        public static bool CircleAABB(Circle circle, AABB box)
        {
            Vector2 closestPoint = circle.Center;
            if (closestPoint.X < box.Min.X)
            {
                closestPoint.X = box.Min.X;
            } else if (closestPoint.X > box.Max.X)
            {
                closestPoint.X = box.Max.X;
            }
            
            if (closestPoint.Y < box.Min.Y)
            {
                closestPoint.Y = box.Min.Y;
            } else if (closestPoint.Y > box.Max.Y)
            {
                closestPoint.Y = box.Max.Y;
            }

            Vector2 circleToBox = circle.Center - closestPoint;
            return circleToBox.LengthSquared() <= circle.Radius * circle.Radius;
        }

        public static bool CircleBox2D(Circle circle, Box2D box)
        {
            // Treat box like AABB, after rotation
            Vector2 min = new Vector2();
            Vector2 max = box.HalfSize * 2.0f;
            
            // Create circle in box's local space
            Vector2 r = circle.Center - box.Position;
            Util.Math.Rotate(r, Vector2.Zero, -box.Rotation);

            Vector2 localCirclePos = r + box.HalfSize;
            
            Vector2 closestPoint = localCirclePos;
            if (closestPoint.X < box.LocalMin.X)
            {
                closestPoint.X = box.LocalMin.X;
            } else if (closestPoint.X > box.LocalMax.X)
            {
                closestPoint.X = box.LocalMax.X;
            }
            
            if (closestPoint.Y < box.LocalMin.Y)
            {
                closestPoint.Y = box.LocalMin.Y;
            } else if (closestPoint.Y > box.LocalMax.Y)
            {
                closestPoint.Y = box.LocalMax.Y;
            }

            Vector2 circleToBox = localCirclePos - closestPoint;
            return circleToBox.LengthSquared() <= circle.Radius * circle.Radius;
        }

        #endregion

        #region AABB

        public static bool AABBCircle(AABB box, Circle circle)
        {
            return CircleAABB(circle, box);
        }

        public static bool AABBAABB(AABB box1, AABB box2)
        {
            Vector2[] axesToTest = new[] {
                Vector2.UnitX, Vector2.UnitY
            };

            foreach (var axis in axesToTest)
            {
                if (!OverlapOnAxis(box1, box2, axis))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AABBBox2D(AABB box1, Box2D box2)
        {
            Vector2[] axesToTest = new[] {
                Vector2.UnitX, Vector2.UnitY, Vector2.UnitX, Vector2.UnitY
            };

            axesToTest[2] = Util.Math.Rotate(axesToTest[2], Vector2.Zero, box2.Rotation);
            axesToTest[3] = Util.Math.Rotate(axesToTest[3], Vector2.Zero, box2.Rotation);

            foreach (var axis in axesToTest)
            {
                if (!OverlapOnAxis(box1, box2, axis))
                {
                    return false;
                }
            }

            return true;
        }
        
        #endregion

        #region Box2D

        public static bool Box2DCircle(Box2D box, Circle circle)
        {
            return CircleBox2D(circle, box);
        }

        public static bool Box2DAABB(Box2D box1, AABB box2)
        {
            return AABBBox2D(box2, box1);
        }
        
        public static bool Box2DBox2D(Box2D box1, Box2D box2)
        {
            Vector2[] axesToTest = new[] {
                Vector2.UnitX, Vector2.UnitY, Vector2.UnitX, Vector2.UnitY
            };
            axesToTest[0] = Util.Math.Rotate(axesToTest[2], Vector2.Zero, box1.Rotation);
            axesToTest[1] = Util.Math.Rotate(axesToTest[3], Vector2.Zero, box1.Rotation);
            
            axesToTest[2] = Util.Math.Rotate(axesToTest[2], Vector2.Zero, box2.Rotation);
            axesToTest[3] = Util.Math.Rotate(axesToTest[3], Vector2.Zero, box2.Rotation);

            foreach (var axis in axesToTest)
            {
                if (!OverlapOnAxis(box1, box2, axis))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Helpers

        private static bool OverlapOnAxis(AABB box1, AABB box2, Vector2 axis)
        {
            var interval1 = GetInterval(box1, axis);
            var interval2 = GetInterval(box2, axis);
            return ((interval2.X <= interval1.Y) && (interval1.X <= interval2.Y));
        }
        
        private static bool OverlapOnAxis(AABB box1, Box2D box2, Vector2 axis)
        {
            var interval1 = GetInterval(box1, axis);
            var interval2 = GetInterval(box2, axis);
            return ((interval2.X <= interval1.Y) && (interval1.X <= interval2.Y));
        }
        
        private static bool OverlapOnAxis(Box2D box1, Box2D box2, Vector2 axis)
        {
            var interval1 = GetInterval(box1, axis);
            var interval2 = GetInterval(box2, axis);
            return ((interval2.X <= interval1.Y) && (interval1.X <= interval2.Y));
        }

        private static Vector2 GetInterval(AABB rect, Vector2 axis)
        {
            Vector2 result = Vector2.Zero;

            Vector2[] vertices = new[] {
                new Vector2(rect.Min.X, rect.Min.Y), new Vector2(rect.Min.X, rect.Max.Y),
                new Vector2(rect.Max.X, rect.Min.Y), new Vector2(rect.Max.X, rect.Max.Y)
            };

            // Initialise to First Value
            result.X = Vector2.Dot(axis, vertices[0]);
            result.Y = result.X;
            for (int i = 0; i < 4; i++)
            {
                float projection = Vector2.Dot(axis, vertices[i]);
                if (projection < result.X)
                {
                    result.X = projection;
                }

                if (projection > result.Y)
                {
                    result.Y = projection;
                }
            }

            return result;
        }
        
        private static Vector2 GetInterval(Box2D rect, Vector2 axis)
        {
            Vector2 result = Vector2.Zero;

            Vector2[] vertices = rect.GetVertices();

            // Initialise to First Value
            result.X = Vector2.Dot(axis, vertices[0]);
            result.Y = result.X;
            for (int i = 0; i < 4; i++)
            {
                float projection = Vector2.Dot(axis, vertices[i]);
                if (projection < result.X)
                {
                    result.X = projection;
                }

                if (projection > result.Y)
                {
                    result.Y = projection;
                }
            }

            return result;
        }

        
        #endregion
    }
}
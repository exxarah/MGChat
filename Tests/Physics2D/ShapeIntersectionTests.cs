using MGChat.Physics2D.Primitives;
using MGChat.Physics2D;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.Physics2D
{
    public class ShapeIntersectionTests
    {
        #region Circle Tests

        [Test]
        [TestCase(5, 0, 0, 5, 9, 0, TestName = "General")]
        public void Intersects_CircleCircle_ReturnsTrue(
                float radius1, float posX1, float posY1,
                float radius2, float posX2, float posY2
                )
        {
            // Arrange
            var c1 = new Circle(0, radius1);
            c1.Position = new Vector2(posX1, posY1);
            
            var c2 = new Circle(0, radius2);
            c2.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.CircleCircle(c1, c2);
            
            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(5, 0, 0, 5, 11, 0, TestName = "General")]
        public void Intersects_CircleCircle_ReturnsFalse(
            float radius1, float posX1, float posY1,
            float radius2, float posX2, float posY2
        )
        {
            // Arrange
            var c1 = new Circle(0, radius1);
            c1.Position = new Vector2(posX1, posY1);
            
            var c2 = new Circle(0, radius2);
            c2.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.CircleCircle(c1, c2);
            
            // Assert
            Assert.False(result);
        }

        [Test]
        [TestCase(5, 0, 0, 5, 5, 2, 2, TestName = "General")]
        public void Intersects_CircleAABB_ReturnsTrue(
            float radius, float posX1, float posY1,
            float width, float height, float posX2, float posY2
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX1, posY1);

            var box = new AABB(0, width, height);
            box.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.CircleAABB(circle, box);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(5, 0, 0, 5, 5, 11, 2, TestName = "General")]
        public void Intersects_CircleAABB_ReturnsFalse(
            float radius, float posX1, float posY1,
            float width, float height, float posX2, float posY2
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX1, posY1);

            var box = new AABB(0, width, height);
            box.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.CircleAABB(circle, box);

            // Assert
            Assert.False(result);
        }
        
        [Test]
        [TestCase(5, 0, 0, 5, 5, 0, 2, 2, TestName = "General")]
        public void Intersects_CircleBox2D_ReturnsTrue(
            float radius, float posX1, float posY1,
            float width, float height, float rotation, float posX2, float posY2
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX1, posY1);

            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX2, posY2);
            box.Rotation = rotation;

            // Act
            var result = ShapeIntersection.CircleBox2D(circle, box);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(5, 0, 0, 5, 5, 0, 11, 2, TestName = "General")]
        public void Intersects_CircleBox2D_ReturnsFalse(
            float radius, float posX1, float posY1,
            float width, float height, float rotation, float posX2, float posY2
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX1, posY1);

            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX2, posY2);
            box.Rotation = rotation;

            // Act
            var result = ShapeIntersection.CircleBox2D(circle, box);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Box Tests

        [Test]
        [TestCase(10, 10, 0, 0, 10, 10, 5, 5, TestName = "General")]
        public void Intersects_AABBAABB_ReturnsTrue(
            float width1, float height1, float posX1, float posY1,
            float width2, float height2, float posX2, float posY2
            )
        {
            // Arrange
            var box1 = new AABB(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            
            var box2 = new AABB(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.AABBAABB(box1, box2);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 10, 10, 11, 5, TestName = "General")]
        public void Intersects_AABBAABB_ReturnsFalse(
            float width1, float height1, float posX1, float posY1,
            float width2, float height2, float posX2, float posY2
        )
        {
            // Arrange
            var box1 = new AABB(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            
            var box2 = new AABB(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);

            // Act
            var result = ShapeIntersection.AABBAABB(box1, box2);

            // Assert
            Assert.False(result);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 10, 10, 0, 5, 5, TestName = "General")]
        public void Intersects_AABBBox2D_ReturnsTrue(
            float width1, float height1, float posX1, float posY1,
            float width2, float height2, float rotation, float posX2, float posY2
        )
        {
            // Arrange
            var box1 = new AABB(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            
            var box2 = new Box2D(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);
            box2.Rotation = rotation;

            // Act
            var result = ShapeIntersection.AABBBox2D(box1, box2);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 10, 10, 0, 11, 5, TestName = "General")]
        public void Intersects_AABBBox2D_ReturnsFalse(
            float width1, float height1, float posX1, float posY1,
            float width2, float height2, float rotation, float posX2, float posY2
        )
        {
            // Arrange
            var box1 = new AABB(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            
            var box2 = new Box2D(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);
            box2.Rotation = rotation;

            // Act
            var result = ShapeIntersection.AABBBox2D(box1, box2);

            // Assert
            Assert.False(result);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 0, 10, 10, 0, 5, 5, TestName = "General")]
        public void Intersects_Box2DBox2D_ReturnsTrue(
            float width1, float height1, float rotation1, float posX1, float posY1,
            float width2, float height2, float rotation2, float posX2, float posY2
        )
        {
            // Arrange
            var box1 = new Box2D(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            box1.Rotation = rotation1;
            
            var box2 = new Box2D(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);
            box2.Rotation = rotation2;

            // Act
            var result = ShapeIntersection.Box2DBox2D(box1, box2);

            // Assert
            Assert.True(result);
        }

        [Test] [TestCase(10, 10, 0, 0, 0, 10, 10, 0, 11, 5, TestName = "General")]
        public void Intersects_Box2DBox2D_ReturnsFalse(
            float width1, float height1, float rotation1, float posX1, float posY1,
            float width2, float height2, float rotation2, float posX2, float posY2
        )
        {
            // Arrange
            var box1 = new Box2D(0, width1, height1);
            box1.Position = new Vector2(posX1, posY1);
            box1.Rotation = rotation1;
            
            var box2 = new Box2D(0, width2, height2);
            box2.Position = new Vector2(posX2, posY2);
            box2.Rotation = rotation2;

            // Act
            var result = ShapeIntersection.Box2DBox2D(box1, box2);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}
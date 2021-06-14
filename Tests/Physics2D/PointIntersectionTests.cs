using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.Physics2D
{
    public class PointIntersectionTests
    {
        #region Line2D Tests
        
            [Test]
            [TestCase(6, 2, 0, 0, 12, 4, TestName = "General")]
            [TestCase(16, 12, 10, 10, 22, 14, TestName = "General")]
            [TestCase(0, 0, 0, 0, 12, 4, TestName = "Start Of Line")]
            [TestCase(10, 10, 10, 10, 22, 14, TestName = "Start Of Line")]
            [TestCase(12, 4, 0, 0, 12, 4, TestName = "End Of Line")]
            [TestCase(5, 0, 0, 0, 10, 0, TestName = "Horizontal Line")]
            [TestCase(0, 5, 0, 0, 0, 10, TestName = "Vertical Line")]
            public void Contains_Line2D_ReturnsTrue(
                float pointX, float pointY,
                float startX, float startY,
                float endX, float endY
                )
            {
                // Arrange
                var point = new Vector2(pointX, pointY);
                var line = new Line2D(
                    new Vector2(startX, startY),
                    new Vector2(endX, endY)
                    );
                
                // Act
                var actual = line.Contains(point);
                
                // Assert
                Assert.True(actual);
            }
            
            [Test]
            [TestCase(4, 2, 0, 0, 12, 4, TestName = "General")]
            [TestCase(14, 12, 10, 10, 22, 14, TestName = "General")]
            public void Contains_Line2D_ReturnsFalse(
                float pointX, float pointY,
                float startX, float startY,
                float endX, float endY
                )
            {
                // Arrange
                var point = new Vector2(pointX, pointY);
                var line = new Line2D(
                    new Vector2(startX, startY),
                    new Vector2(endX, endY)
                );
                
                // Act
                var actual = line.Contains(point);
                
                // Assert
                Assert.False(actual);
            }
            
        #endregion

        #region AABB Tests

        [Test]
        [TestCase(10, 10, 0, 0, 3, 7, TestName = "General")]
        [TestCase(10, 10, 0, 0, 0, 0, TestName = "Origin")]
        [TestCase(10, 10, 0, 0, 10, 10, TestName = "Extreme")]
        [TestCase(10, 10, 0, 0, 5, 5, TestName = "Center Point")]
        public void Contains_AABB_ReturnsTrue(
            float width, float height, float posX, float posY,
            float pointX, float pointY
        )
        {
            // Arrange
            var aabb = new AABB(0, width, height);
            aabb.Position = new Vector2(posX, posY);

            var point = new Vector2(pointX, pointY);
            
            // Act
            var result = aabb.Contains(point);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, -1, 0, TestName = "General")]
        public void Contains_AABB_ReturnsFalse(
            float width, float height, float posX, float posY,
            float pointX, float pointY
        )
        {
            // Arrange
            var aabb = new AABB(0, width, height);
            aabb.Position = new Vector2(posX, posY);

            var point = new Vector2(pointX, pointY);
            
            // Act
            var result = aabb.Contains(point);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region Box2D Tests

        [Test]
        [TestCase(10, 10, 0, 0, 0, 5, 5, TestName = "General")]
        [TestCase(10, 10, 0, 0, 0, 4, 4.3f, TestName = "Decimals")]
        [TestCase(10, 10, 0, 0, 0, 0.1f, 0.1f, TestName = "Borderline")]
        [TestCase(10, 10, 45, -5, -5, -1, -1, TestName = "Rotated")]
        [TestCase(10, 10, 45, -5, -5, -3.43553390593f, -3.43553390593f, TestName = "Rotated Borderline")]
        public void Contains_Box2D_ReturnsTrue(
            float width, float height, float rotation, float posX, float posY,
            float pointX, float pointY
        )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var point = new Vector2(pointX, pointY);
            
            // Act
            var actual = box.Contains(point);
            
            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 0, 20, 5, TestName = "General")]
        [TestCase(10, 10, 0, 0, 0, 0, -0.1f, TestName = "Borderline")]
        [TestCase(10, 10, 45, 5, 5, -3.63553390593f, -3.63553390593f, TestName = "Rotated Borderline")]
        [TestCase(10, 10, 45, 5, 5, -3.63553390593f + 10, -3.63553390593f + 10, TestName = "Rotated Borderline")]
        public void Contains_Box2D_ReturnsFalse(
            float width, float height, float rotation, float posX, float posY,
            float pointX, float pointY
        )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var point = new Vector2(pointX, pointY);
            
            // Act
            var actual = box.Contains(point);
            
            // Assert
            Assert.False(actual);
        }

        #endregion

        #region Circle Tests

        [Test]
        [TestCase(5, 0, 0, 5, 5, TestName = "General")]
        [TestCase(5, -5, -5, 3, -2, TestName = "General")]
        [TestCase(5, -5, -5, -4.9f, 0, TestName = "Borderline")]
        public void Contains_Circle_ReturnsTrue(
            float radius, float posX, float posY,
            float pointX, float pointY
            )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var point = new Vector2(pointX, pointY);
            
            // Act
            var actual = circle.Contains(point);
            
            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(5, 0, 0, 1, 1, TestName = "General")]
        [TestCase(5, -5, -5, -6, -6, TestName = "General")]
        public void Contains_Circle_ReturnsFalse(
            float radius, float posX, float posY,
            float pointX, float pointY
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var point = new Vector2(pointX, pointY);
            
            // Act
            var actual = circle.Contains(point);
            
            // Assert
            Assert.False(actual);
        }

        #endregion
    }
}
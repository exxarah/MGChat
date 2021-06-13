using System;
using System.Diagnostics;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.Physics2D
{
    public class CollisionDetectionTests
    {
        [SetUp]
        public void Setup() { }

        #region Line2D Tests

                [Test]
        [TestCase(0, 0, 0, 0, 12, 4, TestName = "Start Of Line")]
        [TestCase(12, 4, 0, 0, 12, 4, TestName = "End Of Line")]
        [TestCase(5, 0, 0, 0, 10, 0, TestName = "Horizontal Line")]
        [TestCase(0, 5, 0, 0, 0, 10, TestName = "Vertical Line")]
        public void Contains_Line_ReturnsTrue(
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
        [TestCase(1, 0, 0, 0, 12, 4)]
        public void Contains_Line_ReturnsFalse(
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

        [Test]
        [TestCase(0, 5, 10, 5, 5, 0, 5, 10)]
        public void Intersects_Line_ReturnsTrue(
            float line1StartX, float line1StartY, float line1EndX, float line1EndY,
            float line2StartX, float line2StartY, float line2EndX, float line2EndY
            )
        {
            // Arrange
            var line1 = new Line2D(
                new Vector2(line1StartX, line1StartY),
                new Vector2(line1EndX, line1EndY)
            );
            
            var line2 = new Line2D(
                new Vector2(line2StartX, line2StartY),
                new Vector2(line2EndX, line2EndY)
            );

            // Act
            var actual = line1.Intersects(line2);

            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(0, 5, 10, 5, 0, 5, 10, 5)]
        public void Intersects_Line_ReturnsFalse(
            float line1StartX, float line1StartY, float line1EndX, float line1EndY,
            float line2StartX, float line2StartY, float line2EndX, float line2EndY
        )
        {
            // Arrange
            var line1 = new Line2D(
                new Vector2(line1StartX, line1StartY),
                new Vector2(line1EndX, line1EndY)
            );
            
            var line2 = new Line2D(
                new Vector2(line2StartX, line2StartY),
                new Vector2(line2EndX, line2EndY)
            );

            // Act
            var actual = line1.Intersects(line2);

            // Assert
            Assert.False(actual);
        }

        #endregion

        #region AABB Tests

                [Test]
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
        [TestCase(10, 10, 0, 0, -1, 0)]
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

        [Test]
        [TestCase(10, 10, 0, 0, 5, 5, 15, 20)]
        public void Intersects_AABB_ReturnsTrue(
            float width, float height, float posX, float posY,
            float lineStartX, float lineStartY, float lineEndX, float lineEndY
            )
        {
            // Arrange
            var aabb1 = new AABB(0, width, height);
            aabb1.Position = new Vector2(posX, posY);

            var aabb2 = new Line2D(
                new Vector2(lineStartX, lineStartY),
                new Vector2(lineEndX, lineEndY)
            );

            // Act
            var actual = aabb1.Intersects(aabb2);

            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 20, 20, 15, 20)]
        public void Intersects_AABB_ReturnsFalse(
            float width, float height, float posX, float posY,
            float lineStartX, float lineStartY, float lineEndX, float lineEndY
        )
        {
            // Arrange
            var aabb1 = new AABB(0, width, height);
            aabb1.Position = new Vector2(posX, posY);

            var aabb2 = new Line2D(
                new Vector2(lineStartX, lineStartY),
                new Vector2(lineEndX, lineEndY)
            );

            // Act
            var actual = aabb1.Intersects(aabb2);

            // Assert
            Assert.False(actual);
        }

        #endregion

        #region Box2D Tests

        [Test]
        [TestCase(10, 10, 0, 0, 0, 5, 5)]
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
        [TestCase(10, 10, 0, 0, 0, 20, 5)]
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
        
        [Test]
        [TestCase(10, 10, 0, 0, 0, 20, 5, 0, 0)]
        public void Intersects_Box2D_ReturnsTrue(
            float width, float height, float rotation, float posX, float posY,
            float startX, float startY, float endX, float endY
        )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var line = new Line2D(
                new Vector2(startX, startY),
                new Vector2(endX, endY)
            );
            
            // Act
            var actual = box.Intersects(line);
            
            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(10, 10, 0, 0, 0, 20, 5, 30, 10)]
        public void Intersects_Box2D_ReturnsFalse(
            float width, float height, float rotation, float posX, float posY,
            float startX, float startY, float endX, float endY
        )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var line = new Line2D(
                new Vector2(startX, startY),
                new Vector2(endX, endY)
            );
            
            // Act
            var actual = box.Intersects(line);
            
            // Assert
            Assert.False(actual);
        }

        #endregion

        #region Circle Tests

        [Test]
        [TestCase(5, 0, 0, 5, 5)]
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
        [TestCase(5, 0, 0, 1, 1)]
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

        [Test]
        [TestCase(5, 0, 0, 5, 5, 15, 20)]
        public void Intersects_Circle_ReturnsTrue(
            float radius, float posX, float posY,
            float startX, float startY, float endX, float endY
            )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var line = new Line2D(
                new Vector2(startX, startY),
                new Vector2(endX, endY)
            );

            // Act
            var actual = circle.Intersects(line);

            // Assert
            Assert.True(actual);
        }
        
        [Test]
        [TestCase(5, 0, 0, 1, 1, 1, -5)]
        public void Intersects_Circle_ReturnsFalse(
            float radius, float posX, float posY,
            float startX, float startY, float endX, float endY
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var line = new Line2D(
                new Vector2(startX, startY),
                new Vector2(endX, endY)
            );

            // Act
            var actual = circle.Intersects(line);

            // Assert
            Assert.False(actual);
        }

        #endregion
    }
}
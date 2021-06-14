using System;
using System.Diagnostics;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.Physics2D
{
    public class LineIntersectionTests
    {
        #region Line2D Tests
        
        [Test]
        [TestCase(0, 5, 10, 5, 5, 0, 5, 10, TestName = "General")]
        public void Intersects_Line2D_ReturnsTrue(
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
        [TestCase(0, 5, 10, 5, 0, 5, 10, 5, TestName = "General")]
        public void Intersects_Line2D_ReturnsFalse(
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
        [TestCase(10, 10, 0, 0, 5, 5, 15, 20, TestName = "General")]
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
        [TestCase(10, 10, 0, 0, 20, 20, 15, 20, TestName = "General")]
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
        [TestCase(10, 10, 0, 0, 0, 20, 5, 0, 0, TestName = "General")]
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
        [TestCase(10, 10, 0, 0, 0, 20, 5, 30, 10, TestName = "General")]
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
        [TestCase(5, 0, 0, 5, 5, 15, 20, TestName = "General")]
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
        [TestCase(5, 0, 0, 1, 1, 1, -5, TestName = "General")]
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
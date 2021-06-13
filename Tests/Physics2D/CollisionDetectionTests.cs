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

        [Test]
        [TestCase(0, 0, 0, 0, 12, 4, TestName = "Start Of Line")]
        [TestCase(12, 4, 0, 0, 12, 4, TestName = "End Of Line")]
        [TestCase(5, 0, 0, 0, 10, 0, TestName = "Horizontal Line")]
        [TestCase(0, 5, 0, 0, 0, 10, TestName = "Vertical Line")]
        public void Contains_PointOnLine_ReturnsTrue(
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
    }
}
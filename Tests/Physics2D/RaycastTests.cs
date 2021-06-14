using MGChat.Physics2D.Primitives;
using MGChat.Physics2D;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.Physics2D
{
    public class RaycastTests
    {
        #region Circle Tests

        [Test]
        [TestCase(4, 5, 6, 1, 1, 0.8f, 0.6f)]
        [TestCase(4, 5, 6, 8, 8, 1f, 1f)]
        public void Raycast_Circle_ReturnsTrue(
            float radius, float posX, float posY,
            float originX, float originY, float dirX, float dirY
            )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastCircle(circle, ray);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(4, 5, 6, 8, 1, 0.5f, 0.5f)]
        [TestCase(4, 5, 6, 1, 1, -0.5f, -0.8f)]
        public void Raycast_Circle_ReturnsFalse(
            float radius, float posX, float posY,
            float originX, float originY, float dirX, float dirY
        )
        {
            // Arrange
            var circle = new Circle(0, radius);
            circle.Position = new Vector2(posX, posY);

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastCircle(circle, ray);

            // Assert
            Assert.False(result);
        }
        

        #endregion

        #region Box2D Tests

        [Test]
        [TestCase(8, 8, 30, 4, 4, 3, 2, 0.8f, 0.6f)]
        public void Raycast_Box2D_ReturnsTrue(
            float width, float height, float rotation, float posX, float posY,
            float originX, float originY, float dirX, float dirY
            )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastBox2D(box, ray);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(8, 8, 30, 4, 4, 10, 1, -0.8f, -0.6f)]
        public void Raycast_Box2D_ReturnsFalse(
            float width, float height, float rotation, float posX, float posY,
            float originX, float originY, float dirX, float dirY
        )
        {
            // Arrange
            var box = new Box2D(0, width, height);
            box.Position = new Vector2(posX, posY);
            box.Rotation = rotation;

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastBox2D(box, ray);

            // Assert
            Assert.False(result);
        }

        #endregion
        
        #region Box2D Tests

        [Test]
        [TestCase(8, 8, 4, 4, 3, 2, 0.8f, 0.6f)]
        public void Raycast_AABB_ReturnsTrue(
            float width, float height, float posX, float posY,
            float originX, float originY, float dirX, float dirY
        )
        {
            // Arrange
            var box = new AABB(0, width, height);
            box.Position = new Vector2(posX, posY);

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastAABB(box, ray);

            // Assert
            Assert.True(result);
        }
        
        [Test]
        [TestCase(8, 8, 4, 4, 3, 2, -0.8f, -0.6f)]
        public void Raycast_AABB_ReturnsFalse(
            float width, float height, float posX, float posY,
            float originX, float originY, float dirX, float dirY
        )
        {
            // Arrange
            var box = new AABB(0, width, height);
            box.Position = new Vector2(posX, posY);

            var ray = new Ray2D(
                new Vector2(originX, originY),
                new Vector2(dirX, dirY)
            );

            // Act
            var result = Raycast.RaycastAABB(box, ray);

            // Assert
            Assert.False(result);
        }

        #endregion
    }
}
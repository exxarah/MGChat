using System.Diagnostics;
using MGChat.TileMap;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Tests.TileMap
{
    public class GridTests
    {
        private Grid _grid;

        [SetUp]
        public void Setup()
        {
            // TODO: Fill Grid with junk cells
            _grid = new Grid(10, 10);
        }
        
        [Test]
        [TestCase(5, 5, 0, 5, TestName = "General")]
        [TestCase(5, 5, 1, 4, TestName = "General")]
        [TestCase(5, 5, 2, 3, TestName = "General")]
        public void GetNeighbourhoodMoore_ReturnsTrue(int x, int y, int r, int offsetVal)
        {
            // Arrange
            var expected = new Grid(2 * r + 1, 2 * r + 1, new Vector2(offsetVal, offsetVal)).GridActual;
            
            // Act
           var actual =  _grid.GetNeighbourhoodMoore(x, y, r);

           // Assert
            Assert.AreEqual(expected.Length, actual.Length);
            var length = System.Math.Min(actual.GetLength(0), expected.GetLength(0));
            for (int xIndex = 0; xIndex < length; xIndex++)
            {
                for (int yIndex = 0; yIndex < length; yIndex++)
                {
                    Assert.AreEqual(expected[xIndex, yIndex].X, actual[xIndex, yIndex].X);
                    Assert.AreEqual(expected[xIndex, yIndex].Y, actual[xIndex, yIndex].Y);
                }
            }
        }

        [Test]
        [TestCase(5, 5, 2, 5, TestName = "General")]
        [TestCase(5, 5, 3, 5, TestName = "General")]
        public void GetNeighbourhoodMoore_ReturnsFalse(int x, int y, int r, int offsetVal)
        {
            // Arrange
            var expected = new Grid(2 * r + 1, 2 * r + 1, new Vector2(offsetVal, offsetVal)).GridActual;
            
            // Act
            var actual =  _grid.GetNeighbourhoodMoore(x, y, r);

            // Assert
            var length = System.Math.Min(actual.GetLength(0), expected.GetLength(0));
            for (int xIndex = 0; xIndex < length; xIndex++)
            {
                for (int yIndex = 0; yIndex < length; yIndex++)
                {
                    Assert.AreNotEqual(expected[xIndex, yIndex].X, actual[xIndex, yIndex].X);
                    Assert.AreNotEqual(expected[xIndex, yIndex].Y, actual[xIndex, yIndex].Y);
                }
            }
        }
    }
}
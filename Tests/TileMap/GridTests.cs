using System;
using System.Collections.Generic;
using System.Diagnostics;
using MGChat.TileMap;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using SharpDX.Direct3D9;

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
        
        public bool Equals(Cell c1, Cell c2)
        {
            if (c1 is null && c2 is null)
            {
                return true;
            }

            if (c1 is null || c2 is null)
            {
                return false;
            }

            return (c1.X == c2.X) && (c1.Y == c2.Y);
        }

        public bool Equals(Cell[,] list1, Cell[,] list2)
        {
            if (list1.GetLength(0) != list2.GetLength(0) ||
                list1.GetLength(1) != list2.GetLength(1))
            {
                return false;
            }
            
            for (int xIndex = 0; xIndex < list1.GetLength(0); xIndex++)
            {
                for (int yIndex = 0; yIndex < list1.GetLength(1); yIndex++)
                {
                    bool equal = Equals(list1[xIndex, yIndex], list2[xIndex, yIndex]);
                    if (!equal)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        [Test]
        [TestCase(5, 5, 0, 5, TestName = "General1")]
        [TestCase(5, 5, 1, 4, TestName = "General2")]
        [TestCase(5, 5, 2, 3, TestName = "General3")]
        public void GetNeighbourhoodMoore_ReturnsTrue(int x, int y, int r, int offsetVal)
        {
            // Arrange
            var expected = new Grid(2 * r + 1, 2 * r + 1, new Vector2(offsetVal, offsetVal)).GridActual;
            
            // Act
           var actual =  _grid.GetNeighbourhoodMoore(x, y, r);

           // Assert
           Assert.True(Equals(expected, actual));
        }

        [Test]
        [TestCase(5, 5, 2, 5, TestName = "General1")]
        [TestCase(5, 5, 3, 5, TestName = "General2")]
        public void GetNeighbourhoodMoore_ReturnsFalse(int x, int y, int r, int offsetVal)
        {
            // Arrange
            var expected = new Grid(2 * r + 1, 2 * r + 1, new Vector2(offsetVal, offsetVal)).GridActual;
            
            // Act
            var actual =  _grid.GetNeighbourhoodMoore(x, y, r);

            // Assert
            Assert.False(Equals(expected, actual));
        }

        [Test]
        public void GetNeighbourhoodMoore_CheckEdges()
        {
            // Arrange
            Cell[,] expected1 = new Cell[3, 3]
            {
                {null, null, null},
                {null, new Cell(0, 0), new Cell(0, 1)},
                {null, new Cell(1, 0), new Cell(1, 1)}
            };
            Cell[,] expected2 = new Cell[5, 5]
            {
                {new Cell(7, 7), new Cell(7, 8), new Cell(7, 9), null, null},
                {new Cell(8, 7), new Cell(8, 8), new Cell(8, 9), null, null},
                {new Cell(9, 7), new Cell(9, 8), new Cell(9, 9), null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
            };

            // Act
            Cell[,] actual1 = _grid.GetNeighbourhoodMoore(0, 0);
            Cell[,] actual2 = _grid.GetNeighbourhoodMoore(9, 9, 2);

            // Assert
            Assert.True(Equals(expected1, actual1));
            Assert.True(Equals(expected2, actual2));
        }

        [Test]
        public void GetNeighbourhoodVonNeumann_ReturnsTrue()
        {
            // Arrange
            Cell[,] expected1 = new Cell[3, 3]
            {
                {null, new Cell(4, 5), null},
                {new Cell(5, 4), new Cell(5, 5), new Cell(5, 6)},
                {null, new Cell(6, 5), null}
            };
            Cell[,] expected2 = new Cell[5, 5]
            {
                {null,               null,               new Cell(3, 5), null,               null              },
                {null,               new Cell(4, 4), new Cell(4, 5), new Cell(4, 6), null              },
                {new Cell(5, 3), new Cell(5, 4), new Cell(5, 5), new Cell(5, 6), new Cell(5, 7)},
                {null,               new Cell(6, 4), new Cell(6, 5), new Cell(6, 6), null              },
                {null,               null,               new Cell(7, 5), null,               null              },
            };
            
            // Act
            var actual1 = _grid.GetNeighbourhoodVonNeumann(5, 5, 1);
            var actual2 = _grid.GetNeighbourhoodVonNeumann(5, 5, 2);
            
            //Assert
            Assert.True(Equals(expected1, actual1));
            Assert.True(Equals(expected2, actual2));
        }

        [Test]
        public void GetNeighbourhoodVonNeumann_ReturnsFalse()
        {
            // Arrange
            Cell[,] expected1 = new Cell[3, 3]
            {
                {null, new Cell(4, 5), null},
                {new Cell(5, 4), new Cell(0, 0), new Cell(5, 6)},
                {null, new Cell(6, 5), null}
            };
            
            // Act
            var actual1 = _grid.GetNeighbourhoodVonNeumann(4, 4, 1);
            
            //Assert
            Assert.False(Equals(expected1, actual1));
        }
    }
}
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MGChat.TileMap
{
    public class Grid
    {
        private Cell[,] _grid;
        private int _width, _height;

        public Cell[,] GridActual => _grid;

        public Grid(int width, int height, Vector2 offset=default(Vector2))
        {
            _grid = new Cell[width, height];
            _width = width;
            _height = height;
            if (offset == default(Vector2))
            {
                offset = Vector2.Zero;
            }
            
            for (int x = 0 + (int)offset.X; x < (width + offset.X); x++)
            {
                for (int y = 0 + (int)offset.Y; y < (height + offset.Y); y++)
                {
                    ChangeTile(x - (int)offset.X, y - (int)offset.Y, new Cell(x, y));
                }
            }
        }

        public Cell GetTile(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return null;
            }
            return _grid[x, y];
        }

        public bool ChangeTile(int x, int y, Cell newCell)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return false;
            }
            _grid[x, y] = newCell;
            return true;
        }

        public Cell[,] GetNeighbourhoodMoore(int centerX, int centerY, int r = 1)
        {
            int size = 2 * r + 1;
            Cell[,] result = new Cell[size, size];

            int minX = centerX - r;
            int maxX = centerX + r;
            int minY = centerY - r;
            int maxY = centerY + r;
            
            Debug.WriteLine($"{minX}, {minY}, {maxX}, {maxY}");

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    result[x - minX, y - minY] = GetTile(x, y);
                }
            }

            return result;
        }

        public Cell[,] GetNeighbourhoodVonNeumann(int centerX, int centerY, int r = 1)
        {
            int size = 2 * r + 1;   // Reuse Moore size, because we need equal sizing
            Cell[,] result = new Cell[size, size];

            RecursiveVonNeumann(r, r, centerX, centerY, r, ref result);

            return result;
        }

        private void RecursiveVonNeumann(int resultX, int resultY, int gridX, int gridY, int r, ref Cell[,] result)
        {
            result[resultX, resultY] = _grid[gridX, gridY];
            if (r > 0)
            {
                RecursiveVonNeumann(resultX - 1, resultY, gridX - 1, gridY, r - 1, ref result);
                RecursiveVonNeumann(resultX + 1, resultY, gridX + 1, gridY, r - 1, ref result);
                RecursiveVonNeumann(resultX, resultY - 1, gridX, gridY - 1, r - 1, ref result);
                RecursiveVonNeumann(resultX, resultY + 1, gridX, gridY + 1, r - 1, ref result);
            }
        }
    }
}
using MineSweeper.Models;
using System.Collections.Generic;

namespace MineSweeper
{
    public static class Utility
    {
        public static readonly int[,] ShiftMatrix = new int[,]
        {
            { -1, 0, 1, 1, 1, 0, -1, -1 },
            { -1, -1, -1, 0, 1, 1, 1, 0 }
        };

        public static IEnumerable<Cell> GetNeighbors(Cell[,] cells, int x, int y)
        {
            for (int i = 0; i < ShiftMatrix.GetLength(1); i++)
            {
                int indexX = x + ShiftMatrix[0, i];
                int indexY = y + ShiftMatrix[1, i];
                if (indexX >= 0 && indexX < cells.GetLength(0) && indexY >= 0 && indexY < cells.GetLength(1))
                    yield return cells[indexX, indexY];
            }
        }
    }
}

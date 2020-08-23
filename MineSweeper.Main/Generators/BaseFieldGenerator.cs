using MineSweeper.Models;
using System.Collections.Generic;

namespace MineSweeper.Generators
{
    public abstract class BaseFieldGenerator<T>
    {
        protected virtual void InitializeField(Cell[,] cells, T context)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = new Cell(x, y);
                    InitializeCell(cells[x, y], context);
                }
            }
        }

        protected void CalculateNumbers(Cell[,] cells)
        {
            for(int x = 0; x < cells.GetLength(0); x++)
            {
                for(int y = 0; y < cells.GetLength(1); y++)
                {
                    if(cells[x, y].Mine)
                    {
                        foreach (var neighbor in Utility.GetNeighbors(cells, x, y))
                            neighbor.Number++;
                    }
                }
            }
        }

        protected virtual void InitializeCell(Cell cell, T context)
        {
        }
    }
}
using MineSweeper.Models;

namespace MineSweeper.Generators
{
    public abstract class BaseFieldGenerator<T>
    {
        protected virtual void InitializeField(Cell[,] cells, T context)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(1); x++)
                {

                    cells[y, x] = new Cell(x, y);
                    InitializeCell(cells[y, x], context);
                }
            }
        }

        protected void CalculateNumbers(Cell[,] cells)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(1); x++)
                {

                    if (cells[y, x].Mine)
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
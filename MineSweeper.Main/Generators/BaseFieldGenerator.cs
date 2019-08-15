namespace MineSweeper.Generators
{
    public abstract class BaseFieldGenerator<T>
    {
        public virtual void InitializeField(Cell[,] cells, int width, int height, T context)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = new Cell
                    {
                        X = i,
                        Y = j,
                    };
                    InitializeCell(cells[i, j], context);
                }
            }
        }

        public virtual void InitializeCell(Cell cell, T context)
        {
        }
    }
}
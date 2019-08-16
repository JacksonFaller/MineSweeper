using MineSweeper.Generators.Interfaces;

namespace MineSweeper
{
    public class GameField
    {
        protected Cell[,] Cells { get; }

        public int Width { get; }

        public int Height { get; }

        public int Seed { get; }

        public int MinesTotal { get; }

        public int OpenedCells { get; private set; }

        public Cell this[int x, int y] => Cells[x, y];

        public Cell GetCell(int x, int y) => this[x, y];

        public GameField(int width, int height, int density, int seed,
            IFieldGenerator fieldGenerator)
        {
            Width = width;
            Height = height;
            Seed = seed;
            Cells = new Cell[width, height];
            MinesTotal = fieldGenerator.GenerateField(Cells, seed, width, height, density);
        }

        public void MarkCell(int x, int y)
        {
            Cells[x, y].IsMarked = true;
            OpenedCells++;
        }

        public void UnMarkCell(int x, int y)
        {
            Cells[x, y].IsMarked = false;
            OpenedCells--;
        }
        
        public void OpenCell(int x, int y)
        {
            OpenedCells++;
        }

        public int GetCellNumber(int x, int y)
        {
            int number = 0;
            if (x - 1 >= 0)
            {
                if (Cells[x - 1, y].IsMine) number++;
                if (y - 1 >= 0 && Cells[x - 1, y - 1].IsMine) number++;
                if (y + 1 < Height && Cells[x - 1, y + 1].IsMine) number++;                
            }
            if (x + 1 < Width)
            {
                if (Cells[x + 1, y].IsMine) number++;
                if (y - 1 >= 0 && Cells[x + 1, y - 1].IsMine) number++;
                if (y + 1 < Height && Cells[x + 1, y + 1].IsMine) number++;
            }

            if (y - 1 >= 0)
            {
                if (Cells[x, y - 1].IsMine) number++;
                if (x - 1 >= 0 && Cells[x - 1, y - 1].IsMine) number++;
                if (x + 1 < Height && Cells[x + 1, y - 1].IsMine) number++;
            }
            if (y + 1 < Height)
            {
                if (Cells[x, y + 1].IsMine) number++;
                if (x - 1 >= 0 && Cells[x - 1, y + 1].IsMine) number++;
                if (x + 1 < Height && Cells[x + 1, y + 1].IsMine) number++;
            }
            return number;
        }
    }
}
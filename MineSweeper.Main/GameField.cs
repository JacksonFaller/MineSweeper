using MineSweeper.Interfaces;

namespace MineSweeper
{
    public class GameField
    {
        protected Cell[,] Cells { get; }

        public int Width { get; }

        public int Height { get; }

        public int Seed { get; }

        public int MinesCount { get; set; }

        public Cell this[int x, int y] => Cells[x, y];

        public Cell GetCell(int x, int y) => this[x, y];

        public GameField(int width, int height, int density, int seed,
            IFieldGenerator fieldGenerator)
        {
            Width = width;
            Height = height;
            Seed = seed;
            Cells = new Cell[width, height];
            MinesCount = fieldGenerator.GenerateField(Cells, seed, width, height, density);
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
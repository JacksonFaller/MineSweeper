namespace MineSweeper.Models
{
    public class ResultCell
    {
        public int X { get; }

        public int Y { get; }

        public int Number { get; }

        public bool Mine { get; }

        public ResultCell(int x, int y, int number, bool mine)
        {
            X = x;
            Y = y;
            Number = number;
            Mine = mine;
        }

        public ResultCell(Cell cell) : this(cell.X, cell.Y, cell.Number, cell.Mine)
        {
        }
    }
}

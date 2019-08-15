namespace MineSweeper
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsMine { get; set; }
        public bool IsMarked { get; set; }
    }
}
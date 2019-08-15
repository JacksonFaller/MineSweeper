namespace MineSweeper
{
   public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MoveType Type { get; set; }
    }

    public enum MoveType
    {
        Click,
        Mark
    }
}
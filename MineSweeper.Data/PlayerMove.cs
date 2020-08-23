namespace MineSweeper.Data
{
    public class PlayerMove
    {
        public int X { get; }
        public int Y { get; }
        public MoveType Type { get; }

        public PlayerMove(int x, int y, MoveType type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }

    public enum MoveType
    {
        Click,
        Flag,
        Unflag
    }
}
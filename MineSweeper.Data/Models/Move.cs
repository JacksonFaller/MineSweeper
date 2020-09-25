namespace MineSweeper.Data.Models
{
    public class Move
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Type of a move
        /// </summary>
        public MoveType Type { get; set; }

        public Move(int x, int y, MoveType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public Move()
        {
        }

        public override int GetHashCode() => System.HashCode.Combine(X, Y, Type);

        public override bool Equals(object obj) => Equals(obj as Move);

        public bool Equals(Move move)
        {
            if (move is null)
                return false;

            if (ReferenceEquals(this, move))
                return true;

            return X == move.X && Y == move.Y && Type == move.Type;
        }
    }
}
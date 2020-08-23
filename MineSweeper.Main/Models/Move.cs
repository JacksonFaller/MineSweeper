using MineSweeper.Data;

namespace MineSweeper.Models
{
    public class Move
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Type of a move
        /// </summary>
        public MoveType Type { get; }


        public Move(int x, int y, MoveType type)
        {
            X = x;
            Y = y;
            Type = type;
        }


        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            hash = (hash * 7) + Type.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Move);
        }

        public bool Equals(Move move)
        {
            // Check for null
            if (move is null)
                return false;

            // Check for same reference
            if (ReferenceEquals(this, move))
                return true;

            return X == move.X && Y == move.Y && Type == move.Type;
        }

        public static implicit operator PlayerMove(Move move)
        {
            return new PlayerMove(move.X, move.Y, move.Type);
        }

        public static implicit operator Move(PlayerMove playerMove)
        {
            return new Move(playerMove.X, playerMove.Y, playerMove.Type);
        }
    }
}
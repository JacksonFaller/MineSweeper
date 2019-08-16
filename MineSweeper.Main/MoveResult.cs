using System.Collections.Generic;

namespace MineSweeper
{
    public class MoveResult
    {
        public List<ResultCell> OpenedCells { get; }
        public MoveResultType Type { get; }

        public MoveResult(MoveResultType type, List<ResultCell> openedCells = null)
        {
            Type = type;
            OpenedCells = openedCells;
        }
    }

    public class ResultCell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsMarked { get; }
        public int Number { get; }

        public ResultCell(int x, int y, bool isMarked, int number)
        {
            X = x;
            Y = y;
            IsMarked = isMarked;
            Number = number;
        }

        public ResultCell(Cell cell, int number) : this(cell.X, cell.Y, cell.IsMarked, number)
        {
        }
    }
        
    public enum MoveResultType
    {
        Opened,
        Marked,
        UnMarked,
        GameOver,
        Victory
    }
}
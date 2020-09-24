using MineSweeper.Enums;
using System.Collections.Generic;

namespace MineSweeper.Models
{
    public class MoveResult
    {
        public IList<ResultCell> OpenedCells { get; }
        public MoveResultType ResultType { get; }

        public MoveResult(MoveResultType type, IList<ResultCell> openedCells = null)
        {
            ResultType = type;
            OpenedCells = openedCells;
        }
    }
}
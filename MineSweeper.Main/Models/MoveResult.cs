using MineSweeper.Enums;
using System.Collections.Generic;

namespace MineSweeper.Models
{
    public class MoveResult
    {
        public List<ResultCell> OpenedCells { get; }
        public MoveResultType ResultType { get; }

        public MoveResult(MoveResultType type, List<ResultCell> openedCells = null)
        {
            ResultType = type;
            OpenedCells = openedCells;
        }
    }
}
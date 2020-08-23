using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Data;
using MineSweeper.Enums;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;

namespace MineSweeper.Models
{
    public class Game<T> where T : FieldGeneratorParamsBase
    {
        public IEnumerable<Move> PlayerMoves => Moves;
        public IFieldGenerator<T> FieldGenerator => Field.FieldGenerator;

        protected GameField<T> Field { get; }

        protected HashSet<Move> Moves { get; }

        public bool Finished { get; private set; }

        public Game(IFieldGenerator<T> fieldGenerator, int width, int height)
        {
            Field = new GameField<T>(width, height, fieldGenerator);
            Moves = new HashSet<Move>();
        }

        public MoveResult MakeMove(Move playerMove)
        {
            if (Finished)
                return new MoveResult(MoveResultType.Finished);

            Moves.Add(playerMove);
            switch (playerMove.Type)
            {
                case MoveType.Click:
                {
                    return OpenCell(playerMove.X, playerMove.Y);
                }
                case MoveType.Flag:
                {
                    Field.FlagCell(playerMove.X, playerMove.Y);
                    return new MoveResult(MoveResultType.Flagged);
                }
                case MoveType.Unflag:
                {
                    Field.UnflagCell(playerMove.X, playerMove.Y);
                    Moves.Remove(playerMove);
                    Moves.Remove(new Move(playerMove.X, playerMove.Y, MoveType.Flag));
                    return new MoveResult(MoveResultType.Unflagged);
                }
                default:
                    throw new ArgumentOutOfRangeException("Invalid move type.", nameof(playerMove));
            }
        }

        protected MoveResult OpenCell(int x, int y)
        {
            Cell cell = Field[x, y];
            if (cell.Mine)
            {
                Finished = true;
                List<ResultCell> unopenedCells = Field.GetUnflaggedMines().Select(x => new ResultCell(x)).ToList();
                return new MoveResult(MoveResultType.GameOver, unopenedCells);
            }

            List<ResultCell> openedCells = Field.OpenCell(x, y).Select(x => new ResultCell(x)).ToList();
            MoveResultType resultType = Field.CellsToOpen == 0 ? MoveResultType.Victory : MoveResultType.Opened;
            return new MoveResult(resultType, openedCells);
        }

        public void CheckNeighbors(Cell cell, List<ResultCell> result)
        {
            result.Add(new ResultCell(cell));
            if (cell.Number != 0)
                return;

            foreach (Cell neighbor in Field.GetNeighbors(cell.X, cell.Y))
                CheckNeighbors(neighbor, result);
        }
    }
}
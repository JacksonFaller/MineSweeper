using MineSweeper.Data;
using MineSweeper.Enums;
using MineSweeper.Generators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper.Models
{
    public class Game
    {
        public IEnumerable<Move> PlayerMoves => Moves;
        public IFieldGenerator FieldGenerator => Field.FieldGenerator;
        public bool Finished { get; private set; }

        protected GameField Field { get; }
        protected HashSet<Move> Moves { get; }

        public Game(IFieldGenerator fieldGenerator, int width, int height)
        {
            Field = new GameField(width, height, fieldGenerator);
            Moves = new HashSet<Move>();
        }

        public MoveResult MakeMove(Move playerMove)
        {
            if (Finished)
                return new MoveResult(MoveResultType.Finished);

            switch (playerMove.Type)
            {
                case MoveType.Click:
                {
                    Moves.Add(playerMove);
                    return OpenCell(playerMove.X, playerMove.Y);
                }
                case MoveType.Flag:
                {
                    Moves.Add(playerMove);
                    Field.FlagCell(playerMove.X, playerMove.Y);
                    return new MoveResult(MoveResultType.Flagged);
                }
                case MoveType.Unflag:
                {
                    Field.UnflagCell(playerMove.X, playerMove.Y);
                    Moves.Remove(new Move(playerMove.X, playerMove.Y, MoveType.Flag));
                    return new MoveResult(MoveResultType.Unflagged);
                }
                case MoveType.OpenNeighbors:
                {
                    var validationResult = ValidateNeighbors(playerMove.X, playerMove.Y);
                    if (validationResult != null)
                        return validationResult;

                    Moves.Add(playerMove);
                    return OpenNeighbors(playerMove.X, playerMove.Y);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerMove), $"Move type.{playerMove.Type} is not supported");
            }
        }

        protected MoveResult ValidateNeighbors(int x, int y)
        {
            int flaggedCount = 0;
            bool gameOver = false;
            foreach (var cell in Field.GetNeighbors(x, y))
            {
                if (cell.Flagged)
                    flaggedCount++;
                else if (cell.Mine)
                    gameOver = true;
            }

            if (flaggedCount != Field[y, x].Number)
                return new MoveResult(MoveResultType.Opened, new List<ResultCell>());

            return gameOver ? GameOver() : null;
        }

        protected MoveResult OpenNeighbors(int x, int y)
        {
            List<ResultCell> openedCells = Field.GetNeighbors(x, y)
                .Where(x => !x.Flagged && !x.Oppened)
                .SelectMany(cell => Field.OpenCell(cell.X, cell.Y))
                .ToList();

            if (openedCells.Any(x => x.Mine))
                return GameOver();

            MoveResultType resultType = CheckForVictory();
            return new MoveResult(resultType, openedCells);
        }

        protected MoveResult OpenCell(int x, int y)
        {
            if (Field[y, x].Mine)
                return GameOver();

            List<ResultCell> openedCells = Field.OpenCell(x, y).ToList();
            MoveResultType resultType = CheckForVictory();
            return new MoveResult(resultType, openedCells);
        }

        protected MoveResult GameOver()
        {
            Finished = true;
            List<ResultCell> unopenedCells = Field.GetUnflaggedMines().Select(x => new ResultCell(x)).ToList();
            return new MoveResult(MoveResultType.GameOver, unopenedCells);
        }

        protected MoveResultType CheckForVictory()
        {
            if (Field.CellsToOpen == 0)
            {
                Finished = true;
                return MoveResultType.Victory;
            }

            return MoveResultType.Opened;
        }
    }
}
using System;
using System.Collections.Generic;
using MineSweeper.Data;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper
{
    public class Game
    {
        protected GameField Field { get; }

        protected HashSet<Move> Moves { get; }

        public IEnumerable<Move> PlayerMoves => Moves;

        public int Seed => Field.Seed;

        public Game(IFieldGenerator fieldGenerator, int seed, int width, int height, int density)
        {
            Field = new GameField(width, height, density, seed, fieldGenerator);
            Moves = new HashSet<Move>();
        }

        public MoveResult MakeMove(Move playerMove)
        {
            Moves.Add(playerMove);
            switch (playerMove.Type)
            {
                case MoveType.Click:
                {
                    if (Field[playerMove.X, playerMove.Y].IsMine)
                        // TODO: Actually need to open the whole field and calculate number of mines for each cell
                        return new MoveResult(MoveResultType.GameOver);
                    return OpenCell(playerMove.X, playerMove.Y);
                }

                case MoveType.Mark:
                {
                    Field.MarkCell(playerMove.X, playerMove.Y);
                    return new MoveResult(MoveResultType.Marked);
                }

                case MoveType.UnMark:
                {
                    Field.UnMarkCell(playerMove.X, playerMove.Y);
                    Moves.Remove(playerMove);
                    Moves.Remove(new Move(playerMove.X, playerMove.Y, MoveType.Mark));
                    return new MoveResult(MoveResultType.UnMarked);
                }

                default: throw new ArgumentException("Invalid move type.", nameof(playerMove));
            }
        }

        protected MoveResult OpenCell(int x, int y)
        {
            // TODO: Find opened cells and cinternalConsolealculate number for each of them
            return new MoveResult(MoveResultType.Opened, new List<ResultCell>());
        }
    }
}
using System;
using System.Collections.Generic;
using MineSweeper.Data;
using MineSweeper.Interfaces;

namespace MineSweeper
{
    public class Game
    {
        protected GameField Field { get; }

        protected List<Move> MovesList { get; }

        public IEnumerable<Move> PlayerMoves => MovesList;

        public int Seed => Field.Seed;

        public Game(IFieldGenerator fieldGenerator, int seed, int width, int height, int density)
        {
            Field = new GameField(width, height, density, seed, fieldGenerator);
            MovesList = new List<Move>();
        }

        public MoveResult MakeMove(Move move)
        {
            MovesList.Add(move);
            switch (move.Type)
            {
                case MoveType.Click:
                    {
                        if (Field[move.X, move.Y].IsMine)
                            // TODO: Actually need to open the whole field and calculate number of mines for each cell
                            return new MoveResult(MoveResultType.GameOver);
                        else
                            return OpenCell(move.X, move.Y);
                    }
                case MoveType.Mark:
                    {
                        Field[move.X, move.Y].IsMarked = true;
                        return new MoveResult(MoveResultType.Marked);
                    }
                default: throw new ArgumentException("Invalid move type.", nameof(move));
            }
        }

        protected MoveResult OpenCell(int x, int y)
        {
            // TODO: Find opened cells and cinternalConsolealculate number for each of them
            return new MoveResult(MoveResultType.Opened, new List<ResultCell>());
        }
    }
}
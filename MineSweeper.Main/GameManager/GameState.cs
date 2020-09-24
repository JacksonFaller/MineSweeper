using MineSweeper.Models;
using System.Collections.Generic;

namespace MineSweeper.GameManager
{
    public class GameState
    {
        private List<MoveResult> MoveResults { get; }

        public IEnumerable<MoveResult> PlayerMovesResults => MoveResults;

        public Game Game { get; }

        public GameState(Game game, List<MoveResult> moveResults)
        {
            Game = game;
            MoveResults = moveResults;
        }
    }
}

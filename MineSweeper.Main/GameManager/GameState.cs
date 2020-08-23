using MineSweeper.Generators.Params;
using MineSweeper.Models;
using System.Collections.Generic;

namespace MineSweeper.GameManager
{
    public class GameState<T> where T : FieldGeneratorParamsBase
    {
        private List<MoveResult> MoveResults { get; }

        public IEnumerable<MoveResult> PlayerMovesResults => MoveResults;

        public Game<T> Game { get; }

        public GameState(Game<T> game, List<MoveResult> moveResults)
        {
            Game = game;
            MoveResults = moveResults;
        }
    }
}

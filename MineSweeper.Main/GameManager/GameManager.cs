using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MineSweeper.Data;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.GameManager
{
    public class GameManager<T>
    {
        private readonly IDataProvider<T> _dataProvider;
        private readonly IFieldGenerator _fieldGenerator;

        public GameManager(IDataProvider<T> dataProvider, IFieldGenerator fieldGenerator)
        {
            _dataProvider = dataProvider;
            _fieldGenerator = fieldGenerator;
        }

        public async Task<T> SaveGameAsync(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            var gameSave = new GameSave<T>
            {
                Seed = game.Seed,
                PlayerMoves = game.PlayerMoves.Cast<PlayerMove>().ToList()
            };
            return await _dataProvider.SaveGameAsync(gameSave);
        }

        public async Task<GameState> LoadGameAsync(T id)
        {
            var gameSave = await _dataProvider.GetGameAsync(id);
            var game = new Game(_fieldGenerator, 
                gameSave.Seed, gameSave.Width, gameSave.Height, gameSave.Density);
            var moveResults = new List<MoveResult>();
            foreach (var move in gameSave.PlayerMoves)
            {
                moveResults.Add(game.MakeMove(move));
            }
            return new GameState(game, moveResults);
        }
    }

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
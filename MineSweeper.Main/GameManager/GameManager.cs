using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MineSweeper.Data;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;
using MineSweeper.Models;

namespace MineSweeper.GameManager
{
    public class GameManager<T, G> where G : FieldGeneratorParamsBase
    {
        private readonly IDataProvider<T, G> _dataProvider;
        private readonly IFieldGenerator<G> _fieldGenerator;

        public GameManager(IDataProvider<T, G> dataProvider, IFieldGenerator<G> fieldGenerator)
        {
            _dataProvider = dataProvider;
            _fieldGenerator = fieldGenerator;
        }

        public async Task<T> SaveGameAsync(Game<G> game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            var gameSave = new GameSave<T, G>
            {
                GeneratorParams = game.FieldGenerator.Parameters,
                PlayerMoves = game.PlayerMoves.Cast<PlayerMove>().ToList()
            };
            return await _dataProvider.SaveGameAsync(gameSave);
        }

        public async Task<GameState<G>> LoadGameAsync(T id)
        {
            var gameSave = await _dataProvider.GetGameAsync(id);
            var game = new Game<G>(_fieldGenerator, gameSave.Width, gameSave.Height);
            var moveResults = new List<MoveResult>();
            foreach (var move in gameSave.PlayerMoves)
            {
                moveResults.Add(game.MakeMove(move));
            }
            return new GameState<G>(game, moveResults);
        }
    }
}
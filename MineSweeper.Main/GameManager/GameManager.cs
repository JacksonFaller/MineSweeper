using MineSweeper.Data;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                GeneratorParams = game.FieldGenerator.Parameters,
                PlayerMoves = game.PlayerMoves.ToList()
            };
            return await _dataProvider.SaveGameAsync(gameSave);
        }

        public async Task<GameState> LoadGameAsync(T id)
        {
            var gameSave = await _dataProvider.GetGameAsync(id);
            var game = new Game(_fieldGenerator, gameSave.Width, gameSave.Height);
            var moveResults = new List<MoveResult>();
            foreach (var move in gameSave.PlayerMoves)
            {
                moveResults.Add(game.MakeMove(move));
            }
            return new GameState(game, moveResults);
        }
    }
}
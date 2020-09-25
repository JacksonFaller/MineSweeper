using MineSweeper.Data.DataProviders;
using MineSweeper.Data.Models;
using MineSweeper.Generators;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class GameManager
    {
        private readonly IDataProvider DataProvider;
        private readonly IFieldGeneratorFactory FieldGeneratorFactory;

        public GameManager(IDataProvider dataProvider, IFieldGeneratorFactory fieldGeneratorFactory)
        {
            DataProvider = dataProvider;
            FieldGeneratorFactory = fieldGeneratorFactory;
        }

        public async Task<string> SaveGameAsync(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));
            var gameSave = new GameSave
            {
                GeneratorParams = game.FieldGenerator.Parameters,
                PlayerMoves = game.PlayerMoves.ToList()
            };
            return await DataProvider.SaveGameAsync(gameSave);
        }

        public async Task<Game> LoadGameAsync(string id)
        {
            var gameSave = await DataProvider.LoadGameAsync(id);
            IFieldGenerator generator = FieldGeneratorFactory.Create(gameSave.GeneratorParams);
            DateTime startTime = DateTime.UtcNow.Subtract(gameSave.Timer);

            var game = new Game(generator, gameSave.Width, gameSave.Height, startTime);
            var moveResults = new List<MoveResult>();

            foreach (var move in gameSave.PlayerMoves)
            {
                moveResults.Add(game.MakeMove(move));
            }
            return game;
        }
    }
}
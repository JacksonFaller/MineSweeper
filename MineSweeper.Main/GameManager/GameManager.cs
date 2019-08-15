using System;
using System.Linq;
using System.Threading.Tasks;
using MineSweeper.Data;
using MineSweeper.Interfaces;

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
                PlayerMoves = game.PlayerMoves.ToList()
            };
            return await _dataProvider.SaveGameAsync(gameSave);
        }

        public async Task<Game> LoadGameAsync(T id)
        {
            var gameSave = await _dataProvider.GetGameAsync(id);
            var game = new Game(_fieldGenerator, 
                gameSave.Seed, gameSave.Width, gameSave.Height, gameSave.Density);
            foreach (var move in gameSave.PlayerMoves)
            {
                game.MakeMove(move);
            }
            return game;
        }
    }
}
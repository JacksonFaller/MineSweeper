using MineSweeper.Data.DataProviders;
using MineSweeper.Models;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class GameManager : IGameManager
    {
        private readonly IDataProvider DataProvider;
        private readonly IGameSaveProvider GameSaveProvider;

        public GameManager(IDataProvider dataProvider, IGameSaveProvider gameSaveProvider)
        {
            DataProvider = dataProvider;
            GameSaveProvider = gameSaveProvider;
        }

        public async Task<string> SaveGameAsync(Game game)
        {
            var gameSave = GameSaveProvider.GetGameSave(game);
            return await DataProvider.SaveGameAsync(gameSave);
        }

        public async Task<Game> LoadGameAsync(string id)
        {
            var gameSave = await DataProvider.LoadGameAsync(id);
            var game = GameSaveProvider.GetGameFromSave(gameSave);
          
            return game;
        }

        public Task RemoveGameAsync(string key)
        {
            return DataProvider.RemoveGameAsync(key);
        }
    }
}
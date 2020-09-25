using MineSweeper.Data.Models;
using System.Threading.Tasks;

namespace MineSweeper.Data.DataProviders
{
    public interface IDataProvider
    {
        Task<GameSave> LoadGameAsync(string key);

        Task<string> SaveGameAsync(GameSave game);

        void RemoveGame(string key);
    }
}
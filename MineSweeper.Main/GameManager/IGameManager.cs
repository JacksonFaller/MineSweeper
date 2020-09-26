using MineSweeper.Models;
using System.Threading.Tasks;

namespace MineSweeper
{
    public interface IGameManager
    {
        Task<string> SaveGameAsync(Game game);

        Task<Game> LoadGameAsync(string id);

        Task RemoveGameAsync(string key);
    }
}

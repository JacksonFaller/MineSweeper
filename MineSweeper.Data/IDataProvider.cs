using System.Threading.Tasks;

namespace MineSweeper.Data
{
    public interface IDataProvider<T>
    {
        Task<GameSave<T>> GetGameAsync(T key);

        Task<T> SaveGameAsync(GameSave<T> game);

        void RemoveGame(T key);
    }
}
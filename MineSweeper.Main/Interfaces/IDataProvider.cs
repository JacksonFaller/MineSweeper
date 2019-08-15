using System.Threading.Tasks;
using MineSweeper.Data;

namespace MineSweeper.Interfaces
{
    public interface IDataProvider<T>
    {
        Task<GameSave<T>> GetGameAsync(T key);
        
        Task<T> SaveGameAsync(GameSave<T> game);
        
        void RemoveGame(T key);
    }
}
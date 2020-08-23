using System.Threading.Tasks;

namespace MineSweeper.Data
{
    public interface IDataProvider<T, G>
    {
        Task<GameSave<T, G>> GetGameAsync(T key);
        
        Task<T> SaveGameAsync(GameSave<T, G> game);
        
        void RemoveGame(T key);
    }
}
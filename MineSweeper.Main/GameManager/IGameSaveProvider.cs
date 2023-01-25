using MineSweeper.Data.Models;
using MineSweeper.Models;

public interface IGameSaveProvider
{
    GameSave GetGameSave(Game game);
    Game GetGameFromSave(GameSave gameSave);
}
using MineSweeper.Web.API.Models;
using System;

namespace MineSweeper.Web.API
{
    public interface IGameStorage
    {
        bool HasGame(Guid key);

        void AddGame(Guid key, GameModel game);

        GameModel GetGame(Guid key);

        void RemoveGame(Guid key);

        GameModel this[Guid key] { get; }
    }
}

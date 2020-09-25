using MineSweeper.Models;
using System;

namespace MineSweeper.Web.API
{
    public interface IGameStorage
    {
        bool HasGame(Guid key);

        void AddGame(Guid key, Game game);

        Game GetGame(Guid key);

        void RemoveGame(Guid key);

        Game this[Guid key] { get; }
    }
}

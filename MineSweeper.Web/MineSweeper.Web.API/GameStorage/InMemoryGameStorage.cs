using MineSweeper.Web.API.Models;
using System;
using System.Collections.Generic;

namespace MineSweeper.Web.API
{
    public class InMemoryGameStorage : IGameStorage
    {
        private static readonly Dictionary<Guid, GameModel> Storage = new Dictionary<Guid, GameModel>();

        public GameModel this[Guid key] => GetGame(key);

        public GameModel GetGame(Guid key)
        {
            if (!Storage.ContainsKey(key))
                throw new KeyNotFoundException($"Game with key {key} is not found");

            return Storage[key];
        }

        public void AddGame(Guid key, GameModel game)
        {
            Storage.Add(key, game);
        }

        public void RemoveGame(Guid key)
        {
            if (!Storage.ContainsKey(key))
                throw new KeyNotFoundException($"Game with key {key} is not found");

            Storage.Remove(key);
        }

        public bool HasGame(Guid key)
        {
            return Storage.ContainsKey(key);
        }
    }
}

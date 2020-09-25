using MineSweeper.Models;
using System;
using System.Collections.Generic;

namespace MineSweeper.Web.API
{
    public class InMemoryGameStorage : IGameStorage
    {
        private static readonly Dictionary<Guid, Game> Storage = new Dictionary<Guid, Game>();

        public Game this[Guid key] => GetGame(key);

        public Game GetGame(Guid key)
        {
            if (!Storage.ContainsKey(key))
                throw new KeyNotFoundException($"Game with key {key} is not found");

            return Storage[key];
        }

        public void AddGame(Guid key, Game game)
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

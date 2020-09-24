using System;

namespace MineSweeper.Web.API.DTO
{
    public class StartGameResponse
    {
        public Guid GameKey { get; set; }

        public StartGameResponse(Guid gameKey)
        {
            GameKey = gameKey;
        }
    }
}

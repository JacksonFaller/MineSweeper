using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using System;

namespace MineSweeper.Web.API.Models
{
    public class GameModel : Game
    {
        public DateTime StartTime { get; }

        public GameModel(IFieldGenerator fieldGenerator, int width, int height, DateTime startTime) : base(fieldGenerator, width, height)
        {
            StartTime = startTime;
        }
    }
}

using System;
using System.Collections.Generic;

namespace MineSweeper.Data.Models
{
    public class GameSave
    {
        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public FieldGeneratorParams GeneratorParams { get; set; }

        public List<Move> PlayerMoves { get; set; }

        public TimeSpan Timer { get; set; }
    }
}
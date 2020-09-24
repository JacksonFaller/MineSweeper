using MineSweeper.Models;
using System;
using System.Collections.Generic;

namespace MineSweeper.Generators.Contexts
{
    public class PreciseFieldGeneratorContext
    {
        public Random Random { get; }

        public int MinesCount { get; set; }

        public int Density { get; }

        public List<Cell> EmptyCells { get; } = new List<Cell>();

        public PreciseFieldGeneratorContext(Random random, int minesCount, int density)
        {
            Random = random;
            MinesCount = minesCount;
            Density = density;
        }
    }
}

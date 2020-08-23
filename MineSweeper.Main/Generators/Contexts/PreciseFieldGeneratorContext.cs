using System;

namespace MineSweeper.Generators.Contexts
{
    public class PreciseFieldGeneratorContext
    {
        public Random Random { get; private set; }

        public int MinesCount { get; set; }

        public PreciseFieldGeneratorContext(Random random, int minesCount)
        {
            Random = random;
            MinesCount = minesCount;
        }
    }
}

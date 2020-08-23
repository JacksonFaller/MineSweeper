using System;

namespace MineSweeper.Generators.Contexts
{
    public class DensitryFieldGeneratorContext
    {
        public Random Random { get; private set; }

        public int MinesCount { get; set; }

        public int Density { get; private set; }

        public DensitryFieldGeneratorContext(Random random, int density)
        {
            Random = random;
            Density = density;
        }
    }
}

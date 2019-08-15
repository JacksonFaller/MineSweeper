using System;
using MineSweeper.Interfaces;

namespace MineSweeper.Generators
{
    public class DensitryFieldGenerator : BaseFieldGenerator<DensitryFieldGeneratorContext>, IFieldGenerator
    {
        public int GenerateField(Cell[,] cells, int seed, int width, int height, int density)
        {
            if (density < 1 || density > 100) throw new ArgumentException(nameof(density));
            var context = new DensitryFieldGeneratorContext(new Random(seed), density);
            InitializeField(cells, width, height, context);
            return context.MinesCount;
        }

        public override void InitializeCell(Cell cell, DensitryFieldGeneratorContext context)
        {
            cell.IsMine = context.Random.Next(1, 101) <= context.Density;
            if (cell.IsMine) context.MinesCount++;
        }
    }

    public class DensitryFieldGeneratorContext
    {
        public Random Random { get; set; }

        public int MinesCount { get; set; }

        public int Density { get; set; }

        public DensitryFieldGeneratorContext(Random random, int density)
        {
            Random = random;
            Density = density;
        }
    }
}
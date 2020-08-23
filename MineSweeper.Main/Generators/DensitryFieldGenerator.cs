using System;
using MineSweeper.Generators.Contexts;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;
using MineSweeper.Models;

namespace MineSweeper.Generators
{
    public class DensityFieldGenerator : BaseFieldGenerator<DensitryFieldGeneratorContext>, IFieldGenerator<DensityFieldGeneratorParams>
    {
        public DensityFieldGeneratorParams Parameters { get; }

        public DensityFieldGenerator(DensityFieldGeneratorParams parameters)
        {
            Parameters = parameters;
        }

        public int GenerateField(Cell[,] cells)
        {
            if (Parameters.Density < 1 || Parameters.Density > 100)
                throw new ArgumentOutOfRangeException(nameof(Parameters), "Density should be between 1 and 100 %");

            var context = new DensitryFieldGeneratorContext(new Random(Parameters.Seed), Parameters.Density);
            InitializeField(cells, context);
            CalculateNumbers(cells);
            return context.MinesCount;
        }

        protected override void InitializeCell(Cell cell, DensitryFieldGeneratorContext context)
        {
            cell.Mine = context.Random.Next(1, 101) <= context.Density;
            if (cell.Mine)
                context.MinesCount++;
        }
    }
}
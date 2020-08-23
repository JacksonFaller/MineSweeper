using System;
using MineSweeper.Generators.Contexts;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;
using MineSweeper.Models;

namespace MineSweeper.Generators
{
    public class PreciseFieldGenerator : BaseFieldGenerator<PreciseFieldGeneratorContext>, IFieldGenerator<PreciseFieldGeneratorParams>
    {
        public PreciseFieldGeneratorParams Parameters { get; }

        public PreciseFieldGenerator(PreciseFieldGeneratorParams parameters)
        {
            Parameters = parameters;
        }

        public int GenerateField(Cell[,] cells)
        {
            var random = new Random(Parameters.Seed);
            var context = new PreciseFieldGeneratorContext(random, Parameters.MinesCount);
            InitializeField(cells, context);
            CalculateNumbers(cells);
            return Parameters.MinesCount;
        }

        protected override void InitializeCell(Cell cell, PreciseFieldGeneratorContext context)
        {
            if (context.MinesCount != 0 && context.Random.Next(2) == 1)
            {
                cell.Mine = true;
                context.MinesCount--;
            }
        }
    }
}
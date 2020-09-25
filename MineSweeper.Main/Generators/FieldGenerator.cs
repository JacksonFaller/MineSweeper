using MineSweeper.Data.Models;
using MineSweeper.Generators.Contexts;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using System;

namespace MineSweeper.Generators
{
    public class FieldGenerator : BaseFieldGenerator<PreciseFieldGeneratorContext>, IFieldGenerator
    {
        public FieldGeneratorParams Parameters { get; }

        public FieldGenerator(FieldGeneratorParams parameters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        public int GenerateField(Cell[,] cells)
        {
            if (cells == null)
                throw new ArgumentNullException(nameof(cells));

            var random = new Random(Parameters.Seed);
            int density = cells.Length / Parameters.MinesCount;
            var context = new PreciseFieldGeneratorContext(random, Parameters.MinesCount, density);
            InitializeField(cells, context);
            while (context.MinesCount > 0)
            {
                int next = random.Next(context.EmptyCells.Count);
                context.EmptyCells[next].Mine = true;
                context.EmptyCells.RemoveAt(next);
                context.MinesCount--;
            }
            CalculateNumbers(cells);
            return Parameters.MinesCount;
        }

        protected override void InitializeCell(Cell cell, PreciseFieldGeneratorContext context)
        {
            if (context.MinesCount > 0)
            {
                if (context.Random.Next(100) < context.Density)
                {
                    cell.Mine = true;
                    context.MinesCount--;
                }
                else
                {
                    context.EmptyCells.Add(cell);
                }
            }
        }
    }
}
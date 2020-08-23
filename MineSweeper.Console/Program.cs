using MineSweeper.Generators;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;
using MineSweeper.Models;
using System;
using System.Linq;

namespace MineSweeper.Console
{
    class Program
    {
        private static int markX;
        private static int markY;

        static void Main(string[] args)
        {
            var fieldGenerator = new PreciseFieldGenerator(new PreciseFieldGeneratorParams(-690520614, 20));
            int width = 10, height = 10;
            var game = new TestGame(fieldGenerator, width, height);
            markX = 0;
            markY = 0;
            PrintField(width, height, game.Field);
            while (true)
            {
                var key = System.Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    {
                        if (markX == 0)
                            break;

                        markX--;
                        Reprint(width, height, game.Field);
                        break;

                       
                    }
                    case ConsoleKey.DownArrow:
                    {
                        if (markX >= width - 1)
                            break;

                        markX++;
                        Reprint(width, height, game.Field);
                        break;
                    }
                    case ConsoleKey.LeftArrow:
                    {
                        if (markY == 0)
                            break;

                        markY--;
                        Reprint(width, height, game.Field);
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        if (markY >= height - 1)
                            break;

                        markY++;
                        Reprint(width, height, game.Field);
                        break;
                    }
                    case ConsoleKey.F:
                    {
                        game.MakeMove(new Move(markX, markY, Data.MoveType.Flag));
                        Reprint(width, height, game.Field);
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        var result = game.MakeMove(new Move(markX, markY, Data.MoveType.Click));
                        Reprint(width, height, game.Field);
                        switch (result.ResultType)
                        {
                            case Enums.MoveResultType.GameOver:
                                System.Console.WriteLine("Game over.");
                                break;
                            case Enums.MoveResultType.Victory:
                                System.Console.WriteLine("Victory");
                                break;
                            case Enums.MoveResultType.Opened:
                                System.Console.WriteLine(string.Join(", ", result.OpenedCells.Select(x => $"{x.X}: {x.Y}")));
                                break;
                        }
                        break;
                    }
                    default: return;
                }
            }
        }

        public static void Reprint(int width, int height, GameField<PreciseFieldGeneratorParams> field)
        {
            System.Console.Clear();
            PrintField(width, height, field);
        }

        public static void PrintField(int width, int height, GameField<PreciseFieldGeneratorParams> field)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (markX == x && markY == y)
                        System.Console.Write(" X");
                    else if (field.GetCell(x, y).Oppened)
                        System.Console.Write(field.GetCell(x, y).Mine ? " *" : $" {field.GetCell(x, y).Number}");
                    else
                        System.Console.Write(field.GetCell(x, y).Flagged? " ?": " #");
                }
                System.Console.WriteLine();
            }
        }

        public class TestGame : Game<PreciseFieldGeneratorParams>
        {
            public TestGame(IFieldGenerator<PreciseFieldGeneratorParams> fieldGenerator, int width, int height)
                : base(fieldGenerator, width, height)
            {
            }

            public new GameField<PreciseFieldGeneratorParams> Field => base.Field;
        }
    }
}

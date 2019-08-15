using System;
using MineSweeper;
using MineSweeper.Generators;
using MineSweeper.Interfaces;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var fieldGenerator = new PreciseFieldGenerator();
            int width = 5, height = 5;
            var game = new TestGame(fieldGenerator, -690520614, width, height, 60);
            var value = game.Field.GetCellNumber(2, 3);
            Assert.AreEqual(7, value);
        }

        public void PrintField(int width, int height, GameField field)
        {
             for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    Console.Write(field.GetCell(i, j).IsMine.ToString() + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public class TestGame : Game
    {
        public TestGame(IFieldGenerator fieldGenerator, int seed, int width, int height, int density) 
            : base(fieldGenerator, seed, width, height, density)
        {
        }

        public new GameField Field => base.Field;
    }
}
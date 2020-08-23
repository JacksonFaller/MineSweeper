using System;
using MineSweeper.Generators;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Generators.Params;
using MineSweeper.Models;
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
            var fieldGenerator = new DensityFieldGenerator(new DensityFieldGeneratorParams(-690520614, 60));
            int width = 5, height = 5;
            var game = new TestGame(fieldGenerator, width, height);
        }
    }

    public class TestGame : Game<DensityFieldGeneratorParams>
    {
        public TestGame(IFieldGenerator<DensityFieldGeneratorParams> fieldGenerator, int width, int height) 
            : base(fieldGenerator, width, height)
        {
        }

        public new GameField<DensityFieldGeneratorParams> Field => base.Field;
    }
}
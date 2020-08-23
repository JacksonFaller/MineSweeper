using MineSweeper.Generators.Interfaces;
using System;

namespace MineSweeper.Generators
{
    public class SimpleSeedGenerator : ISeedGenerator
    {
        private readonly Random _rand = new Random();
        public int GenerateSeed() => _rand.Next();
    }
}
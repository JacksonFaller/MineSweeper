using MineSweeper.Data.Models;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.Generators
{
    public class FieldGeneratorFactory : IFieldGeneratorFactory
    {
        public IFieldGenerator Create(FieldGeneratorParams generatorParams)
        {
            return new FieldGenerator(generatorParams);
        }
    }
}

using MineSweeper.Data;
using MineSweeper.Generators;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.Web.API
{
    public class FieldGeneratorFactory : IFieldGeneratorFactory
    {
        public IFieldGenerator Create(FieldGeneratorParams generatorParams)
        {
            return new FieldGenerator(generatorParams);
        }
    }
}

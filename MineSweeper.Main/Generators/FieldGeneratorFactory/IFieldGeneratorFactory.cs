using MineSweeper.Data.Models;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.Generators
{
    public interface IFieldGeneratorFactory
    {
        IFieldGenerator Create(FieldGeneratorParams generatorParams);
    }
}

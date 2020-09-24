using MineSweeper.Data;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.Web.API
{
    public interface IFieldGeneratorFactory
    {
        IFieldGenerator Create(FieldGeneratorParams generatorParams);
    }
}

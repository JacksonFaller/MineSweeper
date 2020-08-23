using MineSweeper.Generators.Params;
using MineSweeper.Models;

namespace MineSweeper.Generators.Interfaces
{
    public interface IFieldGenerator<T> where T : FieldGeneratorParamsBase
    {
        /// <summary>
        /// Generates field
        /// </summary>
        /// <param name="cells">field cells</param>
        /// <returns>Amount of mines placed</returns>
        int GenerateField(Cell[,] cells);

        T Parameters { get; }
    }
}
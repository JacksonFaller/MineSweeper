using MineSweeper.Data.Models;
using MineSweeper.Models;

namespace MineSweeper.Generators.Interfaces
{
    public interface IFieldGenerator
    {
        /// <summary>
        /// Generates field
        /// </summary>
        /// <param name="cells">field cells</param>
        /// <returns>Amount of mines placed</returns>
        int GenerateField(Cell[,] cells);

        FieldGeneratorParams Parameters { get; }
    }
}
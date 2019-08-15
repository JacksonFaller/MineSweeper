namespace MineSweeper.Interfaces
{
    public interface IFieldGenerator
    {
        int GenerateField(Cell[,] cells, int seed, int width, int height, int density);
    }
}
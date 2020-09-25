namespace MineSweeper.Data.Models
{
    public class FieldGeneratorParams
    {
        public int Seed { get; }

        public int MinesCount { get; set; }

        public FieldGeneratorParams(int seed, int minesCount)
        {
            Seed = seed;
            MinesCount = minesCount;
        }
    }
}

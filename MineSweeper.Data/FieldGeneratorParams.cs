namespace MineSweeper.Data
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

namespace MineSweeper.Generators.Params
{
    public class PreciseFieldGeneratorParams : FieldGeneratorParamsBase
    {
        public int MinesCount { get; }

        public PreciseFieldGeneratorParams(int seed, int minesCount) : base(seed)
        {
            MinesCount = minesCount;
        }
    }
}

namespace MineSweeper.Generators.Params
{
    public abstract class FieldGeneratorParamsBase
    {
        public int Seed { get; }

        public FieldGeneratorParamsBase(int seed)
        {
            Seed = seed;
        }
    }
}

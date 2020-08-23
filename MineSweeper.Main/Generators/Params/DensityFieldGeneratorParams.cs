namespace MineSweeper.Generators.Params
{
    public class DensityFieldGeneratorParams : FieldGeneratorParamsBase
    {
        public int Density { get; }

        public DensityFieldGeneratorParams(int seed, int density) : base(seed)
        {
            Density = density;
        }
    }
}

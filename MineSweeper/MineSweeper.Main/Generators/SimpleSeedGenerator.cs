using MineSweeper.Interfaces;

namespace MineSweeper.Generators
{
    public class SimpleSeedGenerator : ISeedGenerator
    {
        public int GenerateSeed()
        {
            return System.DateTime.Now.GetHashCode();
        }
    }
}
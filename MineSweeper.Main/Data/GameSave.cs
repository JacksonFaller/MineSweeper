using System.Collections.Generic;

namespace MineSweeper.Data
{
    public class GameSave<T>
    {
        public T Id { get; set; }
        public int Seed { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Density { get; set; }
        
        public List<Move> PlayerMoves { get; set; }
    }
}
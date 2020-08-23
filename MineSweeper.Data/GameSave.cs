using System.Collections.Generic;

namespace MineSweeper.Data
{
    public class GameSave<T, G>
    {
        public T Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public G GeneratorParams { get; set; }
        
        public List<PlayerMove> PlayerMoves { get; set; }
    }
}
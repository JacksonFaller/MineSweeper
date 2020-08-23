namespace MineSweeper.Models
{
    /// <summary>
    /// Game field cell
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// X coordinate of a cell
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of a cell
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Does cell contain mine
        /// </summary>
        public bool Mine { get; set; }

        /// <summary>
        /// Number of mines around
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Is this cell open
        /// </summary>
        public bool Oppened { get; set; }

        /// <summary>
        /// Is this cell flagged
        /// </summary>
        public bool Flagged { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
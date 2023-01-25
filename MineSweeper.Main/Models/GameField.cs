using MineSweeper.Generators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper.Models
{
    public class GameField
    {
        #region Public properties

        /// <summary>
        /// Width of the game field
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height of the game field
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Total amount of mines on the field
        /// </summary>
        public int MinesTotal { get; }

        /// <summary>
        /// Count of opened cells
        /// </summary>
        public int CellsToOpen { get; private set; }

        public IFieldGenerator FieldGenerator { get; }

        public Cell this[int y, int x] => Cells[y, x];

        /// <summary>
        /// Get cell at coordinates X&Y
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        /// <returns>Cell</returns>
        public Cell GetCell(int y, int x) => this[y, x];

        #endregion

        /// <summary>
        /// Game field's cells
        /// </summary>
        protected Cell[,] Cells { get; }

        public GameField(int width, int height, IFieldGenerator fieldGenerator)
        {
            Width = width;
            Height = height;
            Cells = new Cell[width, height];
            FieldGenerator = fieldGenerator;
            MinesTotal = fieldGenerator.GenerateField(Cells);
            CellsToOpen = width * height - MinesTotal;
        }

        /// <summary>
        /// Flag the cell
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        public void FlagCell(int x, int y)
        {
            Cells[y, x].Flagged = true;
        }

        /// <summary>
        /// Remove the flag from the cell
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        public void UnflagCell(int x, int y)
        {
            Cells[y, x].Flagged = false;
        }

        /// <summary>
        /// Opens empty cells recursively
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        /// <returns>List of opened cells</returns>
        public List<ResultCell> OpenCell(int x, int y)
        {
            var openedCells = new List<ResultCell>();
            OpenCellsRecursively(x, y, openedCells);
            return openedCells;
        }
        
        /// <summary>
        /// Opens cells recursively
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        /// <param name="opened">List of opened cells</param>
        private void OpenCellsRecursively(int x, int y, List<ResultCell> opened)
        {
            var cell = Cells[y, x];
            cell.Oppened = true;
            CellsToOpen--;
            opened.Add(new ResultCell(cell));

            if (cell.Number != 0)
                return;

            foreach (var neighbor in GetNeighbors(x, y).Where(x => !x.Oppened))
                OpenCellsRecursively(neighbor.X, neighbor.Y, opened);
        }
        
        /// <summary>
        /// Returns IEnumerable with unflagged mines
        /// </summary>
        public IEnumerable<Cell> GetUnflaggedMines()
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    if (Cells[y, x].Mine && !Cells[y, x].Flagged)
                        yield return Cells[y, x];
                }
            }
        }

        /// <summary>
        /// Returns neighboring cells
        /// </summary>
        /// <param name="x">X coordinate of the cell</param>
        /// <param name="y">Y coordinate of the cell</param>
        public IEnumerable<Cell> GetNeighbors(int x, int y) => Utility.GetNeighbors(Cells, x, y);
    }
}

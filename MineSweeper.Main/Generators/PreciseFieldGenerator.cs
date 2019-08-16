using System;
using System.Collections.Generic;
using MineSweeper.Generators.Interfaces;

namespace MineSweeper.Generators
{
    public class PreciseFieldGenerator : BaseFieldGenerator<List<Cell>>, IFieldGenerator
    {
        public int GenerateField(Cell[,] cells, int seed, int width, int height, int density)
        {
            var random = new Random(seed);
            var cellList = new List<Cell>(width * height);

            InitializeField(cells, width, height, cellList);
            int minesCount = density * width * height / 100;
            for (int i = 0; i < minesCount; i++)
            {
                int index = random.Next(cellList.Count);
                cellList[index].IsMine = true;
                cellList.RemoveAt(index);
            }
            return minesCount;
        }

        public override void InitializeCell(Cell cell, List<Cell> cellList)
        {
            cellList.Add(cell);
        }
    }
}
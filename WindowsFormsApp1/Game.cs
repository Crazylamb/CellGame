using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Game
    {
        private bool[,] cells;
        private readonly int rows;
        private readonly int cols;

        //Create the instance of the game. Should be another for Wireworld
        public Game(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            cells = new bool[cols, rows];
            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    cells[x, y] = random.Next(density) == 0;
                }
            }
        }

        //Get neighbours
        private int CountNeighbours(int x, int y)
        {
            int counter = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool self = col == x && row == y;
                    var hasLife = cells[col, row];
                    if (hasLife && !self)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        //Make a new generation
        public void CreateGeneration()
        {

            var newCells = new bool[cols, rows];


            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int neighbours = CountNeighbours(x, y);
                    bool hasLife = cells[x, y];

                    if (!hasLife && neighbours == 3)
                        newCells[x, y] = true;
                    else if (hasLife && (neighbours < 2 || neighbours > 3))
                        newCells[x, y] = false;
                    else
                        newCells[x, y] = cells[x, y];
                }
            }
            cells = newCells;
        }

        //Get current generation in another array
        public bool[,] GetCurrentGeneration()
        {
            var newCells = new bool[cols, rows];


            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    newCells[x, y] = cells[x, y];
                }
            }
            return newCells;
        }

        //We can't draw outside the field
        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        //Update the cell. Should be different for Wireworld
        private void UpdateCell(int x, int y, bool state)
        {
            if (ValidateCellPosition(x, y))
                cells[x, y] = state;
        }

        public void AddCell(int x, int y) {
                UpdateCell(x,y, true);
        }

        public void DeleteCell(int x, int y)
        {
            UpdateCell(x, y, false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            int generation = 0;
            List<Cell> toBeKilled = new List<Cell>();
            List<Cell> toBeBorn = new List<Cell>();
            int[,] matrix =
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };

            while (true)
            {
                generation++;
                #region Rules
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (matrix[row, col] == 1)
                        {
                            if ((NumberOfNeighbors(matrix, row, col) < 2) || NumberOfNeighbors(matrix, row, col) > 3)
                            {
                                Cell cell = new Cell(row, col);
                                toBeKilled.Add(cell);
                            }
                        }
                        else 
                        {
                            if (NumberOfNeighbors(matrix, row, col) == 3)
                            {
                                Cell cell = new Cell(row, col);
                                toBeBorn.Add(cell);
                            }
                        }
                    }
                }
                #endregion
                matrix = Kill(matrix, toBeKilled);
                matrix =  BringToLife(matrix, toBeBorn);
                #region Printing
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (matrix[row, col] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(matrix[row, col]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(matrix[row, col]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine(generation);
                Console.WriteLine();
                Console.WriteLine();
                #endregion
                if (generation > 100)
                {
                    break;
                }
                toBeBorn.Clear();
                toBeKilled.Clear();
            }
            
        }

        private static int NumberOfNeighbors(int[,] matrix, int x, int y)
        {
            int numbnerOfNeighbors = 0;
            List<int> neighbors = new List<int>();
            try
            {
                neighbors.Add(matrix[x - 1, y - 1]);
                neighbors.Add(matrix[x - 1, y]);
                neighbors.Add(matrix[x - 1, y + 1]);
                neighbors.Add(matrix[x, y - 1]);
                neighbors.Add(matrix[x, y + 1]);
                neighbors.Add(matrix[x + 1, y - 1]);
                neighbors.Add(matrix[x + 1, y]);
                neighbors.Add(matrix[x + 1, y + 1]);
            }
            catch (IndexOutOfRangeException)
            {
            }
            foreach (var neighbor in neighbors)
            {
                if (neighbor == 1)
                {
                    numbnerOfNeighbors += 1;
                }
            }
            return numbnerOfNeighbors;
        }

        private static int[,] Kill(int[,] matrix, List<Cell> toBeKilled)
        {
            foreach (Cell cell in toBeKilled)
            {

                matrix[cell.x, cell.y] = 0;
            }
            return matrix;
        }

        private static int[,] BringToLife(int[,] matrix, List<Cell> toBeBorn)
        {
            foreach (Cell cell in toBeBorn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                matrix[cell.x, cell.y] = 1;
                Console.ResetColor();
            }
            return matrix;
        }
    }
}

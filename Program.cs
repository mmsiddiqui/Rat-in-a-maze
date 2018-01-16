using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class RatinaMaze
    {
        private static int SIZE = 5;

        //Takes input from file and stores it in 1D array form//

        public int[] MazeReader(int Len)
        {
            int[] InputArray = new int[Len];

            try
            {
                using (StreamReader SR = new StreamReader("E:\\input.txt"))
                {
                    Console.WriteLine("Input from text file:\n");
                    for (int i = 0; i < Len; i++)
                    {
                        InputArray[i] = int.Parse(SR.ReadLine());
                        Console.WriteLine("Array[{0}] : {1}", i, InputArray[i]);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error! Input file not found!");
                Console.WriteLine(e.Message);
            }
            return InputArray;
        }

        // Converts 1D array input into a 2D form //

        public int[,] ConvertTo2D(int[] Array2D)
        {
            int k = 0;
            int[,] Maze2D = new int[SIZE, SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Maze2D[i, j] = Array2D[k];
                    k++;
                }
            }

            // Displays input maze in 2D matrix form//

            Console.WriteLine("\n\t    INPUT MAZE:");
            Console.WriteLine("KEY: 0- Free space or open path \n     1- Wall or blocked path\n");
            Console.WriteLine("KEY: S- Starting point \n     F- Exit or ending point\n");
            Console.WriteLine("(S)\n");

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(" {0}\t", Maze2D[i, j]);
                }

                Console.WriteLine("\n\n");

                if (i == SIZE - 1)
                {
                    Console.Write("\t\t\t\t(F)\n");
                }
            }
            return Maze2D;
        }
        private static int[,] SolutionMaze = new int[SIZE, SIZE];

        // Prints solution path through input maze //

        private static void PrintSolution()
        {
            int i, j;
            Console.WriteLine("\t     SOLUTION:\n");
            Console.WriteLine("KEY: 0- Area outside of solution path \n     1- Solution path\n");
            Console.WriteLine("KEY: S- Starting point \n     F- Exit or ending point\n");
            Console.WriteLine("(S)\n");

            for (i = 0; i < SIZE; i++)
            {
                for (j = 0; j < SIZE; j++)
                {
                    Console.Write(" {0}\t", SolutionMaze[i, j]);
                }
                Console.WriteLine("\n\n");

                if (i == SIZE - 1)
                {
                    Console.Write("\t\t\t\t(F)\n");
                }
            }
        }

        // Takes maze in 2D matrix form and attempts to find a path according to counter-clockwise movement priority //

        private static bool MazeSolver(int r, int c, int[,] InputMaze)
        {
            if ((r == SIZE - 1) && (c == SIZE - 1))
            {
                SolutionMaze[r, c] = 1;
                return true;
            }

            if (r >= 0 && c >= 0 && r < SIZE && c < SIZE && SolutionMaze[r, c] == 0 && InputMaze[r, c] == 0)
            {
                // Checks whether there is any free space to move to in an anti-clockwise direction //
                SolutionMaze[r, c] = 1;

                // Checks for free space downwards //
                if (MazeSolver(r + 1, c, InputMaze))
                    return true;
                // Checks for free space right //
                if (MazeSolver(r, c + 1, InputMaze))
                    return true;
                // Checks for free space upwards //
                if (MazeSolver(r - 1, c, InputMaze))
                    return true;
                // Checks for free space left //
                if (MazeSolver(r, c - 1, InputMaze))
                    return true;

                // Backtracking- Return to last visited free space if no further movement is possible //
                SolutionMaze[r, c] = 0;
                return false;
            }
            return false;
        }

        static void Main(string[] args)
        {
            int[] InputArray = new int[25];
            int[,] Maze2D = new int[SIZE, SIZE];

            RatinaMaze Labyrinth = new RatinaMaze();
            InputArray = Labyrinth.MazeReader(25);
            Maze2D = Labyrinth.ConvertTo2D(InputArray);

            if (MazeSolver(0, 0, Maze2D))
                PrintSolution();
            else
                Console.WriteLine("No possible solution found.\n");
            Console.Read();
        }
    }
}

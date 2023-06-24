using MazeMaker.Models;

namespace MazeMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mazeMaker = new MazeMaker(15, 15);

            var minLength = 25;

            //You can specify either your own start and stop locations, or you can use the "mazeMaker.GetRandomMazeRange()" to generate a MazeRange
            var mazeRange = new MazeRange(new CellLocation(0, 0), new CellLocation(14, 14)); //mazeMaker.GetRandomMazeRange();
            var mazeResult = mazeMaker.CreateMaze(mazeRange);

            while(!mazeResult.SuccessfulBuild || mazeResult.Length < minLength)
            {
                mazeResult = mazeMaker.CreateMaze(mazeRange);
            }

            DisplayMaze(mazeResult);
        }

        private static void DisplayMaze(MazeResult result)
        {
            var maxLength = result.GetLongestCellLength();

            var rowCount = result.Maze.GetLength(0);
            var columnCount = result.Maze.GetLength(1);

            for(var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var line = string.Empty;

                for(var colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    line += GetDisplayString(result.Maze[rowIndex, colIndex].ToString(), maxLength);
                }

                Console.WriteLine(line);
            }

            Console.ReadKey();
        }

        private static string GetDisplayString(string rawValue, int maxLength)
        {
            const char CHAR_FILL = ' ';

            var diff = maxLength - rawValue.Length;

            //The "%" operator performs division and grabs the "remainder" of the operation and stores it in the variable
            //For example, doing 3/2 gives an answer of 1 with a remainder of 1. The remainder of 1 is what would get stored in "modulus" in this example.
            var modulus = diff % 2;

            string finalValue;

            if (modulus == 0)
            {
                var perSide = diff / 2;
                finalValue = new string(CHAR_FILL, perSide) + rawValue + new string(CHAR_FILL, perSide);
            }
            else
            {
                var perSide = (diff - modulus) / 2;
                finalValue = new string(CHAR_FILL, perSide) + rawValue + new string(CHAR_FILL, perSide + modulus);
            }

            return finalValue;
        }
    }
}
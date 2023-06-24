namespace MazeMaker.Models
{
    /// <summary>
    /// Object represents the result of generating a maze
    /// </summary>
    public class MazeResult
    {
        /// <summary>
        /// Whether or not the generation of the maze was successful
        /// </summary>
        public bool SuccessfulBuild { get;}

        /// <summary>
        /// The length of the maze (in terms of number of steps to get from beginning to end)
        /// </summary>
        public int Length { get;}

        /// <summary>
        /// The completed maze
        /// </summary>
        public Cell[,] Maze { get; }

        /// <summary>
        /// Instantiates a new instance of the <see cref="MazeResult"/> class
        /// </summary>
        /// <param name="successfulBuild"></param>
        /// <param name="length"></param>
        /// <param name="maze"></param>
        public MazeResult(bool successfulBuild, int length, Cell[,] maze)
        {
            SuccessfulBuild = successfulBuild;
            Length = length;
            Maze = maze;
        }

        /// <summary>
        /// Retrieves the cell with the longest character length when converted to a string
        /// </summary>
        /// <returns></returns>
        public int GetLongestCellLength()
        {
            var rowCount = Maze.GetLength(0);
            var colCount = Maze.GetLength(1);

            var greatestLength = 0;

            for(var r = 0; r < rowCount; r++)
            {
                for(var c = 0; c < colCount; c++)
                {
                    var length = Maze[r, c].ToString().Length;

                    if(length > greatestLength)
                        greatestLength = length;
                }
            }

            return greatestLength;
        }
    }
}

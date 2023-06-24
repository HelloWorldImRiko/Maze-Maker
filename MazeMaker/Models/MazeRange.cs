namespace MazeMaker.Models
{
    /// <summary>
    /// An object representing the starting and ending locations for a maze
    /// </summary>
    public class MazeRange
    {
        /// <summary>
        /// The starting location of the maze
        /// </summary>
        public CellLocation StartingLocation { get; }

        /// <summary>
        /// The ending location of the maze
        /// </summary>
        public CellLocation EndingLocation { get; }

        /// <summary>
        /// Instantiates a new instance of the <see cref="MazeRange"/> class
        /// </summary>
        /// <param name="startingLocation"></param>
        /// <param name="endingLocation"></param>
        public MazeRange(CellLocation startingLocation, CellLocation endingLocation)
        {
            StartingLocation = startingLocation;
            EndingLocation = endingLocation;
        }
    }
}

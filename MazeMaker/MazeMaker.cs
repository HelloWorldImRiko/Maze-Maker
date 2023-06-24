using MazeMaker.Enums;
using MazeMaker.Models;

namespace MazeMaker
{
    /// <summary>
    /// The class containing the logic to generate a maze
    /// </summary>
    public class MazeMaker
    {
        private readonly Random _rand;

        /// <summary>
        /// Number of rows in the maze
        /// </summary>
        public int RowCount { get; }

        /// <summary>
        /// Number of columns in the maze
        /// </summary>
        public int ColumnCount { get; }

        private readonly int _rowColMin = 0;

        /// <summary>
        /// Instantiates a new instance of the <see cref="MazeMaker"/> class
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public MazeMaker(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            _rand = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Retrieves a random <see cref="MazeRange"/> to be used with maze generation
        /// </summary>
        /// <returns></returns>
        public MazeRange GetRandomMazeRange()
        {
            var startingCell = new CellLocation(_rand.Next(_rowColMin, RowCount), _rand.Next(_rowColMin, ColumnCount));
            var endingCell = new CellLocation(_rand.Next(_rowColMin, RowCount), _rand.Next(_rowColMin, ColumnCount));

            while(endingCell.IsMatch(startingCell))
            {
                endingCell = new CellLocation(_rand.Next(_rowColMin, RowCount), _rand.Next(_rowColMin, ColumnCount));
            }

            return new MazeRange(startingCell, endingCell);
        }

        /// <summary>
        /// For the given <see cref="MazeRange"/>, creates and returns a maze in a <see cref="MazeResult"/> instance
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public MazeResult CreateMaze(MazeRange range)
        {
            var maze = InitializeMaze(range.StartingLocation, range.EndingLocation);
            var foundEnding = false;
            var trapped = false;

            var currentCell = maze.Single(m => m.Type == CellType.Start);

            var stepCounter = 0;

            while(!foundEnding && !trapped)
            {
                currentCell.Step = stepCounter;
                var availableLocations = currentCell.PossibleDirections.Select(currentCell.GetLocationForDirection).ToList();
                var availableCells = maze.Where(m => m.Step == null && availableLocations.Any(m.Location.IsMatch)).ToList();

                if(!availableCells.Any())
                {
                    trapped = true;
                    continue;
                }

                var nextIndex = _rand.Next(0, availableCells.Count());
                currentCell = availableCells[nextIndex];
                foundEnding = currentCell.Type == CellType.End;
                stepCounter++;
            }

            var mazeArray = new Cell[RowCount, ColumnCount];

            foreach(var cell in maze.OrderBy(m => m.Location.Row).ThenBy(m => m.Location.Column))
            {
                mazeArray[cell.Location.Row, cell.Location.Column] = cell;
            }

            return new MazeResult(!trapped, stepCounter, mazeArray);
        }

        private List<Cell> InitializeMaze(CellLocation start, CellLocation end)
        {
            var maze = new List<Cell>();

            for(var rowIndex = _rowColMin; rowIndex < RowCount; rowIndex++)
            {
                for(var colIndex = _rowColMin; colIndex < ColumnCount; colIndex++)
                {
                    var directions = new List<Direction>();
                    if (rowIndex > _rowColMin)
                        directions.Add(Direction.Up);
                    if (rowIndex < RowCount - 1)
                        directions.Add(Direction.Down);
                    if (colIndex > _rowColMin)
                        directions.Add(Direction.Left);
                    if (colIndex < ColumnCount - 1)
                        directions.Add(Direction.Right);

                    var currentCell = new CellLocation(rowIndex, colIndex);
                    var type = start.IsMatch(currentCell) ? CellType.Start :
                                end.IsMatch(currentCell) ? CellType.End : CellType.Space;

                    maze.Add(new Cell(currentCell, type, directions.ToArray()));
                }
            }

            return maze;
        }
    }
}

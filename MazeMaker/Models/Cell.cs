using MazeMaker.Enums;

namespace MazeMaker.Models
{
    /// <summary>
    /// An object representing a single cell in the maze
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The location of the cell in the maze
        /// </summary>
        public CellLocation Location { get; }

        /// <summary>
        /// The type of cell that this instance represents
        /// </summary>
        public CellType Type { get; }

        /// <summary>
        /// The potential directions that can be advanced to from this cell
        /// </summary>
        public Direction[] PossibleDirections { get; }

        /// <summary>
        /// The numeric step in the maze path (if null, then this instance is not part of the maze path)
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// Instantiates a new instance of the <see cref="Cell"/> class
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="possibleDirections"></param>
        public Cell(CellLocation location, CellType type, Direction[] possibleDirections)
        {
            Location = location;
            Type = type;
            PossibleDirections = possibleDirections;
        }

        /// <summary>
        /// For the passed in direction, returns the immediate <see cref="CellLocation"/> in that direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public CellLocation GetLocationForDirection(Direction direction)
        {
            var row = Location.Row;
            var column = Location.Column;

            if (direction == Direction.Up)
                row -= 1;
            else if (direction == Direction.Down)
                row += 1;
            else if (direction == Direction.Left)
                column -= 1;
            else if(direction == Direction.Right)
                column += 1;

            return new CellLocation(row, column);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Type == CellType.End ? "End" :
                    Type == CellType.Start ? "Start" :
                    Type == CellType.Space && Step != null ? $"-{Step.Value.ToString("00")}-" : "  X  ";
        }
    }
}

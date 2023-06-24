namespace MazeMaker.Models
{
    /// <summary>
    /// A coordinate location for a cell in the maze
    /// </summary>
    public class CellLocation
    {
        /// <summary>
        /// The zero based index row the cell is located in
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The zero based index column the cell is located in
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Instantiates a new instance of the <see cref="CellLocation"/> class
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public CellLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Returns whether or not the passed in <see cref="CellLocation"/> is the same as the current instance
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsMatch(CellLocation other)
        {
            return Row == other.Row && Column == other.Column;
        }
    }
}

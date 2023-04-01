namespace Maps
{
    /// <summary>
    /// A class representing a GridCell
    /// </summary>
    /// <typeparam name="T">The type of value in the cell</typeparam>
    public class GridCell<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridCell{T}"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="value">The value stored in the cell.</param>
        public GridCell(int x, int y, T value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        public int X { get; }
        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        public int Y { get; }
        /// <summary>
        /// Gets the value stored in the cell.
        /// </summary>
        /// <value>
        /// The value stored int he cell.
        /// </value>
        public T Value { get; }
    }
}
using System.Linq;
using Extensions;
using UnityEngine;
using Grid = Maps.Grid<Maps.GridCell<bool>>;

namespace Maps
{
    /// <summary>
    /// The map we generate. Adapted from https://www.youtube.com/watch?v=u2i8Ga4phBA
    /// </summary>
    public class Dungeon
    {
        /// <summary>
        /// The maximum height
        /// </summary>
        private readonly int _maxHeight;
        /// <summary>
        /// The maximum width
        /// </summary>
        private readonly int _maxWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dungeon"/> class.
        /// </summary>
        /// <param name="maxWidth">The maximum width.</param>
        /// <param name="maxHeight">The maximum height.</param>
        public Dungeon(int maxWidth, int maxHeight)
        {
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            Grid = new Grid<GridCell<bool>>(maxWidth, maxHeight, InitializeAtticCell);
        }

        /// <summary>
        /// Gets the grid.
        /// Each GridCell's value is a bool.
        /// If true, it's a wall, otherwise, it's an empty cell.
        /// </summary>
        /// <value>
        /// The grid.
        /// </value>
        public Grid<GridCell<bool>> Grid { get; private set; }

        /// <summary>
        /// Initializes an attic cell.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>A GridCell with the value set to true </returns>
        private static GridCell<bool> InitializeAtticCell(int x, int y)
        {
            return new GridCell<bool>(x, y, true);
        }

        /// <summary>
        /// Digs the corridors.
        /// </summary>
        /// <param name="cellsToRemove">The cells to remove.</param>
        public void DigCorridors(int cellsToRemove)
        {
            //First we get the position in the middle of the grid
            Vector2Int walkerPosition = new Vector2Int(_maxWidth / 2, _maxHeight / 2);
            //While there are still cells to remove
            while (cellsToRemove > 0)
            {
                //We pick a random direction, go there, and if it's valid, we dig, a.k.a, change the bool to false
                //We set the new position to the dug position and repeat the process until we have removed all cells.
                var randomDirection = RandomUtils.GetRandomEnumValue<Direction>();
                var newWalkerPosition = walkerPosition + randomDirection.ToCoordinates();
                if (!Grid.AreCoordinatesValid(newWalkerPosition, true)) continue;

                var cell = Grid.Get(newWalkerPosition);
                if (cell.Value)
                {
                    Grid.Set(newWalkerPosition, new GridCell<bool>(cell.X, cell.Y, false));
                    cellsToRemove--;
                }

                walkerPosition = newWalkerPosition;
            }
        }
    }
}
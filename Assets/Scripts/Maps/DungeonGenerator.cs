using UnityEngine;
using UnityEngine.Tilemaps;
using Grid = Maps.Grid<Maps.GridCell<bool>>;

namespace Maps
{
    /// <summary>
    /// Generates a map. Adapted from https://www.youtube.com/watch?v=u2i8Ga4phBA
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class DungeonGenerator : MonoBehaviour
    {
        /// <summary>
        /// The walls tilemap
        /// </summary>
        [Header("Blocks")] public GameObject wallsParent;
        /// <summary>
        /// The ground tilemap
        /// </summary>
        public GameObject groundParent;
        /// <summary>
        /// The wall rule tiles
        /// </summary>
        public GameObject wallObject;
        /// <summary>
        /// The ground rule tiles
        /// </summary>
        public GameObject groundObject;
        
        /// <summary>
        /// Gets the attic/map generated.
        /// </summary>
        /// <value>
        /// The attic.
        /// </value>
        public Dungeon Dungeon { get; private set; }
        
        /// <summary>
        /// Generates a map.
        /// </summary>
        /// <param name="width">The map width.</param>
        /// <param name="height">The map height.</param>
        /// <param name="cellsToRemove">The cells to remove from the grid.</param>
        public void Generate(int width, int height, int cellsToRemove)
        {
            //We clear the objects
            
            //Create an attic and dig the corridors
            Dungeon = new Dungeon(width, height);
            Dungeon.DigCorridors(
                Mathf.Clamp(cellsToRemove, 1, width * height - (width + width + height - 2 + height - 2)));
            //For each cell we set the correct tile graphic
            foreach (var cell in Dungeon.Grid.Cells)
                if (cell.Value)
                    Instantiate(wallObject, new Vector3(cell.X, cell.Y, 0), Quaternion.identity, wallObject.transform);
                else
                    Instantiate(groundObject, new Vector3(cell.X, cell.Y, 0), Quaternion.identity, groundObject.transform);
        }
    }
}
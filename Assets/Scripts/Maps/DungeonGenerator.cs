using UnityEditor;
using UnityEngine;
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

        [Header("Editor Generation Values")] public int width;
        public int height;
        public int cellsToRemove;
        
        /// <summary>
        /// Gets the attic/map generated.
        /// </summary>
        /// <value>
        /// The attic.
        /// </value>
        public Dungeon Dungeon { get; private set; }
        
        /// <summary>
        /// Generates a map in the editor.
        /// </summary>
        [ContextMenu("Generate Dungeon (CLEAR MAP FIRST!!!)")]
        public void Generate()
        {
            for (int i = wallsParent.transform.childCount; i > 0; --i)
                DestroyImmediate(wallsParent.transform.GetChild(0).gameObject);

            for (int i = groundParent.transform.childCount; i > 0; --i)
                DestroyImmediate(groundParent.transform.GetChild(0).gameObject);

            Generate(width, height, cellsToRemove, true);
        }
        
        /// <summary>
        /// Generates a map.
        /// </summary>
        /// <param name="w">The map w.</param>
        /// <param name="h">The map h.</param>
        /// <param name="ctr">The cells to remove from the grid.</param>
        public void Generate(int w, int h, int ctr, bool editor=false)
        {
            if (!editor)
            {
                foreach (Transform child in wallsParent.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (Transform child in groundParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            //Create an attic and dig the corridors
            Dungeon = new Dungeon(w, h);
            Dungeon.DigCorridors(ctr);
            //For each cell we set the correct tile graphic
            foreach (var cell in Dungeon.Grid.Cells)
                if (cell.Value)
                    Instantiate(wallObject, new Vector3(cell.X, 0, cell.Y), Quaternion.identity, wallsParent.transform);
                else
                    Instantiate(groundObject, new Vector3(cell.X, 0, cell.Y), Quaternion.identity, groundParent.transform);
        }
    }
}
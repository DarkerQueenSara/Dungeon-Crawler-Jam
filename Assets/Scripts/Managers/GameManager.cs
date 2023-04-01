using System.Collections.Generic;
using Audio;
using Enemies;
using Items;
using Maps;
using Player;
using UnityEngine;
using Grid = Maps.Grid<Maps.GridCell<bool>>;

namespace Managers
{
    /// <summary>
    /// The GameManager handles loading scenes, generating new levels and other high-level tasks
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class GameManager : MonoBehaviour
    {
        #region SingleTon

        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static GameManager Instance { get; private set; }

        /// <summary>
        /// Awakes this instance (if none already exists).
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        #endregion
        
        /// <summary>
        /// The AudioManager
        /// </summary>
        [HideInInspector] public AudioManager audioManager;

        /// <summary>
        /// The player prefab
        /// </summary>
        [Header("Prefabs")] public GameObject playerPrefab;
        /// <summary>
        /// The portal prefab
        /// </summary>
        public GameObject portalPrefab;

        /// <summary>
        /// The layers of the objects we spawn
        /// </summary>
        public LayerMask spawnables;

        /// <summary>
        /// The starting width of the level
        /// </summary>
        [Header("Initial values")] public int startingWidth;
        /// <summary>
        /// The starting height of the level
        /// </summary>
        public int startingHeight;
        /// <summary>
        /// The starting cells to remove from the level
        /// </summary>
        public int startingCellsToRemove;

        /// <summary>
        /// The width increase when the level expands
        /// </summary>
        [Header("Map changes per level")] public int widthIncrease;
        /// <summary>
        /// The height increase when the level expands
        /// </summary>
        public int heightIncrease;
        /// <summary>
        /// The cells to remove increase when the level expands
        /// </summary>
        public int cellsToRemoveIncrease;
        /// <summary>
        /// The turns required to expand the level
        /// </summary>
        public int turnsToIncrease;

        /// <summary>
        /// The hud UI
        /// </summary>
        [Header("UI Elements")] public GameObject hudUI;
        /// <summary>
        /// The shop UI
        /// </summary>
        public GameObject shopUI;
        /// <summary>
        /// The attic generator
        /// </summary>
        private DungeonGenerator _dungeonGenerator;
        /// <summary>
        /// The current cells to remove
        /// </summary>
        private int _currentCellsToRemove;
        /// <summary>
        /// The current height
        /// </summary>
        private int _currentHeight;
        /// <summary>
        /// The current width
        /// </summary>
        private int _currentWidth;
        /// <summary>
        /// The player start position
        /// </summary>
        private Vector3 _playerStartPosition;
        /// <summary>
        /// The spawned end portal
        /// </summary>
        private bool _spawnedEndPortal;
        
        /// <summary>
        /// Gets the current level.
        /// </summary>
        /// <value>
        /// The current level.
        /// </value>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            _dungeonGenerator = GetComponent<DungeonGenerator>();
            CurrentLevel = 1;
            _currentWidth = startingWidth;
            _currentHeight = startingHeight;
            _currentCellsToRemove = startingCellsToRemove;

            //On Start, we create the first level, and start playing music
            StartLevel();

            audioManager = GetComponent<AudioManager>();
            audioManager.Play("Music");
        }

        /// <summary>
        /// Starts the level.
        /// </summary>
        private void StartLevel()
        {
            //Generate the map through the attic generator
            _dungeonGenerator.Generate(_currentWidth, _currentHeight, _currentCellsToRemove);
            var g = _dungeonGenerator.Dungeon.Grid;
            
            //Place the player on the first free cell
            for (var i = 0; i < g.Cells.Length; i++)
            {
                var cell = g.Get(i);
                var coordinates = g.GetCoordinates(cell);
                var playerPos = new Vector3(coordinates.x, coordinates.y, 0);

                if (!cell.Value)
                {
                    if (PlayerEntity.Instance == null) Instantiate(playerPrefab, playerPos, Quaternion.identity);
                    PlayerEntity.Instance.gameObject.transform.position = playerPos;
                    _playerStartPosition = playerPos;
                    break;
                }
            }

            //Increase level and check if the level needs to be changed.
            CurrentLevel++;
            if (CurrentLevel - 1 % turnsToIncrease == 0)
            {
                _currentWidth += widthIncrease;
                _currentHeight += heightIncrease;
                _currentCellsToRemove += cellsToRemoveIncrease;
            }
        }
        /// <summary>
        /// Spawns the end portal.
        /// </summary>
        public void SpawnEndPortal()
        {
            if (!_spawnedEndPortal)
            {
                var portal = Instantiate(portalPrefab, _playerStartPosition, Quaternion.identity);
                TurnManager.Instance.portalInMap = portal.GetComponent<EndPortal>();
            }
        }
    }
}
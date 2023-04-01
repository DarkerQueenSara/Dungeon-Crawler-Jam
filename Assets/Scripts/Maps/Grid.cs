using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Maps
{
    /// <summary>
    /// A custom grid of objects. Adapted from https://github.com/x1r15/Grid/blob/master/Grid.cs
    /// </summary>
    /// <typeparam name="T">The type of the cells in the grid</typeparam>
    public class Grid<T>
    {
        /// <summary>
        /// Initializes the grid cell at position (x,y)
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns></returns>
        public delegate T InitFunction(int x, int y);

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private Grid(int width, int height)
        {
            Cells = new T[width * height];
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// Sets a default value to each grid cell
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="init">The initialization function.</param>
        public Grid(int width, int height, InitFunction init) : this(width, height)
        {
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                Set(x, y, init(x, y));
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>
        /// The cells.
        /// </value>
        public T[] Cells { get; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; }

        /// <summary>
        /// Translates coordinates to Cells index.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>Cells index of the coordinates</returns>
        public int CoordinatesToIndex(int x, int y)
        {
            return y * Width + x;
        }

        /// <summary>
        /// Translates coordinates to Cells index.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>Cells index of the coordinates</returns>
        public int CoordinatesToIndex(Vector2Int coordinates)
        {
            return coordinates.y * Width + coordinates.x;
        }

        /// <summary>
        /// Translates an index to the cell's coordinates.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The coordinates of the cell corresponding to the index.</returns>
        public Vector2Int IndexToCoords(int index)
        {
            return new Vector2Int(index % Width, index / Width);
        }

        /// <summary>
        /// Sets the cell at the specified coordinates.
        /// </summary>
        /// <param name="x">The x coordinate of the cell.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="value">The value.</param>
        public void Set(int x, int y, T value)
        {
            Cells[CoordinatesToIndex(x, y)] = value;
        }

        /// <summary>
        /// Sets the cell at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="value">The value.</param>
        public void Set(Vector2Int coordinates, T value)
        {
            Cells[CoordinatesToIndex(coordinates.x, coordinates.y)] = value;
        }

        /// <summary>
        /// Gets the cell at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public void Get(int index, T value)
        {
            Cells[index] = value;
        }

        /// <summary>
        /// Gets the cell at the specified coordinates.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The cell</returns>
        public T Get(int x, int y)
        {
            return Cells[CoordinatesToIndex(x, y)];
        }

        /// <summary>
        /// Gets the value at the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>The cell value</returns>
        public T Get(Vector2Int coordinates)
        {
            return Cells[CoordinatesToIndex(coordinates.x, coordinates.y)];
        }

        /// <summary>
        /// Gets the value at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The cell value</returns>
        public T Get(int index)
        {
            return Cells[index];
        }

        /// <summary>
        /// Checks if the the coordinates are valid.
        /// Walls, a.k.a., the edges of the grid are not valid.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="safeWalls">if set to <c>true</c> [safe walls].</param>
        /// <returns>If the coordinate is valid</returns>
        public bool AreCoordinatesValid(int x, int y, bool safeWalls = false)
        {
            return safeWalls
                ? x > 0 && x < Width - 1 && y > 0 && y < Height - 1
                : x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Checks if the the coordinates are valid.
        /// Walls, a.k.a., the edges of the grid are not valid.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="safeWalls">if set to <c>true</c> [safe walls].</param>
        /// <returns>If the coordinate is valid</returns>
        public bool AreCoordinatesValid(Vector2Int coordinates, bool safeWalls = false)
        {
            return AreCoordinatesValid(coordinates.x, coordinates.y, safeWalls);
        }

        /// <summary>
        /// Gets the coordinates of a cell.
        /// </summary>
        /// <param name="value">The cell.</param>
        /// <returns>The coordinates of the cell</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public Vector2Int GetCoordinates(T value)
        {
            var i = Array.IndexOf(Cells, value);
            if (i == -1) throw new ArgumentException();

            return IndexToCoords(i);
        }

        /// <summary>
        /// Gets the neighbours of a cell.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="safeWalls">if set to <c>true</c> [safe walls].</param>
        /// <returns>A list of cells, with the (up to eight) neighbours of a cell</returns>
        public List<T> GetNeighbours(int x, int y, bool safeWalls = false)
        {
            var directions = (Direction[])Enum.GetValues(typeof(Direction));
            var neighbours = new List<T>();

            foreach (var direction in directions)
            {
                var neighbourCoordinates = new Vector2Int(x, y) + direction.ToCoordinates();
                if (AreCoordinatesValid(neighbourCoordinates, safeWalls)) neighbours.Add(Get(neighbourCoordinates));
            }

            return neighbours;
        }

        /// <summary>
        /// Gets the neighbours of a cell.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="safeWalls">if set to <c>true</c> [safe walls].</param>
        /// <returns>A list of cells, with the (up to eight) neighbours of a cell</returns>
        public List<T> GetNeighbours(Vector2Int coordinates, bool safeWalls = false)
        {
            return GetNeighbours(coordinates.x, coordinates.y, safeWalls);
        }
    }
}
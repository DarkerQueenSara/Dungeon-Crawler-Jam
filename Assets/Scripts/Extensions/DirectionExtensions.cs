using System;
using Maps;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// An extension to convert the Direction enum members to coordinates
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Converts Direction to coordinates.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        public static Vector2Int ToCoordinates(this Direction self)
        {
            return self switch
            {
                Direction.North => new Vector2Int(0, 1),
                Direction.South => new Vector2Int(0, -1),
                Direction.East => new Vector2Int(1, 0),
                Direction.West => new Vector2Int(-1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
            };
        }

        /// <summary>
        /// Converts Vector2Int to direction.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">self - null</exception>
        public static Direction ToDirection(this Vector2Int self)
        {
            if (self == new Vector2Int(0, 1)) return Direction.North;
            if (self == new Vector2Int(0, -1)) return Direction.South;
            if (self == new Vector2Int(1, 0)) return Direction.East;
            if (self == new Vector2Int(-1, 0)) return Direction.West;
            throw new ArgumentException(nameof(self), self.ToString(), null);
        }
    }
}
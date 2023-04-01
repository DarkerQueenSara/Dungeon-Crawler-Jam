using System;
using Random = UnityEngine.Random;

namespace Maps
{
    /// <summary>
    /// A class to help get a random direction.
    /// </summary>
    public static class RandomUtils
    {
        /// <summary>
        /// Gets a random enum value.
        /// </summary>
        /// <typeparam name="T">The enum</typeparam>
        /// <returns>A random member of the enumerate</returns>
        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            return (T)values.GetValue(Random.Range(0, values.Length));
        }
    }
}
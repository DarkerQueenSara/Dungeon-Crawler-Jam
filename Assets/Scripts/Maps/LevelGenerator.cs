using System;
using UnityEngine;

namespace Maps
{
    public class LevelGenerator : MonoBehaviour
    {
        public Texture2D map;
        public ColorToPrefab[] colorMappings;
        public Vector3 startPosition;

        [ContextMenu("Generate Level")]
        private void GenerateLevel()
        {
            foreach (ColorToPrefab colorMapping in colorMappings)
            {
                for (int i = colorMapping.parentObject.transform.childCount; i > 0; --i)
                    DestroyImmediate(colorMapping.parentObject.transform.GetChild(0).gameObject);
            }

            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y);
                }
            }
        }

        private void GenerateTile(int x, int y)
        {
            Color pixelColor = map.GetPixel(x, y);
            if (pixelColor.a == 0) return;

            foreach (ColorToPrefab colorMapping in colorMappings)
            {
                if (EqualColors(pixelColor, colorMapping.color))
                {
                    //se não for parede ou chão, tenho de meter chão por baixo
                    if (colorMapping != colorMappings[0] && colorMapping != colorMappings[1])
                    {
                        Instantiate(colorMappings[1].prefab, startPosition + new Vector3(x, 0, y), Quaternion.identity,
                            colorMappings[1].parentObject.transform);
                    }
                    Instantiate(colorMapping.prefab, startPosition + colorMapping.prefab.transform.position + new Vector3(x, 0, y), Quaternion.identity,
                        colorMapping.parentObject.transform);
                }
            }
        }

        private static bool EqualColors(Color color1, Color color2)
        {
            bool equal = Math.Abs(color1.r - color2.r) <= 0.2 &&
                         Math.Abs(color1.g - color2.g) <= 0.2 &&
                         Math.Abs(color1.b - color2.b) <= 0.2;
            return equal;
        }

    }
}

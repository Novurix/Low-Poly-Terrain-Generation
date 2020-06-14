using System;
namespace DTerrainGeneration
{
    public class PerlinNoise
    {
        float[] heightMapIndexs;
        int scale;

        public PerlinNoise(int perlinScale)
        {
            scale = perlinScale * perlinScale * 4;
            heightMapIndexs = new float[scale];
        }

        public float[] generateHeightIndexs()
        {
            for (int height = 0; height < heightMapIndexs.Length; height++)
            {
                Random random = new Random();
                float newHeight = random.Next(1, 7);
                heightMapIndexs[height] = newHeight;
            }
            return heightMapIndexs;
        }
    }
}

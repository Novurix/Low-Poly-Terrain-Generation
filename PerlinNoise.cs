﻿using System;
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
            for (int perlinIndex = 0; perlinIndex < heightMapIndexs.Length; perlinIndex++) {

                Random random = new Random();
                float newHeight = random.Next(1, 7);
                heightMapIndexs[perlinIndex] = newHeight;

                try {
                    float heightMapBeforeZ = heightMapIndexs[perlinIndex - (int)Math.Sqrt(heightMapIndexs.Length)];
                    float heightMapBeforeX = heightMapIndexs[perlinIndex - 1];

                    float averageHeight = (heightMapBeforeX + heightMapBeforeZ) / 2;

                    Random randomOp = new Random();
                    int randomOperator = randomOp.Next(1, 3);
                    newHeight = 0;

                    random = new Random();
                    float height = random.Next(1, 3);

                    if (randomOperator == 1) newHeight = averageHeight + height/2;
                    else newHeight = averageHeight - height/3;

                    heightMapIndexs[perlinIndex] = newHeight;

                }catch (Exception e){}
            }
            return heightMapIndexs;
        }
    }
}

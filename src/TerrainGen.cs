using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameMono;

public delegate void TerrainGenerator(int width, int height, ref Material[,] data);

public class TerrainGen
{
    public static Material[,] GenTerrain(int width, int height, TerrainGenerator Surface)
    {
        Material[,] data = new Material[width, height];
        for(int x = 0; x < data.GetLength(0); x++)
        {
            for(int y = 0; y < data.GetLength(1); y++)
            {
                data[x, y] = Material.Nothing;
            }

        }
        Surface(width, height, ref data);
        return data;
    }
}

public static class TerrainGenTypes
{
    public static void SurfaceGen(int width, int height, ref Material[,] data)
    {
        Random rand = new Random();

        float ratio = (float)height/width;

        List<Vector2> heightmap = [new Vector2(0, height*0.2f), new Vector2(width, height*0.2f)];

        while(heightmap.Count < width)
        {
            for(int i = 1; i < heightmap.Count; i++)
            {
                if(heightmap[i].X > heightmap[i-1].X + 1)
                {
                    float centerpoint = (heightmap[i].X + heightmap[i-1].X) / 2;
                    float gapSize = heightmap[i].X - heightmap[i-1].X;
                    float heightInterpol =((heightmap[i].Y + heightmap[i-1].Y) / 2);
                    float heightVariance = (rand.NextSingle() - 0.5f) * gapSize / 3 * ratio;
                    heightmap.Insert(i, new Vector2(centerpoint, heightInterpol + heightVariance));
                }
            }
        }

        for(int x = 0; x < width; x++)
        {
            for(int y = (int)heightmap[x].Y; y < height; y++)
            {
                data[x, y] = Material.Mats.ElementAt(rand.Next(Material.Mats.Count)).Value;
            }

        }
    }
}
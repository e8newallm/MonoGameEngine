using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameMono;

public class TerrainGen
{
    public static Material[,] genTerrain(int width, int height, Func<int, int, Material[,]> Surface)
    {
        Material[,] data = Surface(width, height);
        return data;
    }

}

public static class TerrainGenTypes
{
    public static Material[,] surfaceGen(int width, int height)
    {
        Material[,] data = new Material[width, height];
        Random rand = new Random();

        for(int x = 0; x < data.GetLength(0); x++)
        {
            for(int y = 0; y < data.GetLength(1); y++)
            {
                data[x, y] = Material.Nothing;
            }

        }

        float ratio = (float)height/width;

        List<Vector2> heightmap = new List<Vector2>();
        heightmap.Insert(0, new Vector2(0, height*0.2f));
        heightmap.Insert(1, new Vector2(width, height*0.2f));

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

        for(int x = 0; x < data.GetLength(0); x++)
        {
            for(int y = (int)heightmap[x].Y; y < data.GetLength(1); y++)
            {
                data[x, y] = Material.Mats.ElementAt(rand.Next(Material.Mats.Count)).Value;
            }

        }

        return data;
    }
}
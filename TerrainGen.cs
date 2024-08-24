using System;

public class TerrainGen
{
    public static int[,] genTerrain(int width, int height)
    {
        int[,] data = new int[width, height];
        Random rand = new Random();

        for (int x = 0; x < data.GetLength(0); x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                if(y >= 50 || true) data[x, y] = rand.Next(0, 2);
                else data[x, y] = 0;
            }
        }
        return data;
    }
}
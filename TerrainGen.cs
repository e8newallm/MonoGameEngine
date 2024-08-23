using System;

public class TerrainGen
{
    public static int[,] genTerrain()
    {
        int[,] data = new int[1000, 200];
        Random rand = new Random();

        for (int x = 0; x < data.GetLength(0); x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                if(y >= 50) data[x, y] = rand.Next(0, 2);
                else data[x, y] = 0;
            }
        }

        for (int x = 0; x < 200; x++)
        {
            for (int y = 00; y < 200; y++)
            {
                if(y == 0 || y == 199) data[x, y] = 1;
                if(x == 0 || x == 199) data[x, y] = 1;
            }
        }

        return data;
    }
}
using System.Data;
using Microsoft.Xna.Framework;

namespace GameMono;

public class World(int width, int height)
{
    readonly int[,] Map = TerrainGen.genTerrain(width, height);
    public int Width { get => Map.GetLength(0); }
    public int Height { get => Map.GetLength(1); }

    public int this[int x, int y]
    {
        get => Map[x,y];
        set => Map[x,y] = value;
    }
    public int this[Vector2 cell]
    {
        get => Map[(int)cell.X, (int)cell.Y];
        set => Map[(int)cell.X, (int)cell.Y] = value;
    }
}
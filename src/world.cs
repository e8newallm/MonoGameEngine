using System;
using Microsoft.Xna.Framework;

namespace GameMono;

public class World(int width, int height, Func<int, int, Material[,]> Surface)
{
    readonly Material[,] Map = TerrainGen.genTerrain(width, height, Surface);
    private readonly int _width = width;
    private readonly int _height = height;
    public int Width { get => _width; }
    public int Height { get => _height; }

    public Material this[uint x, uint y]
    {
        get => Map[x,y];
        set => Map[x,y] = value;
    }
    public Material this[Vector2 cell]
    {
        get => Map[(uint)cell.X, (uint)cell.Y];
        set => Map[(uint)cell.X, (uint)cell.Y] = value;
    }
}
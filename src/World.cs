using System;
using Microsoft.Xna.Framework;

namespace GameMono;

public class World(int width, int height)
{
    readonly Material[,] Map = GenerateMap(width, height);
    private readonly int _width = width;
    private readonly int _height = height;
    public int Width { get => _width; }
    public int Height { get => _height; }

    public Material this[int x, int y]
    {
        get => Map[x,y];
        set => Map[x,y] = value;
    }
    public Material this[Vector2 cell]
    {
        get => Map[(int)cell.X, (int)cell.Y];
        set => Map[(int)cell.X, (int)cell.Y] = value;
    }

    private static Material[,] GenerateMap(int width, int height)
    {
        Material[,] map = new Material[width, height];
        for(int x = 0; x < map.GetLength(0); x++)
            for(int y = 0; y < map.GetLength(1); y++)
                map[x, y] = Material.Nothing;

        return map;
    }
}
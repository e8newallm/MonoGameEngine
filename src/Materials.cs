using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameMono;

public class Material(String textureName)
{
    private readonly bool _isNothing = textureName == "";
    private readonly bool _isSomething = textureName != "";

    public String TextureName { get; } = textureName;
    public bool IsNothing { get => _isNothing; }
    public bool IsSomething { get => _isSomething; }
    public Texture2D Texture {get; set;}

    static readonly public Dictionary<String, Material> Mats = new()
    {
        {"Dirt", new("Tile")},
        {"Stone", new("Stone")},
    };

    static public readonly Material Nothing = new("");
}
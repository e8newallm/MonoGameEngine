using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameMono;

public class Material(String textureName)
{
    private Texture2D _texture = null;

    public String TextureName { get; } = textureName;
    public Texture2D Texture
    {
        get => _texture;
        set { _texture = value;}
    }

    static readonly public Dictionary<String, Material> Mats = new()
    {
        {"Dirt", new("Tile")},
        {"Stone", new("Stone")},
    };

    static public readonly Material Nothing = new("");
}
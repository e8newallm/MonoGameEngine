using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMono;

public class Texture
{
    virtual public void Draw(SpriteBatch spriteBatch, Rectangle Position)
    {
        spriteBatch.Draw(Material.Mats["Dirt"].Texture, Position, null, Color.White);
    }

    static readonly public Dictionary<String, Texture> Textures;
    static public readonly Texture NoTexture = new();
}
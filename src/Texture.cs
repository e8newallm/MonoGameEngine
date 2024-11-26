using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameMono;

public class Texture(string texName) : DataStore<Texture2D>
{
    private readonly Texture2D texture = Get(texName);

    virtual public void Draw(SpriteBatch spriteBatch, Rectangle Position)
    {
        spriteBatch.Draw(texture, Position, null, Color.White);
    }
}
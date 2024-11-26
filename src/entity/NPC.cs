using System;
using GameMono;
using Microsoft.Xna.Framework;

public class NPC : PhysicsObj
{
    AI ai;

    public NPC(Vector2 position, Vector2 size, string nameAI, Texture texture = null) : base(position, size, texture)
    {
        ai = AI.CreateAI(nameAI);
        ai.RegThis(this);
    }

    public override void Update(World terrain, GameTime gameTime)
    {
        ai.Update(terrain, gameTime);
        base.Update(terrain, gameTime);
    }
}
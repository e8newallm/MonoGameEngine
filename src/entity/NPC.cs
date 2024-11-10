using System;
using GameMono;
using Microsoft.Xna.Framework;

public class NPC : PhysicsObj
{
    AI ai;

    public NPC(Vector2 position, Vector2 size, string nameAI) : base(position, size)
    {
        ai = AI.CreateAI(nameAI);
        ai.RegThis(this);
    }

    public override void Update(World terrain, GameTime gameTime)
    {
        ai.Update(terrain, gameTime);
        Console.WriteLine(XVel);
        base.Update(terrain, gameTime);
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

class Player(Vector2 position, Vector2 size) : PhysicsObj(position, size)
{
    public override void Update(World terrain)
    {
        KeyboardState keyboard = Keyboard.GetState();

        if(OnGround(terrain))
            _velocity.X *= 0.5f;

        if(keyboard.IsKeyDown(Keys.A))
            _velocity.X = Math.Max(-3.0f, _velocity.X - 0.5f);

        else if(keyboard.IsKeyDown(Keys.D))
            _velocity.X = Math.Min(3.0f, _velocity.X + 0.5f);

        if(keyboard.IsKeyDown(Keys.Space) && OnGround(terrain))
            _velocity.Y -= 2.0f;

        base.Update(terrain);
    }
}
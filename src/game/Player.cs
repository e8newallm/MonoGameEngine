using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

class Player(Vector2 position, Vector2 size) : PhysicsObj(position, size)
{
    const float MAXSPEED = 30.0f;

    public override void Update(World terrain, GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();

        if(keyboard.IsKeyDown(Keys.A))
            _velocity.X = (-MAXSPEED + _velocity.X) / 2;

        else if(keyboard.IsKeyDown(Keys.D))
            _velocity.X = (MAXSPEED + _velocity.X) / 2;

        else
            _velocity.X = (_velocity.X) / 2;

        if(keyboard.IsKeyDown(Keys.Space) && OnGround(terrain))
            _velocity.Y -= 70.0f;

        base.Update(terrain, gameTime);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameMono;

public class PhysicsObj(Vector2 position, Vector2 size) : Entity(position, size)
{
    public const float Gravity = 5.0f;

    public Vector2 _velocity = new(0.0f, 0.0f);
    public float XVel { get => _velocity.X; set => _velocity.X = value; }
    public float YVel { get => _velocity.Y; set => _velocity.Y = value; }

    public virtual void Update(World terrain, GameTime gameTime)
    {
        if(!OnGround(terrain))
            YVel += Gravity;

        Vector2 scaledVelocity = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        uint steps = (uint)Math.Ceiling(_velocity.Length());
        Vector2 stepSize = new(scaledVelocity.X / steps, scaledVelocity.Y / steps);

        //Console.WriteLine("-------------------------------------");
        //Console.WriteLine("Position: (" + X + ", " + Y + ") - Bottom right (" + (X + Width) + "," + (Y + Height) + ")");
        //Console.WriteLine("Velocity: " + _velocity);
        //Console.WriteLine("scaledVelocity: " + scaledVelocity);
        for(uint i = 0; i < steps; i++)
        {
            //HORIZONTAL MOVEMENT
            X += stepSize.X;
            if(X < 0.0f)
            {
                _velocity.X = 0.0f;
                stepSize.X = 0.0f;
                X = 0.0f;
            }
            if(X + Width >= terrain.Width)
            {
                XVel = 0.0f;
                stepSize.X = 0.0f;
                X = (float)(terrain.Width - Width);
            }

            for(int x = (int)X; x <= (int)(X + Width - 0.01f); x++)
            {
                for(int y = (int)Y; y <= (int)(Y + Height - 0.01f); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        if(x > X)
                            X = (float)(x - Width);
                        else
                            X = x+1;
                        XVel = 0;
                        stepSize.X = 0;
                    }
                }
            }

            //VERTICAL MOVEMENT
            Y += stepSize.Y;
            if(Y < 0.0f)
            {
                stepSize.Y = 0.0f;
                YVel = 0.0f;
                Y = 0.0f;
            }
            if(Y + Height > terrain.Height - 0.001f)
            {
                stepSize.Y = 0.0f;
                YVel = 0.0f;
                Y = terrain.Height - Height;
            }
            for(int x = (int)X; x <= (int)(X + Width - 0.01f); x++)
            {
                for(int y = (int)Y; y <= (int)(Y + Height - 0.01f); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        if(y > Y)
                            Y = (float)(y - Height);
                        else
                            Y = y+1;

                        stepSize.Y = 0.0f;
                        YVel = 0.0f;
                    }
                }
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Material.Mats["Dirt"].Texture, GetBody(50), Color.White);
    }

    public virtual bool OnGround(World terrain)
    {
        int y = (int)(Y + Height);

        if(y == terrain.Height)
            return true;

        for(int x = (int)X; x <= (int)(X + Width - 0.001f); x++)
        {
            if(terrain[x, y].IsSomething)
                return true;
        }
        return false;
    }
}
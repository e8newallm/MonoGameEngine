using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameMono;

public class PhysicsObj(Vector2 position, Vector2 size) : Entity(position, size)
{
    public const float Gravity = 0.0981f;

    public Vector2 _velocity = new(0.0f, 0.0f);
    public float XVel { get => _velocity.X; set => _velocity.X = value; }
    public float YVel { get => _velocity.Y; set => _velocity.Y = value; }

    public virtual void Update(World terrain, GameTime gameTime)
    {
        //Console.WriteLine("-------------------------------------");
        Vector2 OldPosition = new(X, Y);

        if(!OnGround(terrain))
            YVel += Gravity;
        //Console.WriteLine("Velocity: " + _velocity);
        //Console.WriteLine("Position before: " + GetBody());

        Vector2 scaledVelocity = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Constants.CELLSIZE;
        uint steps = 1;
        Vector2 stepSize = scaledVelocity;

        float magnitude = _velocity.Length();
        if(magnitude > 1.0f)
        {
            steps = (uint)Math.Ceiling(magnitude);
            stepSize = new(scaledVelocity.X / steps, scaledVelocity.Y / steps);
        }
        //Console.WriteLine("Steps: " + steps);

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
            if(X + Width > terrain.Width)
            {
                XVel = 0.0f;
                stepSize.X = 0.0f;
                X = terrain.Width - Width;
            }

            for(uint x = (uint)X; x <= (uint)(X + Width); x++)
            {
                for(uint y = (uint)Y; y <= (uint)(Y + Height); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        Rectangle body = GetBody();
                        Rectangle cell = new((int)x*Constants.CELLSIZE, (int)y*Constants.CELLSIZE, Constants.CELLSIZE, Constants.CELLSIZE);
                        Rectangle intersection = Rectangle.Intersect(GetBody(), cell);

                        if(intersection.Width > 0)
                        {
                            if(x > X)
                                X -= (float)intersection.Width/Constants.CELLSIZE;
                            else
                                X += (float)intersection.Width/Constants.CELLSIZE;
                            XVel = 0;
                            stepSize.X = 0;
                        }
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
            if(Y + Height > terrain.Height)
            {
                stepSize.Y = 0.0f;
                YVel = 0.0f;
                Y = terrain.Height - Height;
            }
            for(uint x = (uint)X; x <= (uint)(X + Width); x++)
            {
                for(uint y = (uint)Y; y <= (uint)(Y + Height); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        Rectangle body = GetBody();
                        Rectangle cell = new((int)x*Constants.CELLSIZE, (int)y*Constants.CELLSIZE, Constants.CELLSIZE, Constants.CELLSIZE);
                        Rectangle intersection = Rectangle.Intersect(GetBody(), cell);
                        if(intersection.Width > 0)
                        {
                            if(y > Y)
                                Y -= (float)intersection.Height/Constants.CELLSIZE;
                            else
                                Y += (float)intersection.Height/Constants.CELLSIZE;

                            stepSize.Y = 0.0f;
                            YVel = 0.0f;
                        }
                    }
                }
            }
        }
        //Console.WriteLine("Position after: " + GetBody());
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Material.Mats["Dirt"].Texture, GetBody(), Color.White);
    }

    public virtual bool OnGround(World terrain)
    {
        uint y = (uint)(Y + Height);

        if(y == terrain.Height)
            return true;

        for(uint x = (uint)X; x < (uint)(X + Width); x++)
        {
            if(terrain[x, y].IsSomething)
                return true;
        }
        return false;
    }
}
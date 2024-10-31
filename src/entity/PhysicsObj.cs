using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameMono;

class PhysicsObj(Vector2 position, Vector2 size) : Entity(position, size)
{
    public const float Gravity = 0.0981f;

    protected Vector2 _velocity = new(0.0f, 0.0f);
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }

    public virtual void Update(World terrain, GameTime gameTime)
    {
        Console.WriteLine("-------------------------------------");
        Vector2 OldPosition = new(_position.X, _position.Y);

        if(!OnGround(terrain))
            _velocity.Y += Gravity;
        Console.WriteLine("Velocity: " + _velocity);
        Console.WriteLine("Position before: " + GetBody());

        Vector2 scaledVelocity = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds * Constants.CELLSIZE;
        uint steps = 1;
        Vector2 stepSize = scaledVelocity;

        float magnitude = _velocity.Length();
        if(magnitude > 1.0f)
        {
            steps = (uint)Math.Ceiling(magnitude);
            stepSize = new(scaledVelocity.X / steps, scaledVelocity.Y / steps);
        }
        Console.WriteLine("Steps: " + steps);

        for(uint i = 0; i < steps; i++)
        {
            //HORIZONTAL MOVEMENT
            _position.X += stepSize.X;
            if(_position.X < 0.0f)
            {
                _velocity.X = 0.0f;
                stepSize.X = 0.0f;
                _position.X = 0.0f;
            }
            if(_position.X + _size.X > terrain.Width)
            {
                _velocity.X = 0.0f;
                stepSize.X = 0.0f;
                _position.X = terrain.Width - _size.X;
            }

            for(uint x = (uint)_position.X; x <= (uint)(_position.X + _size.X); x++)
            {
                for(uint y = (uint)_position.Y; y <= (uint)(_position.Y + _size.Y); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        Rectangle body = GetBody();
                        Rectangle cell = new((int)x*Constants.CELLSIZE, (int)y*Constants.CELLSIZE, Constants.CELLSIZE, Constants.CELLSIZE);
                        Rectangle intersection = Rectangle.Intersect(GetBody(), cell);

                        if(intersection.Width > 0)
                        {
                            if(x > _position.X)
                                _position.X -= (float)intersection.Width/Constants.CELLSIZE;
                            else
                                _position.X += (float)intersection.Width/Constants.CELLSIZE;
                            _velocity.X = 0;
                            stepSize.X = 0;
                        }
                    }
                }
            }

            //VERTICAL MOVEMENT
            _position.Y += stepSize.Y;
            if(_position.Y < 0.0f)
            {
                stepSize.Y = 0.0f;
                _velocity.Y = 0.0f;
                _position.Y = 0.0f;
            }
            if(_position.Y + _size.Y > terrain.Height)
            {
                stepSize.Y = 0.0f;
                _velocity.Y = 0.0f;
                _position.Y = terrain.Height - _size.Y;
            }
            for(uint x = (uint)_position.X; x <= (uint)(_position.X + _size.X); x++)
            {
                for(uint y = (uint)_position.Y; y <= (uint)(_position.Y + _size.Y); y++)
                {
                    if(terrain[x, y].IsSomething)
                    {
                        Rectangle body = GetBody();
                        Rectangle cell = new((int)x*Constants.CELLSIZE, (int)y*Constants.CELLSIZE, Constants.CELLSIZE, Constants.CELLSIZE);
                        Rectangle intersection = Rectangle.Intersect(GetBody(), cell);
                        if(intersection.Width > 0)
                        {
                            if(y > _position.Y)
                                _position.Y -= (float)intersection.Height/Constants.CELLSIZE;
                            else
                                _position.Y += (float)intersection.Height/Constants.CELLSIZE;

                            stepSize.Y = 0.0f;
                            _velocity.Y = 0.0f;
                        }
                    }
                }
            }
        }
        Console.WriteLine("Position after: " + GetBody());
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Material.Mats["Dirt"].Texture, GetBody(), Color.White);
    }

    public virtual bool OnGround(World terrain)
    {
        uint y = (uint)(_position.Y + _size.Y);

        if(y == terrain.Height)
            return true;

        for(uint x = (uint)_position.X; x < (uint)(_position.X + _size.X); x++)
        {
            if(terrain[x, y].IsSomething)
                return true;
        }
        return false;
    }
}
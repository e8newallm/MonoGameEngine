using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

namespace GameMono;

class PhysicsObj(Vector2 position, Vector2 size) : Entity(position, size)
{
    public const float Gravity = 0.1f;

    protected Vector2 _velocity = new(0.0f, 0.0f);
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }

    public virtual void Update(World terrain)
    {
        Console.WriteLine("-------------------------------------");

        if(!OnGround(terrain))
            _velocity.Y += Gravity;

        Console.WriteLine("Velocity: " + _velocity);
        uint steps = 1;
        Vector2 stepSize = _velocity;

        float magnitude = _velocity.Length();
        if(magnitude > 2.0f)
        {
            steps = (uint)magnitude;
            stepSize = new(_velocity.X / steps, _velocity.Y / steps);
        }
        Console.WriteLine("Steps: " + steps);

        for(uint i = 0; i < steps; i++)
        {
            _position += stepSize;
            if(_position.X < 0.0f)
            {
                _velocity.X = 0.0f;
                _position.X = 0.0f;
            }
            if(_position.X + _size.X > terrain.Width)
            {
                _velocity.X = 0.0f;
                _position.X = terrain.Width - _size.X;
            }
            if(_position.Y < 0.0f)
            {
                _velocity.Y = 0.0f;
                _position.Y = 0.0f;
            }
            if(_position.Y + _size.Y > terrain.Height)
            {
                _velocity.Y = 0.0f;
                _position.Y = terrain.Height - _size.Y;
            }


            Console.WriteLine("Position: " + _position);
            for(uint x = (uint)_position.X; x < (uint)Math.Ceiling(_position.X + _size.X); x++)
                for(uint y = (uint)_position.Y; y < (uint)Math.Ceiling(_position.Y + _size.Y); y++)
                {
                    if(terrain[x, y].IsSomething())
                    {
                        Rectangle body = GetBody();
                        Rectangle cell = new((int)x*Constants.CELLSIZE, (int)y*Constants.CELLSIZE, Constants.CELLSIZE, Constants.CELLSIZE);
                        Rectangle intersection = Rectangle.Intersect(GetBody(), cell);
                        Console.WriteLine("INTERSECTION: " + intersection);
                        Console.WriteLine("BODY: " + body);
                        Console.WriteLine("CELL: " + cell);

                        if(intersection.Width < intersection.Height)
                        {
                            if(x > _position.X)
                            {
                                Console.WriteLine("x > _position.X");
                                _position.X -= (float)intersection.Width/Constants.CELLSIZE;
                            }
                            else
                            {
                                Console.WriteLine("x <= _position.X");
                                _position.X += (float)intersection.Width/Constants.CELLSIZE;
                            }
                            stepSize.X = 0.0f;
                            _velocity.X = 0.0f;
                        }
                        else
                        {
                            if(y > _position.Y)
                            {
                                Console.WriteLine("y > _position.Y");
                                _position.Y -= (float)intersection.Height/Constants.CELLSIZE;
                            }
                            else
                            {
                                Console.WriteLine("y <= _position.Y");
                                 _position.Y += (float)intersection.Height/Constants.CELLSIZE;
                            }
                            stepSize.Y = 0.0f;
                            _velocity.Y = 0.0f;
                        }
                        break;
                    }
                }

        }
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
            if(terrain[x, y].IsSomething())
                return true;
        }
        return false;
    }
}
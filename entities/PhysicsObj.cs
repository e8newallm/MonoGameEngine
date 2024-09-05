using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

namespace GameMono;

class PhysicsObj(Vector2 position, Vector2 size) : Entity(position, size)
{
    public const float Gravity = 0.1f;

    private Vector2 _velocity = new(0.0f, 0.0f);
    public Vector2 Velocity { get => _velocity; set => _velocity = value; }

    public virtual void Update(World terrain)
    {
        _velocity.Y += Gravity;
        Console.WriteLine("-------------------------------------");

        Console.WriteLine("Velocity: " + _velocity);
        uint steps = 1;
        Vector2 stepSize = _velocity;

        float magnitude = _velocity.Length();
        if(magnitude > 1.0f)
        {
            steps = (uint)magnitude;
            stepSize = new(_velocity.X / steps, _velocity.Y / steps);
        }
        Console.WriteLine("Steps: " + steps);

        for(uint i = 0; i < steps; i++)
        {
            _position += stepSize;
            Console.WriteLine("Position: " + _position);
            for(uint x = (uint)_position.X; x < (uint)Math.Ceiling(_position.X + _size.X); x++)
                for(uint y = (uint)_position.Y; y < (uint)Math.Ceiling(_position.Y + _size.Y); y++)
                {
                    if(!terrain[x, y].IsNothing())
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
}
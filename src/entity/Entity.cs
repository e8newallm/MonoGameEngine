using Microsoft.Xna.Framework;

namespace GameMono;

public class Entity(Vector2 position, Vector2 size)
{
    protected Vector2 _position = position;
    protected Vector2 _size = size;

    public Vector2 Position { get => _position;   set => _position = value;   }
    public float X          { get => _position.X; set => _position.X = value; }
    public float Y          { get => _position.Y; set => _position.Y = value; }

    public Vector2 Size     { get => _size;       set => _size = value;       }
    public float Width      { get => _size.X;     set => _size.X = value;     }
    public float Height     { get => _size.Y;     set => _size.Y = value;     }

    public Rectangle GetBody(int scale)
    {
        return new((int)(X*scale), (int)(Y*scale), (int)(Width*scale), (int)(Height*scale));
    }
}
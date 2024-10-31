using Microsoft.Xna.Framework;

namespace GameMono;

class Entity(Vector2 position, Vector2 size)
{
    protected Vector2 _position = position;
    protected Vector2 _size = size;

    public Vector2 Position { get => _position; set => _position = value; }
    public Vector2 Size     { get => _size;     set => _size = value;     }

    public Rectangle GetBody()
    {
        return new((int)(_position.X*Constants.CELLSIZE), (int)(_position.Y*Constants.CELLSIZE), (int)(_size.X*Constants.CELLSIZE), (int)(_size.Y*Constants.CELLSIZE));
    }
}
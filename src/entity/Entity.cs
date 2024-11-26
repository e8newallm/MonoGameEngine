using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMono;

public class Entity(Vector2 position, Vector2 size, Texture texture = null)
{
    protected Vector2 _position = position;
    protected Vector2 _size = size;
    protected Texture _texture = texture ?? Texture.NoTexture;

    public Vector2 Position { get => _position;   set => _position = value;   }
    public float X          { get => _position.X; set => _position.X = value; }
    public float Y          { get => _position.Y; set => _position.Y = value; }

    public Vector2 Size     { get => _size;       set => _size = value;       }
    public float Width      { get => _size.X;     set => _size.X = value;     }
    public float Height     { get => _size.Y;     set => _size.Y = value;     }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _texture.Draw(spriteBatch, GetBody());
    }

    public Rectangle GetBody()
    {
        return new((int)(X*GameState.Scale), (int)(Y*GameState.Scale),
                    (int)(Width*GameState.Scale), (int)(Height*GameState.Scale));
    }
}
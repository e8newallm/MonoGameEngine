using System;
using Microsoft.Xna.Framework;

namespace GameMono;

public class Camera
{
    private float _zoom = 0.2f;
    private float _x = 0.0f;
    private float _y = 0.0f;

    public float Zoom
    {
        get { return _zoom;}
        set { if(value >= 0.2f && value <= 0.3f) _zoom = value;}
    }

    public float X
    {
        get { return _x;}
        set { _x = value;}
    }

    public float Y
    {
        get { return _y;}
        set { _y = value;}
    }

    public Matrix GetTransform()
    {
        return Matrix.CreateTranslation(new Vector3(-_x, -_y, 0)) * Matrix.CreateScale(_zoom, _zoom, 1);
    }
}
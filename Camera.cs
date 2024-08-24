using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

public class Camera(Viewport view)
{
    private float _zoom = 0.2f;
    private float _x = 0.0f;
    private float _y = 0.0f;
    private Viewport viewport = view;

    public float Zoom
    {
        get { return _zoom;}
        set { if(value >= 0.1f && value <= 0.3f) _zoom = value;}
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

    public void UpdateViewport(Viewport view)
    {
        viewport = view;
    }

    public float GetCameraWidth()
    {
        return viewport.Width / Zoom;
    }

    public float GetCameraHeight()
    {
        return viewport.Height / Zoom;
    }

    public Vector2 ViewToCell(MouseState mouse)
    {
        if(viewport.Bounds.Contains(mouse.Position))
            return new Vector2((int)((mouse.X + (_x*_zoom)) / (Constants.CELLSIZE * _zoom)), (int)((mouse.Y + (_y*_zoom)) / (Constants.CELLSIZE * _zoom)));
        else
            return new Vector2(-1.0f, -1.0f);
    }

    public Matrix GetTransform()
    {
        return Matrix.CreateTranslation(new Vector3(-_x, -_y, 0)) * Matrix.CreateScale(_zoom, _zoom, 1);
    }
}
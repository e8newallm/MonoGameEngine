using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

public static class Constants
{
    public const int CELLSIZE = 50;
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;

    private Camera cam;

    private readonly World terrain = new(1000, 1000);
    private KeyboardState prevKeyboard;
    private MouseState prevMouse;
    private Texture2D cell;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        cam = new Camera(_graphics.GraphicsDevice.Viewport);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        cell = Content.Load<Texture2D>("Tile");
        font = Content.Load<SpriteFont>("DebugFont");
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
            Exit();

        cam.Zoom += (prevMouse.ScrollWheelValue - mouse.ScrollWheelValue)/120 * 0.02f;

        if(keyboard.IsKeyDown(Keys.A))
            cam.X -= 50.0f;

        else if(keyboard.IsKeyDown(Keys.D))
            cam.X += 50.0f;


        if(keyboard.IsKeyDown(Keys.W))
            cam.Y -= 50.0f;

        else if(keyboard.IsKeyDown(Keys.S))
            cam.Y += 50.0f;

        if(cam.X < 0.0f) cam.X = 0.0f;
        if(cam.X + cam.GetCameraWidth() > terrain.Width*Constants.CELLSIZE) cam.X = (float)terrain.Width*Constants.CELLSIZE - cam.GetCameraWidth();
        if(cam.Y < 0.0f) cam.Y = 0.0f;
        if(cam.Y + cam.GetCameraHeight() > terrain.Height*Constants.CELLSIZE) cam.Y = (float)terrain.Height*Constants.CELLSIZE - cam.GetCameraHeight();

        Vector2 cell = cam.ViewToCell(mouse);
        if((int)cell.X != -1 && this.IsActive)
        {
            Console.WriteLine(cell.X + " " + cell.Y);
            if(prevMouse.LeftButton == ButtonState.Pressed)
                terrain[cell] = 1;

            else if(prevMouse.RightButton == ButtonState.Pressed)
                terrain[cell] = 0;
        }

        prevKeyboard = keyboard;
        prevMouse = mouse;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Drawing cells to screen
        _spriteBatch.Begin(transformMatrix: cam.GetTransform());
        float lastX = Math.Min(terrain.Width, (int)cam.X/Constants.CELLSIZE + cam.GetCameraWidth()/Constants.CELLSIZE + 1);
        float lastY = Math.Min(terrain.Height, (int)cam.Y/Constants.CELLSIZE + cam.GetCameraHeight()/Constants.CELLSIZE + 1);
        for (int x = (int)cam.X/Constants.CELLSIZE; x < lastX; x++)
        {
            for (int y = (int)cam.Y/Constants.CELLSIZE; y < lastY; y++)
            {
                if(terrain[x, y] == 1)
                    _spriteBatch.Draw(cell, new Vector2(x*Constants.CELLSIZE, y*Constants.CELLSIZE), Color.White);
            }
        }
        _spriteBatch.End();

        _spriteBatch.Begin();
        _spriteBatch.DrawString(font, cam.ViewToCell(prevMouse).ToString(), new Vector2(10, 10), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

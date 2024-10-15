using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameMono;

public static class Constants
{
    public const int CELLSIZE = 50;
}

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;

    private Camera cam;

    private readonly World terrain = new(1000, 600, TerrainGenTypes.surfaceGen);
    private KeyboardState prevKeyboard;
    private MouseState prevMouse;

    private readonly Player testObj = new(new(4.0f, 0.0f), new(1.5f, 2.5f));

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
        foreach(KeyValuePair<String, Material> mat in Material.Mats)
        {
            mat.Value.Texture = Content.Load<Texture2D>(mat.Value.TextureName);
        }
        font = Content.Load<SpriteFont>("DebugFont");
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if(keyboard.IsKeyDown(Keys.P) && prevKeyboard.IsKeyUp(Keys.P))
            GameState.Paused = !GameState.Paused;
        else if(keyboard.IsKeyDown(Keys.O) && prevKeyboard.IsKeyUp(Keys.O))
            GameState.DebugStep();

        if(!GameState.Paused)
            testObj.Update(terrain, gameTime);

        GameState.DebugUnstep();

        // Camera positioning
        cam.Zoom -= (prevMouse.ScrollWheelValue - mouse.ScrollWheelValue)/120 * 0.02f;
        cam.MoveTo(testObj.Position);
        if(cam.X < 0.0f) cam.X = 0.0f;
        if(cam.X + cam.GetCameraWidth() > terrain.Width*Constants.CELLSIZE) cam.X = (float)terrain.Width*Constants.CELLSIZE - cam.GetCameraWidth();
        if(cam.Y < 0.0f) cam.Y = 0.0f;
        if(cam.Y + cam.GetCameraHeight() > terrain.Height*Constants.CELLSIZE) cam.Y = (float)terrain.Height*Constants.CELLSIZE - cam.GetCameraHeight();

        Vector2 cell = cam.ViewToCell(mouse);
        if((int)cell.X != -1 && this.IsActive)
        {
            if(prevMouse.LeftButton == ButtonState.Pressed)
                terrain[cell] = Material.Mats["Dirt"];

            else if(prevMouse.RightButton == ButtonState.Pressed)
                terrain[cell] = Material.Nothing;
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
        for (uint x = (uint)cam.X/Constants.CELLSIZE; x < lastX; x++)
        {
            for (uint y = (uint)cam.Y/Constants.CELLSIZE; y < lastY; y++)
            {
                if(terrain[x, y] != Material.Nothing)
                    _spriteBatch.Draw(terrain[x, y].Texture, new Vector2(x*Constants.CELLSIZE, y*Constants.CELLSIZE), Color.White);
            }
        }

        testObj.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        _spriteBatch.DrawString(font, cam.ViewToCell(prevMouse).ToString(), new Vector2(10, 10), Color.White);
        if(GameState.Paused) _spriteBatch.DrawString(font, "Paused!", new Vector2(10, 60), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

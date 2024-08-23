using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Camera cam;

    private int[,] terrain;
    private KeyboardState prevKeyboard;
    private Texture2D cell;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        terrain = TerrainGen.genTerrain();
        cam = new Camera(_graphics.GraphicsDevice.Viewport);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        cell = Content.Load<Texture2D>("Tile");
    }

    protected override void Update(GameTime gameTime)
    {
        //Console.WriteLine(gameTime.ElapsedGameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(Keyboard.GetState().IsKeyDown(Keys.OemPlus) && prevKeyboard.IsKeyUp(Keys.OemPlus))
        {
            cam.Zoom += 0.01f;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.OemMinus) && prevKeyboard.IsKeyUp(Keys.OemMinus))
        {
            cam.Zoom -= 0.01f;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.A))
        {
            cam.X -= 55.0f;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.D))
        {
            cam.X += 55.0f;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.W))
        {
            cam.Y -= 50.0f;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.S))
        {
            cam.Y += 50.0f;
        }

        if(cam.X < 0.0f) cam.X = 0.0f;
        if(cam.X + cam.GetCameraWidth() > terrain.GetLength(0)*50) cam.X = (float)terrain.GetLength(0)*50 - cam.GetCameraWidth();
        if(cam.Y < 0.0f) cam.Y = 0.0f;
        if(cam.Y + cam.GetCameraHeight() > terrain.GetLength(1)*50) cam.Y = (float)terrain.GetLength(1)*50 - cam.GetCameraHeight();

        prevKeyboard = Keyboard.GetState();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //Console.WriteLine(1000/(float)gameTime.ElapsedGameTime.Milliseconds);
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Drawing cells to screen
        _spriteBatch.Begin(transformMatrix: cam.GetTransform());
        float lastX = Math.Min(terrain.GetLength(0), (int)cam.X/50 + cam.GetCameraWidth()/50 + 1);
        float lastY = Math.Min(terrain.GetLength(1), (int)cam.Y/50 + cam.GetCameraHeight()/50 + 1);
        for (int x = (int)cam.X/50; x < lastX; x++)
        {
            for (int y = (int)cam.Y/50; y < lastY; y++)
            {
                if(terrain[x, y] == 1)
                    _spriteBatch.Draw(cell, new Vector2(x*50, y*50), Color.White);
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

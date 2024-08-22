using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMono;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Viewport viewport;
    private Rectangle Camera;
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
        viewport = _graphics.GraphicsDevice.Viewport;
        Camera = new Rectangle(0, 25, 100, (int)(100/viewport.AspectRatio));

        terrain = TerrainGen.genTerrain();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        cell = Content.Load<Texture2D>("Tile");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        //Console.WriteLine(gameTime.ElapsedGameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {
            //Camera.Z = Math.Min(Camera.Z + 0.001f*gameTime.ElapsedGameTime.Milliseconds, 2.0f);
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            //Camera.Z = Math.Max(Camera.Z - 0.001f*gameTime.ElapsedGameTime.Milliseconds, 1.0f);
        }

        if(Keyboard.GetState().IsKeyDown(Keys.A))
        {
            if(Camera.X > 0)
                Camera.X -= 1;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.D))
        {
            if(Camera.X < terrain.GetLength(0) - (Camera.Width + 1))
            Camera.X += 1;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.W))
        {
            if(Camera.Y > 0)
                Camera.Y -= 1;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.S))
        {
            if(Camera.Y < terrain.GetLength(1) - (Camera.Height + 1))
                Camera.Y += 1;
        }

        prevKeyboard = Keyboard.GetState();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //Console.WriteLine(1000/(float)gameTime.ElapsedGameTime.Milliseconds);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        int scale = viewport.Width/Camera.Width;

        Console.WriteLine(Camera);
        _spriteBatch.Begin();
        for (int x = Camera.X; x <= Camera.X + Camera.Width; x++)
        {
            for (int y = Camera.Y; y < Camera.Y + Camera.Height; y++)
            {

                if(terrain[x, y] == 1)
                    _spriteBatch.Draw(cell, new Rectangle((x - Camera.X) * scale, (y - Camera.Y) * scale, scale, scale), Color.White);
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

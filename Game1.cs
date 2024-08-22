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
    private int scale;

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
        scale = 10;
        Camera = new Rectangle(0, 25, viewport.Width/scale, (int)((viewport.Width/scale)/viewport.AspectRatio) + 1);

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
            if(scale < 10)
            {
                scale++;
                int diff = Camera.Width - viewport.Width/scale;
                Camera.X += diff/2;
                Camera.Y += diff/2;
                Camera.Width = viewport.Width/scale;
                Camera.Height = (int)(Camera.Width/viewport.AspectRatio) + 1;
            }
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            if(scale > 5)
            {
                scale--;
                int diff = Camera.Width - viewport.Width/scale;
                Camera.X += diff/2;
                Camera.Y += diff/2;
                Camera.Width = viewport.Width/scale;
                Camera.Height = (int)(Camera.Width/viewport.AspectRatio) + 1;
            }
        }

        if(Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Camera.X -= 1;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Camera.X += 1;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.W))
        {
            Camera.Y -= 1;
        }
        else if(Keyboard.GetState().IsKeyDown(Keys.S))
        {
            Camera.Y += 1;
        }

        if(Camera.X < 0) Camera.X = 0;
        if(Camera.X + Camera.Width >= terrain.GetLength(0)) Camera.X = terrain.GetLength(0) - Camera.Width;
        if(Camera.Y < 0) Camera.Y = 0;
        if(Camera.Y + Camera.Height >= terrain.GetLength(1)) Camera.Y = terrain.GetLength(1) - Camera.Height;

        prevKeyboard = Keyboard.GetState();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //Console.WriteLine(1000/(float)gameTime.ElapsedGameTime.Milliseconds);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        int scale = viewport.Width/Camera.Width;

        Console.WriteLine(Camera + " scale: " + scale);
        _spriteBatch.Begin();
        for (int x = Camera.X; x < Camera.X + Camera.Width; x++)
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

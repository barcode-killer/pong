using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D paddle;
        Texture2D ball;
        Vector2 paddleOne;
        Vector2 paddleTwo;
        Vector2 ballCoord;
        Random rand;
        SpriteFont font;

        float incrementer = 3f;
        static float x = 10f;
        float y = 10f;
        float upDown = 10f;

        bool goinUp = false;
        bool gameOn = false;
        bool paddleTouch = false;
        bool reset = false;

        int direction;
        int onePoint = 0;
        int twoPoint = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            rand = new Random();
            direction = rand.Next(0, 4);

            paddleOne = new Vector2(0, 0);
            paddleTwo = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballCoord = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2);

            ball = new Texture2D(spriteBatch.GraphicsDevice, 15, 15);
            Color[] data = new Color[15 * 15];

            for (int i = 0; i < data.Length; i++) data[i] = Color.White;
            ball.SetData(data);

            paddle = new Texture2D(spriteBatch.GraphicsDevice, 15, 85);
            data = new Color[15 * 85];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            paddle.SetData(data);

            font = Content.Load<SpriteFont>("Score");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Viewport vp = spriteBatch.GraphicsDevice.Viewport;

            if (reset == true)
            {
                ballCoord = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2);
                gameOn = false;
                direction = rand.Next(0, 4);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                y = y + incrementer;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                y = y - incrementer;

            paddleOne.X = x;
            paddleOne.Y = y;

            if (goinUp == false)
                upDown = upDown + incrementer;
            else
                upDown = upDown - incrementer;

            paddleTwo.X = vp.Width - 25;
            paddleTwo.Y = upDown;

            if (paddleTwo.Y <= 10)
                goinUp = false;
            else if (paddleTwo.Y >= 380)
                goinUp = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameOn == false)
            {
                gameOn = true;
                reset = false;
            }

            if (ballCoord.Y > vp.Height 
                || ballCoord.Y < 0
                || paddleTouch == true)
            {
                switch (direction)
                {
                    case 0:
                        direction = 2;
                        break;

                    case 1:
                        direction = 0;
                        break;

                    case 2:
                        direction = 3;
                        break;

                    case 3:
                        direction = 1;
                        break;
                }
            }
            else if (ballCoord.X > vp.Width + 25 || ballCoord.X < -25)
            {
                if (ballCoord.X > vp.Width + 25)
                    onePoint++;
                else
                    twoPoint++;

                reset = true;
            }
            else if (ballCoord.X <= paddleOne.X + 10 &&
                    ballCoord.Y > paddleOne.Y &&
                    ballCoord.Y < paddleOne.Y + 85)
            {
                switch (direction)
                {
                    case 0:
                        direction = 2;
                        break;

                    case 1:
                        direction = 0;
                        break;

                    case 2:
                        direction = 3;
                        break;

                    case 3:
                        direction = 1;
                        break;
                }
            }
            else if (ballCoord.X >= paddleTwo.X &&
                    ballCoord.Y > paddleTwo.Y &&
                    ballCoord.Y < paddleTwo.Y + 85)
            {
                switch (direction)
                {
                    case 0:
                        direction = 2;
                        break;

                    case 1:
                        direction = 0;
                        break;

                    case 2:
                        direction = 3;
                        break;

                    case 3:
                        direction = 1;
                        break;
                }
            }

            if (gameOn == true)
            {
                switch (direction)
                {
                    case 0:
                        ballCoord.X = ballCoord.X + incrementer;
                        ballCoord.Y = ballCoord.Y + incrementer;
                        break;

                    case 1:
                        ballCoord.X = ballCoord.X - incrementer;
                        ballCoord.Y = ballCoord.Y + incrementer;
                        break;

                    case 2:
                        ballCoord.X = ballCoord.X + incrementer;
                        ballCoord.Y = ballCoord.Y - incrementer;
                        break;

                    case 3:
                        ballCoord.X = ballCoord.X - incrementer;
                        ballCoord.Y = ballCoord.Y - incrementer;
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(paddle, paddleOne, Color.White);
            spriteBatch.Draw(paddle, paddleTwo, Color.White);
            spriteBatch.Draw(ball, ballCoord, Color.White);
            spriteBatch.DrawString(font, onePoint + ":" + twoPoint, new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2 - 30, 25), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

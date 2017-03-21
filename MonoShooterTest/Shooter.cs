using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MonoShooterTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Shooter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D[] PlayerTexture;
        private Texture2D[] BulletTexture;
        private Texture2D StarTexture;
        private SpriteFont font;
        private Vector2 PlayerPosition;
        private List<Bullet> BulletList;
        private SoundEffect ShootingSound;
        private int PlayerLastShot;
        private int lastFPS= 0;

        private int FPSCounter = 60;
        private double FPSElapsed = 0;

        public Shooter()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();

          //  graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            PlayerPosition = new Vector2(50,50);
            BulletList = new List<Bullet>();
            PlayerLastShot = 0;
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
            

            PlayerTexture = new Texture2D[4];
            BulletTexture = new Texture2D[4];

            // TODO: use this.Content to load your game content here

            PlayerTexture[0] = Content.Load<Texture2D>(@"Texture/Player/Player1");
            PlayerTexture[1] = Content.Load<Texture2D>(@"Texture/Player/Player2");
            PlayerTexture[2] = Content.Load<Texture2D>(@"Texture/Player/Player3");
            PlayerTexture[3] = Content.Load<Texture2D>(@"Texture/Player/Player2");


            BulletTexture[0] = Content.Load<Texture2D>("Texture/Bullet/Bullet1");
            BulletTexture[1] = Content.Load<Texture2D>("Texture/Bullet/Bullet2");
            BulletTexture[2] = Content.Load<Texture2D>("Texture/Bullet/Bullet3");
            BulletTexture[3] = Content.Load<Texture2D>("Texture/Bullet/Bullet4");

            StarTexture = Content.Load<Texture2D>("Texture/Background/StarBackground");

            Song song = Content.Load<Song>("Audio/Dark Clouds - Bio Metal");  // Put the name of your song here instead of "song_title"
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            ShootingSound = Content.Load<SoundEffect>("Audio/Shooting");

            font = Content.Load<SpriteFont>("Font/Font");
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

            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.A) ||
                Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.D) || 
                Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    PlayerPosition += new Vector2(0, -0.3f * gameTime.ElapsedGameTime.Milliseconds);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    PlayerPosition += new Vector2(-0.2f * gameTime.ElapsedGameTime.Milliseconds, 0);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    PlayerPosition += new Vector2(0, 0.3f * gameTime.ElapsedGameTime.Milliseconds);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    PlayerPosition += new Vector2(0.2f * gameTime.ElapsedGameTime.Milliseconds, 0);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameTime.TotalGameTime.TotalMilliseconds - PlayerLastShot > 250)
                {
                    PlayerLastShot = (int) gameTime.TotalGameTime.TotalMilliseconds;
                    BulletList.Add(new Bullet((int) gameTime.TotalGameTime.TotalMilliseconds,
                        PlayerPosition +
                        new Vector2(PlayerTexture[(gameTime.TotalGameTime.Milliseconds/250)%4].Width - 6,
                            PlayerTexture[(gameTime.TotalGameTime.Milliseconds/250)%4].Height/2.0f),
                        GraphicsDevice.PresentationParameters.BackBufferWidth + 10));
                    ShootingSound.Play();
                    
                }
            }

            for (int i = BulletList.Count - 1; i >= 0; i--)
            {
                if (BulletList[i].Update((int)gameTime.TotalGameTime.TotalMilliseconds))
                {
                    BulletList.RemoveAt(i);
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
            FPSCounter++;
            


            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin();

            if (gameTime.TotalGameTime.TotalMilliseconds - FPSElapsed > 1000)
            {
                lastFPS = (int)(FPSCounter/((gameTime.TotalGameTime.TotalMilliseconds - FPSElapsed)/1000));
                
                FPSElapsed = gameTime.TotalGameTime.TotalMilliseconds;
                FPSCounter = 1;
            }
            
            for (int i = 0; i < 2; i++)
            {
                spriteBatch.Draw(StarTexture,
                    new Rectangle(
                        (int)
                        -(gameTime.TotalGameTime.TotalMilliseconds/2%
                          GraphicsDevice.PresentationParameters.BackBufferWidth) +
                        GraphicsDevice.PresentationParameters.BackBufferWidth*i - 1, 0,
                        GraphicsDevice.PresentationParameters.BackBufferWidth + 2,
                        GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);
            }

            spriteBatch.Draw(PlayerTexture[(gameTime.TotalGameTime.Milliseconds/150)%4],
                new Rectangle((int) PlayerPosition.X, (int) PlayerPosition.Y,
                    PlayerTexture[(gameTime.TotalGameTime.Milliseconds/150)%4].Width,
                    PlayerTexture[(gameTime.TotalGameTime.Milliseconds/150)%4].Height), Color.White);
            for (int i = BulletList.Count - 1; i >= 0; i--)
            {
                spriteBatch.Draw(BulletTexture[BulletList[i].Transition],
                    new Rectangle((int) BulletList[i].Position.X, (int) BulletList[i].Position.Y,
                        BulletTexture[BulletList[i].Transition].Width, BulletTexture[BulletList[i].Transition].Height),
                    Color.White);
            }

            spriteBatch.DrawString(font, "FPS: " + lastFPS, new Vector2(10, 10), Color.Yellow);

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}

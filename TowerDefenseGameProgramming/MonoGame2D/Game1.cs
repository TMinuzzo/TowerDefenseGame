using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace MonoGame2D
{
	public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        Map level = new Map();

        List<Enemy> enemies = new List<Enemy>();

        List<Texture2D> enemyTextures = new List<Texture2D>();

        Player player;
        Toolbar toolBar;

        float screenWidth;
        float screenHeight;
        bool gameOver;
        bool gameStarted = false;

        Texture2D startGameSplash;
        Texture2D gameOverSplash;

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            startConfigScreen();
            graphics.PreferredBackBufferWidth = level.Width * Constants.MAP_TILE_SIZE;
            graphics.PreferredBackBufferHeight = 256 + level.Height * Constants.MAP_TILE_SIZE;
            //graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            startConfigScreen();
			base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D topBar = Content.Load<Texture2D>("menu");
            SpriteFont font = Content.Load<SpriteFont>("GameState");       

            toolBar = new Toolbar(topBar, font, new Vector2(0, level.Height * Constants.MAP_TILE_SIZE));

            startGameSplash = Content.Load<Texture2D>("start-splash");
            gameOverSplash = Content.Load<Texture2D>("GameOver");

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            Texture2D tree1 = Content.Load<Texture2D>("tree1");
            Texture2D tree2 = Content.Load<Texture2D>("tree2");
            
            enemyTextures.Add(Content.Load<Texture2D>("blackEnemy"));
            enemyTextures.Add(Content.Load<Texture2D>("orange_cute_enemy"));
            enemyTextures.Add(Content.Load<Texture2D>("enemyOnion"));

            level.AddTexture(grass);
            level.AddTexture(path);
            level.AddTexture(tree1);
            level.AddTexture(tree2);

            Texture2D towerTexture = Content.Load<Texture2D>("tower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");

            player = new Player(level, towerTexture, bulletTexture);
           
		}

        protected override void UnloadContent()
        {

        }

        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardHandler();

            UpdateEnemies(gameTime);

            LoadEnemies();

            player.Update(gameTime, enemies);

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            level.Draw(spriteBatch);

            DrawEnemies();

            player.Draw(spriteBatch);

            toolBar.Draw(spriteBatch, player);

            if (!gameStarted)
            {
                spriteBatch.Draw(startGameSplash, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);
            }
            else
            {
            }
            if (gameOver)
            {
                spriteBatch.Draw(gameOverSplash, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void KeyboardHandler()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            /* Starts the game if the Enter key was pressed */
            if (!gameStarted)
            {
                if (state.IsKeyDown(Keys.Enter))
                {
                    gameStarted = true;
                }
                return;
            }
            
             /* Restarts the game if the Enter key was pressed after a Game Over*/
            if (gameOver && state.IsKeyDown(Keys.Enter))
            {
                restartTheGame();           

            }

        }

        protected void LoadEnemies()
        {
            if (spawn >= 3) // Respawns an enemy every second
            {
                spawn = 0;
                if (enemies.Count <= Constants.MAX_ENEMIES) // Limits the respawn
                {
                    Enemy enemy = new Enemy(enemyTextures[random.Next(0, enemyTextures.Count)], Vector2.Zero);
                    enemy.SetWaypoints(level.Waypoints);

                    enemies.Add(enemy);
                }
            }
        

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsOutOfScreen())
                {
                    enemies.RemoveAt(i);
                    i--;

					player.setLives(player.getLives() - 1);

                    if (player.getLives() == 0)
                    {
                        gameOver = true;
                    }
                } else if (!enemies[i].IsAlive())
				{
					enemies.RemoveAt(i);
					i--;
				}
            }

        }
        protected void DrawLives()
        {

        }
        protected void UpdateEnemies(GameTime gameTime)
        {
            foreach (Enemy enemy in enemies)
                enemy.Update(gameTime);

        }

        protected void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }

        protected void startConfigScreen()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            screenHeight = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Height);
            screenWidth = ScaleToHighDPI((float)ApplicationView.GetForCurrentView().VisibleBounds.Width);
		}

        public float ScaleToHighDPI(float f)
        {
            DisplayInformation d = DisplayInformation.GetForCurrentView();
            f *= (float)d.RawPixelsPerViewPixel;
            return f;
        }

        protected void restartTheGame()
        {
            gameStarted = true;
            gameOver = false;
            player.setLives(Constants.PLAYER_START_LIFES);
            player.setGold(Constants.PLAYER_START_GOLD);
            enemies.Clear();
			player.ClearTowers();
        }
    }
}

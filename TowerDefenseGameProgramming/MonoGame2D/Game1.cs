﻿using Microsoft.Xna.Framework;
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
        Map map = new Map();

        List<Enemy> enemies = new List<Enemy>();

		GenericList<Texture2D> enemyTextures = new GenericList<Texture2D>();

        Player player;
        Toolbar toolBar;
        Button button;

        float screenWidth;
        float screenHeight;
        bool gameOver;
        bool gameStarted = false;

        Texture2D startGameSplash;
        Texture2D gameOverSplash;
        Texture2D towerTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            startConfigScreen();
            graphics.PreferredBackBufferWidth = map.Width * Constants.MAP_TILE_SIZE;
            graphics.PreferredBackBufferHeight = 256 + map.Height * Constants.MAP_TILE_SIZE;
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
            button = new Button(); //construtor alternativo de button 
            toolBar = new Toolbar(topBar, font, new Vector2(0, map.Height * Constants.MAP_TILE_SIZE));

            startGameSplash = Content.Load<Texture2D>("start-splash");
            gameOverSplash = Content.Load<Texture2D>("GameOver");

            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            Texture2D tree1 = Content.Load<Texture2D>("tree1");
            Texture2D tree2 = Content.Load<Texture2D>("tree2");
            
            enemyTextures.Add(Content.Load<Texture2D>("blackEnemy"));
            enemyTextures.Add(Content.Load<Texture2D>("orange_cute_enemy"));
            enemyTextures.Add(Content.Load<Texture2D>("enemyOnion"));

            map.AddTexture(grass);
			map.AddTexture(path);
			map.AddTexture(tree1);
			map.AddTexture(tree2);

            towerTexture = Content.Load<Texture2D>("tower");
            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");

            player = new Player(map, towerTexture, bulletTexture);
            button.OnPress += new EventHandler(tower_OnPress);
            button.Clicked += new EventHandler(tower_OnClicked);

        }

        protected override void UnloadContent()
        {
            Content.Unload();
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

            map.Draw(spriteBatch);

            DrawEnemies();

            player.Draw(spriteBatch);

            toolBar.Draw(spriteBatch, player);

            if (!gameStarted)
            {
                spriteBatch.Draw(startGameSplash, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);
            }

            if (gameOver)
            {
                spriteBatch.Draw(gameOverSplash, new Rectangle(0, 0, (int)screenWidth, (int)screenHeight), Color.White);
            }
            player.DrawPreview(spriteBatch, towerTexture);

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
            if (spawn >= Constants.ENEMY_SPAWN_TIME) // Respawns an enemy each ENEMY_SPAWN_TIME
			{
                spawn = 0;
                if (enemies.Count <= Constants.MAX_ENEMIES) // Limits the respawn
                {
					int enemyTexturesSize = enemyTextures.getSize();

                    Enemy enemy = new Enemy(enemyTextures.getOnIndex(random.Next(0, enemyTexturesSize)), Vector2.Zero);
                    enemy.SetWaypoints(map.GetWaypoints());

                    enemies.Add(enemy);
                }
            }
        
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetOutOfScreen()) // Removes enemies out of screen
                {
                    enemies.RemoveAt(i);
                    i--;

					player.SetLives(player.GetLives() - 1); // If a enemie arrived at the end of the screen, player loses a life

                    if (player.GetLives() == 0)
                    {
                        gameOver = true;
                    }
                } else if (!enemies[i].GetAlive()) // Removes dead enemies
				{
					enemies.RemoveAt(i);
					i--;
				}
            }

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
            player.SetLives(Constants.PLAYER_START_LIFES);
            player.SetGold(Constants.PLAYER_START_GOLD);
            enemies.Clear();
			player.ClearTowers();
        }

       protected void  tower_OnPress(object sender, EventArgs e)
       {

       }
        protected void tower_OnClicked(object sender, EventArgs e)
        {

        }
        
    }
}

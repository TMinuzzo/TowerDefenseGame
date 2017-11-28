using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace MonoGame2D
{
	public class Contants
	{
		public static int MAX_ENEMIES = 3;
	}

	public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Random random = new Random();

		Level level = new Level();

        List<Enemy> enemies = new List<Enemy>();

		List<Texture2D> enemyTextures = new List<Texture2D>();

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = level.Width * 64;
            graphics.PreferredBackBufferHeight = level.Height * 64;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D grass = Content.Load<Texture2D>("grass"); 
            Texture2D path = Content.Load<Texture2D>("path");
			Texture2D tree1 = Content.Load<Texture2D>("tree1");
			Texture2D tree2 = Content.Load<Texture2D>("tree2");

			enemyTextures.Add(Content.Load<Texture2D>("blackEnemy"));
			enemyTextures.Add(Content.Load<Texture2D>("enemyCute"));
			enemyTextures.Add(Content.Load<Texture2D>("enemyOnion"));

			level.AddTexture(grass); 
            level.AddTexture(path);
			level.AddTexture(tree1);
			level.AddTexture(tree2);

        }

        protected override void UnloadContent()
        {
            
        }

		float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
			spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

			UpdateEnemies(gameTime);

			LoadEnemies();

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); 

            level.Draw(spriteBatch);

			DrawEnemies();

            spriteBatch.End();

            base.Draw(gameTime);
        }

		protected void LoadEnemies()
		{
			if (spawn >= 3) // Respawns an enemy every second
			{
				spawn = 0;
				if (enemies.Count <= Contants.MAX_ENEMIES) // Limits the respawn
				{
					Enemy enemy = new Enemy(enemyTextures[random.Next(0, enemyTextures.Count)], Vector2.Zero, 100, 10, 0.5f);
					enemy.SetWaypoints(level.Waypoints);

					enemies.Add(enemy);
				}
			}

			for (int i = 0; i < enemies.Count; i++)
			{
				if (!enemies[i].IsAlive())
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

    }
}

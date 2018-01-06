using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public class Player
    {
		/* Attributes */
        protected int gold = Constants.PLAYER_START_GOLD;
        protected int lives = Constants.PLAYER_START_LIFES;

        private ImmutableList<Tower> towers = ImmutableList.Create<Tower>();

        private Texture2D towerTexture;

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        private Texture2D bulletTexture;

		private Map map;

		private int cellX;
		private int cellY;

		private int tileX;
		private int tileY;

        /* Getters */
        public int GetGold()
        {
            return gold;
        }
        public int GetLives()
        {
            return lives;
        }

		/* Setters */
        public void SetLives(int lives)
        {
            this.lives = lives;
        }
        public void SetGold(int gold)
        {
            this.gold = gold;
        }

		/* Constructor */
        public Player(Map map, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.map = map;

            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
        }

		/* Others */
        public void Update(GameTime gameTime, ImmutableList<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / Constants.MAP_TILE_SIZE); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / Constants.MAP_TILE_SIZE); // from array space to level space

            tileX = cellX * Constants.MAP_TILE_SIZE; // Convert from array space to level space
            tileY = cellY * Constants.MAP_TILE_SIZE; // Convert from array space to level space

            if (mouseState.LeftButton == ButtonState.Released
                && oldState.LeftButton == ButtonState.Pressed)
            {
				if (IsCellClear())
				{
					ArrowTower tower = new ArrowTower(towerTexture, bulletTexture, new Vector2(tileX, tileY));
					if (tower.GetCost() <= gold)
					{
						towers = towers.Add(tower);
						gold -= tower.GetCost();
					}
				}
			}

            foreach (Tower tower in towers)
            {
				tower.GetClosestEnemy(enemies);
                tower.Update(gameTime);
            }

            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < map.Width && cellY < map.Height;

            bool spaceClear = true;

			List<Tower> filteredList = towers.Where(tower => tower.GetPosition() == new Vector2(tileX, tileY)).ToList();

			if (filteredList.Count > 0)
				spaceClear = false;

            /* Players only can put their towers in the grass */
            bool onGrass = (map.GetIndex(cellX, cellY) == 0);

            return inBounds && spaceClear && onGrass; // If both checks are true return true
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }

		public void ClearTowers()
		{
			towers = towers.Clear();
		}

        public void DrawPreview(SpriteBatch spriteBatch, Texture2D towerTexture)
        {
            // Draw the tower preview.
                int cellX = (int)(mouseState.X / Constants.MAP_TILE_SIZE); // Convert the position of the mouse
                int cellY = (int)(mouseState.Y / Constants.MAP_TILE_SIZE); // from array space to level space

                int tileX = cellX * Constants.MAP_TILE_SIZE; // Convert from array space to level space
                int tileY = cellY * Constants.MAP_TILE_SIZE; // Convert from array space to level space

            Texture2D previewTexture = towerTexture;
            spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY,
                previewTexture.Width, previewTexture.Height), Color.White);
        }
    }
}

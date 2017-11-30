using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public class Player
    {
        private int gold = Constants.PLAYER_START_GOLD;
        private int lives = Constants.PLAYER_START_LIFES;

        private List<Tower> towers = new List<Tower>();

        private Texture2D towerTexture;

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        private Texture2D bulletTexture;

        public int Gold
        {
            get { return gold; }
        }
        public int Lives
        {
            get { return lives; }
  
        }
        public void setLives(int livesReceived)
        {
            lives = livesReceived;
        }
        public void setGold(int goldReceived)
        {
            gold = goldReceived;
        }
        public int getLives()
        {
            return lives;
        }

        private Map map;

        public Player(Map map, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.map = map;

            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
        }

        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        public void Update(GameTime gameTime, List<Enemy> enemies)
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
					if (tower.Cost <= gold)
					{
						towers.Add(tower);
						gold -= tower.Cost;
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

            foreach (Tower tower in towers) // Check that there is no tower here
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

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
			towers.Clear();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
	class Constants
	{
		/* ABOUT THE GAME */
		static public int MAX_ENEMIES = 7;
		static public int ENEMY_SPAWN_TIME = 5;

		/* ABOUT THE ENEMIES */
		static public int ENEMY_START_HEALTH = 100;
		static public float ENEMY_SPEED = 4f;

		/* ABOUT THE MAP */
		static public int MAP_TILE_SIZE = 64;

		/* ABOUT THE PLAYER */
		static public int PLAYER_START_GOLD = 50;
		static public int PLAYER_START_LIFES = 3;

		/* ABOUT THE ARROW TOWER TOWER */
		static public int ARROW_TOWER_DAMAGE = 50;
		static public int ARROW_TOWER_RADIUS = 150;
        static public int ARROW_TOWER_COST = 0;
        static public int ARROW_TOWER_BULLET_SPEED = 5;

        /* ABOUT THE MESSAGES */
        static public string toolBarMessageLives = " >>> {0} <<<";
        static public string toolBarMessageGold = " {0} ";

    }
}

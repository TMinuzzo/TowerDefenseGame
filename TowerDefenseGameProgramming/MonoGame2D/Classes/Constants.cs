﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
	class Constants
	{
		/* ABOUT THE GAME */
		static public int MAX_ENEMIES = 3;
		static public int ENEMY_SPAWN_TIME = 4;

		/* ABOUT THE ENEMIES */
		static public int ENEMY_START_HEALTH = 100;
		static public float ENEMY_SPEED = 2f;

		/* ABOUT THE MAP */
		static public int MAP_TILE_SIZE = 64;

		/* ABOUT THE PLAYER */
		static public int PLAYER_START_GOLD = 50;
		static public int PLAYER_START_LIFES = 2;

		/* ABOUT THE ARROW TOWER TOWER */
		static public int ARROW_TOWER_DAMAGE = 20;
		static public int ARROW_TOWER_RADIUS = 150;
        static public int ARROW_TOWER_COST = 15;
        static public int ARROW_TOWER_BULLET_SPEED = 4;
	}
}

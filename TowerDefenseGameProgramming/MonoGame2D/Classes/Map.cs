﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Map
    {
		/* Map */
		static private int[,] mapMatrix = new int[,]
		{
			{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			{0,0,1,1,0,0,0,2,0,0,0,0,0,0,0,0,3,0,0,0,0,0},
			{0,0,0,1,1,0,0,0,0,0,3,0,1,1,1,0,0,0,0,0,0,0},
			{0,3,0,0,1,0,0,0,1,1,1,0,1,0,1,1,1,0,2,0,0,0},
			{0,0,0,1,1,0,0,0,1,0,1,0,1,0,3,0,1,0,0,0,0,0},
			{0,0,1,1,0,0,3,0,1,0,1,1,1,0,0,0,1,1,0,0,3,0},
			{0,2,1,0,0,0,0,0,1,0,0,0,2,0,0,0,0,1,1,0,1,1},
			{0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,2,1,1,1,0},
		};

		/* Attributes */
        public Queue<Vector2> waypoints = new Queue<Vector2>();
		private List<Texture2D> tileTextures = new List<Texture2D>();

		/* Getters */
		public Queue<Vector2> GetWaypoints()
        {
            return waypoints;
        }

		private List<Texture2D> GetTileTextures()
		{
			return tileTextures;
		}

		/* Setters */
		public void SetWaypoints(Queue<Vector2> waypoints)
		{
			this.waypoints = waypoints;
		}

		private void SetTileTextures(List<Texture2D> tileTextures)
		{
			this.tileTextures = tileTextures;
		}

		/* Constructor */
		public Map()
		{
			/* Add each point of enemy's path: position * tile size */
			waypoints.Enqueue(new Vector2(0, 0) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(1, 0) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(2, 0) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(2, 1) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(3, 1) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(3, 2) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(4, 2) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(4, 4) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(3, 4) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(3, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(2, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(2, 7) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(8, 7) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(8, 3) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(10, 3) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(10, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(12, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(12, 2) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(14, 2) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(14, 3) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(16, 3) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(16, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(17, 5) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(17, 6) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(18, 6) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(18, 7) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(20, 7) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(20, 6) * Constants.MAP_TILE_SIZE);
			waypoints.Enqueue(new Vector2(21, 6) * Constants.MAP_TILE_SIZE);
		}

		/* Others */
		public int Width
        {
            get { return mapMatrix.GetLength(1); }
        }
        public int Height
        {
            get { return mapMatrix.GetLength(0); }
        }

         public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = mapMatrix[y, x];

                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(texture, new Rectangle(x * Constants.MAP_TILE_SIZE, y * Constants.MAP_TILE_SIZE, Constants.MAP_TILE_SIZE, Constants.MAP_TILE_SIZE), Color.White);
                }
            }
        }

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return mapMatrix[cellY, cellX];
        }

    }
}

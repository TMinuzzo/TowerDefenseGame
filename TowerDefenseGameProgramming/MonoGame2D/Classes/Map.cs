using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Map
    {
        /* List of tile textures */
        private List<Texture2D> tileTextures = new List<Texture2D>();

        /* Queue of enemy's path */
        public Queue<Vector2> waypoints = new Queue<Vector2>();

        /* Map */
        public int[,] mapMatrix = new int[,]
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

        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }

        public int Width
        {
            get { return mapMatrix.GetLength(1); }
        }
        public int Height
        {
            get { return mapMatrix.GetLength(0); }
        }

        /* Constructor with tileSize passed as an argument */
        public Map(int tileSize)
        {
            /* Add each point of enemy's path: position * tile size */
            waypoints.Enqueue(new Vector2(0, 0) * tileSize);
            waypoints.Enqueue(new Vector2(1, 0) * tileSize);
            waypoints.Enqueue(new Vector2(2, 0) * tileSize);
            waypoints.Enqueue(new Vector2(2, 1) * tileSize);
            waypoints.Enqueue(new Vector2(3, 1) * tileSize);
            waypoints.Enqueue(new Vector2(3, 2) * tileSize);
            waypoints.Enqueue(new Vector2(4, 2) * tileSize);
            waypoints.Enqueue(new Vector2(4, 4) * tileSize);
            waypoints.Enqueue(new Vector2(3, 4) * tileSize);
            waypoints.Enqueue(new Vector2(3, 5) * tileSize);
            waypoints.Enqueue(new Vector2(2, 5) * tileSize);
            waypoints.Enqueue(new Vector2(2, 7) * tileSize);
            waypoints.Enqueue(new Vector2(8, 7) * tileSize);
            waypoints.Enqueue(new Vector2(8, 3) * tileSize); 
            waypoints.Enqueue(new Vector2(10, 3) * tileSize);
            waypoints.Enqueue(new Vector2(10, 5) * tileSize);
            waypoints.Enqueue(new Vector2(12, 5) * tileSize);
            waypoints.Enqueue(new Vector2(12, 2) * tileSize);
            waypoints.Enqueue(new Vector2(14, 2) * tileSize);
            waypoints.Enqueue(new Vector2(14, 3) * tileSize);
            waypoints.Enqueue(new Vector2(16, 3) * tileSize);
            waypoints.Enqueue(new Vector2(16, 5) * tileSize);
            waypoints.Enqueue(new Vector2(17, 5) * tileSize);
            waypoints.Enqueue(new Vector2(17, 6) * tileSize);
            waypoints.Enqueue(new Vector2(18, 6) * tileSize);
            waypoints.Enqueue(new Vector2(18, 7) * tileSize);
            waypoints.Enqueue(new Vector2(20, 7) * tileSize);
            waypoints.Enqueue(new Vector2(20, 6) * tileSize);
            waypoints.Enqueue(new Vector2(21, 6) * tileSize);
        }

        /* Constructor with tileSize default */
        public Map()
        {
            /* Add each point of enemy's path: position * tile size */
            waypoints.Enqueue(new Vector2(0, 0) * 64);
            waypoints.Enqueue(new Vector2(1, 0) * 64);
            waypoints.Enqueue(new Vector2(2, 0) * 64);
            waypoints.Enqueue(new Vector2(2, 1) * 64);
            waypoints.Enqueue(new Vector2(3, 1) * 64);
            waypoints.Enqueue(new Vector2(3, 2) * 64);
            waypoints.Enqueue(new Vector2(4, 2) * 64);
            waypoints.Enqueue(new Vector2(4, 4) * 64);
            waypoints.Enqueue(new Vector2(3, 4) * 64);
            waypoints.Enqueue(new Vector2(3, 5) * 64);
            waypoints.Enqueue(new Vector2(2, 5) * 64);
            waypoints.Enqueue(new Vector2(2, 7) * 64);
            waypoints.Enqueue(new Vector2(8, 7) * 64);
            waypoints.Enqueue(new Vector2(8, 3) * 64);
            waypoints.Enqueue(new Vector2(10, 3) * 64);
            waypoints.Enqueue(new Vector2(10, 5) * 64);
            waypoints.Enqueue(new Vector2(12, 5) * 64);
            waypoints.Enqueue(new Vector2(12, 2) * 64);
            waypoints.Enqueue(new Vector2(14, 2) * 64);
            waypoints.Enqueue(new Vector2(14, 3) * 64);
            waypoints.Enqueue(new Vector2(16, 3) * 64);
            waypoints.Enqueue(new Vector2(16, 5) * 64);
            waypoints.Enqueue(new Vector2(17, 5) * 64);
            waypoints.Enqueue(new Vector2(17, 6) * 64);
            waypoints.Enqueue(new Vector2(18, 6) * 64);
            waypoints.Enqueue(new Vector2(18, 7) * 64);
            waypoints.Enqueue(new Vector2(20, 7) * 64);
            waypoints.Enqueue(new Vector2(20, 6) * 64);
            waypoints.Enqueue(new Vector2(21, 6) * 64);
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

                    batch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
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

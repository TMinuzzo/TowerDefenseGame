﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    abstract public class Tower : Sprite
	{
		/* Attributes */
        protected float bulletTimer; // How long ago was a bullet fire
		protected ImmutableList<Bullet> bulletList = ImmutableList.Create<Bullet>();
        protected Enemy target;

        protected int cost; // How much will the tower cost to make
        protected int damage; // The damage done to enemy's
        protected float radius; // How far the tower can shoot
		protected int bulletSpeed;

		protected Texture2D bulletTexture;

		/* Getters */
        public int GetCost()
		{
            return cost;
        }

        public int GetDamage()
        {
            return damage;
		}

        public float GetRadius()
        {
			return radius;
		}

		/* Setters */
		public void SetCost(int cost)
		{
            this.cost = cost;
        }

		public void SetDamage(int damage)
		{
			this.damage = damage;
		}

		public void SetRadius(float radius)
		{
			this.radius = radius;
		}

		/* Constructor */
		public Tower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
			: base(texture, position)
        {
            this.bulletTexture = bulletTexture;
        }

		/* Others */
        public bool IsInRange(Vector2 position)
        {
            return Vector2.Distance(GetCenter(), position) <= radius;
        }

        public void GetClosestEnemy(ImmutableList<Enemy> enemies)
        {
            target = null;
            float smallestRange = radius;

            foreach (Enemy enemy in enemies)
            {
				bool outOfScreen = enemy.GetOutOfScreen();
				bool alive = enemy.GetAlive();

				if (Vector2.Distance(GetCenter(), enemy.GetCenter()) < smallestRange && !outOfScreen && alive)
                {
                    smallestRange = Vector2.Distance(GetCenter(), enemy.GetCenter());
                    target = enemy;
                }
            }
        }

        protected void FaceTarget()
        {
            Vector2 direction = GetCenter() - target.GetCenter();
            direction.Normalize();

            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target != null)
            {
                FaceTarget();

                if (!IsInRange(target.GetCenter()))
                {
                    target = null;
                    bulletTimer = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bulletList)
                bullet.Draw(spriteBatch);

            base.Draw(spriteBatch);
		}

	}
}

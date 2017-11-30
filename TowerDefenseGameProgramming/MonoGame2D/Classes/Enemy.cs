using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Enemy : Sprite
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();

		public float startHealth = Constants.ENEMY_START_HEALTH;
        protected float speed = Constants.ENEMY_SPEED;

		public float currentHealth;
		public bool outOfScreen = false;
		public bool alive = true;

        public bool IsAlive()
        {
            return alive;
        }
        public bool IsOutOfScreen()
        {
            return outOfScreen;
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public bool IsDead
        {
            get { return currentHealth <= 0; }
            set { currentHealth  = 0; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            this.currentHealth = startHealth;
        }
       
        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			if (waypoints.Count > 0)
			{
				if (DistanceToDestination < 1f)
				{
					position = waypoints.Peek();
					waypoints.Dequeue();
				}

				else
				{
					Vector2 direction = waypoints.Peek() - position;
					direction.Normalize();

					velocity = Vector2.Multiply(direction, speed);

					position += velocity;
				}
			}
			else
			{
				outOfScreen = true;
				alive = false;
			}

			if (currentHealth <= 0)
			{
				alive = false;
			}
                
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                float healthPercentage = (float)currentHealth / (float)startHealth;              

                base.Draw(spriteBatch, Color.White);
            }      
        }
	}
}

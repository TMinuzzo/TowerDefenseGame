using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public class Bullet : Sprite
    {
		/* Attributes */
        protected int damage;
        protected int age;
        protected int speed;

		/* Getters */
		public int GetDamage()
		{
			return damage;
		}

		public int GetAge()
		{
			return age;
		}

		public int GetSpeed()
		{
			return speed;
		}

		/* Setters */
		public void SetDamage(int damage)
		{
			this.damage = damage;
		}

		public void SetAge(int age)
		{
			this.age = age;
		}

		public void SetSpeed(int speed)
		{
			this.speed = speed;
		}

		public bool IsDead()
        {
            return age > 100;
        }

		/* Constructor */
        public Bullet(Texture2D texture, Vector2 position, float rotation,
            int speed, int damage) : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
        }

		/* Others */
        public void Kill()
        {
            this.age = 200;
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            Vector2 position = GetPosition();
            position += velocity;
            SetPosition(position);

            base.Update(gameTime);
        }

        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Sprite
    {
		/* Attributes */
        protected Texture2D texture;

		private Vector2 position;
        protected Vector2 velocity;

		private Vector2 center;
        protected Vector2 origin;

        protected float rotation;

		/* Getters */
        public Vector2 GetCenter()
        {
			return center;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

		/* Setters */
		public void SetCenter(Vector2 center)
		{
			this.center = center;
		}

		public void SetPosition(Vector2 position)
		{
			this.position = position;
		}

		/* Constructor */
		public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;

            position = pos;
            velocity = Vector2.Zero;

            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2,
                position.Y + texture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White,
                rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
	class Player
	{
		private Texture2D texture;
		private Vector2 position = new Vector2(0,192);
		private Vector2 velocity = new Vector2(0.4f, 0f);
		private Rectangle rectangle;

		public Vector2 Position
		{
			get { return position; }
		}

		public Player() { }

		public void Load(ContentManager Content)
		{
			texture = Content.Load<Texture2D>("Tile6");
		}

		public void Update(GameTime gameTime)
		{
			position.X += velocity.X;
			position.Y += velocity.Y;
			rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

			//if (velocity.Y < 10)
				//velocity.Y += 0.4f;
		}

		public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
		{
			if (rectangle.TouchTopOf(newRectangle))
			{
				rectangle.Y = newRectangle.Y - rectangle.Height;
				velocity.Y = 0.4f;
				velocity.X = 0f;
			}

			if (rectangle.TouchLeftOf(newRectangle))
			{
				position.X = newRectangle.X - rectangle.Width - 2;
			}

			if (rectangle.TouchRightOf(newRectangle))
			{
				position.X = newRectangle.X + newRectangle.Width + 2;
				velocity.Y = -0.4f;
			}

			if (rectangle.TouchBottomOf(newRectangle))
			{
				velocity.Y = 0.4f;
			}

			if (position.X < 0) position.X = 0;
			if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
			if (position.Y < 0) velocity.Y = 1f;
			if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
	
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, rectangle, Color.White);
		}

	}
}

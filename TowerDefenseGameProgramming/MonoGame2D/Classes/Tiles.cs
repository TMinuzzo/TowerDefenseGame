using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame2D
{
	public class Tiles
	{
		protected Texture2D texture;

		private Rectangle rectangle;
		public Rectangle Rectangle
		{
			get { return rectangle; }
			protected set { rectangle = value; }
		}

		private static ContentManager content;
		public static ContentManager Content
		{
			protected get { return content; }
			set { content = value; }
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, rectangle, Color.White);
		}
	}

	public class CollisionTiles : Tiles
	{
		public CollisionTiles(int i, Rectangle newRectangle)
		{
			texture = Content.Load<Texture2D>("Tile" + i);
			this.Rectangle = newRectangle;
		}
	}

}

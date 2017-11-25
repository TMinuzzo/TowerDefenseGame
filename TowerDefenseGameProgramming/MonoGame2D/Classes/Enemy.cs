using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public class Enemy : Struct
    {
		public float dX; // Aceleração em X do obstáculo
		public float dY; // Aceleração em Y do obstáculo

		/* Getters */
		public float GetDX()
		{
			return dX;
		}

		public float GetDY()
		{
			return dY;
		}

		/* Setters */
		public void SetDX(float dX)
		{
			this.dX = dX;
		}

		public void SetDY(float dY)
		{
			this.dY = dY;
		}

		// Construtor
		public Enemy(GraphicsDevice graphicsDevice, string textureName, float scale)
        {

            this.scale = scale;
            var stream = TitleContainer.OpenStream(textureName);
            texture = Texture2D.FromStream(graphicsDevice, stream);
        }

        public void Update(float elapsedTime)
        {
            this.x = this.x + this.dX * 3;
            this.y = this.y + this.dY * 3;
       
        }

        // Renderiza o(s) inimigo(s)
        public void Draw(SpriteBatch spriteBatch)
        {
           
            // Determina posição dos inimigos
            Vector2 spritePosition = new Vector2(this.x, this.y);
            // Desenha o sprite
            spriteBatch.Draw(texture, spritePosition, null, Color.White, this.angle, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), SpriteEffects.None, 0f);
        }
    }
}
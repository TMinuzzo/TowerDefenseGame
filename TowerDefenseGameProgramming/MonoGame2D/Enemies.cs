using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public class Enemies
    {

        public Texture2D texture
        {
            get;
        }

        // Coordenada x do centro do inimigo
        public float x
        {
            get;
            set;
        }

        // Coordenada y do centro do inimigo
        public float y
        {
            get;
            set;
        }

        // Angulo central do inimigo
        public float angle
        {
            get;
            set;
        }

        // Escala do sprite do inimigo
        public float scale
        {
            get;
            set;
        }
        // Aceleração em x do obstaculo
        public float dX
        {
            get;
            set;
        }

        // Aceleração em y do obstaculo
        public float dY
        {
            get;
            set;
        }
     
        // Construtor da classe
        public Enemies(GraphicsDevice graphicsDevice, string textureName, float scale)
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    class Enemies
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

        // Construtor da classe
        public Enemies(GraphicsDevice graphicsDevice, string textureName, float scale)
        {
            this.scale = scale;
            var stream = TitleContainer.OpenStream(textureName);
            texture = Texture2D.FromStream(graphicsDevice, stream);
        }

        // Função de atualização do estado do inimigo
        public void Update(float elapsedTime)
        {

        }

        // Renderiza o(s) inimigo(s)
        public void Draw(SpriteBatch spriteBatch)
        {
            // Determina posição do inimigo
            Vector2 spritePosition = new Vector2(this.x, this.y);
            // Desenha o sprite
            spriteBatch.Draw(texture, spritePosition, null, Color.White, this.angle, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), SpriteEffects.None, 0f);
        }
    }
}
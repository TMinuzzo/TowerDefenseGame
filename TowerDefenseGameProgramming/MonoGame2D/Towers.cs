using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    class Towers
    {
        // Imagem do sprite
        public Texture2D texture
        {
            get;
        }

        // Coordenadas x centrais do objeto
        public float x
        {
            get;
            set;
        }

        // Coordenadas y centrais do objeto
        public float y
        {
            get;
            set;
        }

        // Angulo central do objeto
        public float angle
        {
            get;
            set;
        }


        // Escala do sprite
        public float scale
        {
            get;
            set;
        }

        // Construtor
        public Towers(GraphicsDevice graphicsDevice, string textureName, float scale)
        {
            this.scale = scale;
            var stream = TitleContainer.OpenStream(textureName);
            texture = Texture2D.FromStream(graphicsDevice, stream);
        }

        // Renderiza o objeto
        public void Draw(SpriteBatch spriteBatch)
        {
            // Determina sua posição
            Vector2 spritePosition = new Vector2(this.x, this.y);
            // Renderiza
            spriteBatch.Draw(texture, spritePosition, null, Color.White, this.angle, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), SpriteEffects.None, 0f);
        }

        // Destrutor
        ~Towers()
        {

        }
    }
}
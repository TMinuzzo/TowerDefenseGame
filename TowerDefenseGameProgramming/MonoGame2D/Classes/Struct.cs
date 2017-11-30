using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    public abstract class Struct
    {
        public Texture2D texture; // Imagem do sprit
        public float x; // Coordenada x central do objeto
        public float y; // Coordenada y central do objeto
        public float angle; // Ângulo central do objeto
        public float scale; // Escala do sprite

        /* Getters */
        public Texture2D GetTexture()
        {
            return texture;
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetAngle()
        {
            return angle;
        }

        public float GetScale()
        {
            return scale;
        }

        /* Setters */
        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public void SetY(float y)
        {
            this.y = y;
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
        }

        // Construtores
        public Struct()
        {
        }

        // Destrutor
        ~Struct()
        {

        }
    }
}
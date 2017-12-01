using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame2D
{
    public class Toolbar
    {
        private Texture2D texture;
        // A class to access the font we created
        private SpriteFont font;

        // The position of the toolbar
        private Vector2 position;
        // The position of the text
        private Vector2 textPositionLives;
        private Vector2 textPositionGold;

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;
            this.font = font;

            this.position = position;
            // Offset the text to the bottom right corner
            textPositionLives = new Vector2(position.X + 620, position.Y + 90);
            textPositionGold = new Vector2(position.X + 1050, position.Y + 90);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, position, Color.White);
           
            string textLives = string.Format(">>> {0} <<<", player.GetLives());
            string textGold = string.Format(" {0} ", player.GetLives());
            spriteBatch.DrawString(font, textLives, textPositionLives, Color.SaddleBrown);
            spriteBatch.DrawString(font, textGold, textPositionGold, Color.SaddleBrown);
        }
    }
}

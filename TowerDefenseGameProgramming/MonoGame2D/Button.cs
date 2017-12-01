using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Windows.Graphics.Display;

namespace MonoGame2D
{
    public enum ButtonStatus
    {
        Normal,
        MouseOver,
        Pressed,
    }

    public class Button
    {
        // Store the MouseState of the last frame.
        private MouseState previousState;

        // The the different state textures.
        private Texture2D hoverTexture;
        private Texture2D pressedTexture;
        int lives = 3;

        // A rectangle that covers the button.
        private Rectangle bounds;

        // Store the current state of the button.
        private ButtonStatus state = ButtonStatus.Normal;

        // Gets fired when the button is pressed.
        public event EventHandler Clicked;
        // Gets fired when the button is held down.
        public event EventHandler OnPress;

        public Texture2D texture
        {
            get;
        }
        // Escala do sprite
        public float scale
        {
            get;
            set;
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
        public Button()
        {
            
        }
        /// <summary>
        /// Constructs a new button.
        /// </summary>
        /// <param name="texture">The normal texture for the button.</param>
        /// <param name="hoverTexture">The texture drawn when the mouse is over the button.</param>
        /// <param name="pressedTexture">The texture drawn when the button has been pressed.</param>
        /// <param name="position">The position where the button will be drawn.</param>
        public Button(Vector2 position, GraphicsDevice graphicsDevice, string textureName, float scale)
        {
            this.scale = scale;
            var stream = TitleContainer.OpenStream(textureName);
            texture = Texture2D.FromStream(graphicsDevice, stream);

            this.bounds = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
        }

        /// <summary>
        /// Updates the buttons state.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public void Update(float elapsedTime)
        {
            // Determine if the mouse if over the button.
            MouseState mouseState = Mouse.GetState();

            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            bool isMouseOver = bounds.Contains(mouseX, mouseY);

            // Update the button state.
            if (isMouseOver && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.MouseOver;
            }
            else if (isMouseOver == false && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.Normal;
            }

            // Check if the player holds down the button.
            if (mouseState.LeftButton == ButtonState.Pressed &&
                previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver == true)
                {
                    // Update the button state.
                    state = ButtonStatus.Pressed;

                    if (OnPress != null)
                    {
                        // Fire the OnPress event. //lembrar de signal slot
                        OnPress(this, EventArgs.Empty);
                    }
                }
            }

            // Check if the player releases the button.
            if (mouseState.LeftButton == ButtonState.Released &&
                previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    // update the button state.
                    state = ButtonStatus.MouseOver;

                    // Fire the clicked event.
                    Clicked?.Invoke(this, EventArgs.Empty);
                }

                else if (state == ButtonStatus.Pressed)
                {
                    state = ButtonStatus.Normal;
                }
            }

            previousState = mouseState;
        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="spriteBatch">A SpriteBatch that has been started</param>
        public void Draw(SpriteBatch spriteBatch)
        {   
            //spriteBatch.Draw(texture, bounds, Color.White);
            Vector2 spritePosition = new Vector2(this.x, this.y);
            spriteBatch.Draw(texture, spritePosition, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), new Vector2(scale, scale), SpriteEffects.None, 0f);

        }

    }
}

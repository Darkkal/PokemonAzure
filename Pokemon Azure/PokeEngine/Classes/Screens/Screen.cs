using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PokeEngine.Screens
{
    public abstract class Screen
    {
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;
        protected SpriteFont font;

        /// <summary>
        /// IsAlive:    if false, ScreenHandler will dispose the screen; 
        /// IsVisible:  Checked for drawing; 
        /// </summary>
        public bool IsAlive {get; set;}
        public bool IsVisible {get; set;}
        public bool IsActive { get; set; }
        public string Name { get; set; }
        
        public Screen(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
        {
            graphics = g;
            content = c;
            font = f;

            IsAlive = true;
            IsVisible = true;
            IsActive = true;
            Name = "ScreenName not set";
        }

        public abstract void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void Close()
        {
            IsAlive = false;
            IsVisible = false;
        }
    }
}

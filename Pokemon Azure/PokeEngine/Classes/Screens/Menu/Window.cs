using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Screens;

namespace PokeEngine.Menu
{
    class Window
    {

        public struct Size
        {
            public int x;
            public int y;
        }

        public Size size;
        public Vector2 Position;

        private Texture2D cornerTexture; 
        private Texture2D sideTexture; 
        private Texture2D backgroundTexture; 
        private Texture2D backgroundCornerTexture;
        private Texture2D backgroundSideTexture;

        public Window()
        {
            size.x = 0;
            size.y = 0;
            Position = new Vector2(0, 0);
            LoadTextures();
        }

        public Window(int SizeX, int SizeY, Vector2 position)
        {
            size.x = SizeX;
            size.y = SizeY;
            Position = position;
            LoadTextures();
        }

        public Window(Vector2 position)
        {
            Position = position;
            LoadTextures();
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            //draw top left corner
            spriteBatch.Draw(backgroundCornerTexture,
                    new Rectangle((int)Position.X, (int)Position.Y, cornerTexture.Width, cornerTexture.Height),
                    color);

            spriteBatch.Draw(
                    cornerTexture,
                    new Rectangle((int)Position.X, (int)Position.Y, cornerTexture.Width, cornerTexture.Height),
                    color);
            //draw top of menu
            for (int i = 1; i < size.x - 1; i++)
            {
                spriteBatch.Draw(
                    backgroundSideTexture,
                    new Rectangle((int)Position.X + i*sideTexture.Width, (int)Position.Y, sideTexture.Width, sideTexture.Height),
                    color);

                spriteBatch.Draw(
                    sideTexture,
                    new Rectangle((int)Position.X + i * sideTexture.Width, (int)Position.Y, sideTexture.Width, sideTexture.Height),
                    color);
            }
            //draw top right corner
            spriteBatch.Draw(
                    backgroundCornerTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

            spriteBatch.Draw(
                    cornerTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    MathHelper.PiOver2,
                    new Vector2(0,0),
                    SpriteEffects.None,
                    0f);
            //draw left side
            for (int i = 1; i < size.y - 1; i++)
            {
                spriteBatch.Draw(
                    backgroundSideTexture,
                    new Rectangle((int)Position.X, (int)Position.Y + (i+1) * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    3 * MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

                spriteBatch.Draw(
                    sideTexture,
                    new Rectangle((int)Position.X, (int)Position.Y + (i + 1) * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    3 * MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);
            }
            //draw right side
            for (int i = 1; i < size.y - 1; i++)
            {
                spriteBatch.Draw(
                    backgroundSideTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y + i * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

                spriteBatch.Draw(
                    sideTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y + i * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);
            }
            //draw bottom left corner
            spriteBatch.Draw(
                    backgroundCornerTexture,
                    new Rectangle((int)Position.X, (int)Position.Y + size.y * sideTexture.Height, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    3*MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

            spriteBatch.Draw(
                    cornerTexture,
                    new Rectangle((int)Position.X, (int)Position.Y + size.y * sideTexture.Height, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    3*MathHelper.PiOver2,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);
            //draw bottom right corner
            spriteBatch.Draw(
                    backgroundCornerTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y + size.y * sideTexture.Height, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    MathHelper.Pi,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

            spriteBatch.Draw(
                    cornerTexture,
                    new Rectangle((int)Position.X + size.x * sideTexture.Width, (int)Position.Y + size.y * sideTexture.Height, cornerTexture.Width, cornerTexture.Height),
                    null,
                    color,
                    MathHelper.Pi,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);
            //draw bottom side
            for (int i = 1; i < size.x - 1; i++)
            {
                spriteBatch.Draw(
                    backgroundSideTexture,
                    new Rectangle((int)Position.X + (i + 1) * sideTexture.Height, (int)Position.Y + size.y * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    MathHelper.Pi,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);

                spriteBatch.Draw(
                    sideTexture,
                    new Rectangle((int)Position.X + (i + 1) * sideTexture.Height, (int)Position.Y + size.y * sideTexture.Height, sideTexture.Width, sideTexture.Height),
                    null,
                    color,
                    MathHelper.Pi,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    0f);
            }
            //fill in the middle
            for (int x = 1; x < size.x - 1; x++)
            {
                for (int y = 1; y < size.y - 1; y++)
                {
                    spriteBatch.Draw(
                        backgroundTexture,
                        new Rectangle((int)Position.X + x * backgroundTexture.Width, (int)Position.Y + y * backgroundTexture.Height, backgroundTexture.Width, backgroundTexture.Height),
                        color);
                }
            }
            
        }

        private void LoadTextures()
        {

            cornerTexture           = ScreenHandler.WindowCorner;
            sideTexture             = ScreenHandler.WindowSide;
            backgroundTexture       = ScreenHandler.WindowBackground;
            backgroundCornerTexture = ScreenHandler.WindowBackgroundCorner;
            backgroundSideTexture   = ScreenHandler.WindowBackgroundSide;

        }

    }
}

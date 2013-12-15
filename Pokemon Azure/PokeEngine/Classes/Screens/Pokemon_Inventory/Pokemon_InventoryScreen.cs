using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PokeEngine.Input;
using PokeEngine.Screens;
using PokeEngine.Trainers;
using PokeEngine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PokeEngine.Menu
{

    struct pokemonWindow
    {

        public Window window; //= new Window(8, 4, Vector2.Zero);

        public void initialize()
        {
            window = new Window(8, 5, Vector2.Zero);
        }

        public Vector2 getNamePos()
        {
            Vector2 pos = window.Position;
            pos.X += 35;
            pos.Y += 35;
            return pos;
        }

        public Vector2 getIconPos()
        {
            Vector2 pos = window.Position;
            pos.X += 70;
            pos.Y += 43;
            return pos;

        }

        public Vector2 getLevelPos()
        {
            Vector2 pos = window.Position;
            pos.X += 35;
            pos.Y += 77;
            return pos;
        }

        public Vector2 getHPPos()
        {
            Vector2 pos = window.Position;
            pos.X += 94;
            pos.Y += 61;
            return pos;
        }

        public Vector2 getHPBarPos()
        {
            Vector2 pos = window.Position;
            pos.X += 114;
            pos.Y += 65;
            return pos;
        }

        public Vector2 getHPTextPos()
        {
            Vector2 pos = window.Position;
            pos.X += 114;
            pos.Y += 77;
            return pos;
        }

        public Vector2 getGenderPos()
        {
            Vector2 pos = window.Position;
            pos.X += 190;
            pos.Y += 43;
            return pos;
        }

    }

    class Pokemon_InventoryScreen : Screen
    {

        private int selection;
        private pokemonWindow[] pokeWindows;
        //private Texture2D[] pokeTextures;
        private Player player;

        private Texture2D hpbarTexture, hpTexture;

        /*  Layout of pokemon
         *  [0] [1]
         *  [2] [3]
         *  [4] [5]
         */ 

        public Pokemon_InventoryScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f, Player player) :
            base(g, c, f)
        {

            selection = 0;

            this.player = player;

            pokeWindows = new pokemonWindow[player.numCurrentPokemon];
            //pokeTextures = new Texture2D[player.numCurrentPokemon];

            for (int i = 0; i < player.numCurrentPokemon; i++)
            {
                pokeWindows[i] = new pokemonWindow();
                pokeWindows[i].initialize();
                //pokeTextures[i] = 

                if (i == 0)
                {
                    pokeWindows[i].window.Position = new Vector2(15, 15);
                }
                else if (i == 1)
                {
                    pokeWindows[i].window.Position = new Vector2(
                        pokeWindows[0].window.Position.X + (pokeWindows[i].window.size.x * 32) + 32,
                        pokeWindows[0].window.Position.Y + 16);
                }
                else if (i % 2 == 0)
                {
                    pokeWindows[i].window.Position = new Vector2(
                        pokeWindows[i - 2].window.Position.X,
                        pokeWindows[i - 2].window.Position.Y + (pokeWindows[i].window.size.y * 32));
                }
                else
                {
                    pokeWindows[i].window.Position = new Vector2(
                        pokeWindows[i - 2].window.Position.X,
                        pokeWindows[i - 2].window.Position.Y + (pokeWindows[i].window.size.y * 32));
                }

            }

            //  Create hpbar and hp textures
            {

                hpTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                Color[] hpColor = { Color.Yellow };
                hpTexture.SetData<Color>(hpColor);

                hpbarTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                Color[] hpbarColor = { Color.Black };
                hpbarTexture.SetData<Color>(hpbarColor);

            }

        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {

            if (InputHandler.WasKeyPressed(keyState, Keys.Down, 10))
            {
                if (selection < player.numCurrentPokemon - 2)
                    selection += 2;
                else
                    selection = selection == player.numCurrentPokemon - 2 ? 0 : 1;
            }
            if (InputHandler.WasKeyPressed(keyState, Keys.Up, 10))
            {
                if (selection > 1)
                    selection -= 2;
                else
                    selection = selection == 1 ? player.numCurrentPokemon - 1 : player.numCurrentPokemon - 2;
            }
            if (InputHandler.WasKeyPressed(keyState, Keys.Left, 10))
            {
                if (selection > 0)
                    selection--;
                else
                    selection = player.numCurrentPokemon - 1;
            }
            if (InputHandler.WasKeyPressed(keyState, Keys.Right, 10))
            {
                if (selection < player.numCurrentPokemon - 1)
                    selection++;
                else
                    selection = 0;
            }
            if (InputHandler.WasKeyPressed(keyState, KeyConfig.Cancel, 10))
            {
                Close();
            }
            if (InputHandler.WasKeyPressed(keyState, KeyConfig.Action, 10))
            {
                //  Bring up screen for messing with pokemon
            }

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < player.numCurrentPokemon; i++)
            {
                if (i == selection)
                {
                    pokeWindows[i].window.Draw(spriteBatch, Color.LightBlue);

                    /* spriteBatch.Draw(
                        pokeTextures[i],
                        new Rectangle(
                            pokeWindows[i].getIconPos().X,
                            pokeWindows[i].getIconPos().Y,
                            32,
                            32),
                        Color.LightBlue); */

                    /* spriteBatch.Draw(
                        player.currentPokemon[i].baseStat.GenderValue == 0 ? MaleTexture : FemaleTexture,
                        new Rectangle(
                            pokeWindows[i].getGenderPos().X,
                            pokeWindows[i].getGenderPos().Y,
                            16,
                            16),
                        Color.LightBlue); */
                }
                else
                    pokeWindows[i].window.Draw(spriteBatch, Color.White);

                spriteBatch.Draw(
                        hpbarTexture,
                        new Rectangle(
                            (int)pokeWindows[i].getHPBarPos().X,
                            (int)pokeWindows[i].getHPBarPos().Y,
                            95,
                            5),
                        Color.White);

                spriteBatch.Draw(
                    hpTexture,
                    new Rectangle(
                        (int)pokeWindows[i].getHPBarPos().X + 3,
                        (int)pokeWindows[i].getHPBarPos().Y + 1,
                        (int)(89 * player.currentPokemon[i].currentHP) / player.currentPokemon[i].HP,
                        3),
                    Color.White);

                spriteBatch.DrawString(
                    font,
                    "HP",
                    pokeWindows[i].getHPTextPos(),
                    Color.White);

                spriteBatch.DrawString(
                    font,
                    "Lv." + player.currentPokemon[i].level,
                    pokeWindows[i].getLevelPos(),
                    Color.White);

                spriteBatch.DrawString(
                    font,
                    player.currentPokemon[i].isNamed ? player.currentPokemon[i].Nickname : player.currentPokemon[i].baseStat.Name,
                    pokeWindows[i].getNamePos(),
                    Color.White);

            }
        }
    }
}
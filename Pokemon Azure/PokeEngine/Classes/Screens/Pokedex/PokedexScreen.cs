using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PokeEngine.Screens;
using PokeEngine.Input;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Tools;
using PokeEngine.Trainers;

namespace PokeEngine.Menu
{

    public enum SortMethod { NUMBER, NAME, TYPE, AREA, MOVESET, COLOR }

    class PokedexScreen : Screen
    {

        Rectangle SEARCH_BAR_RECT = new Rectangle(99, 0, 440, 32);
        Vector2 INITIAL_ENTRY_POSITION = new Vector2(104, 42);
        string UNKNOWN_ENTRY = "-------";
        int totalPokemon;

        KeyboardInput textBoxInput;

        Texture2D background;
        int selectedIndex;
        byte cursorIndex;
        SortedDictionary<int, String> visibleNames;

        public PokedexScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f, Player inPlayer) : base(g, c, f)
        {
            font = content.Load<SpriteFont>(@"aansa");

            textBoxInput = new KeyboardInput();
            textBoxInput.IsDone = true;

            background = SaveLoad.LoadTexture2D(@"Content\Textures\Game\pokedex_background.png", g.GraphicsDevice);
            selectedIndex = 1;
            cursorIndex = 0;
            visibleNames = new SortedDictionary<int, String>();
            for (int i = 1; i < 13; i++)
            {
                if (inPlayer.IdentifiedPokemon[i])
                    visibleNames.Add(i, Pokemon.BaseStatsList.GetBaseStats(i).Name); 
                else
                    visibleNames.Add(i, UNKNOWN_ENTRY);
            }

            totalPokemon = inPlayer.IdentifiedPokemon.Length;

            Name = "PokedexScreen";

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (IsVisible)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);

                if(!textBoxInput.GetText().Contains('\0'))
                    spriteBatch.DrawString(font, textBoxInput.GetText(), new Vector2(0, 30), Color.White);

                for (int i = 0; i < visibleNames.Count; i++)
                {
                    try
                    {
                        //  Draw Entry number
                        spriteBatch.DrawString(
                            font,
                            visibleNames.Keys.ToArray()[i].ToString(),
                            new Vector2(
                                INITIAL_ENTRY_POSITION.X + (visibleNames.Keys.ToArray()[i] == selectedIndex ? 10: 0),
                                INITIAL_ENTRY_POSITION.Y + (i * 32)),
                            visibleNames.Keys.ToArray()[i] == selectedIndex ? Color.Red : Color.Black);

                        //  Draw Entry name
                        spriteBatch.DrawString(
                            font,
                            visibleNames.Values.ToArray()[i].ToString(),
                            new Vector2(
                                INITIAL_ENTRY_POSITION.X + 63 + (visibleNames.Keys.ToArray()[i] == selectedIndex ? 10: 0),
                                INITIAL_ENTRY_POSITION.Y + (i * 32)),
                            visibleNames.Keys.ToArray()[i] == selectedIndex ? Color.Red : Color.Black);
                    }
                    catch (ArgumentException) { }
                }
            }

        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
            
            if (!textBoxInput.IsDone)
            {
                textBoxInput.Update(keyState);
            }
            else
            {
                if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[1], 10))
                {
                    selectedIndex = selectedIndex < totalPokemon - 1 ? selectedIndex + 1 : 1;

                    if (cursorIndex < 11)
                        cursorIndex++;
                    else
                    {
                        visibleNames.Remove(selectedIndex - 12);
                        visibleNames.Add(selectedIndex, Pokemon.BaseStatsList.GetBaseStats(selectedIndex).Name);
                    }
                }
                if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[0], 10))
                {
                    if (selectedIndex > 1)
                    {
                        selectedIndex--;

                        if (cursorIndex > 0)
                            cursorIndex--;
                        else
                        {
                            visibleNames.Remove(selectedIndex + 12);
                            visibleNames.Add(selectedIndex, Pokemon.BaseStatsList.GetBaseStats(selectedIndex).Name);
                        }
                    }
                }
                if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[5], 10))
                {
                    this.Close();
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    int x = (int)Mouse.GetState().X;
                    int y = (int)Mouse.GetState().Y;

                    if (SEARCH_BAR_RECT.Contains(x, y))
                        textBoxInput.IsDone = false;
                    else
                    {
                        textBoxInput.IsDone = true;

                        //Here goes logic to handle opening a new window
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            

        }
    }
}
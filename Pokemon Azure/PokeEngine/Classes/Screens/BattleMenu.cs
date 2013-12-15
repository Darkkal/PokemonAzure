using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Screens;
using PokeEngine.Pokemon;
using PokeEngine.Trainers;
using PokeEngine.Battle;
using System.Threading;
using PokeEngine.Items;
using PokeEngine.Tools;

namespace PokeEngine.Menu
{
    public struct Textures
    {
        public static Texture2D fightMenu;
        public static Texture2D pokeMenu;
        public static Texture2D itemMenu;
        public static Texture2D runMenu;
        public static Texture2D moveButton;
    }

    public struct Rectangles
    {
        public static Rectangle menuRect;
        public static Rectangle[] moveRect = new Rectangle[4];
    }

    public static class BattleMenu
    {
        static GraphicsDeviceManager graphics;
        static ContentManager content;
        static SpriteFont font;

        public static bool isVisible;

        public static Player player;
        public static BattlePokemon pokemon;
        public static PokeBattle battle;

        public static bool inMenu = false; //if the cursor is inside the menu or selecting an option

        public static byte[] options = { 0, 1, 2, 3 }; // 0 = FIGHT, 1 = POKE, 2 = ITEM, 3 = RUN
        public static byte optionChoice;

        public static byte[] fightOptions = { 0, 1, 2, 3 }; //four moves on a pokemon :P
        public static byte fightChoice;

        public static byte[] pokeOptions = { 0, 1, 2, 3, 4, 5 }; //six pokemon on a trainer :P
        public static byte pokeChoice;

        public static int[] itemOptions;
        public static int itemChoice;

        static public void Initialize(GraphicsDeviceManager g, ContentManager c, SpriteFont f, PokeBattle inBattle, BattleScreen battleScreen)
        {
            graphics = g;
            content = c;
            font = f;
            isVisible = true;
            optionChoice = 0;
            battle = inBattle;
            pokemon = battle.Positions[0].pokemon;
            player = (Player)battle.Positions[0].trainer;
            fightChoice = 0;
            pokeChoice = 0;
            itemOptions = new int[player.inventory.Count];
            itemChoice = 0;

            Textures.fightMenu = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\fightMenu.png", g.GraphicsDevice);
            Textures.pokeMenu = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\pokeMenu.png", g.GraphicsDevice);
            Textures.itemMenu = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\itemMenu.png", g.GraphicsDevice);
            Textures.runMenu = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\runMenu.png", g.GraphicsDevice);
            Textures.moveButton = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\moveButton.png", g.GraphicsDevice);

            Rectangles.menuRect = new Rectangle(
                graphics.PreferredBackBufferWidth / 2 - Textures.fightMenu.Width / 2,
                graphics.PreferredBackBufferHeight - Textures.fightMenu.Height,
                Textures.fightMenu.Width,
                Textures.fightMenu.Height);

            
            Rectangles.moveRect[0] = new Rectangle(
                Rectangles.menuRect.X + 40,
                Rectangles.menuRect.Y + 40,
                Textures.moveButton.Width,
                Textures.moveButton.Height);

            Rectangles.moveRect[1] = new Rectangle(
                Rectangles.moveRect[0].X + Textures.moveButton.Width + 10,
                Rectangles.moveRect[0].Y,
                Textures.moveButton.Width,
                Textures.moveButton.Height);

            Rectangles.moveRect[2] = new Rectangle(
                Rectangles.moveRect[0].X,
                Rectangles.moveRect[0].Y + Textures.moveButton.Height + 10,
                Textures.moveButton.Width,
                Textures.moveButton.Height);

            Rectangles.moveRect[3] = new Rectangle(
                Rectangles.moveRect[0].X + Textures.moveButton.Width + 10,
                Rectangles.moveRect[0].Y + Textures.moveButton.Height + 10,
                Textures.moveButton.Width,
                Textures.moveButton.Height);
        }

        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (optionChoice)
            {
                //FIGHT
                case 0:
                    DrawFightOptions(spriteBatch);
                    break;

                //POKE
                case 1:
                    DrawPokeOptions(spriteBatch);
                    break;

                //ITEM
                case 2:
                    DrawItemOptions(spriteBatch);
                    break;

                //RUN
                case 3:
                    DrawRunOption(spriteBatch);
                    break;
            }
        }

        public static void Unload()
        {
            graphics = null;
            content = null;
            font = null;
            isVisible = false;
            optionChoice = 0;
            pokemon = null;
            player = null;
            fightChoice = 0;
            pokeChoice = 0;
            itemOptions = null;
            itemChoice = 0;
            Textures.fightMenu = null;
            Textures.pokeMenu = null;
            Textures.itemMenu = null;
            Textures.runMenu = null;
            Textures.moveButton = null;
        }

        public static void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState, BattleScreen battleScreen)
        {
            if (battleScreen.state == State.Input)
            {
                if (!inMenu) //if selecting an option
                {
                    //go in the menu
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.Z, 10) || Input.InputHandler.WasKeyPressed(keyState, Keys.Down, 10))
                    {
                        inMenu = true;
                    }

                    //go right
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.Right, 10))
                    {
                        optionChoice = (byte)(optionChoice < (byte)3 ? optionChoice + 1 : 0);
                    }

                    //go left
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.Left, 10))
                    {
                        optionChoice = (byte)(optionChoice > (byte)0 ? optionChoice - 1 : 3);
                    }
                }
                else //else if currently in an option
                {
                    //go out of the menu to select an option
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.X, 10) || Input.InputHandler.WasKeyPressed(keyState, Keys.Up, 10))
                    {
                        inMenu = false;
                    }

                    lock (battle.lockObject)
                    {
                        switch (optionChoice)
                        {
                            //FIGHT
                            case 0:

                                //use move
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Z))
                                {
                                    if (pokemon.pokemon.move[fightChoice] != null)
                                    {
                                        battle.Positions[battle.waitingIndex].choice =
                                            BattleChoice.UseMove(battle.Positions[battle.waitingIndex].pokemon.move[fightChoice],
                                            battle.Positions[1]);
                                        battle.waitingForPlayerInput = false;
                                        Monitor.Pulse(battle.lockObject);
                                    }
                                }

                                //go right
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Right, 10))
                                {
                                    fightChoice = (byte)(fightChoice < (byte)3 ? fightChoice + 1 : 0);
                                }

                                //go left
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Left, 10))
                                {
                                    fightChoice = (byte)(fightChoice > (byte)0 ? fightChoice - 1 : 3);
                                }
                                break;

                            //POKE
                            case 1:

                                //switch pokemon
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Z))
                                {
                                    if (player.currentPokemon[pokeChoice] != null)
                                    {
                                        battle.Positions[0].SwitchPokemon(player.currentPokemon[pokeChoice]);
                                        battle.waitingForPlayerInput = false;
                                        Monitor.Pulse(battle.lockObject);
                                    }
                                }

                                //go right
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Right, 10))
                                {
                                    pokeChoice = (byte)(pokeChoice < (byte)5 ? pokeChoice + 1 : 0);
                                }

                                //go left
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Left, 10))
                                {
                                    pokeChoice = (byte)(pokeChoice > (byte)0 ? pokeChoice - 1 : 5);
                                }
                                break;

                            //ITEM
                            case 2:

                                //use item
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Z))
                                {
                                    if (player.inventory[itemChoice] != null)
                                    {
                                        battle.Positions[battle.waitingIndex].choice =
                                            BattleChoice.UseItem(Item.getItem(battle.Positions[battle.waitingIndex].trainer.inventory[itemChoice].itemID),
                                            battle.Positions[1]);
                                        battle.waitingForPlayerInput = false;
                                        Monitor.Pulse(battle.lockObject);
                                    }
                                }

                                //go right
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Right, 10))
                                {
                                    itemChoice = (itemChoice < itemOptions.Length - 1 ? itemChoice + 1 : 0);
                                }

                                //go left
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Left, 10))
                                {
                                    itemChoice = (itemChoice > 0 ? itemChoice - 1 : itemOptions.Length - 1);
                                }
                                break;

                            //RUN
                            case 3:

                                //get the fuck out of there (or try to atleast)
                                if (Input.InputHandler.WasKeyPressed(keyState, Keys.Z))
                                {
                                    battle.Positions[battle.waitingIndex].choice =
                                        BattleChoice.RunFromBattle();
                                    battle.waitingForPlayerInput = false;
                                    Monitor.Pulse(battle.lockObject);
                                }

                                break;
                        }
                    }
                }
            }
        }


        public static void DrawFightOptions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.fightMenu, Rectangles.menuRect, Color.White);

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(Textures.moveButton, Rectangles.moveRect[i], fightChoice == i && inMenu ? Color.Blue : Color.White);

                if (pokemon.move[i] != null)
                {
                    spriteBatch.DrawString(
                        font,
                        pokemon.move[i].bMove.name,
                        new Vector2(
                            (Rectangles.moveRect[i].X) + (Rectangles.moveRect[i].Width / 2),
                            (Rectangles.moveRect[i].Y) + (Rectangles.moveRect[i].Height / 2)),
                            Color.Black);
                }
            }
        }

        public static void DrawPokeOptions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.pokeMenu, Rectangles.menuRect, Color.White);
        }

        public static void DrawItemOptions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.itemMenu, Rectangles.menuRect, Color.White);
        }

        public static void DrawRunOption(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.runMenu, Rectangles.menuRect, Color.White);
        }

    }
}

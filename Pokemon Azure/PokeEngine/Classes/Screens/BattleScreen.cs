using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Battle;
using PokeEngine.Trainers;
using PokeEngine.Pokemon;
using PokeEngine.Menu;
using System.Threading;
using System.IO;
using PokeEngine.Tools;

namespace PokeEngine.Screens
{
    public class BattleScreen : Screen
    {
        public PokeBattle battle;
        private Thread thread;
        public State state;
        Texture2D red, orange, green; //textures to use for health bars
        private Texture2D playerHPBarTexture;
        private Texture2D opponentHPBarTexture;
        private Rectangle playerHPBarRect;
        private Rectangle opponentHPBarRect;
        private Rectangle playerPokeRect;
        private Rectangle opponentPokeRect;

        public BattleScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f, Trainer trainerOne, Trainer trainerTwo)
            : base(g, c, f)
        {
            font = content.Load<SpriteFont>("aansa");

            //make some colour textures to use when drawing health bars
            Color[] colour = new Color[1];
            green = new Texture2D(g.GraphicsDevice, 1, 1);
            colour[0] = new Color(47, 215, 71);
            green.SetData<Color>(colour);
            orange = new Texture2D(g.GraphicsDevice, 1, 1);
            colour[0] = new Color(249, 207, 78);
            orange.SetData<Color>(colour);
            red = new Texture2D(g.GraphicsDevice, 1, 1);
            colour[0] = new Color(255, 34, 34);
            red.SetData<Color>(colour);

            playerHPBarTexture = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\playerHP.png", g.GraphicsDevice);
            opponentHPBarTexture = SaveLoad.LoadTexture2D(@"Content\Textures\Battle\opponentHP.png", g.GraphicsDevice);
            playerHPBarRect = new Rectangle(340, 265, playerHPBarTexture.Width, playerHPBarTexture.Height);
            opponentHPBarRect = new Rectangle(20,40,opponentHPBarTexture.Width,opponentHPBarTexture.Height);
            playerPokeRect = new Rectangle(70,200,192,192);
            opponentPokeRect = new Rectangle(350,20,192,192);

            startSingleBattle(trainerOne, trainerTwo);
            Name = "BattleScreen";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawPlayerHealthBar(battle.Positions[0], spriteBatch);
            DrawOpponentHealthBar(battle.Positions[1], spriteBatch);
            DrawPlayerPokemon(battle.Positions[0], spriteBatch);
            DrawOpponentPokemon(battle.Positions[1], spriteBatch);

            if (BattleMenu.isVisible)
            {
                BattleMenu.Draw(spriteBatch);
            }
        }

        private void DrawPlayerPokemon(BattlePosition battlePosition, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(battlePosition.backTexture, playerPokeRect, Color.White);
        }

        private void DrawOpponentPokemon(BattlePosition battlePosition, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(battlePosition.frontTexture, opponentPokeRect, Color.White);
        }

        private void DrawPlayerHealthBar(BattlePosition position, SpriteBatch spriteBatch)
        {
            if (position.pokemon == null)
                return;

            int currentHealth = position.pokemon.currentHealth;
            int maxHealth = position.pokemon.maxHealth;
            int currentEXP = position.pokemon.pokemon.currentExp;
            int goalEXP = position.pokemon.pokemon.expAtLevel(position.pokemon.pokemon.level);
            

            Texture2D colour;
            if (currentHealth > (maxHealth / 2))
                colour = green;
            else if (currentHealth > (maxHealth / 10))
                colour = orange;
            else
                colour = red;

            //draw HPBar
            spriteBatch.Draw(playerHPBarTexture, playerHPBarRect, Color.White);
            //draw HP
            spriteBatch.Draw(colour, new Rectangle(playerHPBarRect.X + 73, playerHPBarRect.Y + 5, (int)(144.0 * ((double)currentHealth / (double)maxHealth)), 4), Color.White);
            //draw EXP
            spriteBatch.Draw(orange, new Rectangle(playerHPBarRect.X + 42, playerHPBarRect.Y + 19, (int)(217.0 * ((double)currentEXP / (double)goalEXP)), 2), Color.White);
            //draw Pokemon name
            spriteBatch.DrawString(font, position.pokemon.pokemon.Nickname, new Vector2(playerHPBarRect.X + 40, playerHPBarRect.Y - 15), Color.White);
            spriteBatch.DrawString(font, position.pokemon.pokemon.Nickname, new Vector2(playerHPBarRect.X + 41, playerHPBarRect.Y - 16), Color.Black);
            //draw Level
            spriteBatch.DrawString(font, "Lv. " + position.pokemon.pokemon.level, new Vector2(playerHPBarRect.X + 226, playerHPBarRect.Y - 15), Color.White);
            spriteBatch.DrawString(font, "Lv. " + position.pokemon.pokemon.level, new Vector2(playerHPBarRect.X + 227, playerHPBarRect.Y - 16), Color.Black);
            //draw HP Text
            spriteBatch.DrawString(font, currentHealth + "/" + maxHealth, new Vector2(playerHPBarRect.X + 226, playerHPBarRect.Y + 2), Color.White);
            spriteBatch.DrawString(font, currentHealth + "/" + maxHealth, new Vector2(playerHPBarRect.X + 227, playerHPBarRect.Y + 1), Color.Black);
        }

        private void DrawOpponentHealthBar(BattlePosition position, SpriteBatch spriteBatch)
        {
            if (position.pokemon == null)
                return;

            int currentHealth = position.pokemon.currentHealth;
            int maxHealth = position.pokemon.maxHealth;
            int currentEXP = position.pokemon.pokemon.currentExp;
            int goalEXP = position.pokemon.pokemon.expAtLevel(position.pokemon.pokemon.level);

            Texture2D colour;
            if (currentHealth > (maxHealth / 2))
                colour = green;
            else if (currentHealth > (maxHealth / 10))
                colour = orange;
            else
                colour = red;

            //draw HPBar
            spriteBatch.Draw(opponentHPBarTexture, opponentHPBarRect, Color.White);
            //draw HP
            spriteBatch.Draw(colour, new Rectangle(opponentHPBarRect.X + 66, opponentHPBarRect.Y + 5, (int)(195.0 * ((double)currentHealth / (double)maxHealth)), 4), Color.White);
            //draw Pokemon name
            spriteBatch.DrawString(font, position.pokemon.pokemon.Nickname, new Vector2(opponentHPBarRect.X + 40, opponentHPBarRect.Y - 15), Color.White);
            spriteBatch.DrawString(font, position.pokemon.pokemon.Nickname, new Vector2(opponentHPBarRect.X + 41, opponentHPBarRect.Y - 16), Color.Black);
            //draw Level
            spriteBatch.DrawString(font, "Lv. " + position.pokemon.pokemon.level, new Vector2(opponentHPBarRect.X + 237, opponentHPBarRect.Y - 15), Color.White);
            spriteBatch.DrawString(font, "Lv. " + position.pokemon.pokemon.level, new Vector2(opponentHPBarRect.X + 238, opponentHPBarRect.Y - 16), Color.Black);
        }


        public override void Update(GameTime gameTime)
        {
            //check whether the battle is over so we can end the thread
            if (battle.isOver && state != State.BattleEnd)
            {
                thread.Join();
                state = State.BattleEnd;
                DialogBox.newMessage("Battle is now Over");
                //DialogBox.newMessage("Battle is now Over");
                if (battle.YouWin)
                {
                    battle.Positions[0].pokemon.pokemon.addExp(4000); // add some made up amount of exp for testing
                    DialogBox.newMessage("You are the Victor!");
                }
                else
                {
                    battle.Positions[1].pokemon.pokemon.addExp(4000);
                    DialogBox.newMessage("You Lose!");
                }
                BattleMenu.isVisible = false;
                BattleMenu.Unload();
            }
            //check whether we are waiting for player input
            else if (battle.waitingForPlayerInput)
            {
                state = State.Input;
                BattleMenu.isVisible = true;
            }
            //otherwise we are in the action state I guess
            else if(state != State.BattleEnd)
            {
                state = State.Action;
            }

            if(BattleMenu.isVisible)
                BattleMenu.Update();

            if(battle.isOver && state == State.BattleEnd)
            {
                ScreenHandler.PopScreen();
            }

        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
            BattleMenu.HandleInput(gamePadState, keyState, mouseState, this);
            //if the battle is waiting for player input
            if (state == State.Input)
            {
                //get input based on key presses
                lock (battle.lockObject)
                {
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.D1, 10) && battle.Positions[battle.waitingIndex].pokemon.move[0] != null)
                    {
                        BattleChoice nextChoice = BattleChoice.UseMove(battle.Positions[battle.waitingIndex].pokemon.move[0], battle.Positions[1]);
                        battle.Positions[battle.waitingIndex].choice = nextChoice;
                        battle.waitingForPlayerInput = false;
                        Monitor.Pulse(battle.lockObject);
                    }
                    else if (Input.InputHandler.WasKeyPressed(keyState, Keys.D2, 10) && battle.Positions[battle.waitingIndex].pokemon.move[1] != null)
                    {
                        BattleChoice nextChoice = BattleChoice.UseMove(battle.Positions[battle.waitingIndex].pokemon.move[1], battle.Positions[1]);
                        battle.Positions[battle.waitingIndex].choice = nextChoice;
                        battle.waitingForPlayerInput = false;
                        Monitor.Pulse(battle.lockObject);
                    }
                    else if (Input.InputHandler.WasKeyPressed(keyState, Keys.D3, 10) && battle.Positions[battle.waitingIndex].pokemon.move[2] != null)
                    {
                        BattleChoice nextChoice = BattleChoice.UseMove(battle.Positions[battle.waitingIndex].pokemon.move[2], battle.Positions[1]);
                        battle.Positions[battle.waitingIndex].choice = nextChoice;
                        battle.waitingForPlayerInput = false;
                        Monitor.Pulse(battle.lockObject);
                    }
                    else if (Input.InputHandler.WasKeyPressed(keyState, Keys.D4, 10))
                    {
                        BattleChoice nextChoice = BattleChoice.RunFromBattle();
                        battle.Positions[battle.waitingIndex].choice = nextChoice;
                        battle.waitingForPlayerInput = false;
                        Monitor.Pulse(battle.lockObject);
                    }
                }
            }
        }

        public void startSingleBattle(Trainer trainerOne, Trainer trainerTwo)
        {
            //our state is currently at the start of the battle
            state = State.BattleStart;            

            //create instance of battle
            battle = new PokeBattle(trainerOne, trainerTwo);            

            //start the battle thread
            thread = new Thread(battle.BattleLoop);
            thread.Start();

            BattleMenu.Initialize(graphics, content, font, battle, this);
        }
    }

public enum State
{
    Input, Action, BattleStart, BattleEnd, PostBattle
}
}

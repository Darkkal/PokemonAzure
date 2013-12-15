using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Trainers;
using PokeEngine.Pokemon;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using SD = System.Drawing;
using System.IO;
using PokeEngine;

namespace PokeEngine.Battle
{
    /// <summary>
    /// Represents a 'place' on the battlefield, 
    /// </summary>
    public class BattlePosition
    {
        public int index;
        public Trainer trainer;
        public BattlePokemon pokemon;
        public PokeBattle battle; // a reference to the current pokemon battle
        public BattleChoice choice;
        public int runAttempts = 0;

        public Texture2D frontTexture; // the textures used for this battle position
        public Texture2D backTexture;

        public BattlePosition()
        {
            index = 0;
            trainer = null;
            pokemon = null;
        }

        public BattlePosition(int inIndex, Trainer inTrainer, PokeBattle inBattle)
        {
            index = inIndex;
            trainer = inTrainer;
            pokemon = null;
            battle = inBattle;
        }

        /// <summary>
        /// Gets the next decision of the owner of this battle position
        /// Uses AI if an NPC, otherwise gets player input
        /// </summary>
        /// <returns>the next battle choice</returns>
        public void GetNextDecision()
        {
            battle.waitingForPlayerInput = false;
            BattleChoice nextChoice = null;

            if (trainer.GetType() == typeof(Trainer))
            {
                //get random move from pokemon's movepool
                Random random = new Random();
                while (nextChoice == null)
                {
                    int num = random.Next(0, 3);
                    if (pokemon.move[num] != null)
                    {
                        nextChoice = BattleChoice.UseMove(pokemon.move[num], GetRandomOpponentTarget());
                    }
                }
                choice = nextChoice;
            }
            else
            {
                lock (battle.lockObject)
                {
                    battle.waitingForPlayerInput = true;
                    battle.waitingIndex = index;
                    //get player input
                    Monitor.Wait(battle.lockObject);
                    if (choice == null)
                        throw new Exception();
                }
            }
           
        }

        private BattlePosition GetRandomOpponentTarget()
        {
            BattlePosition target;

            //If you're one one side attack the other side and vica versa
            if (battle.NumberOfPositions / 2 <= index)
            {
                Random random = new Random();
                int targetIndex = random.Next(battle.NumberOfPositions / 2);
                target = battle.Positions[targetIndex];
            }
            else
            {
                Random random = new Random();
                int targetIndex = random.Next(battle.NumberOfPositions / 2) + (battle.NumberOfPositions / 2);
                target = battle.Positions[targetIndex];
            }

            return target;
        }

        /// <summary>
        /// Switches Pokemon in this spot
        /// </summary>
        /// <param name="inPokemon">pokemon to switch to</param>
        public void SwitchPokemon(BattlePokemon inPokemon)
        {
            pokemon = inPokemon;
            //update texture
            UpdateTexture();

        }

        /// <summary>
        /// Switches Pokemon in this spot
        /// </summary>
        /// <param name="inPokemon">pokemon to switch to</param>
        public void SwitchPokemon(ActivePokemon inPokemon)
        {
            pokemon = new BattlePokemon(inPokemon);
            //update texture
            UpdateTexture();
        }

        public void UpdateTexture()
        {
            //get paths to pokemon
            String texturesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Sprites\\Pokemon");
            String frontPath = texturesDir + "\\" + pokemon.pokemon.baseStat.ID + "_front";
            if (pokemon.pokemon.isShiny)
                frontPath += "_shiny";
            frontPath += ".png";
            String backPath = texturesDir + "\\" + pokemon.pokemon.baseStat.ID + "_back";
            if (pokemon.pokemon.isShiny)
                backPath += "_shiny";
            backPath += ".png";

            //SD is using the system drawing namespace
            //read front image
            SD.Bitmap image = new SD.Bitmap(frontPath);
            SD.Graphics imageGraphics = SD.Graphics.FromImage(image);
            frontTexture = new Texture2D(Game1.Graphics.GraphicsDevice, image.Width, image.Height);
            frontTexture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, new FileStream(frontPath, FileMode.Open, FileAccess.Read, FileShare.Read));

            //read back image
            image = new SD.Bitmap(backPath);
            imageGraphics = SD.Graphics.FromImage(image);
            backTexture = new Texture2D(Game1.Graphics.GraphicsDevice, image.Width, image.Height);
            backTexture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, new FileStream(backPath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        public int PriorityIndex
        { 
            /*  This will be an easy calculation of which move goes first.
             *  The point is to make it easier by the stack to determine which move goes first.
             *  Here is the breakdown of the formula:
             *  basePriority        :Moves with a higher priority will always go first, even with moves like Trick Room
             *  MAX_EFFECTIVE_SPEED :(Default 1024) This is the maximum speed any Pokémon can achieve through the internal formula.
             *  effectiveSpeed      :A Pokemon's speed including EV and in-battle calculations
             *  It seemed easier to combine priority and speed values instead of sorting them individually.
             *  The MAX_EFFECTIVE_SPEED multiplier was used as a way to seperate the two values from conflicting. 
             *  It is multiplied by two, due to effects like Trick Room that reverse how the engine sorts speed,
             *  but not priority. The plan is to flip the sign of the speed modifier for Trick Room, without the
             *  need for implementing a special sorting method for the stack. This will also make items, like
             *  Quick Claw, easier to implement by temporarily changing the effective speed to
             *  MAX_EFFECTIVE_SPEED.
             *  
             * -Sid
             */
            get {return (choice.move.bMove.basePriority)*BattlePokemon.MAX_EFFECTIVE_SPEED*2+pokemon.effectiveSpeed;} 
        }
    }
}

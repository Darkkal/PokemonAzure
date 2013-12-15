using System;
using PokeEngine.Pokemon;
using PokeEngine.Trainers;
using LuaInterface;
using PokeEngine.TFSH;
using PokeEngine.Classes.Mechanics_Classes.Battle;

namespace PokeEngine.Battle
{
    public class PokeBattle
    {
        //the array of positions on the battlefield
        //the first half are 'friendly' the second half are 'enemy'
        //must, therefore, have even number of positions
        private BattlePosition[] position;
        public BattlePosition[] Positions { get { return position; } }
        //private BattleStack battleStack;

        private bool youWin; //after the battle ends this tells you if you are the victor or not
        public bool YouWin { get { return youWin; } }

        public int NumberOfPositions { get { return position.Length; } }

        //private int[] speedOrder; //

        public bool isOver = false;
        public bool waitingForPlayerInput = false;
        public int waitingIndex;

        public object lockObject; //for thread synchronisation

        /// <summary>
        /// Start a battle between two trainers
        /// </summary>
        /// <param name="player">the player (or npc I guess)</param>
        /// <param name="opponent">the opponent</param>
        public PokeBattle(Trainer player, Trainer opponent)
        {
            position = new BattlePosition[2];
            position[0] = new BattlePosition(0, player, this);
            position[1] = new BattlePosition(1, opponent, this);

            lockObject = new object();
            waitingForPlayerInput = false;

            //send out first pokemon in party
            for (int i = 0; i < 6; i++)
            {
                if (player.currentPokemon[i] != null)
                {
                    if (player.currentPokemon[i].currentHP > 0)
                    {
                        Positions[0].SwitchPokemon(new BattlePokemon(player.currentPokemon[i]));
                        break;
                    }
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (player.currentPokemon[i] != null)
                {
                    if (player.currentPokemon[i].currentHP > 0)
                    {
                        Positions[1].SwitchPokemon(new BattlePokemon(opponent.currentPokemon[i]));
                        break;
                    }
                }
            }
            BattleStack.initializeStacks(this);
        }

        /// <summary>
        /// Start a battle between two trainers
        /// </summary>
        /// <param name="player">the player (or npc I guess)</param>
        /// <param name="opponent">the opponent</param>
        /// <param name="numberOfPositions">number of places on the field (two is the normal)</param>
        public PokeBattle(Trainer player, Trainer opponent, int numberOfPositions)
        {
            lockObject = new object();
            waitingForPlayerInput = false;

            //throw an error if given an odd number
            if((numberOfPositions % 2) != 0)
                throw( new ArgumentException("Number of Positions must be even!"));

            position = new BattlePosition[numberOfPositions];

            //set the first half to the player
            for (int i = 0; i < (numberOfPositions / 2); i++)
            {
                position[i] = new BattlePosition(i, player, this);
            }
            //second half to the opponent
            for (int i = (numberOfPositions / 2); i < numberOfPositions; i++)
            {
                position[i] = new BattlePosition(i, opponent, this);
            }
            BattleStack.initializeStacks(this);
            //speedOrder = GetSpeedOrder();
        }

        /// <summary>
        /// This is the main loop of the pokemon battle
        /// </summary>
        public void BattleLoop()
        {
            //intro dialog
            doDialog();

            //battle loop
            while (isOver != true)
            {
                //get the order in which participants go
                //speedOrder = GetSpeedOrder(); 
                //get each player's decisions
                getDecisions();

                //battle loop

                //items
                //doItems(); 
                //flee
                //doFlees(); 
                //moves (remember priorities)
                //doMoves(); 
                //Decides which moves go first.
                //Pops and executes each move.
                executeStack();
                
                //status
                //doStatusEffects(); 
                //weather
                //doWeatherEffects(); 

                //dialog
                doDialog();

                isOver = CheckForBattleEnd();
            }
        }

        private void getDecisions()
        {
            foreach (BattlePosition p in position)
            {
                p.GetNextDecision();
            }
        }

        private bool CheckForBattleEnd()
        {
            //if the battle is already over don't worry about doing this
            if (isOver)
            {
                return true;
            }

            bool sideOneOver = true;
            bool sideTwoOver = true;
            //go through each trainer on one side
            //make sure they have at least one pokemon still alive
            for( int i = 0; i < NumberOfPositions / 2; i++)
            {
                //go through pokemon
                for(int p = 0; p < 6; p++)
                {
                    if (position[i].trainer.currentPokemon[p] != null)
                    {
                        if (position[i].trainer.currentPokemon[p].currentHP > 0)
                        {
                            sideOneOver = false;
                        }
                    }
                }
            }
            //go through each trainer on the other side
            //make sure they have at least one pokemon still alive
            for (int i = NumberOfPositions / 2; i < NumberOfPositions; i++)
            {
                //go through pokemon
                for (int p = 0; p < 6; p++)
                {
                    if (position[i].trainer.currentPokemon[p] != null)
                    {
                        if (position[i].trainer.currentPokemon[p].currentHP > 0)
                        {
                            sideTwoOver = false;
                        }
                    }
                }
            }

            //check whether the player has won or not (drawing does not count as a win)
            youWin = true;
            if (sideOneOver)
                youWin = false;

            return (sideOneOver || sideTwoOver);
        }

        private void doDialog()
        {
            //go through all participants
            //show dialog from any participant that wants to 'speak'
        }
        /*
        private void doWeatherEffects()
        {
            //apply weather effects to all participants
        }

        private void doStatusEffects()
        {
            //go through each participant
                //apply any status effects to the participant
                //remove any status effects that may be removed
        }
        */

        #region moves

        private void doMoves()
        {
            /*
            for (int p = 7; p >= -7; p--)
            {
                for (int i = 0; i < NumberOfPositions; i++)
                {
                    //if that participant is using a move with that priority
                    if (position[speedOrder[i]].choice.move != null &&
                        position[speedOrder[i]].choice.move.bMove.priority == p)
                    {
                        useMove(position[speedOrder[i]]);
                    }
                }               
            }
            */
        }

        private void executeStack()
        {
            BattleStack.insertPositions(Positions);
            while (BattleStack.remainingMoves > 0)
            {
                nextPosition = BattleStack.popPosition();
                if (nextPosition.pokemon.currentHealth > 0)
                    useMove(nextPosition);
                BattleStack.checkState();
            }
        }        

        private void useMove(BattlePosition position)
        {
            //show dialog message
            PokeEngineScriptHelper.ShowMessage(position.pokemon.pokemon.Nickname + " uses " + position.choice.move.bMove.name);
            //use the move
            Lua lua = new Lua();
            //put in the persistant variables
            TFSH.ScriptVariables.PutVariables(lua);
            lua["user"] = position.pokemon;
            lua["move"] = position.choice.move;
            lua["effect"] = position.choice.move.bMove.effectScript;
            lua["target"] = position.choice.target.pokemon;
            LuaRegistrationHelper.TaggedStaticMethods(lua, typeof(TFSH.PokeEngineScriptHelper));
            lua.DoString(position.choice.move.bMove.moveScript);
            //store any changes to the persistant variables
            TFSH.ScriptVariables.TakeVariables(lua);
        }

        #endregion

        private void doFlees()
        {
            //currently running is implemented to be running away from 
            //the first pokemon on the other side of the field

            //go through participants in order
            for (int i = 0; i < NumberOfPositions; i++)
            {
                //if that participant is fleeing
                if (position[i].choice.choiceType == BattleChoice.ChoiceType.RUN)
                {
                    //flee if able to (not trainer battle)
                    if (position[i].pokemon.RunFrom(position[GetFirstEnemy(position[i].index)].pokemon, position[i].runAttempts))
                    {
                        isOver = true;
                    }
                }
            }
        }

        private void doItems()
        {
            //go through participants in order

            //if that participant is using an item
            //use the item
        }
        //Utility methods
        #region utility

        /*private int[] GetSpeedOrder()
        {
            //order to return
            int[] order = new int[position.Length];
            //whether the position index has been added to the order list
            bool[] used = new bool[position.Length];

            //set all to unused
            for (int i = 0; i < position.Length; i++)
                used[i] = false;

            //loop through each order
            for (int i = 0; i < position.Length; i++)
            {
                int highestIndex = -1;
                //search positions for highest unused speed
                for (int a = 0; a < position.Length; a++)
                {
                    if (position[a].pokemon.effectiveSpeed > highestIndex &&
                        used[a] == false)
                    {
                        highestIndex = a;
                    }
                }
                //store the index
                order[i] = highestIndex;
                //mark number as used
                used[highestIndex] = true;

            }

            Random random = new Random();
            //go through to find if there are any pokemon with the same speed
            for (int i = 0; i < position.Length - 1; i++)
            {
                //if so then swap 50% of the time
                if (position[order[i]].pokemon.effectiveSpeed == position[order[i + 1]].pokemon.effectiveSpeed)
                {
                    int randomValue = random.Next(2);
                    if (randomValue == 0)
                    {
                        int temp = order[i];
                        order[i] = order[i + 1];
                        order[i + 1] = temp;
                    }
                }
            }

            return order;
        }*/

        /// <summary>
        /// Gets the next available enemy target
        /// </summary>
        /// <param name="ownIndex">the index of the position from which the query is launched (we need to know where we are before we can pick an enemy)</param>
        /// <returns></returns>
        private int GetFirstEnemy(int ownIndex)
        {
            int index = -1;
            bool found = false;
            if (ownIndex < NumberOfPositions / 2)
            {
                index = NumberOfPositions / 2;
            }
            else
            {
                index = 0; 
            }

            while (found == false)
            {
                if (position[index].pokemon != null)
                {
                    if (position[index].pokemon.currentHealth > 0)
                    {
                        found = true;
                        break;
                    }
                }
                index++;
            }

            return index;
        }

        #endregion

        public void createEffect(BattlePosition[] targets, int turnsLeft, String effect)
        {
            BattleStack.insertTriggerable(new BattleEffect(this, targets, effect, turnsLeft));
        }

        #region Outdated Code


        

        

        
        #endregion

        public BattlePosition nextPosition { get; set; }
    }
}

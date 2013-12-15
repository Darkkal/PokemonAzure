using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Moves;
using PokeEngine.Battle;
using PokeEngine.Items;

namespace PokeEngine.Pokemon
{
    public class BattleChoice
    {
        public enum ChoiceType
        {NONE,RUN,ITEM,SWITCH,MOVE}
        public ChoiceType choiceType { get; set; }        
        //TODO add item

        private ActiveMove pMove; //not null if player chooses a move
        public ActiveMove move { get { return pMove; } }

        private ActivePokemon pSwitchTo; // pokemon to switch to
        public ActivePokemon switchTo
        {
            get { return pSwitchTo; }
        }

        private Item pItem; //not null if player uses an item
        public Item item { get { return pItem; } }

        public BattlePosition target;


        public BattleChoice()
        {
            choiceType = ChoiceType.NONE;
            pMove = null;
            pSwitchTo = null;
            target = null;
            pItem = null;
        }

        /// <summary>
        /// Returns a new battlechoice containing instructions to switch to the specified pokemon
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public static BattleChoice SwitchPokemon(ActivePokemon pokemon)
        {
            BattleChoice choice = new BattleChoice();
            choice.choiceType = ChoiceType.SWITCH;
            choice.pSwitchTo = pokemon;

            return choice;
        }

        /// <summary>
        /// Returns a new battlechoice containing instructions to use the specified move on the specified target
        /// </summary>
        /// <param name="move">move to use</param>
        /// <param name="inTarget">target battleposition</param>
        /// <returns></returns>
        public static BattleChoice UseMove(ActiveMove move, BattlePosition inTarget)
        {
            BattleChoice choice = new BattleChoice();
            choice.choiceType = ChoiceType.MOVE;
            choice.target = inTarget;
            choice.pMove = move;

            return choice;
        }

        /// <summary>
        /// Returns a new battlechoice containing instructions to run from the battlefield
        /// </summary>
        /// <returns></returns>
        public static BattleChoice RunFromBattle()
        {
            BattleChoice choice = new BattleChoice();

            //choice.pRun = true;
            choice.choiceType = ChoiceType.RUN;

            return choice;
        }

        public static BattleChoice UseItem(Item item, BattlePosition inTarget)
        {
            BattleChoice choice = new BattleChoice();

            choice.choiceType = ChoiceType.ITEM;
            choice.pItem = item;
            choice.target = inTarget;

            return choice;
        }

    }
}

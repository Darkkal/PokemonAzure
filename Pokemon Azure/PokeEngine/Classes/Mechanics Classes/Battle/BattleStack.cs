using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Battle;
using PokeEngine.Moves;
using LuaInterface;

namespace PokeEngine.Classes.Mechanics_Classes.Battle
{
    class BattleStack
    {
        public static PokeBattle battle;
        public static List<BattleEffect> weather, move, item, effect;//Post-battle stacks
        public static List<BattleEffect> triggered;//Effects that require trigger conditions 
        public static List<BattlePosition> positions;
        private static BattlePosition lastPosition;
        public static int remainingMoves
        {
            get { return positions.Count; }
        }
        public static void insertPositions(BattlePosition[] bmoves)
        {
            foreach (BattlePosition b in bmoves)
            {
                positions.Add(b);//Adds and sorts positions by speed and priority.
                insertTriggerable(b);//Checks to see if the Pokémon has any triggerable abilities or items.
            }
        }

        private static void insertTriggerable(BattlePosition b)
        {
            //Searches the Pokémon for any items or abilities that require immediate insertion in the triggerable stack.
        }

        public static void initializeStacks(PokeBattle currentBattle)
        {
            battle = currentBattle;
            //Gets items and abilities that can be triggered.
            weather = new List<BattleEffect>();
            weather.OrderBy(e => e.speed);

            move = new List<BattleEffect>();
            move.OrderBy(e => e.speed);
            item = new List<BattleEffect>();
            item.OrderBy(e => e.speed);
            effect = new List<BattleEffect>();
            effect.OrderBy(e => e.speed);
            triggered = new List<BattleEffect>();
            triggered.OrderBy(e => e.speed);
            positions = new List<BattlePosition>();
            positions.OrderBy(pos => pos.PriorityIndex);
            
        }
        public static void insertTriggerable(BattleEffect e)
        {
            if (e.EType == BattleEffect.EffectType.TRIGGERABLE)
            {
                triggered.Add(e);
            }
        }
        public static BattlePosition popPosition()
        {
            positions.Reverse();
            if (positions.Count > 0)
            {
                lastPosition = positions.ElementAt(0);
                positions.RemoveAt(0);
                return lastPosition;
            }
            return null;
        }
        public static void checkState()
        {
            if(remainingMoves > 0)
            {
                foreach (BattlePosition p in battle.Positions)
                {
                    //if (p.pokemon.currentHealth == 0)
                    //Remove fainted Pokémon
                }
            }
            foreach (BattleEffect e in triggered)
            {
                executeEffect(e);
            }
            //When a Pokémon makes a move, check to see if an item or effect triggers from the move.
            //If all moves are done, go through post round cleanup.
        }

        private static void executeEffect(BattleEffect e)
        {
            //Due to the current system, it is indeterminable if PokeBattle or BattleStack should be responsible for
            //effect and move execution. This may be replaced at a later time.
        }

        internal void executePostStack()
        {
            executeStack(weather);
            executeStack(move);
            executeStack(item);
            executeStack(effect);
            throw new NotImplementedException();
        }

        private void executeStack(List<BattleEffect> battleEffect)
        {
            BattleEffect[] list = battleEffect.ToArray();
            battleEffect.Clear();
            foreach (BattleEffect e in list)
            {
                executeEffect(e);
                if (!e.remove)
                    battleEffect.Add(e);
            }

            //Executes the stack by individually executing each battleEffect.
            //Each item should be able to know when it is no longer needed by the system,   
            //either by having a non-legal or non-existant target, or expiring its countdown. 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Battle;

namespace PokeEngine.Classes.Mechanics_Classes.Battle
{
    ///<summary>
    ///This class will be used for any effects occuring during battle as the result of weather, status, or field effects.
    ///Scripts for a BattleEffect could theoretically be implemented by either a move script, or outside weather.
    ///</summary>
    class BattleEffect
    {
        public enum EffectType : byte
        {
            TRIGGERABLE = Byte.MaxValue,
            WEATHER = 3,
            MOVE = 2,
            ITEM = 1,
            EFFECT = 0
        }
        public const int DEFAULT_SPEED = 0;
        public string effectScript; //The script goes here.
        public int turnsLeft; //The number of turns left before the effect expires. Use '-1' for effect to last until end of turn.
        public int speed; //Some effects require speed.
        public PokeBattle battle; //Reference to current battle.
        public BattlePosition[] targets; //Targets of the effect.
        public bool remove; //Determines if the effect may be removed from the stack.
        public EffectType EType
        {
            get { return EType;}
            set { EType = value; }
        }
        public BattleEffect(PokeBattle battle, BattlePosition[] targets, string effectScript, int turnsLeft)
        {
            this.targets = targets;
            this.turnsLeft = turnsLeft;
            this.battle = battle;
            this.effectScript = effectScript;
        }
        public BattleEffect(PokeBattle battle, BattlePosition[] targets, string effectScript)
        {
            this.targets = targets;
            this.turnsLeft = -1;
            this.battle = battle;
            this.effectScript = effectScript;
        }
        /*
         * Example of usage in a move.
         * BaseMove poison_gas = new BaseMove("Poison Gas", "A cloud of poison gas is sprayed in the face of opposing Pokémon.", 40, 80, "Poison", "Status", 0);
         * poison_gas.effectScript = @" if target:poisoned then
         *                                  target:dealDamage(target:maxHP/8)
         *                              else
         *                                  effect:remove=true
         *                              end";
         * poison_gas.moveScript =   @" if user:hits(move, target) then
         *                                  target:poisoned = true
         *                                  effectStack:createEffect(target, -1, effect)
         *                              end";
         */
    }
}

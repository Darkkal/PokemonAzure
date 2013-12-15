using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Pokemon;
using PokeEngine.Moves;
using PokeEngine.Moves.TypeStrengths;
using PokeEngine.Screens;

namespace PokeEngine.Pokemon
{
    /// <summary>
    /// Contains all the information needed for a battle
    /// WARNING: LOTS OF VARIABLES!!!
    /// </summary>
    public class BattlePokemon
    {

        #region class_fields
        public ActivePokemon pokemon;
        public ActiveMove previousMove = null;
        public ActiveMove nextMove = null;

        //volatile and battle statuses
        public int confusedRemaining; // number of turns left till confusion wears off automatically, -1 for NA
        public bool cursed; // whether the pokemon is cursed (1/4 health removed every turn)
        public bool flinched; // whether the pokemon has flinched
        public bool identified; //evasion will be ignored if true, caused by odor sluth, foresight etc
        public bool canHitGhost; //whether normal and fighting moves can hit a ghost pokemon, irrelevant for other types
        public bool canHitDark; //whether psychic moves can hit a dark pokemon, irrelevant for other types
        public bool infatuated; //whether the pokemon is infatuated, will be unable to attack 50% of the time
        public bool leechSeeded; //whether the pokemon is leech seeded, 1/16 of health drained each turn
        public bool definiteHit; //whether next attack will definitely land, caused by mind reader, lock on etc
        public bool nightmared; //whether the pokemon has nightmare, 1/4 health removed every turn, removed when woken
        public int partialTrapRemaining; // number of turns till partial trap wears off automatically, -1 for NA
        public int perishSongRemaining; // number of turns till perish song kills pokemon, -1 for NA
        public int tauntRemaining; //number of turns till taunt wears off, -1 for NA
        public bool tormented; //whether the pokemon is tormented, may not use same move twice in a row

        public bool defenseCurled; //whether the pokemon is defense curled, rollout and iceball damage doubled
        public bool focusEnergy; //quadruples critical strike rate
        public bool minimized; //minimize doubles damage taken from Hard Roller and Stomp
        public bool misted; //whether the pokemon is misted, prevents stat changes
        public int reflectRemaining; //how many turns reflect has left, -1 for irrelevant
        public int lightScreenRemaining; //how many turns light screen has left, -1 for irrelevant
        public int substituteHealth; //the health of the pokemon's substitue, -1 for no substitute
        public bool trapped; //whether the pokemon is trapped, ie can not switch or run
        public bool safeguarded; //if true then the move that turn is ineffective
        public int badPoisonLevel; //increases by one each turn, does more damage

        public const int MAX_EFFECTIVE_SPEED = 1024;

        #region atk
        private int atk;
        public double atkModifier = 1.0;
        public int attackLevel //how many attack raises or lowers there are, max 6 up or down
        {
            get
            {
                return atk; 
            }
            set
            {
                //atk = value;
                //if (atk >= 6)
                //    atk = 6;
                //else if (atk <= -6)
                //    atk = -6;
                atk = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        public int effectiveAtk // how much attack the pokemon has after all modifiers
        {
            get { return Convert.ToInt32(Convert.ToDouble(levelCalc(pokemon.attack, attackLevel)) * atkModifier); }
        }
        #endregion

        #region def
        private int def;
        public double defModifier = 1.0;
        public int defenseLevel //how many defense raises or lowers there are, max 6 up or down
        {
            get
            {
                return def;
            }
            set
            {
                //def = value;
                //if (def >= 6)
                //    def = 6;
                //else if (def <= -6)
                //    def = -6;
                def = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        public int effectiveDef // how much defense the pokemon has after all modifiers
        {
            get { return Convert.ToInt32(Convert.ToDouble(levelCalc(pokemon.defense, defenseLevel)) * defModifier); }
        }
        #endregion

        #region sp atk
        private int spAtk;
        public double spAtkModifier = 1.0;
        public int spAtkLevel //how many spAtk raises or lowers there are, max 6 up or down
        {
            get
            {
                return spAtk;
            }
            set
            {
                //spAtk = value;
                //if (spAtk >= 6)
                //    spAtk = 6;
                //else if (spAtk <= -6)
                //    spAtk = -6;
                spAtk = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        public int effectiveSpAtk // how much Special attack the pokemon has after all modifiers
        {
            get { return Convert.ToInt32(Convert.ToDouble(levelCalc(pokemon.SPAtk, spAtkLevel)) * spAtkModifier); }
        }
        #endregion

        #region sp def
        private int spDef;
        public double spDefModifier = 1.0;
        public int spDefLevel //how many spDefense raises or lowers there are, max 6 up or down
        {
            get
            {
                return spDef;
            }
            set
            {
                //spDef = value;
                //if (spDef >= 6)
                //    spDef = 6;
                //else if (spDef <= -6)
                //    spDef = -6;
                spDef = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        public int effectiveSpDef // how much Special defense the pokemon has after all modifiers
        {
            get { return Convert.ToInt32(Convert.ToDouble(levelCalc(pokemon.SPDef, spDefLevel)) * spDefModifier); }
        }
        #endregion

        #region speed
        private int speed;
        public double speedModifier = 1.0;
        public int speedLevel //how many speed raises or lowers there are, max 6 up or down
        {
            get
            {
                return speed;
            }
            set
            {
                //speed = value;
                //if (speed >= 6)
                //    speed = 6;
                //else if (speed <= -6)
                //    speed = -6;
                speed = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        public int effectiveSpeed // how much speed the pokemon has after all modifiers
        {
            //TODO Modify the function to include the effects of Quick Claw, Lagging Tail, etc, using MAX_EFFECTIVE_SPEED
            //TODO For Trick Room Calculations, make speedModifier negative.
            //SUGGESTION: Trick Room calcuations done externally inside battle engine.
            get 
            { 
                return Convert.ToInt32(Convert.ToDouble(levelCalc(pokemon.Speed, speedLevel)) * speedModifier); 
            }
        }
        #endregion

        #region evasion
        private int evasion;
        public double evasionModifier = 1.0;
        public int evasionLevel //how many evasion raises or lowers there are, max 6 up or down
        {
            get
            {
                return evasion;
            }
            set
            {
                //evasion = value;
                //if (evasion >= 6)
                //    evasion = 6;
                //else if (evasion <= -6)
                //    evasion = -6;
                evasion = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        #endregion

        #region accuracy
        private int accuracy;
        public double accuracyModifier = 1.0;
        public int accuracyLevel //how many accuracy raises or lowers there are, max 6 up or down
        {
            get
            {
                return accuracy;
            }
            set
            {
                //accuracy = value;
                //if (accuracy >= 6)
                //    accuracy = 6;
                //else if (accuracy <= -6)
                //    accuracy = -6;
                accuracy = value > 0 ? Math.Min(value, 6) : Math.Max(value, -6);
            }
        }
        #endregion

        public int currentHealth
        {
            get { return pokemon.currentHP; }

            set
            {
                pokemon.currentHP = value;
                if (pokemon.currentHP > pokemon.HP)
                    pokemon.currentHP = pokemon.HP;
                else if (pokemon.currentHP < 0)
                    pokemon.currentHP = 0;
            }
 
        } //get or set the current health of the pokemon
        public int maxHealth
        {
            get { return pokemon.HP; }
        } // get the max health of the pokemon

        public ActiveMove[] move
        {
            get { return pokemon.move; }
            set { pokemon.move = value; }
        }
        #endregion

        /// <summary>
        /// Constructor which makes a battle pokemon from an active pokemon
        /// These are default settings
        /// </summary>
        /// <param name="inPoke">Active Pokemon</param>
        public BattlePokemon(ActivePokemon inPoke)
        {
            pokemon = inPoke;

            confusedRemaining = -1;
            cursed = false;
            flinched = false;
            identified = false;
            canHitGhost = false;
            canHitDark = false;
            infatuated = false;
            leechSeeded = false;
            definiteHit = false;
            nightmared = false;
            partialTrapRemaining = -1;
            perishSongRemaining = -1;
            tauntRemaining = -1;
            tormented = false;

            defenseCurled = false;
            focusEnergy = false;
            minimized = false;
            misted = false;
            reflectRemaining = -1;
            lightScreenRemaining = -1;
            substituteHealth = -1;
            trapped = false;
            safeguarded = false;
            badPoisonLevel = -1;

            attackLevel = 0;
            defenseLevel = 0;
            spAtkLevel = 0;
            spDefLevel = 0;
            speedLevel = 0;
            evasionLevel = 0;
            accuracyLevel = 0;
            
        }

        #region inflict_status
        public void burn()
        {
            if(pokemon.status == MajorStatus.None)
            {
                //half attack stat
                atkModifier = atkModifier / 2;
                pokemon.status = MajorStatus.Burned;
            }
        }

        public void freeze()
        {
            if (pokemon.status == MajorStatus.None)
            {
                pokemon.status = MajorStatus.Frozen;
            }
        }

        public void paralyze()
        {
            if (pokemon.status == MajorStatus.None)
            {
                pokemon.status = MajorStatus.Paralyzed;
            }
        }

        public void poison()
        {
            if (pokemon.status == MajorStatus.None)
            {
                pokemon.status = MajorStatus.Poisoned;
            }
        }

        public void badPoison()
        {
            if (pokemon.status == MajorStatus.None)
            {
                pokemon.status = MajorStatus.BadPoisoned;
                badPoisonLevel = 1;
            }
        }

        public void sleep()
        {
            if (pokemon.status == MajorStatus.None)
            {
                pokemon.status = MajorStatus.Sleep;
            }
        }

        /// <summary>
        /// confuses for 4 turns, may wear off earlier
        /// </summary>
        public void confuse()
        {
            confusedRemaining = 4;
        }

        public void curse()
        {
            cursed = true;
        }

        public void flinch()
        {
            //should wear off at start of next turn
            flinched = true;
        }

        public void identify()
        {
            identified = true;
        }

        public void hitGhost()
        {
            canHitGhost = true;
        }

        public void hitDark()
        {
            canHitDark = true;
        }

        public void infatuate()
        {
            infatuated = true;
        }

        public void leech()
        {
            leechSeeded = true;
        }

        public void lockOn()
        {
            definiteHit = true;
        }

        public void partiallyTrap()
        {
            Random random = new Random();
            int val = random.Next(4 - 5);
            partialTrapRemaining = val;
        }

        public void partiallyTrap(bool hasExtendingItem)
        {
            if (hasExtendingItem)
            {
                partialTrapRemaining = 5;
            }
        }

        public void perish()
        {
            perishSongRemaining = 3;
        }

        public void taunt()
        {
            Random random = new Random();
            int val = random.Next(2 - 4);
            tauntRemaining = val;
        }

        public void torment()
        {
            tormented = true;
        }

        #endregion //inflict various volatile and nonvolatile status effects

        /// <summary>
        /// returns a double representing the chance to hit this pokemon
        /// Over 1 means a sure hit, less than one represents a percentage chance to hit
        /// </summary>
        /// <param name="inPoke">pokemon using the move</param>
        /// <param name="inMove">move being used</param>
        /// <returns></returns>
        public double chanceToHit(BattlePokemon inPoke, BaseMove inMove)
        {
            double chance = 0.0;
            double acc = 0.0;
            double eva = 0.0;
            //convert evasion and accuracy levels into actual values
            if (accuracyLevel >= 0)
            {
                acc = (Convert.ToDouble(inPoke.accuracyLevel) + 3.0) / 3.0;
                acc *= inPoke.accuracyModifier;
            }
            if (accuracyLevel < 0)
            {
                acc = 3.0 / (3.0 + Convert.ToDouble(inPoke.accuracyLevel));
                acc *= inPoke.accuracyModifier;
            }
            if (evasionLevel >= 0)
            {
                eva = (Convert.ToDouble(evasionLevel) + 3.0) / 3.0;
                eva *= evasionModifier;
            }
            if (evasionLevel < 0)
            {
                eva = 3.0 / (3.0 + Convert.ToDouble(evasionLevel));
                eva *= evasionModifier;
            }

            //if the accuracy is set to -1 it will always hit
            if (inMove.accuracy == -1)
            {
                chance = 1.0;
            }
            else
            {
                chance = (Convert.ToDouble(inMove.accuracy) / 100) * (acc / eva);
            }
            return chance;
        }
        
        /// <summary>
        /// Whether this pokemon hits the target pokemon
        /// </summary>
        /// <param name="attack">attack used</param>
        /// <param name="target">target pokemon</param>
        /// <returns></returns>
        public bool hits( ActiveMove attack, BattlePokemon target)
        {
            bool doesHit = false;

            double chance = target.chanceToHit(this, attack.bMove);
            if(chance < 1.0 && chance > 0.0)
            {
                Random random = new Random();
                double roll = random.NextDouble();
                if (roll < chance)
                {
                    doesHit = true;
                }
                else
                {
                    DialogBox.newMessage(this.pokemon.Nickname + " misses " + target.pokemon.Nickname);
                    doesHit = false;
                }
            }
            //chance greater than 1 will always hit
            else if (chance >= 1.0)
            {
                doesHit = true;
            }
            //chance less than 0 will never hit
            else
            {
                DialogBox.newMessage(this.pokemon.Nickname + " misses " + target.pokemon.Nickname);
                doesHit = false;
            }

            return doesHit;
        }

        /// <summary>
        /// does damage to the target based on stats and attack power and type and also STAB
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="target"></param>
        public void doDamageTo( ActiveMove attack, BattlePokemon target)
        {
            double dDamage = 0.0;
            int damage = 0;
            double strength = typeModifier(attack, target);
            double stab = 1.0;
            
            //calculate Same Type Attack Bonus
            if (attack.bMove.moveType == pokemon.baseStat.Type1.ToString() || attack.bMove.moveType == pokemon.baseStat.Type2.ToString())
            {
                stab = 1.5;
            }

            if (strength > 0.0)
            {
                Random random = new Random();
                double modifier = Convert.ToDouble(random.Next(85, 100)) / 100.0;
                if(attack.bMove.moveKind == "Special")
                {
                    dDamage = stab * strength * modifier * (((2.0 * pokemon.level + 10.0) / 250.0) * (Convert.ToDouble(effectiveSpAtk) / Convert.ToDouble(target.effectiveSpDef)) * attack.bMove.power + 2);
                    damage = Convert.ToInt32(Math.Floor(dDamage));
                    target.currentHealth -= damage;
                }
                else if(attack.bMove.moveKind == "Physical")
                {
                    dDamage = stab * strength * modifier * (((2.0 * pokemon.level + 10.0) / 250.0) * (Convert.ToDouble(effectiveAtk) / Convert.ToDouble(target.effectiveDef)) * attack.bMove.power + 2);
                    damage = Convert.ToInt32(Math.Floor(dDamage));
                    target.currentHealth -= damage;
                }

                //show message saying it hit, or was supereffective
            }
            else
            {
                //show message saying it was ineffective
            }
        }

        /// <summary>
        /// Does damage to the target based on stats and attack power and hidden move type and also STAB
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="target"></param>
        public void doHiddenPowerDamageTo(ActiveMove attack, BattlePokemon target)
        {
            double dDamage = 0.0;
            int damage = 0;
            //only this part changes for hidden power
            double strength = TypeStrengths.typeStrength(pokemon.hiddenPowerType, target);
            double stab = 1.0;

            //calculate Same Type Attack Bonus
            if (attack.bMove.moveType == pokemon.baseStat.Type1.ToString() || attack.bMove.moveType == pokemon.baseStat.Type2.ToString())
            {
                stab = 1.5;
            }

            if (strength > 0.0)
            {
                Random random = new Random();
                double modifier = Convert.ToDouble(random.Next(85, 100)) / 100.0;
                if (attack.bMove.moveKind == "Special")
                {
                    dDamage = stab * strength * modifier * (((2.0 * pokemon.level + 10.0) / 250.0) * (Convert.ToDouble(effectiveSpAtk) / Convert.ToDouble(target.effectiveSpDef)) * pokemon.hiddenPowerPower + 2);
                    damage = Convert.ToInt32(Math.Floor(dDamage));
                    target.currentHealth -= damage;
                }
                //I know that hidden power is only special, but screw you!
                else if (attack.bMove.moveKind == "Physical")
                {
                    dDamage = stab * strength * modifier * (((2.0 * pokemon.level + 10.0) / 250.0) * (Convert.ToDouble(effectiveAtk) / Convert.ToDouble(target.effectiveDef)) * pokemon.hiddenPowerPower + 2);
                    damage = Convert.ToInt32(Math.Floor(dDamage));
                    target.currentHealth -= damage;
                }

                //show message saying it hit, or was supereffective
            }
            else
            {
                //show message saying it was ineffective
            }
        }

        /// <summary>
        /// do a set amount of damage to a target
        /// </summary>
        /// <param name="amount">amount of damage to be done</param>
        /// <param name="target">target pokemon</param>
        public void doSetDamage(int amount, BattlePokemon target)
        {
            target.currentHealth -= amount;

            //show message saying it hit
        }

        public void changeStat(string stat, int amount)
        {
            switch (stat)
            {
                case "Attack":
                    attackLevel += amount; break;
                case "Defense":
                    defenseLevel += amount; break;
                case "Sp Atk":
                    spAtkLevel += amount; break;
                case "Sp Def":
                    spDefLevel += amount; break;
                case "Speed":
                    speedLevel += amount; break;
                case "Evasion":
                    evasionLevel += amount; break;
                case "Accuracy":
                    accuracyLevel += amount; break;
                default:
                    DialogBox.newMessage("\"" + stat + "\" is not a valid input for changeStat(string, int)"); break; 
            }
        }

        /// <summary>
        /// Inflicts a major status effect on the target pokemon
        /// </summary>
        /// <param name="status">String containing the status to inflict</param>
        /// <param name="target">target pokemon</param>
        public void inflictMajorStatus(String status, BattlePokemon target)
        {
            MajorStatus stat;
            switch (status)
            {
                case "Poison":
                    target.poison();
                    break;
                case "Bad Poison":
                    target.badPoison();
                    break;
                case "Paralyze":
                    target.paralyze();
                    break;
                case "Paralyse":
                    target.paralyze();
                    break;
                case "Sleep":
                    target.sleep();
                    break;
                case "Burn":
                    target.burn();
                    break;
                case "Freeze":
                    target.freeze();
                    break;
                default:
                    break;
            }
        }

        public void heal(int amount)
        {
            currentHealth += amount;
        }

        private double typeModifier( ActiveMove attack, BattlePokemon target)
        {
            return TypeStrengths.typeStrength(attack, target);         
        }

        /// <summary>
        /// Calculates whether a pokemon is successfully able to run from an opponent
        /// </summary>
        /// <param name="target">pokemon we are running from</param>
        /// <param name="C">The number of times a run has been attempted</param>
        /// <returns>Whether the pokemon successfully runs</returns>
        public bool RunFrom(BattlePokemon target, int C)
        {
            bool runs = false;
            
            int A = effectiveSpeed;
            int B = (target.effectiveSpeed / 4) % 256;

            int F = (A * 32) / B + 30 * C;

            if (F > 255)
            {
                runs = true;
            }
            else
            {
                Random random = new Random();
                int randomNum = random.Next(256); //generate a number between 0 and 256

                if (randomNum < F)
                    runs = true;
            }

            return runs;
        }


        /// <summary>
        /// returns percentage that stat is affected by stat increases and decreases
        /// </summary>
        /// <param name="inStat"></param>
        /// <param name="inLevel"></param>
        /// <returns></returns>
        private int levelCalc(int inStat, int inLevel)
        {
            double temp = 1.0;
            int val = 0;

            if (inLevel >= 0)
            {
                temp = Math.Floor((2.0 + Convert.ToDouble(inLevel)) / 2.0);
                val = Convert.ToInt32(Convert.ToDouble(inStat) * temp);
            }
            else
            {
                temp = Math.Floor(2.0 / (2.0 + Convert.ToDouble(-1 * inLevel)));
                val = Convert.ToInt32(Convert.ToDouble(inStat) * temp);
            }

            return val;
        }
    }
}

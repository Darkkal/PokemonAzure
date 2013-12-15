using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Pokemon;

namespace PokeEngine.Pokemon
{
    public enum Type { Normal, Fire, Poison, Fighting, Water, Electric, Flying, Grass, Ground, Psychic, Rock, Ice, Bug, Dragon, Ghost, Dark, Steel, Blank };
    public enum EXPType { Erratic, Fast, MedSlow, MedFast, Slow, Fluctuating };
    
    /// <summary>
    /// Holds all the information about the base pokemon stats.
    /// </summary>
    
    public class BasePokemon
    {
        //TODO add pokemon abilities
        //TODO add egg groups
        //TODO add possible moves
        //TODO add height and weight?
        //TODO add pokemon evolution - evolution class added.

        public int baseHP;
        public int baseAttack;
        public int baseDefense;
        public int baseSPAttack;
        public int baseSPDefense;
        public int baseSpeed;

        public Type baseTypeOne;
        public Type baseTypeTwo;    

        public int PDexNo;
        public String PDexEntry;
        public String Name;

        public EXPType EXP;

        public String evolutionScript; //lua script for handling evolutions

        //information about the sprites here?

        //information about moves goes here?

        /// <summary>
        /// Creates a new pokemon object with two types
        /// </summary>
        /// <param name="Name">Name of Pokemon</param>
        /// <param name="TypeOne">Primary type of the Pokemon as a String</param>
        /// <param name="TypeTwo">Secondary type of the Pokemon as a String</param>
        /// <param name="HP">Base HP of Pokemon</param>
        /// <param name="Attack">Base Attack of Pokemon</param>
        /// <param name="Defense">Base Defense of Poekmon</param>
        /// <param name="SPAttack">Base SPAtk of Pokemon</param>
        /// <param name="SPDefense">Base SPDef of Pokemon</param>
        /// <param name="Speed">Base Speed of Pokemon</param>
        /// <param name="PDexNo">National Pokedex Number of Pokemon</param>
        /// <param name="PDexEntry">Pokedex Entry of Pokemon</param>
        /// <param name="EXPT">Type of Experience growth - Erratic, Fast, Slow, MedFast, MedSlow, Fluctuating</param>
        public BasePokemon(String Name, String TypeOne, String TypeTwo , int HP, int Attack, int Defense, int SPAttack, int SPDefense, int Speed, int PDexNo, String PDexEntry, String EXPT)
        {
            this.Name = Name;
            this.baseHP = HP;
            this.baseAttack = Attack;
            this.baseDefense = Defense;
            this.baseSPAttack = SPAttack;
            this.baseSPDefense = SPDefense;
            this.baseSpeed = Speed;
            this.PDexEntry = PDexEntry;
            this.PDexNo = PDexNo;

            this.baseTypeOne = convertToType(TypeOne);
            this.baseTypeTwo = convertToType(TypeTwo);

            this.EXP = convertToExp(EXPT);


        }

        /// <summary>
        /// Creates a new pokemon object with one type
        /// </summary>
        /// <param name="Name">Name of Pokemon</param>
        /// <param name="TypeOne">Primary type of the Pokemon as a String</param>
        /// <param name="HP">Base HP of Pokemon</param>
        /// <param name="Attack">Base Attack of Pokemon</param>
        /// <param name="Defense">Base Defense of Poekmon</param>
        /// <param name="SPAttack">Base SPAtk of Pokemon</param>
        /// <param name="SPDefense">Base SPDef of Pokemon</param>
        /// <param name="Speed">Base Speed of Pokemon</param>
        /// <param name="PDexNo">National Pokedex Number of Pokemon</param>
        /// <param name="PDexEntry">Pokedex Entry of Pokemon</param>
        /// <param name="EXPT">Type of Experience growth - Erratic, Fast, Slow, MedFast, MedSlow, Fluctuating</param>
        public BasePokemon(String Name, String TypeOne, int HP, int Attack, int Defense, int SPAttack, int SPDefense, int Speed, int PDexNo, String PDexEntry, String EXPT)
        {
            this.Name = Name;
            this.baseHP = HP;
            this.baseAttack = Attack;
            this.baseDefense = Defense;
            this.baseSPAttack = SPAttack;
            this.baseSpeed = Speed;
            this.PDexEntry = PDexEntry;
            this.PDexNo = PDexNo;

            this.baseTypeOne = convertToType(TypeOne);
            this.baseTypeTwo = Type.Blank;

            this.EXP = convertToExp(EXPT);
        }

        /// <summary>
        /// Converts a string into a type
        /// </summary>
        /// <param name="val"></param>
        private Type convertToType(String val)
        {
            //Type ret = Type.Blank;

            try
            {
                return (Type)Enum.Parse(typeof(Type), val);
            }
            catch (Exception)
            {
                return Type.Blank;
            }
            
            //switch (val)
            //{
            //    case "Normal":
            //        ret = Type.Normal;
            //        break;
            //    case  "Fire":
            //        ret = Type.Fire;
            //        break;
            //    case "Poison":
            //        ret = Type.Poison;
            //        break;
            //    case "Fighting":
            //        ret = Type.Fighting;
            //        break;
            //    case "Water":
            //        ret = Type.Water;
            //        break;
            //    case "Electric":
            //        ret = Type.Electric;
            //        break;
            //    case "Flying":
            //        ret = Type.Flying;
            //        break;
            //    case "Grass":
            //        ret = Type.Grass;
            //        break;
            //    case "Ground":
            //        ret = Type.Ground;
            //        break;
            //    case "Psychic":
            //        ret = Type.Psychic;
            //        break;
            //    case "Rock":
            //        ret = Type.Rock;
            //        break;
            //    case "Ice":
            //        ret = Type.Ice;
            //        break;
            //    case "Bug":
            //        ret = Type.Bug;
            //        break;
            //    case "Dragon":
            //        ret = Type.Dragon;
            //        break;
            //    case "Ghost":
            //        ret = Type.Ghost;
            //        break;
            //    case "Dark":
            //        ret = Type.Dark;
            //        break;
            //    case "Steel":
            //        ret = Type.Steel;
            //        break;
            //    default:
            //        ret = Type.Blank;
            //        break;
            //}
            //return ret;

        }

        /// <summary>
        /// Converts a string into an Experience type
        /// the types are Erratic, Fast, MedSlow, MedFast, Slow, Fluctuating
        /// See the internet for explanations
        /// </summary>
        /// <param name="EXPT">string containing type of experience you want</param>
        /// <returns>EXPType enumeration</returns>
        private EXPType convertToExp(String EXPT)
        {
            //EXPType ret = EXPType.Slow;
            try
            {
                return (EXPType)Enum.Parse(typeof(EXPType), EXPT);
            }
            catch (Exception)
            {

                return EXPType.Slow;
            }
            
            //switch (EXPT)
            //{
            //    case "Erratic":
            //        ret = EXPType.Erratic;
            //        break;
            //    case "Fast":
            //        ret = EXPType.Fast;
            //        break;
            //    case "MedFast":
            //        ret = EXPType.MedFast;
            //        break;
            //    case "MedSlow":
            //        ret = EXPType.MedSlow;
            //        break;
            //    case "Slow":
            //        ret = EXPType.Slow;
            //        break;
            //    case "Fluctuating":
            //        ret = EXPType.Fluctuating;
            //        break;
            //    default:
            //        break;
            //}

            //return ret;
        }
        
    }
}

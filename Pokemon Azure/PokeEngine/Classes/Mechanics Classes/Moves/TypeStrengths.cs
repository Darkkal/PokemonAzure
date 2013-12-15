using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Pokemon;

namespace PokeEngine.Moves.TypeStrengths
{
    public class TypeStrengths
    {

        #region overloads
        public static double typeStrength(String atkType, BattlePokemon inPoke)
        {
            return typeStrength(atkType, inPoke.pokemon.baseStat.Type1.ToString(), inPoke.pokemon.baseStat.Type2.ToString());
        }

        public static double typeStrength(String atkType, ActivePokemon inPoke)
        {
            return typeStrength(atkType, inPoke.baseStat.Type1.ToString(), inPoke.baseStat.Type2.ToString());
        }

        public static double typeStrength(BaseMove inMove, BattlePokemon inPoke)
        {
            return typeStrength(inMove.moveType, inPoke.pokemon.baseStat.Type1.ToString(), inPoke.pokemon.baseStat.Type2.ToString());
        }

        public static double typeStrength(BaseMove inMove, ActivePokemon inPoke)
        {
            return typeStrength(inMove.moveType, inPoke.baseStat.Type1.ToString(), inPoke.baseStat.Type2.ToString());
        }

        public static double typeStrength(ActiveMove inMove, BattlePokemon inPoke)
        {
            return typeStrength(inMove.bMove.moveType, inPoke.pokemon.baseStat.Type1.ToString(), inPoke.pokemon.baseStat.Type2.ToString());
        }

        public static double typeStrength(ActiveMove inMove, ActivePokemon inPoke)
        {
            return typeStrength(inMove.bMove.moveType, inPoke.baseStat.Type1.ToString(), inPoke.baseStat.Type2.ToString());
        }
        #endregion

        public static double typeStrength(String atkType, String pokeTypeOne, String pokeTypeTwo)
        {
            double strength = 1.0;
            double[] normal = new double[18] { 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 0.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
            double[] fighting = new double[18] { 2.0, 1.0, 0.5, 0.5, 1.0, 2.0, 0.5, 0.0, 2.0, 1.0, 1.0, 1.0, 1.0, 0.5, 2.0, 1.0, 2.0, 1.0 };
            double[] flying = new double[18] { 1.0, 2.0, 1.0, 1.0, 1.0, 0.5, 2.0, 1.0, 0.5, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0 };
            double[] poison = new double[18] { 1.0, 1.0, 1.0, 0.5, 0.5, 0.5, 1.0, 0.5, 0.0, 1.0, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
            double[] ground = new double[18] { 1.0, 1.0, 0.0, 2.0, 1.0, 2.0, 0.5, 1.0, 2.0, 2.0, 1.0, 0.5, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
            double[] rock = new double[18] { 1.0, 0.5, 2.0, 1.0, 0.5, 1.0, 2.0, 1.0, 0.5, 2.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 1.0 };
            double[] bug = new double[18] { 1.0, 0.5, 0.5, 0.5, 1.0, 1.0, 1.0, 0.5, 0.5, 0.5, 1.0, 2.0, 1.0, 2.0, 1.0, 1.0, 2.0, 1.0 };
            double[] ghost = new double[18] { 0.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 0.5, 1.0 };
            double[] steel = new double[18] { 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 0.5, 0.5, 0.5, 1.0, 0.5, 1.0, 2.0, 1.0, 1.0, 1.0 };
            double[] fire = new double[18] { 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 2.0, 1.0, 2.0, 0.5, 0.5, 2.0, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0 };
            double[] water = new double[18] { 1.0, 1.0, 1.0, 1.0, 2.0, 2.0, 1.0, 1.0, 1.0, 2.0, 0.5, 0.5, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0 };
            double[] grass = new double[18] { 1.0, 1.0, 0.5, 0.5, 2.0, 2.0, 0.5, 1.0, 0.5, 0.5, 2.0, 0.5, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0 };
            double[] electric = new double[18] { 1.0, 1.0, 2.0, 1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 0.5, 1.0, 1.0, 0.5, 1.0, 1.0 };
            double[] psychic = new double[18] { 1.0, 2.0, 1.0, 2.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0, 0.0, 1.0 };
            double[] ice = new double[18] { 1.0, 1.0, 2.0, 1.0, 2.0, 1.0, 1.0, 1.0, 0.5, 0.5, 0.5, 2.0, 1.0, 1.0, 0.5, 2.0, 1.0, 1.0 };
            double[] dragon = new double[18] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0 };
            double[] dark = new double[18] { 1.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 2.0, 0.5, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 1.0, 0.5, 1.0 };
            double[] none = new double[18] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            int t1 = 18;
            int t2 = 18;
            t1 = getType(pokeTypeOne);
            t2 = getType(pokeTypeTwo);

            switch (atkType)
            {
                case "Normal":
                    strength = normal[t1] * normal[t2];
                    break;
                case "Fighting":
                    strength = fighting[t1] * fighting[t2];
                    break;
                case "Flying":
                    strength = flying[t1] * flying[t2];
                    break;
                case "Poison":
                    strength = poison[t1] * poison[t2];
                    break;
                case "Ground":
                    strength = ground[t1] * ground[t2];
                    break;
                case "Rock":
                    strength = rock[t1] * rock[t2];
                    break;
                case "Bug":
                    strength = bug[t1] * bug[t2];
                    break;
                case "Ghost":
                    strength = ghost[t1] * ghost[t2];
                    break;
                case "Steel":
                    strength = steel[t1] * steel[t2];
                    break;
                case "Fire":
                    strength = fire[t1] * fire[t2];
                    break;
                case "Water":
                    strength = water[t1] * water[t2];
                    break;
                case "Grass":
                    strength = grass[t1] * grass[t2];
                    break;
                case "Electric":
                    strength = electric[t1] * electric[t2];
                    break;
                case "Psychic":
                    strength = psychic[t1] * psychic[t2];
                    break;
                case "Ice":
                    strength = ice[t1] * ice[t2];
                    break;
                case "Dragon":
                    strength = dragon[t1] * dragon[t2];
                    break;
                case "Dark":
                    strength = dark[t1] * dark[t2];
                    break;
                case "None":
                    strength = none[t1] * none[t2];
                    break;
                default:
                    break;
            }

            return strength;
        }

        private static int getType(String word)
        {
            int type = 17;

            switch (word)
            {
                case "Normal":
                    type = 0;
                    break;
                case "Fighting":
                    type = 1;
                    break;
                case "Flying":
                    type = 2;
                    break;
                case "Poison":
                    type = 3;
                    break;
                case "Ground":
                    type = 4;
                    break;
                case "Rock":
                    type = 5;
                    break;
                case "Bug":
                    type = 6;
                    break;
                case "Ghost":
                    type = 7;
                    break;
                case "Steel":
                    type = 8;
                    break;
                case "Fire":
                    type = 9;
                    break;
                case "Water":
                    type = 10;
                    break;
                case "Grass":
                    type = 11;
                    break;
                case "Electric":
                    type = 12;
                    break;
                case "Psychic":
                    type = 13;
                    break;
                case "Ice":
                    type = 14;
                    break;
                case "Dragon":
                    type = 15;
                    break;
                case "Dark":
                    type = 16;
                    break;
                case "None":
                    type = 17;
                    break;
                default:
                    type = 17;
                        break;
            }

            return type;
        }
    }
}

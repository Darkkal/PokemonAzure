using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokeEngine.Pokemon
{
    /// <summary>
    /// This is a list which holds every pokemon
    /// They can be added and removed etc
    /// </summary>
    
    static class PokemonList
    {
        public static SortedList<int, BasePokemon> pokemon = new SortedList<int,BasePokemon>(); //links dex number to pokemon
        public static SortedList<String, int> names = new SortedList<String,int>();    //links name to dex number



        public static int numberOfPokemon
        {
            get { return pokemon.Count; }
        }

        /// <summary>
        /// Adds the specified base pokemon to the pokemon list
        /// NOTE: Will overwrite any pokemon with the same name
        /// </summary>
        /// <param name="newMove">instance of base pokemon</param>
        public static void addPokemon(BasePokemon newPokemon)
        {

            try
            {
                pokemon.Add(newPokemon.PDexNo, newPokemon);
                names.Add(newPokemon.Name, newPokemon.PDexNo);
            }
            catch (ArgumentException)
            {
                pokemon.Remove(newPokemon.PDexNo);
                names.Remove(newPokemon.Name);
                pokemon.Add(newPokemon.PDexNo, newPokemon);
                names.Add(newPokemon.Name, newPokemon.PDexNo);
            }
        }

        /// <summary>
        /// returns a BasePokemon object from the list with the given name
        /// </summary>
        /// <param name="moveName">integer of pokedex you wish to find</param>
        /// <returns>BasePokemon OR null if unsuccessful</returns>
        public static BasePokemon getPokemon(int pokeNum)
        {
            BasePokemon temp = null;
            if (pokemon.ContainsKey(pokeNum))
            {
                temp = pokemon[pokeNum];
            }
            return temp;
        }

        public static BasePokemon getPokemon(String pokeName)
        {
            BasePokemon temp = null;

            try
            {
                temp = pokemon[names[pokeName]];
            }
            catch (KeyNotFoundException)
            {
                temp = null;
            }

            return temp;
        }

        /// <summary>
        /// Removes a pokemon with the specified number
        /// </summary>
        /// <param name="moveName">int of pokedex number</param>
        public static void removePokemon(int pokeNum)
        {
            if (pokemon.ContainsKey(pokeNum))
            {
                names.Remove(pokemon[pokeNum].Name);
                pokemon.Remove(pokeNum);
            }
        }

        /// <summary>
        /// Removes a pokemon with the specified name
        /// </summary>
        /// <param name="moveName">Pokemon Name you wish to remove</param>
        public static void removePokemon(String pokeName)
        {
            try
            {
                pokemon.Remove(names[pokeName]);
                names.Remove(pokeName);
            }
            catch (KeyNotFoundException)
            { }
        }

        /// <summary>
        /// Gets the pokemon name for the given pokedex number
        /// </summary>
        /// <param name="dexNo"></param>
        public static String getName(int dexNo)
        {
            return pokemon[dexNo].Name;

        }

        public static int getDexNo(String name)
        {
            return names[name];
        }
    }
}

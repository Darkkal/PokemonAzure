using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PokeEngine.Classes.Screens;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Screens;

namespace PokeEngine.Trainers
{
    
    public class Player : Trainer
    {

        //TODO add some way to hold badges
        //TODO add more complex inventory
        //TODO maybe story variables?
        //TODO add boxxed pokemon
        
        public int secretID;
        public int secretIDTwo;
        public String pDexType; //for national and regional pokedexes if you wanna use them
        public Texture2D textureSheet;
        public byte Badges;
        public bool[] IdentifiedPokemon = new bool[649];

        public Player() : base()
        {
            Random random = new Random();
            //TODO replace this random stuff
            secretID = Math.Abs(random.Next());
            secretIDTwo = Math.Abs(random.Next());
            pDexType = "Regional";
            tileCoords = Point.Zero;
            nextTile = tileCoords;
            Badges = 0;
            for (int i = 0; i < IdentifiedPokemon.Length; i++)
                IdentifiedPokemon[i] = false;
        }

        //moved the movement shit to npc to compensate for animation

    }

}

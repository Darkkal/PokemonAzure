using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Pokemon;
using PokeEngine.Items;
using System.IO;
using PokeEngine.Tools;

namespace PokeEngine.Trainers
{
    /// <summary>
    /// 
    /// </summary>
    
    public class InventoryItem
    {
        public int itemID;
        public int quantity;
    }

    
    public class Trainer : NPC
    {

        public ActivePokemon[] currentPokemon;
        public int money;
        public List<InventoryItem> inventory;
        public int trainerID;

        public int numCurrentPokemon
        {
            get
            {
                int count = 0;
                foreach (ActivePokemon a in currentPokemon)
                {
                    if (a != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        } //number of pokemon the trainer currently has

        /// <summary>
        /// Default constructor
        /// </summary>
        public Trainer()
        {
            name = "DURP";
            money = 3000;
            isMale = true;
            Random random = new Random();
            trainerID = Math.Abs(random.Next());
            currentPokemon = new ActivePokemon[6];
            inventory = new List<InventoryItem>();
            sightRange = 0;  
        }

        public Trainer(Trainer inTrainer)
        {
            //mwahahaha, using memory streams rather than having to make more copy contructors
            //pretty clever huh?
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                
                SaveLoad.SaveNPC(inTrainer, writer);
                
                stream.Position = 0;
                BinaryReader reader = new BinaryReader(stream);

                Trainer temp = (Trainer)SaveLoad.LoadNPC(reader);
                inventory = temp.inventory;
                currentPokemon = temp.currentPokemon;

                stream.Dispose();
                writer.Dispose();
                reader.Dispose();
            }

            money = inTrainer.money;
            name = inTrainer.name;
            isMale = inTrainer.isMale;
            zoneLocation = inTrainer.zoneLocation;
            tileCoords = new Point(inTrainer.tileCoords.X, inTrainer.tileCoords.Y);
            facing = inTrainer.facing;
            if (inTrainer.spritePosition != null)
                spritePosition = new Rectangle(inTrainer.spritePosition.X,
                                           inTrainer.spritePosition.Y,
                                           inTrainer.spritePosition.Width,
                                           inTrainer.spritePosition.Height);
            if (inTrainer.spritesheetSize != null)
                spritesheetSize = new Rectangle(inTrainer.spritesheetSize.X,
                                           inTrainer.spritesheetSize.Y,
                                           inTrainer.spritesheetSize.Width,
                                           inTrainer.spritesheetSize.Height);
            spriteSheet = inTrainer.spriteSheet;
            sightRange = inTrainer.sightRange;
            nextTile = inTrainer.nextTile;
            currentZ = inTrainer.currentZ;
            speed = inTrainer.speed;
            movementIndex = inTrainer.movementIndex;
            isMoving = inTrainer.isMoving;
            directionMoving = inTrainer.directionMoving;
            standCoolDown = inTrainer.standCoolDown;
            standCoolMax = inTrainer.standCoolMax;
            animationFrame = inTrainer.animationFrame;
            leftFoot = inTrainer.leftFoot;
            isTurning = inTrainer.isTurning;
            turningSpeed = inTrainer.turningSpeed;
            movement = inTrainer.movement;
            actionIndex = inTrainer.actionIndex;
            if (inTrainer.actions != null)
                actions = new List<Action>(inTrainer.actions);
            if (inTrainer.wanderArea != null)
                wanderArea = new Rectangle(inTrainer.wanderArea.X,
                                       inTrainer.wanderArea.Y,
                                       inTrainer.wanderArea.Width,
                                       inTrainer.wanderArea.Height);


        }

        /// <summary>
        /// adds a pokemon to the player's current pokemon
        /// </summary>
        /// <param name="inPoke">pokemon you want to add</param>
        public void addPokemon(ActivePokemon inPoke)
        {
            if (numCurrentPokemon < 6)
            {
                int i = 0;
                bool done = false;
                while (done == false)
                {
                    if (currentPokemon[i] == null)
                    {
                        done = true;
                        currentPokemon[i] = inPoke;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// removes a pokemon from the player's current pokemon
        /// </summary>
        /// <param name="poke">pokemon you wish to remove</param>
        public void removePoke(ActivePokemon inPoke)
        {
            if(currentPokemon.Contains(inPoke))
            {
                int i = 0;
                bool done = false;
                while (done == false)
                {
                    if (currentPokemon[i] == inPoke)
                    {
                        done = true;
                        currentPokemon[i] = null;
                    }
                    else
                    {
                        i++;
                    }
                }                
            }
        }

        public void addItem(Item item, int number)
        {
            addItem(item.itemID, number);
        }

        public void addItem(InventoryItem item)
        {
            inventory.Add(item);
        }

        public void addItem(int ID, int number)
        {
            bool added = false;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == ID)
                {
                    inventory[i].quantity += number;
                    added = true;
                }
            }
            if (!added)
            {
                InventoryItem item = new InventoryItem();
                item.itemID = ID;
                item.quantity = number;
                inventory.Add(item);
            }
        }

        public bool hasPokemon(string species)
        {
            for(int i = 0; i < 6; i++)
            {
                if (currentPokemon[i] != null)
                {
                    if (currentPokemon[0].baseStat.Name == species)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}

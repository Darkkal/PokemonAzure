using System;
using PokeEngine.Moves;
using System.IO;
using Pokemon_Base_Stats_Editor;
using PokeEngine.Pokemon;
using PokeEngine.Items;
using PokeEngine.Map;
using Microsoft.Xna.Framework;
using PokeEngine.Trainers;
using PokeEngine.Screens;
using System.Collections.Generic;
 
namespace PokeEngine.Tools
{
    public class SaveLoad
    {
        /// <summary>
        /// Writes a byte array to a file
        /// </summary>
        /// <param name="bytearray">the array to write, use other methods in SaveLoad to get these</param>
        /// <param name="fileName">path to filezz</param>
        public static void SaveToFile(byte[] bytearray, string fileName)
        {
            FileStream stream = File.OpenWrite(fileName);
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Close();
        }

        //This region is for loading content from the content folder
        #region Content Loading

        /// <summary>
        /// Loads a texture2d from a file, the path must be relative to the application directory
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static Microsoft.Xna.Framework.Graphics.Texture2D LoadTexture2D(String relativePath, Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
        {
            string rootDir = Directory.GetCurrentDirectory();
            string path = rootDir + "\\" + relativePath;
            return Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(graphics, new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        public static Microsoft.Xna.Framework.Audio.SoundEffect LoadSoundEffect(String relativePath)
        {
            string rootDir = Directory.GetCurrentDirectory();
            string path = rootDir + "\\" + relativePath;
            return Microsoft.Xna.Framework.Audio.SoundEffect.FromStream(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        #endregion

        //This region is for loading and saving Base Moves, mostly for the Move Index
        #region BaseMove
        public static void SaveBaseMove(BaseMove inMove, BinaryWriter writer)
        {
            //name
            //description
            //movescript
            //effectscript
            //type
            //kind
            //basepp
            //priority
            //power
            //accuracy

            writer.Write(inMove.name);
            writer.Write(inMove.description);
            writer.Write(inMove.moveScript);
            writer.Write(inMove.effectScript);
            writer.Write(inMove.moveType);
            writer.Write(inMove.moveKind);
            writer.Write(inMove.basePP);
            writer.Write(inMove.basePriority);
            writer.Write(inMove.power);
            writer.Write(inMove.accuracy);
        }

        public static BaseMove LoadBaseMove(BinaryReader reader)
        {
            BaseMove move = new BaseMove();

            //name
            //description
            //movescript
            //effectscript
            //type
            //kind
            //basepp
            //priority
            //power
            //accuracy

            move.name = reader.ReadString();
            move.description = reader.ReadString();
            move.moveScript = reader.ReadString();
            move.effectScript = reader.ReadString();
            move.moveType = reader.ReadString();
            move.moveKind = reader.ReadString();
            move.basePP = reader.ReadInt32();
            move.basePriority = reader.ReadInt32();
            move.power = reader.ReadInt32();
            move.accuracy = reader.ReadInt32();

            return move;
        }
        #endregion

        //This region saves and loads the Move List, which is a static list of all moves
        #region MoveList

        /// <summary>
        /// Saves the MoveList to a file
        /// </summary>
        /// <param name="writer">Binary Writer</param>
        public static void SaveMoveList(BinaryWriter writer)
        {
            int num = MoveList.numberOfMoves;

            writer.Write(num);

            for (int i = 0; i < num; i++)
            {
                SaveBaseMove(MoveList.move[i], writer);
            }
        }


        /// <summary>
        /// Loads moves to the MoveList
        /// </summary>
        /// <param name="reader">Binary Reader</param>
        public static void LoadMoveList(BinaryReader reader)
        {
            MoveList.move.Clear();
            int num = reader.ReadInt32();

            for (int i = 0; i < num; i++)
            {
                BaseMove temp = LoadBaseMove(reader);

                MoveList.move.Add(temp);
            }
        }

        #endregion

        //This region is for saving and loading moves that active pokemon have
        #region ActiveMove

        public static void SaveActiveMove(ActiveMove inMove, BinaryWriter writer)
        {
            //moveID
            //currentPP
            //PPUPuses

            writer.Write(MoveList.getIndex(inMove.bMove.name));
            writer.Write(inMove.currentPP);
            writer.Write(inMove.PPUpUses);
        }

        public static ActiveMove LoadActiveMove(BinaryReader reader)
        {
            ActiveMove move = new ActiveMove(MoveList.move[reader.ReadInt32()]);
            move.currentPP = reader.ReadInt32();
            move.PPUpUses = reader.ReadByte();
            move.maxPP = move.bMove.basePP + Convert.ToInt32(0.2 * Convert.ToDouble(move.PPUpUses));

            return move;
        }

        #endregion

        //This regions is for saving and loading individual item properties
        #region Item

        public static void SaveItem(Item inItem, BinaryWriter writer)
        {
            Item.SaveItem(inItem, writer);
        }

        public static Item LoadItem(BinaryReader reader)
        {
            return Item.LoadItem(reader);
        }

        #endregion

        //This region saves and loads the static item list
        #region ItemList

        public static void SaveItemList(BinaryWriter writer)
        {
            Item.SaveItemList(writer);
        }

        public static void LoadItemList(BinaryReader reader)
        {
            Item.LoadItemList(reader);
        }

        #endregion

        //This region is for saving and loading BasePokemon, which is the base stats of a pokemon
        #region BaseStats

        public static void SaveBaseStats(BaseStat stats, BinaryWriter writer)
        {
            //ID byte
            //name
            //dexentry
            //hp byte
            //form byte
            //attack
            //defense
            //speed
            //spatk
            //spdef
            //type1
            //type2
            //catchrate
            //expyield
            //effortyield ushort
            //item1 uint
            //item2 uint
            //gendervalue byte
            //levelingtype byte
            //hasalternative bool
            //ability1 ushort
            //ability2 ushort
            //ability3 ushort
            //MoveList int[]
            //MoveLevels int[]
            //egggroups byte[]

            writer.Write(stats.ID);
            writer.Write(stats.Name);
            writer.Write(stats.DexEntry);
            writer.Write(stats.BaseHP);
            writer.Write(stats.FormID);
            writer.Write(stats.BaseAttack);
            writer.Write(stats.BaseDefense);
            writer.Write(stats.BaseSpeed);
            writer.Write(stats.BaseSpecialAttack);
            writer.Write(stats.BaseSpecialDefense);
            writer.Write((byte)stats.Type1);
            writer.Write((byte)stats.Type2);
            writer.Write(stats.CatchRate);
            writer.Write(stats.ExpYield);
            writer.Write(stats.EffortYield);
            writer.Write(stats.Item1);
            writer.Write(stats.Item2);
            writer.Write(stats.GenderValue);
            writer.Write((byte)stats.LevelingType);
            writer.Write(stats.HasAlternate);
            writer.Write(stats.Ability1);
            writer.Write(stats.Ability2);
            writer.Write(stats.Ability3);
            writer.Write(stats.MoveList.Length);
            for (int i = 0; i < stats.MoveList.Length; i++)
            {
                writer.Write(stats.MoveList[i]);
            }
            writer.Write(stats.MoveLevels.Length);
            for (int i = 0; i < stats.MoveLevels.Length; i++)
            {
                writer.Write(stats.MoveLevels[i]);
            }
            writer.Write(stats.Egg_Groups.Length);
            for (int i = 0; i < stats.Egg_Groups.Length; i++)
            {
                writer.Write(stats.Egg_Groups[i]);
            }

        }

        public static BaseStat LoadBaseStats(BinaryReader reader)
        {
            //This is implemented in the BaseStat class
            return BaseStat.LoadBasePokemon(reader);
        }

        #endregion

        //This region is for saving and loading the static Base Stats List
        #region BaseStatsList

        public static void SaveBaseStatsList(BinaryWriter writer)
        {
            BaseStatsList.SaveBaseStatsList(writer);
        }

        public static void LoadBaseStatsList(BinaryReader reader)
        {
            BaseStatsList.LoadBaseStatsList(reader);
        }

        #endregion

        //This region saves and loads Active Pokemon, including EV and IVs and level, etc
        #region ActivePokemon

        public static void SaveActivePokemon(ActivePokemon pokemon, BinaryWriter writer)
        {
            writer.Write(pokemon.baseStat.ID);
            writer.Write(pokemon.baseStat.FormID);
            writer.Write(pokemon.IVHP);
            writer.Write(pokemon.IVAttack);
            writer.Write(pokemon.IVDefense);
            writer.Write(pokemon.IVSPAtk);
            writer.Write(pokemon.IVSPDef);
            writer.Write(pokemon.IVSpeed);
            writer.Write(pokemon.EVHP);
            writer.Write(pokemon.EVAttack);
            writer.Write(pokemon.EVDefense);
            writer.Write(pokemon.EVSPAtk);
            writer.Write(pokemon.EVSPDef);
            writer.Write(pokemon.EVSpeed);
            writer.Write(pokemon.currentHP);
            writer.Write((short)pokemon.status);
            writer.Write(pokemon.ability);
            writer.Write(pokemon.happiness);
            writer.Write(pokemon.isNamed);
            writer.Write(pokemon.Nickname);
            writer.Write(pokemon.level);
            writer.Write(pokemon.currentExp);
            writer.Write(pokemon.isShiny);
            writer.Write((short)pokemon.nature);

            int moves = 0;
            for (int i = 0; i < 4; i++)
            {
                if (pokemon.move[i] != null)
                    moves++;
            }
            writer.Write(moves);
            for (int i = 0; i < 4; i++)
            {
                if (pokemon.move[i] != null)
                    SaveActiveMove(pokemon.move[i], writer);
            }
        }

        /// <summary>
        /// Must have Pokemon List loaded in memory before calling this function
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Active Pokemon</returns>
        public static ActivePokemon LoadActivePokemon(BinaryReader reader)
        {
            int ID = reader.ReadInt32();
            byte form = reader.ReadByte();
            ActivePokemon pokemon = new ActivePokemon(BaseStatsList.basestats);
            pokemon.baseStat = BaseStatsList.GetBaseStats(ID, form);
            pokemon.IVHP = reader.ReadInt32();
            pokemon.IVAttack = reader.ReadInt32();
            pokemon.IVDefense = reader.ReadInt32();
            pokemon.IVSPAtk = reader.ReadInt32();
            pokemon.IVSPDef = reader.ReadInt32();
            pokemon.IVSpeed = reader.ReadInt32();
            pokemon.EVHP = reader.ReadInt32();
            pokemon.EVAttack = reader.ReadInt32();
            pokemon.EVDefense = reader.ReadInt32();
            pokemon.EVSPAtk = reader.ReadInt32();
            pokemon.EVSPDef = reader.ReadInt32();
            pokemon.EVSpeed = reader.ReadInt32();
            pokemon.currentHP = reader.ReadInt32();
            pokemon.status = (MajorStatus)reader.ReadInt16();
            pokemon.ability = reader.ReadString();
            pokemon.happiness = reader.ReadInt32();
            pokemon.isNamed = reader.ReadBoolean();
            pokemon.setNickname(reader.ReadString());
            pokemon.level = reader.ReadInt32();
            pokemon.currentExp = reader.ReadInt32();
            pokemon.isShiny = reader.ReadBoolean();
            pokemon.nature = (NatureType)reader.ReadInt16();

            int moves = reader.ReadInt32();
            for (int i = 0; i < moves; i++)
            {
                    pokemon.move[i] = LoadActiveMove(reader);
            }

            return pokemon;
        }

        #endregion

        //This region saves and loads the properties of a scenery object, including it's scale, translation, and rotation
        #region Scenery

        public static void SaveSceneryList(SortedList<string, Scenery> list, BinaryWriter writer)
        {
            writer.Write(list.Count);
            foreach (KeyValuePair<string, Scenery> kvp in list)
            {
                SaveScenery(kvp.Value, writer);
            }
        }

        public static SortedList<string, Scenery> LoadSceneryList(BinaryReader reader)
        {
            SortedList<string, Scenery> list = new SortedList<string, Scenery>();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                Scenery s = LoadScenery(reader);
                list.Add(s.name, s);
            }

            return list;
        }

        public static void SaveScenery(Scenery scenery, BinaryWriter writer)
        {
            writer.Write(scenery.name);
            writer.Write(scenery.interactScript);
            writer.Write(scenery.modelName);
            writer.Write(scenery.position.X);
            writer.Write(scenery.position.Y);
            writer.Write(scenery.size.X);
            writer.Write(scenery.size.Y);
            SaveMatrix(scenery.scale, writer);
            SaveMatrix(scenery.translation, writer);
            SaveMatrix(scenery.rotation, writer);
        }

        public static Scenery LoadScenery(BinaryReader reader)
        {
            Scenery scenery = new Scenery();
            scenery.name = reader.ReadString();
            scenery.interactScript = reader.ReadString();
            scenery.modelName = reader.ReadString();
            scenery.position.X = reader.ReadInt32();
            scenery.position.Y = reader.ReadInt32();
            scenery.size.X = reader.ReadInt32();
            scenery.size.Y = reader.ReadInt32();
            scenery.scale = LoadMatrix(reader);
            scenery.translation = LoadMatrix(reader);
            scenery.rotation = LoadMatrix(reader);

            return scenery;
        }

        public static void SaveMatrix(Matrix mat, BinaryWriter writer)
        {
            writer.Write(mat.M11);
            writer.Write(mat.M12);
            writer.Write(mat.M13);
            writer.Write(mat.M14);
            writer.Write(mat.M21);
            writer.Write(mat.M22);
            writer.Write(mat.M23);
            writer.Write(mat.M24);
            writer.Write(mat.M31);
            writer.Write(mat.M32);
            writer.Write(mat.M33);
            writer.Write(mat.M34);
            writer.Write(mat.M41);
            writer.Write(mat.M42);
            writer.Write(mat.M43);
            writer.Write(mat.M44);
        }

        public static Matrix LoadMatrix(BinaryReader reader)
        {
            Matrix mat = new Matrix();
            mat.M11 = reader.ReadSingle();
            mat.M12 = reader.ReadSingle();
            mat.M13 = reader.ReadSingle();
            mat.M14 = reader.ReadSingle();
            mat.M21 = reader.ReadSingle();
            mat.M22 = reader.ReadSingle();
            mat.M23 = reader.ReadSingle();
            mat.M24 = reader.ReadSingle();
            mat.M31 = reader.ReadSingle();
            mat.M32 = reader.ReadSingle();
            mat.M33 = reader.ReadSingle();
            mat.M34 = reader.ReadSingle();
            mat.M41 = reader.ReadSingle();
            mat.M42 = reader.ReadSingle();
            mat.M43 = reader.ReadSingle();
            mat.M44 = reader.ReadSingle();

            return mat;
        }

        #endregion

        //This region saves and loads tiles, must be passed with a zone object, especially when loading
        #region Tile

        /// <summary>
        /// Must be passed with a zone object, for scenery reference
        /// </summary>
        /// <param name="tile">The Tile</param>
        /// <param name="zone">Zone we are saving</param>
        /// <param name="writer"></param>
        public static void SaveTile(Tile tile, Zone zone, BinaryWriter writer)
        {
            writer.Write(tile.aDirection.north);
            writer.Write(tile.aDirection.east);
            writer.Write(tile.aDirection.south);
            writer.Write(tile.aDirection.west);
            writer.Write(tile.isJumpable());
            writer.Write(tile.hasRandomEncounter());
            writer.Write(tile.isRamp());
            writer.Write(tile.isOccupied());
            writer.Write(tile.isWater);
            //tile type
            if (String.IsNullOrEmpty(tile.tileType))
                writer.Write("");
            else
                writer.Write(tile.tileType);
            //event script
            if (String.IsNullOrEmpty(tile.eventScript))
                writer.Write("");
            else
                writer.Write(tile.eventScript);
            //scenery
            if (tile.sceneryObject == null)
                writer.Write(-1);
            else
                writer.Write(zone.scenery.IndexOf(tile.sceneryObject));
            writer.Write(tile.Z);

        }

        /// <summary>
        /// Must be passed with a zone object that has it's scenery loaded
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Tile LoadTile(Zone zone, BinaryReader reader)
        {
            Tile tile = new Tile();
            bool north, south, east, west;
            north = reader.ReadBoolean();
            east = reader.ReadBoolean();
            south = reader.ReadBoolean();
            west = reader.ReadBoolean();
            tile.setAccessible(north, east, south, west);
            tile.setJumpable(reader.ReadBoolean());
            tile.setRandomEncounter(reader.ReadBoolean());
            tile.setRamp(reader.ReadBoolean());
            tile.setOccupied(reader.ReadBoolean());
            tile.isWater = reader.ReadBoolean();
            tile.tileType = reader.ReadString();
            tile.eventScript = reader.ReadString();
            //scenery object
            int scObj = reader.ReadInt32();
            if (scObj == -1)
                tile.sceneryObject = null;
            else
                tile.sceneryObject = zone.scenery[scObj];
            tile.Z = reader.ReadInt16();

            return tile;
        }

        #endregion

        //This region saves and loads a zone in the world
        #region Zone

        public static void SaveZone(Zone zone, BinaryWriter writer)
        {
            writer.Write(zone.mapWidth);
            writer.Write(zone.mapHeight);
            writer.Write(zone.globalX);
            writer.Write(zone.globalY);
            if (String.IsNullOrEmpty(zone.tileSheetLocation))
                writer.Write("");
            else
                writer.Write(zone.tileSheetLocation);
            writer.Write(zone.isRoom);
            writer.Write(zone.zoneName);
            writer.Write(zone.zoneEnterScript);
            //trainers
            writer.Write(zone.trainerList.Count);
            for (int i = 0; i < zone.trainerList.Count; i++)
            {
                SaveNPC(zone.trainerList[i], writer);
            }
            //random encounters
            writer.Write(zone.randomPokemon.Count);
            for (int i = 0; i < zone.randomPokemon.Count; i++)
            {
                SaveRandomEncounter(zone.randomPokemon[i], writer);
            }
            //scenery objects
            writer.Write(zone.scenery.Count);
            for (int i = 0; i < zone.scenery.Count; i++)
            {
                SaveScenery(zone.scenery[i], writer);
            }
            //tiles
            for (int x = 0; x < zone.mapWidth; x++)
            {
                for (int y = 0; y < zone.mapHeight; y++)
                {
                    SaveTile(zone.tile[x, y], zone, writer);
                }
            }
            //cutscenes
            writer.Write(zone.cutScenes.Count);
            for (int i = 0; i < zone.cutScenes.Count; i++)
            {
                SaveCutScene(zone.cutScenes[i], writer);
            }
        }

        public static Zone LoadZone(BinaryReader reader)
        {
            Zone zone = new Zone();
            try
            {
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                zone.globalX = reader.ReadInt32();
                zone.globalY = reader.ReadInt32();
                zone.tileSheetLocation = reader.ReadString();
                zone.isRoom = reader.ReadBoolean();
                zone.zoneName = reader.ReadString();
                zone.zoneEnterScript = reader.ReadString();
                //trainers
                int trainers = reader.ReadInt32();
                zone.trainerList.Clear();
                for (int i = 0; i < trainers; i++)
                {
                    zone.trainerList.Add(LoadNPC(reader));
                }
                //random encounters
                int rEncs = reader.ReadInt32();
                zone.randomPokemon.Clear();
                for (int i = 0; i < rEncs; i++)
                {
                    zone.randomPokemon.Add(LoadRandomEncounter(reader));
                }
                //scenery objects
                int sObjects = reader.ReadInt32();
                zone.scenery.Clear();
                for (int i = 0; i < sObjects; i++)
                {
                    zone.scenery.Add(LoadScenery(reader));
                }
                //tiles
                zone.tile = new Tile[width, height];
                for (int x = 0; x < zone.mapWidth; x++)
                {
                    for (int y = 0; y < zone.mapHeight; y++)
                    {
                        zone.tile[x, y] = LoadTile(zone, reader);
                    }
                }
                //cutscenes
                int csCount = reader.ReadInt32();
                for (int i = 0; i < csCount; i++)
                {
                    zone.cutScenes.Add(LoadCutScene(reader));
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Reached end of stream while reading Cutscene ");
            }

            return zone;
        }

        public static void SaveRandomEncounter(RandomEncounter enc, BinaryWriter writer)
        {
            writer.Write(enc.PokemonID);
            writer.Write(enc.Chance);
            writer.Write(enc.Group);
        }

        public static RandomEncounter LoadRandomEncounter(BinaryReader reader)
        {
            RandomEncounter enc = new RandomEncounter();
            enc.PokemonID = reader.ReadInt32();
            enc.Chance = reader.ReadSingle();
            enc.Group = reader.ReadString();

            return enc;
        }

        #endregion

        //This region saves and loads cut scenes 
        #region CutScene

        public static void SaveCutScene(CutScene scene, BinaryWriter writer)
        {
            writer.Write(scene.name);
            writer.Write(scene.length);
            //write Timed Commands
            writer.Write(scene.commands.Count);
            for (int i = 0; i < scene.commands.Count; i++)
            {
                writer.Write(scene.commands[i].time);
                writer.Write(scene.commands[i].command);
            }
            //write NPCs
            writer.Write(scene.NPCs.Count);
            for (int i = 0; i < scene.NPCs.Count; i++)
            {
                SaveNPC(scene.NPCs[i], writer);
            }
            //write player actions
            writer.Write(scene.playerActions.Count);
            for (int i = 0; i < scene.playerActions.Count; i++)
            {
                writer.Write((byte)scene.playerActions[i]);
            }
        }

        public static CutScene LoadCutScene(BinaryReader reader)
        {
            CutScene scene = new CutScene();
            scene.name = reader.ReadString();
            scene.length = reader.ReadInt32();
            //read timed commands
            int tCommands = reader.ReadInt32();
            for (int i = 0; i < tCommands; i++)
            {
                TimedCommand c = new TimedCommand();
                c.time = reader.ReadInt32();
                c.command = reader.ReadString();
            }
            //read NPCs
            int npcCount = reader.ReadInt32();
            for (int i = 0; i < npcCount; i++)
            {
                scene.NPCs.Add(LoadNPC(reader));
            }
            //read player actions
            int pActions = reader.ReadInt32();
            for (int i = 0; i < pActions; i++)
            {
                scene.playerActions.Add((Trainers.Action)reader.ReadByte());
            }

            return scene;
        }

        #endregion

        //This region saves and loads the properties of an NPC
        #region Trainer

        public static void SaveNPC(NPC npc, BinaryWriter writer)
        {
            #region NPC Portion
            bool isTrainer = false;
            if(npc.GetType() == typeof(Trainer) || npc.GetType() == typeof(Player))
            {
                isTrainer = true;
            }

            writer.Write(isTrainer);
            writer.Write(npc.name);

            writer.Write((byte)npc.facing);
            writer.Write(npc.speed);
            writer.Write(npc.sightRange);
            writer.Write(npc.tileCoords.X);
            writer.Write(npc.tileCoords.Y);
            writer.Write(npc.interactScript);
            writer.Write(npc.isMale);
            writer.Write(npc.spriteSheet);
            SaveRectangle(npc.spritePosition, writer);
            SaveRectangle(npc.spritesheetSize, writer);
            //movement
            writer.Write((byte)npc.movement);
            //save nothing if movement type is NONE
            //save rectangle if movement type is WANDER
            if (npc.movement == MovementType.WANDER)
            {
                SaveRectangle(npc.wanderArea, writer);
            }
            //save action list if movement type is PATH
            else if (npc.movement == MovementType.PATH)
            {
                writer.Write(npc.actions.Count);
                writer.Write(0); //this is for saving the actionIndex if we need it
                for (int i = 0; i < npc.actions.Count; i++)
                {
                    //save each action in the list
                    writer.Write((byte)npc.actions[i]);
                }
            }

            #endregion

            #region Trainer portion

            if(isTrainer)
            {
                writer.Write(((Trainer)npc).trainerID);
                writer.Write(((Trainer)npc).money);
                //pokemon
                int pokes = ((Trainer)npc).numCurrentPokemon;
                writer.Write(pokes);
                for (int i = 0; i < 6; i++)
                {
                    if (((Trainer)npc).currentPokemon[i] != null)
                    {
                        SaveActivePokemon(((Trainer)npc).currentPokemon[i], writer);
                    }
                }
                //items
                int items = ((Trainer)npc).inventory.Count;
                writer.Write(items);
                for (int i = 0; i < items; i++)
                {
                    SaveInventoryItem(((Trainer)npc).inventory[i], writer);
                }
            }

            #endregion
        }

        public static NPC LoadNPC(BinaryReader reader)
        {
            NPC npc = null;
            #region NPC portion
            bool isTrainer = reader.ReadBoolean();
            if(isTrainer)
            {
                npc = new Trainer();
            }
            else
            {
                npc = new NPC();
            }

            npc.name = reader.ReadString();
            
            npc.facing = (FacingDirection)reader.ReadByte();
            npc.speed = reader.ReadInt32();
            npc.sightRange = reader.ReadInt32();
            Point pos = new Point();
            pos.X = reader.ReadInt32();
            pos.Y = reader.ReadInt32();
            npc.tileCoords = pos;
            npc.interactScript = reader.ReadString();
            npc.isMale = reader.ReadBoolean();
            npc.spriteSheet = reader.ReadString();
            npc.spritePosition = LoadRectangle(reader);
            npc.spritesheetSize = LoadRectangle(reader);
            //movement
            npc.movement = (MovementType)reader.ReadByte();
            //load nothing if movement type is NONE
            //load rectangle if movement type is WANDER
            if (npc.movement == MovementType.WANDER)
            {
                npc.SetWanderArea(LoadRectangle(reader));
            }
            //load action list if movement type is PATH
            else if (npc.movement == MovementType.PATH)
            {
                int numActions = reader.ReadInt32();
                npc.actionIndex = reader.ReadInt32();
                for (int i = 0; i < numActions; i++)
                {
                    //save each action in the list
                    npc.AddAction((Trainers.Action)reader.ReadByte());
                }
            }

            #endregion

            #region Trainer portion

            if(isTrainer)
            {
                ((Trainer)npc).trainerID = reader.ReadInt32();
                ((Trainer)npc).money = reader.ReadInt32();
                //pokemon
                int pokes = reader.ReadInt32();
                for (int i = 0; i < pokes; i++)
                {
                    ((Trainer)npc).currentPokemon[i] = LoadActivePokemon(reader);
                }
                //items
                int items = reader.ReadInt32();
                ((Trainer)npc).inventory.Clear();
                for (int i = 0; i < items; i++)
                {
                    ((Trainer)npc).addItem(LoadInventoryItem(reader));
                }
            }

            #endregion

            return npc;
        }

        public static void SaveRectangle(Rectangle rect, BinaryWriter writer)
        {
            writer.Write(rect.X);
            writer.Write(rect.Y);
            writer.Write(rect.Width);
            writer.Write(rect.Height);
        }

        public static Rectangle LoadRectangle(BinaryReader reader)
        {
            Rectangle rect = new Rectangle();
            rect.X = reader.ReadInt32();
            rect.Y = reader.ReadInt32();
            rect.Width = reader.ReadInt32();
            rect.Height = reader.ReadInt32();

            return rect;
        }

        public static void SaveInventoryItem(InventoryItem item, BinaryWriter writer)
        {
            writer.Write(item.itemID);
            writer.Write(item.quantity);
        }

        public static InventoryItem LoadInventoryItem(BinaryReader reader)
        {
            InventoryItem item = new InventoryItem();
            item.itemID = reader.ReadInt32();
            item.quantity = reader.ReadInt32();
            return item;
        }

        #endregion

        //This region saves and loads the persistant variables that are available in scripts
        #region Script Variables

        public static void SaveScriptVariables(BinaryWriter writer)
        {
            //write the number of variables
            writer.Write(TFSH.ScriptVariables.variables.Keys.Count);

            //write each variable
            foreach (var v in TFSH.ScriptVariables.variables.Keys)
            {
                WriteValue(v, writer);
                WriteValue(TFSH.ScriptVariables.variables[v], writer);
            }
        }

        private static void WriteValue(object val, BinaryWriter writer)
        {
            Type type = val.GetType();

            //first write the type, then write the value
            if (type == typeof(Double))
            {
                writer.Write((byte)TFSH.DataType.DOUBLE);
                writer.Write((Double)val);
            }
            else if (type == typeof(Boolean))
            {
                writer.Write((byte)TFSH.DataType.BOOLEAN);
                writer.Write((Boolean)val);
            }
            else if (type == typeof(String))
            {
                writer.Write((byte)TFSH.DataType.STRING);
                writer.Write((String)val);
            }
        }

        public static void LoadScriptVariables(BinaryReader reader)
        {
            //read the number of variables
            int count = reader.ReadInt32();

            //read each variable
            for (int i = 0; i < count; i++)
            {
                Object key = ReadValue(reader);
                Object value = ReadValue(reader);
                TFSH.ScriptVariables.variables[key] = value;
            }
        }

        private static Object ReadValue(BinaryReader reader)
        {
            //read the type first
            TFSH.DataType type = (TFSH.DataType)reader.ReadByte();
            Object val;

            //then read the variable
            switch (type)
            {
                case TFSH.DataType.BOOLEAN:
                    val = reader.ReadBoolean();
                    break;
                case TFSH.DataType.DOUBLE:
                    val = reader.ReadDouble();
                    break;
                case TFSH.DataType.STRING:
                    val = reader.ReadString();
                    break;
                default:
                    throw new Exception("oh noes, not a supported data type");
            }

            return val;
        }

        #endregion

    }

    
}

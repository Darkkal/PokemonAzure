using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BaseStat = Pokemon_Base_Stats_Editor.BaseStat;

namespace PokeEngine.Pokemon
{
    public enum Egg_Groups : byte
    {
        monster,
        water1,
        bug,
        flying,
        ground,
        fairy,
        plant,
        humanshape,
        water3,
        mineral,
        indeterminate,
        water2,
        ditto,
        dragon,
        noeggs,
    }
    class BaseStatsList
    {
        internal static List<BaseStat> basestats;
        internal static int[] MoveList;
        internal static int[] MoveLevels;
        internal static string workingDir;

        public static void initialize()
        {
            basestats = new List<BaseStat>();
            #region Old Parsing Code
            /*
            string[] strSplit = new string[1];
            strSplit[0] = "\r\n";
            string pokeindex = File.ReadAllText("basestats.csv");
            string[] pokedata = pokeindex.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pokedata.Length; i++)
            {
                int tempindex = 0;
                string[] data = pokedata[i].Split('¶');
                MoveLevels = new int[0];
                MoveList = new int[0];
                for (int index = 22; index < data.Length; index++)
                {
                    if (ushort.Parse(data[index]) < 65535)
                    {
                        Array.Resize(ref MoveLevels, MoveLevels.Length + 1);
                        MoveLevels[MoveLevels.Length - 1] = int.Parse(data[index]);
                    }
                    else
                    {
                        tempindex = index + 1;
                        break;
                    }
                }
                for (int index = tempindex; index < data.Length - 1; index++)
                {
                    if (ushort.Parse(data[index]) < 65535)
                    {
                        Array.Resize(ref MoveList, MoveList.Length + 1);
                        MoveList[MoveList.Length - 1] = int.Parse(data[index]);
                    }
                    else
                    {
                        tempindex = index + 1;
                        break;
                    }
                }
                BaseStat bs = new BaseStat()
                {
                    ID = int.Parse(data[0]),
                    Name = data[1],
                    BaseHP = byte.Parse(data[2]),
                    BaseAttack = byte.Parse(data[3]),
                    BaseDefense = byte.Parse(data[4]),
                    BaseSpecialAttack = byte.Parse(data[5]),
                    BaseSpecialDefense = byte.Parse(data[6]),
                    BaseSpeed = byte.Parse(data[7]),
                    LevelingType = (LevelUpType)int.Parse(data[8]),
                    FormID = byte.Parse(data[9]),
                    GenderValue = byte.Parse(data[10]),
                    Type1 = (PokeType)byte.Parse(data[11]),
                    Type2 = (PokeType)byte.Parse(data[12]),
                    CatchRate = byte.Parse(data[13]),
                    ExpYield = byte.Parse(data[14]),
                    EffortYield = ushort.Parse(data[15]),
                    Item1 = byte.Parse(data[16]),
                    Item2 = byte.Parse(data[17]),
                    Ability1 = byte.Parse(data[18]),
                    Ability2 = byte.Parse(data[19]),
                    Ability3 = byte.Parse(data[20]),
                    DexEntry = data[21],
                    MoveLevels = MoveLevels,
                    MoveList = MoveList
                };
                basestats.Add(bs);
            }
            */
            #endregion

            workingDir = Directory.GetCurrentDirectory();
            basestats = (List<BaseStat>)ByteArrayToObject(File.ReadAllBytes(workingDir + @"\Content\Data\basestats.dat"));
        }
        public static BaseStat GetBaseStats(int id, byte form)
        {
            return basestats.First(bs => bs.ID == id && bs.FormID == form);
        }

        public static BaseStat GetBaseStats(int id)
        {
            return GetBaseStats(id, 0);
        }

        public static BaseStat GetBaseStats(string name)
        {
            return basestats.First(bs => bs.Name == name);
        }

        public static int GetIndex(BaseStat stats)
        {
            return basestats.IndexOf(stats);
        }

        private static Object ByteArrayToObject(byte[] arrBytes)
        {

            MemoryStream memStream = new MemoryStream(arrBytes);
            BinaryFormatter binForm = new BinaryFormatter();

            Object obj = binForm.Deserialize(memStream);
            memStream.Close();
            return obj;
        }


        //These methods are for loading and saving the static Base Stats LIst
        ////////////////////////////////////////
        public static void SaveBaseStatsList(BinaryWriter writer)
        {
            int num = basestats.Count;

            writer.Write(num);

            for (int i = 0; i < num; i++)
            {
                SaveBaseStats(basestats[i], writer);
            }
        }

        public static void LoadBaseStatsList(BinaryReader reader)
        {
            int num = reader.ReadInt32();
            basestats.Clear();

            for (int i = 0; i < num; i++)
            {
                basestats.Add(LoadBaseStats(reader));
            }
        }

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
            //Egg_Groups byte[]

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
            foreach (int t in stats.MoveList)
            {
                writer.Write(t);
            }
            writer.Write(stats.MoveLevels.Length);
            foreach (int t in stats.MoveLevels)
            {
                writer.Write(t);
            }
            writer.Write(stats.Egg_Groups.Length);
            foreach (byte t in stats.Egg_Groups)
            {
                writer.Write(t);
            }
        }

        public static BaseStat LoadBaseStats(BinaryReader reader)
        {
            //This is implemented in the BaseStat class
            return BaseStat.LoadBasePokemon(reader);
        }

    }
}

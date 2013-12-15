using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pokemon_Base_Stats_Editor
{
    [Serializable]
    public class BaseStat
    {

        public int ID
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        public string DexEntry
        {
            get;
            internal set;
        }

        public byte BaseHP
        {
            get;
            internal set;
        }

        public byte FormID
        {
            get;
            internal set;
        }

        public byte BaseAttack
        {
            get;
            internal set;
        }

        public byte BaseDefense
        {
            get;
            internal set;
        }

        public byte BaseSpeed
        {
            get;
            internal set;
        }

        public byte BaseSpecialAttack
        {
            get;
            internal set;
        }

        public byte BaseSpecialDefense
        {
            get;
            internal set;
        }

        public PokeType Type1
        {
            get;
            internal set;
        }

        public PokeType Type2
        {
            get;
            internal set;
        }

        public byte CatchRate
        {
            get;
            internal set;
        }

        public byte ExpYield
        {
            get;
            internal set;
        }

        public ushort EffortYield
        {
            get;
            internal set;
        }

        public uint Item1
        {
            get;
            internal set;
        }

        public uint Item2
        {
            get;
            internal set;
        }

        public byte GenderValue
        {
            get;
            internal set;
        }

        public LevelUpType LevelingType
        {
            get;
            internal set;
        }

        public bool HasAlternate
        {
            get;
            internal set;
        }


        public override string ToString()
        {
            return ID + "," + Name + "...";
        }

        public ushort Ability1
        {
            get;
            internal set;
        }
        
        public ushort Ability2
        {
            get;
            internal set;
        }

        public ushort Ability3
        {
            get;
            internal set;
        }

        public int[] MoveList
        {
            get;
            internal set;
        }

        public int[] MoveLevels
        {
            get;
            internal set;
        }

        public byte[] Egg_Groups
        {
            get;
            internal set;
        }

        public static BaseStat LoadBasePokemon(BinaryReader reader)
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

            BaseStat stat = new BaseStat();

            stat.ID = reader.ReadInt32();
            stat.Name = reader.ReadString();
            stat.DexEntry = reader.ReadString();
            stat.BaseHP = reader.ReadByte();
            stat.FormID = reader.ReadByte();
            stat.BaseAttack = reader.ReadByte();
            stat.BaseDefense = reader.ReadByte();
            stat.BaseSpeed = reader.ReadByte();
            stat.BaseSpecialAttack = reader.ReadByte();
            stat.BaseSpecialDefense = reader.ReadByte();
            stat.Type1 = (PokeType)reader.ReadByte();
            stat.Type2 = (PokeType)reader.ReadByte();
            stat.CatchRate = reader.ReadByte();
            stat.ExpYield = reader.ReadByte();
            stat.EffortYield = reader.ReadUInt16();
            stat.Item1 = reader.ReadUInt32();
            stat.Item2 = reader.ReadUInt32();
            stat.GenderValue = reader.ReadByte();
            stat.LevelingType = (LevelUpType)reader.ReadByte();
            stat.HasAlternate = reader.ReadBoolean();
            stat.Ability1 = reader.ReadUInt16();
            stat.Ability2 = reader.ReadUInt16();
            stat.Ability3 = reader.ReadUInt16();
            int num = 0;
            //MoveList
            num = reader.ReadInt32();
            stat.MoveList = new int[num];
            for (int i = 0; i < num; i++)
            {
                stat.MoveList[i] = reader.ReadInt32();
            }
            //MoveLevels
            num = reader.ReadInt32();
            stat.MoveLevels = new int[num];
            for (int i = 0; i < num; i++)
            {
                stat.MoveLevels[i] = reader.ReadInt32();
            }
            //Egg Groups
            num = reader.ReadInt32();
            stat.Egg_Groups = new byte[num];
            for (int i = 0; i < num; i++)
            {
                stat.Egg_Groups[i] = reader.ReadByte();
            }

            return stat;
        }
    }

    public enum PokeType : byte
    {
        None = 255,
        Normal = 0,
        Fighting,
        Flying,
        Poison,
        Ground,
        Rock,
        Bug,
        Ghost,
        Steel,
        Fire,
        Water,
        Grass,
        Electric,
        Psychic,
        Ice,
        Dragon,
        Dark,
    }

    public enum LevelUpType : byte
    {
        MediumFast = 0,
        Erratic,
        Fluctuating,
        MediumSlow,
        Fast,
        Slow,
    }
}

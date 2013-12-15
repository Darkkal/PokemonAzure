using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pokemon_Base_Stats_Editor.Properties;
using System.Runtime.Serialization.Formatters.Binary;

namespace Pokemon_Base_Stats_Editor
{
    public partial class Main : Form
    {
        internal static List<BaseStat> basestats;
        internal static Dictionary<int, string> movenames;
        internal static Dictionary<int, string> abilities;
        internal static Dictionary<int, string> items;
        internal static int[] MoveList;
        internal static int[] MoveLevels;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadDataFiles();
            foreach (BaseStat bs in basestats) 
            {
                if (bs.FormID > 0)
                {
                    cmbPokemon.Items.Add(bs.Name + " (Forme #" + bs.FormID.ToString() + ")");
                }
                else
                {
                    cmbPokemon.Items.Add(bs.Name);
                }
            }
            foreach (KeyValuePair<int, string> move in movenames)
            {
                cmbAvailableMoves.Items.Add(move.Value);
            }
            foreach (KeyValuePair<int, string> ability in abilities)
            {
                cmbAbility1.Items.Add(ability.Value);
                cmbAbility2.Items.Add(ability.Value);
            }
            foreach (PokeType type in Enum.GetValues(typeof(PokeType)))
            {
                cmbType1.Items.Add(type);
                cmbType2.Items.Add(type);
            }
        }

        private static void LoadDataFiles()
        {
            basestats = new List<BaseStat>();
            movenames = new Dictionary<int, string>();
            items = new Dictionary<int, string>();
            abilities = new Dictionary<int, string>();

            
            string[] strSplit = new string[1];
            strSplit[0] = "\r\n";


            //load moves first
            string[] moves = Resources.moves.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < moves.Length; i++)
            {
                movenames.Add(i + 1, moves[i]);
            }

            //load items
            string[] item = Resources.items.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < item.Length; i++)
            {
                char[] charstotrim = { ' ' };
                items.Add(i + 1, item[i].TrimEnd(charstotrim));
            }

            //load abilities
            string[] ability = Resources.abilities.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ability.Length; i++)
            {
                string[] spl = ability[i].Split(',');
                abilities.Add(int.Parse(spl[0]), spl[1]);
            }

            /*
            string pokeindex = File.ReadAllText("basestats.csv");
            string[] pokedata = pokeindex.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            //string[] importdata = File.ReadAllLines("pokemon_stats.csv");
            for (int i = 0; i < pokedata.Length; i++)
            {
                int tempindex = 0;
                string[] data = pokedata[i].Split('¶');
                MoveLevels = new int[0];
                MoveList = new int[0];
                for (int index = 22; index < data.Length; index++ )
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
            } */
            basestats = (List<BaseStat>)ByteArrayToObject(File.ReadAllBytes("basestats.dat"));
        }
        private static BaseStat GetBaseStats(int id, byte form)
        {
            return basestats.First(bs => bs.ID == id && bs.FormID == form);
        }
        private static BaseStat GetBaseStats(string name)
        {
            return basestats.First(bs => bs.Name == name);
        }

        public static void SetBaseStats(int id, byte form, BaseStat bs)
        {
            int index = Find(id, form);
            basestats[index] = bs;
        }
        private static int Find(int id, byte form)
        {
            return basestats.FindIndex(bs => bs.ID == id && bs.FormID == form);            
        }

        private void cmbPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LoadDataFiles();
            BaseStat bs = basestats[cmbPokemon.SelectedIndex];
            numDex.Value = bs.ID;
            numForme.Value = bs.FormID;
            numHP.Value = bs.BaseHP;
            numATK.Value = bs.BaseAttack;
            numDEF.Value = bs.BaseDefense;
            numSPATK.Value = bs.BaseSpecialAttack;
            numSPDEF.Value = bs.BaseSpecialDefense;
            numSpeed.Value = bs.BaseSpeed;
            if (bs.Type1 == PokeType.None)
            {
                cmbType1.SelectedIndex = cmbType1.Items.Count - 1;
            }
            else
            {
                cmbType1.SelectedIndex = (int)bs.Type1;
            }
            if (bs.Type2 == PokeType.None)
            {
                cmbType2.SelectedIndex = cmbType2.Items.Count - 1;
            }
            else
            {
                cmbType2.SelectedIndex = (int)bs.Type2;
            }
            cmbAbility1.SelectedIndex = bs.Ability1 - 1;
            cmbAbility2.SelectedIndex = bs.Ability2 - 1;
            numGrowth.Value = (int)bs.LevelingType;
            MoveList = basestats[cmbPokemon.SelectedIndex].MoveList;
            MoveLevels = basestats[cmbPokemon.SelectedIndex].MoveLevels;
            lstSelectedMoves.Items.Clear();
            for (int i = 0; i < MoveList.Length; i++)
            {
                int moveid = MoveList[i];
                lstSelectedMoves.Items.Add("Level: " + MoveLevels[i] + " Move: " + movenames[moveid]);
                i++;
            }
            txtDexEntry.Text = bs.DexEntry;
            numGenderRatio.Value = bs.GenderValue;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<int> ListMove = MoveList.ToList();
            List<int> ListLevels = MoveLevels.ToList();
            ListMove.Add(cmbAvailableMoves.SelectedIndex + 1);
            ListLevels.Add((int)numMoveLevel.Value);
            Array.Resize(ref MoveLevels, ListLevels.Count - 1);
            Array.Resize(ref MoveList, ListMove.Count - 1);
            MoveList = ListMove.ToArray();
            MoveLevels = ListLevels.ToArray();
            lstSelectedMoves.Items.Add("Level: " + ((int)numMoveLevel.Value).ToString() + " Move: " + movenames[cmbAvailableMoves.SelectedIndex + 1]);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<int> ListMove = MoveList.ToList();
            List<int> ListLevels = MoveLevels.ToList();
            ListMove.RemoveAt(lstSelectedMoves.SelectedIndex);
            ListLevels.RemoveAt(lstSelectedMoves.SelectedIndex);
            lstSelectedMoves.Items.RemoveAt(lstSelectedMoves.SelectedIndex);
            if (ListLevels.Count >= 1)
            {
                Array.Resize(ref MoveLevels, ListLevels.Count - 1);
            }
            else
            {
                MoveLevels = new int[0];
            }
            if (ListMove.Count >= 1)
            {
                Array.Resize(ref MoveList, ListMove.Count - 1);
            }
            else
            {
                MoveList = new int[0];
            }
            MoveList = ListMove.ToArray();
            MoveLevels = ListLevels.ToArray();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseStat bs = new BaseStat()
            {
                ID = (int)numDex.Value,
                Name = GetBaseStats((int)numDex.Value, (byte)numForme.Value).Name,
                BaseHP = (byte)numHP.Value,
                BaseAttack = (byte)numATK.Value,
                BaseDefense = (byte)numDEF.Value,
                BaseSpecialAttack = (byte)numSPATK.Value,
                BaseSpecialDefense = (byte)numSPDEF.Value,
                BaseSpeed = (byte)numSpeed.Value,
                LevelingType = (LevelUpType)numGrowth.Value,
                FormID = (byte)numForme.Value,
                GenderValue = (byte)numGenderRatio.Value,
                CatchRate = GetBaseStats((int)numDex.Value,(byte)numForme.Value).CatchRate,
                ExpYield = GetBaseStats((int)numDex.Value, (byte)numForme.Value).ExpYield,
                EffortYield = GetBaseStats((int)numDex.Value, (byte)numForme.Value).EffortYield,
                Item1 = GetBaseStats((int)numDex.Value, (byte)numForme.Value).Item1,
                Item2 = GetBaseStats((int)numDex.Value, (byte)numForme.Value).Item2,
                Ability1 = (byte)(cmbAbility1.SelectedIndex + 1),
                Ability2 = (byte)(cmbAbility2.SelectedIndex + 1),
                Ability3 = GetBaseStats((int)numDex.Value,(byte)numForme.Value).Ability3,
                DexEntry = txtDexEntry.Text,
                MoveLevels = MoveLevels,
                MoveList = MoveList,
                Egg_Groups = GetBaseStats((int)numDex.Value, (byte)numForme.Value).Egg_Groups
            };
            if (cmbType1.SelectedIndex == 18) { bs.Type1 = (PokeType)255; }
            else { bs.Type1 = (PokeType)(cmbType1.SelectedIndex); }
            if (cmbType2.SelectedIndex == 18) { bs.Type2 = (PokeType)255; }
            else { bs.Type2 = (PokeType)(cmbType2.SelectedIndex); }
            SetBaseStats(bs.ID, bs.FormID, bs);
            //string[] pokedata = File.ReadAllLines("basestats.csv");
            //int index = Find(bs.ID, bs.FormID);
            //pokedata[index] = bs.ToString();
            //File.WriteAllLines("basestats.csv", pokedata);
            //string pokedata = "";
            //foreach (BaseStat b in basestats)
            //{
            //    pokedata += b.ToString() + "\r\n";
            //}
            //File.WriteAllText("basestats.csv", pokedata);
            byte[] data = ObjectToByteArray(basestats);
            File.WriteAllBytes("basestats.dat", data);
            LoadDataFiles();
            MoveLevels = bs.MoveLevels;
            MoveList = bs.MoveList;
        }
        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        private void importDexEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.InitialDirectory = ".\\";
            fileOpen.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            fileOpen.FilterIndex = 0;
            fileOpen.RestoreDirectory = false; //true;
            //                if (fileOpen.ShowDialog () == DialogResult.Cancel)
            if (fileOpen.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string[] Entries = File.ReadAllLines(fileOpen.FileName);
            foreach (string s in Entries)
            {
                string[] ssplit = s.Split(',');
                BaseStat bsold = GetBaseStats(ssplit[0]);
                if (ssplit.Length > 6)
                {
                    string DexEntry = "";
                    for (int i = 6; i <= (ssplit.Length - 1); i++)
                    {
                        if (i == 6)
                        {
                            DexEntry = ssplit[6];
                        }
                        else
                        {
                            DexEntry = DexEntry + "," + ssplit[i];
                        }
                    }
                    if (DexEntry.Substring(0, 1) == "\"")
                    {
                        DexEntry = DexEntry.Substring(1, (DexEntry.Length - 1));
                    }
                    if (DexEntry.Substring((DexEntry.Length - 1), 1) == "\"")
                    {
                        DexEntry = DexEntry.Substring(0, (DexEntry.Length - 1));
                    }
                    BaseStat bs = new BaseStat()
                    {
                        ID = bsold.ID,
                        Name = bsold.Name,
                        BaseHP = bsold.BaseHP,
                        BaseAttack = bsold.BaseAttack,
                        BaseDefense = bsold.BaseDefense,
                        BaseSpecialAttack = bsold.BaseSpecialAttack,
                        BaseSpecialDefense = bsold.BaseSpecialDefense,
                        BaseSpeed = bsold.BaseSpeed,
                        LevelingType = bsold.LevelingType,
                        FormID = bsold.FormID,
                        GenderValue = bsold.GenderValue,
                        Type1 = bsold.Type1,
                        Type2 = bsold.Type2,
                        CatchRate = bsold.CatchRate,
                        ExpYield = bsold.ExpYield,
                        EffortYield = bsold.EffortYield,
                        Item1 = bsold.Item1,
                        Item2 = bsold.Item2,
                        Ability1 = bsold.Ability1,
                        Ability2 = bsold.Ability2,
                        Ability3 = bsold.Ability3,
                        DexEntry = DexEntry,
                        MoveLevels = bsold.MoveLevels,
                        MoveList = bsold.MoveList
                    };
                    SetBaseStats(bs.ID, 0, bs);
                }
            }
        }
    }
}

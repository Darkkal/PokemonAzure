using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Xna.Framework.Input;


namespace PokeEngine.Input
{
    static class KeyConfig
    {
        public static Keys Up { get { return KeyList[0]; } }
        public static Keys Down { get { return KeyList[1]; } }
        public static Keys Left { get { return KeyList[2]; } }
        public static Keys Right { get { return KeyList[3]; } }
        public static Keys Action { get { return KeyList[4]; } }
        public static Keys Cancel { get { return KeyList[5]; } }
        public static Keys Menu { get { return KeyList[6]; } }
        public static Keys Item { get { return KeyList[7]; } }

        /// <summary>
        /// 0 = Up; 
        /// 1 = Down; 
        /// 2 = Left; 
        /// 3 = Right; 
        /// 4 = Action (Select); 
        /// 5 = Cancel (Back); 
        /// 6 = Menu; 
        /// 7 = Item;
        /// </summary>
        public static Keys[] KeyList;
        private static bool IsMouseEnabled;

        private static string workingDir;

        public static void Initialize()
        {
            workingDir = Assembly.GetExecutingAssembly().Location;
            var name = System.IO.Path.GetFileName(workingDir);
            workingDir = workingDir.Replace(name, string.Empty) + "\\Content\\Input\\";

            if (Directory.Exists(workingDir))
            {
                try
                {
                    Load();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong with the keyconfig: \n" + ex.Message);
                }
            }
            else
            {
                Directory.CreateDirectory(workingDir);
            }
        }

        public static void Save()
        {
            //IFormatter formatter = new BinaryFormatter();
            try
            {
                var stream = new FileStream(workingDir + "keyconfig.dat", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var bw = new BinaryWriter(stream);
                bw.Write(KeyListToByteArray());
                bw.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not save keyconfig file: \n" + ex.Message + "\n\n");
            }
        }

        public static void Load()
        {
            if (File.Exists(workingDir + "keyconfig.dat"))
            {
                //IFormatter formatter = new BinaryFormatter();
                try
                {
                    CreateNewKeyList();
                    Stream stream = new FileStream(workingDir + "keyconfig.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                    var br = new BinaryReader(stream);
                    LoadKeyList(br.ReadBytes(33));
                    br.Close();
                    stream.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not load keyconfig file: \n" + ex.Message + "\n\n");
                }
            }
            else
            {
                Console.WriteLine("No keyconfig file found, creating new...");
                CreateNewKeyList();
                Save();
            }

        }

        public static void CreateNewKeyList()
        {
            KeyList = new Keys[8];

            KeyList[0] = Keys.Up;
            KeyList[1] = Keys.Down;
            KeyList[2] = Keys.Left;
            KeyList[3] = Keys.Right;
            KeyList[4] = Keys.X;
            KeyList[5] = Keys.Z;
            KeyList[6] = Keys.S;
            KeyList[7] = Keys.A;
        }

        public static byte[] KeyListToByteArray()
        {
            using (var ms = new System.IO.MemoryStream())
            {
                for(int i = 0; i < KeyList.Length; i++)
                    ms.Write(BitConverter.GetBytes((int)KeyList[i]), 0, 4);

                ms.Write(BitConverter.GetBytes(IsMouseEnabled), 0, 1);

                return ms.ToArray();
            }
        }

        public static void LoadKeyList(byte[] Data)
        {
            IsMouseEnabled = BitConverter.ToBoolean(Data, 32);
            Array.Resize<byte>(ref Data, 32);
            var ints = new List<Int32>();
            for (int i = 0; i < Data.Length; i += 4)
                ints.Add(BitConverter.ToInt32(Data, i));

            for (int i = 0; i < KeyList.Length; i++)
                KeyList[i] = (Keys)ints[i];
        }
    }
}

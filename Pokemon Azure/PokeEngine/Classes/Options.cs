using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace PokeEngine
{
    [Serializable]
    public class Options
    {
        private string workingDir;
        public byte TextSpeed;
        public byte Frame;
        public bool BattleScene;
        public bool BattleStyle;
        public bool Sound;
               
        //public void Save()
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    try
        //    {
        //        Stream stream = new FileStream(workingDir + "options.dat", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        //        formatter.Serialize(stream, this);
        //        stream.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Could not save options file: \n" + ex.Message + "\n\n");
        //    }
        //}
        //public void Load()
        //{
        //    if (File.Exists(workingDir + "options.dat"))
        //    {
        //        IFormatter formatter = new BinaryFormatter();
        //        try
        //        {
        //            Stream stream = new FileStream(workingDir + "options.dat", FileMode.Open, FileAccess.Read, FileShare.Read);

        //            Options inOp = (Options)formatter.Deserialize(stream);
        //            BattleScene = inOp.BattleScene;
        //            BattleStyle = inOp.BattleStyle;
        //            TextSpeed = inOp.TextSpeed;
        //            Sound = inOp.Sound;

        //            stream.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Could not load options file: \n" + ex.Message + "\n\n");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Options file found, creating new...");
        //        Save();
        //    }

        //}

        public Options()
        {
            workingDir = Assembly.GetExecutingAssembly().Location;
            var name = System.IO.Path.GetFileName(workingDir);
            workingDir = workingDir.Replace(name, string.Empty) + "\\Content\\Input\\";

            TextSpeed = 0;
            BattleScene = true;
            BattleStyle = true;
            Sound = true;
            Frame = 0;

            if (Directory.Exists(workingDir))
            {
                try
                {
                    Load();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong with the options file: \n" + ex.Message);
                }
            }
            else
            {
                Directory.CreateDirectory(workingDir);
                Load();
            }
        }
        public void Save()
        {
            try
            {
                using (var br = new BinaryWriter(new FileStream(workingDir + "options.dat", FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                {
                    br.Write(BattleScene);
                    br.Write(BattleStyle);
                    br.Write(Sound);
                    br.Write(TextSpeed);
                    br.Write(Frame);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Unable to save configuration!");
                Console.WriteLine(e.ToString());
            }
        }
        public void Load()
        {
            if(!File.Exists(workingDir + "options.dat")) { this.Save(); }
            try
            {
                using (var br = new BinaryReader(new FileStream(workingDir + "options.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    BattleScene = br.ReadBoolean();
                    BattleStyle = br.ReadBoolean();
                    Sound = br.ReadBoolean();
                    TextSpeed = br.ReadByte();
                    Frame = br.ReadByte();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Unable to load configuration!");
                Console.WriteLine(e.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PokeEngine.Moves;

namespace movelistparse
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] newLineSplit = { '\n' };
            char[] commaSplit = { ',' };

            string[] rawMoveList = File.ReadAllText(args[0]).Split(newLineSplit);

            MoveList moveList = new MoveList();

            for (int i = 0; i < rawMoveList.Length; i++)
            {
                int equalIndex = rawMoveList[i].IndexOf('=');
                string newRawMove = rawMoveList[i].Remove(0, equalIndex + 11).Replace(");", "");

                string[] moveProperties = newRawMove.Split(commaSplit);

                for (int j = 0; j < moveProperties.Length; j++)
                {
                    if (moveProperties[j].StartsWith(" "))
                        moveProperties[j] = moveProperties[j].Remove(0, 1);

                    MoveList.addMove(new BaseMove(
                        moveProperties[0].Replace("\"", ""),
                        moveProperties[7].Replace("\"", ""),
                        Int32.Parse(moveProperties[3]),
                        Int32.Parse(moveProperties[4]),
                        moveProperties[1].Replace("Movetype.", ""),
                        moveProperties[2].Replace("Category.", ""),
                        Int32.Parse(moveProperties[5])
                        ));
                }
            }

            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream("C:\\" + "movelist.dat", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            formatter.Serialize(stream, moveList);
            stream.Close();

        }
    }
}
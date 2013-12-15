using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace PokeEngine.Map
{
    
    public class Scenery
    {
        public String name;
        public String interactScript; //script used when the object is interacted with
        public String modelName;
        public Point position; //position of the top left corner of the model (looking down)
        public Point size; //size of the model (number of tiles it spans basically)

        //These matrices affect the drawn size,position, and rotation of the model
        public Matrix scale;
        public Matrix translation;
        public Matrix rotation;

        public Scenery()
        {
            name = "Default";
            interactScript = null;
            modelName = null;
            position = new Point(0, 0);
            size = new Point(0, 0);

            rotation = Matrix.CreateRotationY(0f);
            scale = Matrix.CreateScale(1f);
            translation = Matrix.CreateTranslation(0f, 0f, 0f);
        }

        public Scenery(String n, String script, String mod, Point pos, Point s)
        {
            name = n;
            interactScript = script;
            modelName = mod;
            position = pos;
            size = s;

            rotation = Matrix.CreateRotationY(0f);
            scale = Matrix.CreateScale(1f);
            translation = Matrix.CreateTranslation(0f, 0f, 0f);
        }

        public override String ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Scenery))
            {
                Scenery temp = (Scenery)obj;

                if (temp.name == this.name &&
                    temp.position == this.position &&
                    temp.modelName == this.modelName)
                {
                    return true;
                }
            }
            return false;
        }

        public byte[] ToByteArray()
        {

            List<byte> byteList = new List<byte>();

            

            return new byte[] { };
        }
    }
}

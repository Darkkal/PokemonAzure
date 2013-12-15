using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
//using PokeEngine_Engine.Globals;

namespace PokeEngine.Map
{
    //public enum TType { Black, Grass, Dirt, Sand, Brick, Wall }; //TODO add more tile types
    public enum Direction { North, East, South, West };

    
    public struct AFrom
    {
        //if any of these are set to false it means you may not enter the
        //tile from that direction
        //if all are set to false it is an inaccessible tile
        public bool north;
        public bool east;
        public bool south;
        public bool west;


        public AFrom(bool inNorth, bool inEast, bool inSouth, bool inWest)
        {
            north = inNorth;
            east = inEast;
            south = inSouth;
            west = inWest;
        }

        public byte[] ToByteArray()
        {
            List<byte> byteList = new List<byte>();

            foreach (byte b in BitConverter.GetBytes(north))
                byteList.Add(b);
            foreach (byte b in BitConverter.GetBytes(east))
                byteList.Add(b);
            foreach (byte b in BitConverter.GetBytes(south))
                byteList.Add(b);
            foreach (byte b in BitConverter.GetBytes(west))
                byteList.Add(b);

            return byteList.ToArray();
        }
    }

    
    public class Tile
    {
        private bool jumpable; //whether the tile is jumpable, like a ledge
        public AFrom aDirection; //which directions you may enter the tile from
        private bool randomEncounter; //whether random encounters can occur on this tile
        private bool ramp; //whether the tile is a bike ramp
        private bool occupied;
        private Rectangle sourceRect;
        public String tileType;
        public string tilePath; //path to the tile texture
        public String eventScript = null;
        public string zoneName;
        public bool isWater; //whether the tile is water

        public Scenery sceneryObject; //the 'thing' in this tile

        //Note, X, Y, and Z must be global in relation to all other tiles of the map
        //each tile on the map must have a unique combination of X, Y and Z
        //TODO check if we need these or if we can give this to Zone
        //private int X; //global X coordinate of tile
        //private int Y; //global Y coordinate of tile
        public short Z; //global Z coordinate of tile

        #region Constructors
        //default is inaccessable tile
        public Tile()
        {
            jumpable = false;
            aDirection = new AFrom(false, false, false, false);
            randomEncounter = false;
            ramp = false;
            occupied = false;
            sceneryObject = null;
            tileType = "debug_black_tile.png";

            //MUST BE MADE UNIQUE
            //X = 0;
            //Y = 0;
            Z = 0;
        }

        //copy constructor
        public Tile(Tile inTile)
        {
            jumpable = inTile.isJumpable();
            aDirection = new AFrom(inTile.aDirection.north,
                                   inTile.aDirection.east,
                                   inTile.aDirection.south,
                                   inTile.aDirection.west);
            randomEncounter = inTile.hasRandomEncounter();
            ramp = inTile.isRamp();
            occupied = inTile.isOccupied();
            sourceRect = new Rectangle(inTile.sourceRect.X,
                                       inTile.sourceRect.Y,
                                       inTile.sourceRect.Width,
                                       inTile.sourceRect.Height);
            tileType = inTile.tileType;
            tilePath = inTile.tilePath;
            eventScript = inTile.eventScript;
            zoneName = inTile.zoneName;
            isWater = inTile.isWater;

            sceneryObject = inTile.sceneryObject;
            Z = inTile.Z;
        }

        public Tile(string zone, Rectangle srcRect)
        {
            jumpable = false;
            aDirection = new AFrom(false, false, false, false);
            randomEncounter = false;
            ramp = false;
            occupied = false;
            tileType = "debug_black_tile.png";

            //MUST BE MADE UNIQUE
            //X = 0;
            //Y = 0;
            Z = 0;
            zoneName = zone;
            sourceRect = srcRect;
        }

        //constructor with given x,y, and z coords
        //public Tile(int inX, int inY, int inZ)
        public Tile(int inZ)
        {
            jumpable = false;
            aDirection = new AFrom(false, false, false, false);
            randomEncounter = false;
            ramp = false;
            occupied = false;
            tileType = "debug_black_tile.png";

            //MUST BE MADE UNIQUE
            //X = inX;
            //Y = inY;
            Z = (byte)inZ;
        }

        public Tile(int inZ, string zone, Rectangle srcRect)
        {
            jumpable = false;
            aDirection = new AFrom(false, false, false, false);
            randomEncounter = false;
            ramp = false;
            occupied = false;
            tileType = "debug_black_tile.png";

            //MUST BE MADE UNIQUE
            //X = inX;
            //Y = inY;
            Z = (byte)inZ;

            zoneName = zone;
            sourceRect = srcRect;
        }
        #endregion

        
        #region Setters
        public void setJumpable(bool value)
        {
            jumpable = value;
        }

        public void setAccessible(bool inNorth, bool inEast, bool inSouth, bool inWest)
        {
            aDirection.north = inNorth;
            aDirection.east = inEast;
            aDirection.south = inSouth;
            aDirection.west = inWest;
        }


        public void setRandomEncounter(bool value)
        {
            randomEncounter = value;
        }

        public void setRamp(bool value)
        {
            ramp = value;
        }

        public void setOccupied(bool value)
        {
            occupied = value;
        }

        public void setWater(bool value)
        {
            isWater = value;
        }

        /*public void setTType(string type)
        {
            switch (type)
            {
                case "Black":
                    tileType = TType.Black;
                    //tileTexture = Globals.Content.Load<Texture2D>("Zone/Tile/Black"); //<---- we need to do something like this
                    break;
                case "Grass":
                    tileType = TType.Grass;
                    //tileTexture = Globals.Content.Load<Texture2D>("Zone/Tile/Grass");
                    break;
                case "Dirt":
                    tileType = TType.Dirt;
                    //tileTexture = Globals.Content.Load<Texture2D>("Zone/Tile/Dirt");
                    break;
                case "Sand":
                    tileType = TType.Sand;
                    //tileTexture = Globals.Content.Load<Texture2D>("Zone/Tile/Sand");
                    break;
                case "Brick":
                    tileType = TType.Brick;
                    //tileTexture = Globals.Content.Load<Texture2D>("Zone/Tile/Brick");
                    break;
            }
        }*/
        #endregion

        //makes accessible from all directions
        public void setClear()
        {
            aDirection = new AFrom(true, true, true, true);
            occupied = false;
        }

        #region Getters

        public bool isJumpable()
        {
            return jumpable;
        }

        public bool isAccessibleFrom(Direction dir)
        {
            bool isAc;
            if(dir == Direction.North && !occupied)
            {
                isAc = aDirection.north;
            }
            else if (dir == Direction.East && !occupied)
            {
                isAc = aDirection.east;
            }
            else if (dir == Direction.South && !occupied)
            {
                isAc = aDirection.south;
            }
            else if (dir == Direction.West && !occupied)
            {
                isAc = aDirection.west;
            }
            else
            {
                isAc = false;
            }

            return isAc;
        }

        public bool isAccessibleFrom(String dir)
        {
            dir = dir.ToUpper();
            bool isAc;
            if (dir == "UP")
            {
                isAc = aDirection.north;
            }
            else if (dir == "RIGHT")
            {
                isAc = aDirection.east;
            }
            else if (dir == "DOWN")
            {
                isAc = aDirection.south;
            }
            else if (dir == "LEFT")
            {
                isAc = aDirection.west;
            }
            else
            {
                isAc = false;
            }

            return isAc;
        }

        public bool hasRandomEncounter()
        {
            return randomEncounter;
        }

        public bool isRamp()
        {
            return ramp;
        }
        #endregion

        public bool isOccupied()
        {
            return occupied;
        }

        public bool isClear()
        {
            bool val = false;
            if (aDirection.Equals(new AFrom(true, true, true, true)) && !occupied)
                val = true;
            return val;
        }

        /*public TType getTType()
        {
            return tileType;
        }*/
        public String getTType()
        {
            return tileType;
        }

        /*public Texture2D getTexture()
        {
            return tileTexture;
        }*/

        // ------------------------------------------------------------------------------------------------------
        // WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! 
        // WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! 
        // WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! 
        // WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! 
        // WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! WARNING! ENTERING BUG-PRONE ZONE! 
        // ------------------------------------------------------------------------------------------------------
        public byte[] ToByteArray()
        {
            List<byte> byteList = new List<byte>();

            foreach (byte b in BitConverter.GetBytes(jumpable))
                byteList.Add(b);

            foreach (byte b in aDirection.ToByteArray())
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(randomEncounter))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(ramp))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(sourceRect.X))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(sourceRect.Y))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(sourceRect.Width))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(sourceRect.Height))
                byteList.Add(b);

            // PREPARE FOR UNSAFE-AS-FUCK-PROGRAMMING

            // These bytes contain info on how many bytes to read for the file path, since it may vary
            foreach (byte b in BitConverter.GetBytes(System.Text.Encoding.ASCII.GetBytes(tilePath).Length))
                byteList.Add(b);

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(tilePath))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(System.Text.Encoding.ASCII.GetBytes(tileType).Length))
                byteList.Add(b);

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(tileType))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(System.Text.Encoding.ASCII.GetBytes(eventScript).Length))
                byteList.Add(b);

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(eventScript))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(System.Text.Encoding.ASCII.GetBytes(zoneName).Length))
                byteList.Add(b);

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(zoneName))
                byteList.Add(b);


            foreach (byte b in BitConverter.GetBytes(isWater))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(ramp))
                byteList.Add(b);

            foreach (byte b in BitConverter.GetBytes(Z))
                byteList.Add(b);

            return byteList.ToArray();
        }
    }

    
}

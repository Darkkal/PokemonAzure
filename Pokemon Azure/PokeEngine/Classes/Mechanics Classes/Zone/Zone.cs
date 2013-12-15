using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Trainers;
using PokeEngine.Pokemon;
using PokeEngine.Screens;


namespace PokeEngine.Map
{
    
    public class Zone
    {
        /*
        public struct Point
        {
            public int X;
            public int Y;

            public Point(int inX, int inY)
            {
                X = inX;
                Y = inY;
            }
        }*/
        public bool isRoom = false; //whether this zone is a room
                            //a room will not display any adjacent zones.
                            //default is false, meaning outdoors basically(though not necessarily)

        public String zoneName;  //this is the name of the zone, generally "route xxxx"
        //TODO make way to store adjacent zones
        public Tile[,] tile; //a 2D array of nodes, these are individual tiles of a map
        //starts at 0,0 for bottom left corner and is x,y
        //so tile[1][2] is the second tile from the left, and 3 from the bottom
        //NOT global, relative to zone only
        //global coordinate would be global X and Y plus tile x and y

        public List<NPC> trainerList;
        public List<RandomEncounter> randomPokemon;
        public string tileSheetLocation; //directory with tilesheet
        public String zoneEnterScript;

        //private Scenery scenery;

        public int globalX; //global X coordinate of tile [0][0]
        public int globalY; //global Y coordinate of tile [0][0]

        public List<Scenery> scenery; //scenery in the zone
        public List<CutScene> cutScenes;

        #region Constructors
        //default constructor makes a 50x50 zone
        public Zone()
        {
            zoneName = "Default Name";
            tileSheetLocation = "";
            zoneEnterScript = "";
            //these must be changed to global unless only one zone exists
            globalX = 0;
            globalY = 0;
            tile = new Tile[50, 50];
            allToDefault();
            randomPokemon = new List<RandomEncounter>();
            scenery = new List<Scenery>();
            trainerList = new List<NPC>();
            cutScenes = new List<CutScene>();

        }

        //copy constructor
        public Zone(Zone inZone)
        {
            tileSheetLocation = "";
            zoneEnterScript = "";
            zoneName = inZone.zoneName;
            globalX = inZone.globalX;
            globalY = inZone.globalY;
            isRoom = inZone.isRoom;
            scenery = new List<Scenery>(inZone.scenery);
            trainerList = new List<NPC>(inZone.trainerList);
            randomPokemon = new List<RandomEncounter>(inZone.randomPokemon);

            tile = new Tile[inZone.mapWidth, inZone.mapHeight];
            //make copies of each tile too
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    tile[x, y] = new Tile(inZone.tile[x, y]);
                }
            }
            cutScenes = new List<CutScene>(inZone.cutScenes);
        }

        //constructor when given size
        public Zone(int inX, int inY)
        {
            tileSheetLocation = "";
            zoneEnterScript = "";
            zoneName = "Default Name";
            //these must be changed to global unless only one zone exists
            globalX = 0;
            globalY = 0;
            tile = new Tile[inX, inY];
            allToDefault();
            randomPokemon = new List<RandomEncounter>();
            scenery = new List<Scenery>();
            trainerList = new List<NPC>();
            cutScenes = new List<CutScene>();

        }

        //constructor when given size and name
        public Zone(String name, int inX, int inY)
        {
            tileSheetLocation = "";
            zoneEnterScript = "";
            zoneName = name;
            //these must be changed to global unless only one zone exists
            globalX = 0;
            globalY = 0;
            tile = new Tile[inX, inY];
            allToDefault();
            randomPokemon = new List<RandomEncounter>();
            scenery = new List<Scenery>();
            trainerList = new List<NPC>();
            cutScenes = new List<CutScene>();

        }

        //constructor when given global X and Y coordinates and size
        public Zone(int inX, int inY, int inXCoord, int inYCoord)
        {
            tileSheetLocation = "";
            zoneEnterScript = "";
            zoneName = "Default Name";
            globalX = inXCoord;
            globalY = inYCoord;
            tile = new Tile[inX, inY];
            allToDefault();
            randomPokemon = new List<RandomEncounter>();
            scenery = new List<Scenery>();
            trainerList = new List<NPC>();
            cutScenes = new List<CutScene>();

        }

        public Zone(int inX, int inY, int inXCoord, int inYCoord, List<NPC> trainers)
        {
            tileSheetLocation = "";
            zoneEnterScript = "";
            zoneName = "Default Name";
            globalX = inXCoord;
            globalY = inYCoord;
            tile = new Tile[inX, inY];
            allToDefault();
            randomPokemon = new List<RandomEncounter>();
            trainerList = trainers;
            scenery = new List<Scenery>();
            cutScenes = new List<CutScene>();

        }
        #endregion

        #region Getters
        public int mapWidth
        {
            get { return tile.GetLength(0); }
        }

        public int mapHeight
        {
            get { return tile.GetLength(1); }
        }

        public byte[] ToByteArray()
        {
            List<byte> byteList = new List<byte>();

            foreach (byte b in BitConverter.GetBytes(mapHeight))
            {
                byteList.Add(b);
            }
            foreach (byte b in BitConverter.GetBytes(mapWidth))
            {
                byteList.Add(b);
            }
            foreach (byte b in BitConverter.GetBytes(globalX))
            {
                byteList.Add(b);
            }
            foreach (byte b in BitConverter.GetBytes(globalY))
            {
                byteList.Add(b);
            }
            foreach (byte b in BitConverter.GetBytes(isRoom))
            {
                byteList.Add(b);
            }
            /* OOOPS I BROKE THIS
            foreach (int i in randomPokemon)
            {
                foreach (byte b in BitConverter.GetBytes(i))
                {
                    byteList.Add(b);
                }
            }
             */

            foreach (Scenery s in scenery)
            {
                foreach (byte b in s.ToByteArray())
                {
                    byteList.Add(b);
                }
            }

            foreach (Tile t in tile)
            {
                foreach (byte b in t.ToByteArray())
                {
                    byteList.Add(b);
                }
            }

            foreach (byte b in BitConverter.GetBytes(ASCIIEncoding.ASCII.GetBytes(tileSheetLocation).Length))
            {
                byteList.Add(b);
            }

            foreach (byte b in ASCIIEncoding.ASCII.GetBytes(tileSheetLocation))
            {
                byteList.Add(b);
            }

            foreach (NPC n in trainerList)
            {

                if (n.GetType() == typeof(Trainer))
                {
                    //
                }
                else
                {

                }
            }

            return byteList.ToArray();
        }

        #endregion

        private void allToDefault()
        {
            for (int a = 0; a < mapWidth; a++)
            {
                for (int b = 0; b < mapHeight; b++)
                {
                    tile[a, b] = new Tile();
                }
            }
        }

        //returns global coords of tile x y
        public Point convertToGlobal(int x, int y)
        {
            Point xy = new Point(x + globalX, y + globalY);
            return xy;
        }

        //returns local coords of tile x y
        //returns -1,-1 if not on this zone
        public Point convertToLocal(int x, int y)
        {
            Point xy = new Point(-1, -1);
            if(x < (globalX+mapWidth) && (x >= globalX))
            {
                if(y < (globalY+mapHeight) && (y >= globalY))
                {
                    xy.X = x - globalX;
                    xy.Y = y - globalY;
                }
            }
            return xy;
        }

        public NPC GetNPCAtLocation(int x, int y)
        {
            foreach (NPC npc in trainerList)
            {
                if (npc.tileCoords.X == x && npc.tileCoords.Y == y)
                {
                    return npc;
                }
            }

            return null;
        }


        #region pathfinding
        public List<Vector3> FindPath(Vector3 originV3, Vector3 destinationV3)
        {
            Position origin = new Position(convertToLocal((int)originV3.X, (int)originV3.Y).X, convertToLocal((int)originV3.X, (int)originV3.Y).Y);
            Position destination = new Position(convertToLocal((int)destinationV3.X, (int)destinationV3.Y).X, convertToLocal((int)destinationV3.X, (int)destinationV3.Y).Y);
            List<Vector3> path = new List<Vector3>();

            Node[,] node = new Node[mapWidth, mapHeight];

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    node[x, y] = new Node(tile[x, y].isClear() && !tile[x, y].isOccupied(), x, y);
                    node[x, y].h = Math.Max(Math.Abs(destination.x - node[x, y].x), Math.Abs(destination.y - node[x, y].y));
                    node[x, y].g = 0;
                }
            }

            bool done = false;
            open.Add(node[origin.x, origin.y]);

            while (!done && open.Count > 0)
            {
                Node lowest = FindLowest(open, destination);
                open.Remove(lowest);
                closed.Add(lowest);
                if (lowest.x == destination.x && lowest.y == destination.y)
                {
                    done = true;
                    break;
                }

                List<Node> neighbors = FindNeighbors(lowest, node);
                foreach (Node n in neighbors)
                {
                    if (n.isClear && !closed.Contains(n))
                    {
                        if (open.Contains(n))
                        {
                            if (lowest.g + 1 < n.g)
                            {
                                n.parent = lowest;
                                n.g = lowest.g + 1;
                            }
                        }
                        else
                        {
                            open.Add(n);
                            n.parent = lowest;
                            n.g = lowest.g + 1;
                        }
                    }
                }
            }

            if (closed.Contains(node[destination.x, destination.y]))
            {
                //destination has been found!

                List<Node> nodePath = new List<Node>();
                Node temp = node[destination.x, destination.y];
                nodePath.Add(temp);
                temp.isPath = true;
                while (temp != node[origin.x, origin.y])
                {
                    temp.parent.isPath = true;
                    nodePath.Add(temp.parent);
                    temp = temp.parent;
                }
                nodePath.Reverse();
                for (int i = 0; i < nodePath.Count; i++)
                {
                    float z = Convert.ToSingle(tile[nodePath[i].x, nodePath[i].y].Z);
                    Point global = convertToGlobal(nodePath[i].x, nodePath[i].y);
                    path.Add(new Vector3(Convert.ToSingle(global.X), Convert.ToSingle(global.Y), z));
                }
            }
            return path;
        }

        private List<Node> FindNeighbors(Node lowest, Node[,] node)
        {
            List<Node> neighbors = new List<Node>();
            if (lowest.x + 1 < mapWidth)
                neighbors.Add(node[lowest.x + 1, lowest.y]);
            if (lowest.x - 1 >= 0)
                neighbors.Add(node[lowest.x - 1, lowest.y]);
            if (lowest.y + 1 < mapHeight)
                neighbors.Add(node[lowest.x, lowest.y + 1]);
            if (lowest.y - 1 >= 0)
                neighbors.Add(node[lowest.x, lowest.y - 1]);

            return neighbors;
        }

        private Node FindLowest(List<Node> list, Position destination)
        {
            int lowest = list[0].g + list[0].h;
            Node lowestNode = list[0];

            foreach (Node n in list)
            {
                if (n.g + n.h < lowest)
                    lowest = n.g + n.h;
                if (n.x == destination.x && n.y == destination.y)
                    return n;
            }
            foreach (Node n in list)
            {
                if (n.g + n.h == lowest)
                {
                    lowestNode = n;
                    break;
                }
            }

            return lowestNode;
        }

        #endregion
    }

    public struct RandomEncounter
    {
        public int PokemonID;
        public float Chance;
        public String Group;
    }

    internal class Node
    {
        internal Node parent;
        internal int x;
        internal int y;
        internal int h; //heuristic to target
        internal int g; //Path Cost

        internal bool isPath;
        internal bool isClear;

        public Node(bool clear, int inX, int inY)
        {
            parent = null;
            isClear = clear;
            x = inX;
            y = inY;
            isPath = false;
        }
    }

    internal class Position
    {
        internal int x;
        internal int y;

        internal Position(int inX, int inY)
        {
            x = inX;
            y = inY;
        }

        public bool Equals(Position p)
        {
            return (p.x == x && p.y == y);
        }
    }
}

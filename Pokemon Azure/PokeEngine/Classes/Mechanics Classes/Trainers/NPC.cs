using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Pokemon;
using PokeEngine.Screens;
using PokeEngine.Map;

namespace PokeEngine.Trainers
{
    public enum MOVESPEED
    {
        VERY_SLOW = 25,
        SLOW = 20,
        NORMAL = 15,
        FAST = 10,
        VERY_FAST = 5
    }

    public enum Action : byte 
    {
        UP = 1,
        DOWN = 2,
        LEFT = 3,
        RIGHT = 4,
        PAUSE = 5,
        FACEUP = 6,
        FACEDOWN = 7,
        FACELEFT = 8,
        FACERIGHT = 9,
        STOP = 10,
    }

    public enum MovementType : byte
    {
        NONE = 0,
        WANDER = 1,
        PATH = 2,
    }

    public enum FACESPEED
    {
        SLOW = 25,
        NORMAL = 15,
        FAST = 5
    }
    
    public struct TileLocation
    {
        public int x;
        public int y;
    }

    
    public enum FacingDirection:byte { North, South, West, East };

    /// <summary>
    /// Basic NPC, no pokemon, but you can talk to it
    /// </summary>
    
    public class NPC
    {
        private static Random r;
        public Point tileCoords; //actual drawn position is tilesize (32) times mapCoords
                                  //map coords are ALWAYS local coords
        public Point nextTile;  //this is also local
        public short currentZ; //height of NPC 
        public FacingDirection facing;
        public int sightRange; //how far the NPC can 'see', mostly used for trainer battles

        public int speed = (int)MOVESPEED.NORMAL; //must be an int, represents number of frames per tile
        public int movementIndex; //increments from zero to speed, once per update. Indicates the distance moved to the next tile
        public bool isMoving = false;
        public String directionMoving = "";
        public TileLocation tileLocation;
        public String zoneLocation = "";

        public String interactScript = "";

        public String name;
        //TODO add inventory
        public bool isMale; //false for female, true for male
        public string spriteSheet;
        public Rectangle spritePosition; //the current area of the spritesheet to draw
        public Rectangle spritesheetSize;//the total size of the spritesheet

        public int standCoolDown = 0; //player is halted for a brief moment before moving after switching directions, this is the original physics
        public int standCoolMax = 5;
        public int actionCoolDown = 0;

        public int animationFrame = 0; // 0 = standing, 1 = left step, 2 = right step
        public bool leftFoot = true;

        public bool isTurning;
        public FACESPEED turningSpeed = FACESPEED.NORMAL;
        public MovementType movement = MovementType.NONE; //default is no movement
        public int actionIndex = 0;
        public List<Action> actions; //list of actions to perform on a 'path' type movement
        public Rectangle wanderArea; //the area the NPC wanders in, if movement type is wander

        public const int
            VERY_SLOW = 25,
            SLOW = 20,
            NORMAL = 15,
            FAST = 10,
            VERY_FAST = 5;

        /// <summary>
        /// Default constructor
        /// </summary>
        public NPC(NPC inNPC)
        {
            name = inNPC.name;
            isMale = inNPC.isMale;
            zoneLocation = inNPC.zoneLocation;
            tileCoords = new Point(inNPC.tileCoords.X, inNPC.tileCoords.Y);
            facing = inNPC.facing;
            if(inNPC.spritePosition != null)
                spritePosition = new Rectangle(inNPC.spritePosition.X,
                                           inNPC.spritePosition.Y,
                                           inNPC.spritePosition.Width,
                                           inNPC.spritePosition.Height);
            if(inNPC.spritesheetSize != null)
                spritesheetSize = new Rectangle(inNPC.spritesheetSize.X,
                                           inNPC.spritesheetSize.Y,
                                           inNPC.spritesheetSize.Width,
                                           inNPC.spritesheetSize.Height);
            spriteSheet = inNPC.spriteSheet;
            sightRange = inNPC.sightRange;
            nextTile = inNPC.nextTile;
            currentZ = inNPC.currentZ;
            speed = inNPC.speed;
            movementIndex = inNPC.movementIndex;
            isMoving = inNPC.isMoving;
            directionMoving = inNPC.directionMoving;
            standCoolDown = inNPC.standCoolDown;
            standCoolMax = inNPC.standCoolMax;
            animationFrame = inNPC.animationFrame;
            leftFoot = inNPC.leftFoot;
            isTurning = inNPC.isTurning;
            turningSpeed = inNPC.turningSpeed;
            movement = inNPC.movement;
            actionIndex = inNPC.actionIndex;
            if(inNPC.actions != null)
                actions = new List<Action>(inNPC.actions);
            if(inNPC.wanderArea != null)
                wanderArea = new Rectangle(inNPC.wanderArea.X,
                                       inNPC.wanderArea.Y,
                                       inNPC.wanderArea.Width,
                                       inNPC.wanderArea.Height);
        }

        public NPC()
        {
            name = "DURP";
            isMale = true;
            spritePosition = new Rectangle(6, 6, 32, 32);
            sightRange = 0;
        }

        public void Update()
        {
            #region animation
            
            //walking animation
            if (isMoving)
            {
                //animate every half tile shift
                if (movementIndex > speed / 2)
                {
                    //alternate between feet each shift
                    if (leftFoot)
                        animationFrame = 1;
                    else
                        animationFrame = 2;
                }
                else
                    animationFrame = 0;
            }
            //otherwise have a standing pose
            else
                animationFrame = 0;

            #endregion

            spritePosition = new Rectangle(5 + (37 * animationFrame), 5 + (37 * (int)facing), 32, 32);

            if (standCoolDown > 0)
                standCoolDown--;
            if (actionCoolDown > 0)
                actionCoolDown--;
        }

        /// <summary>
        /// Returns false if the tile we are trying to move to is not accessable, or occupied
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool tryToMove(String direction)
        {
            bool canMove = true;
            bool npcTest = true; //false means an npc is occupying the next tile, thus halting movement
            try
            {
                switch (direction)
                {
                    case "Up":
                        nextTile = new Point(tileCoords.X, tileCoords.Y - 1);
                        if (GameScreen.Map.tile[tileCoords.X, tileCoords.Y - 1].isAccessibleFrom(Map.Direction.South))
                        {
                            if (standCoolDown <= 0)
                                startMove("Up");
                        }
                        else if (standCoolDown <= 0)
                        {
                            canMove = false;
                            changeFacingDirection("Up");
                        }
                        break;
                    case "Down":
                        nextTile = new Point(tileCoords.X, tileCoords.Y + 1);
                        if (GameScreen.Map.tile[tileCoords.X, tileCoords.Y + 1].isAccessibleFrom(Map.Direction.North))
                        {
                            if (standCoolDown <= 0)
                                startMove("Down");
                        }
                        else if (standCoolDown <= 0)
                        {
                            canMove = false;
                            changeFacingDirection("Down");
                        }
                        break;
                    case "Left":
                        nextTile = new Point(tileCoords.X - 1, tileCoords.Y);
                        if (GameScreen.Map.tile[tileCoords.X - 1, tileCoords.Y].isAccessibleFrom(Map.Direction.East))
                        {
                            if (standCoolDown <= 0)
                                startMove("Left");
                        }
                        else if (standCoolDown <= 0)
                        {
                            canMove = false;
                            changeFacingDirection("Left");
                        }
                        break;
                    case "Right":
                        nextTile = new Point(tileCoords.X + 1, tileCoords.Y);
                        if (GameScreen.Map.tile[tileCoords.X + 1, tileCoords.Y].isAccessibleFrom(Map.Direction.West))
                        {
                            if (standCoolDown <= 0)
                                startMove("Right");
                        }
                        else if (standCoolDown <= 0)
                        {
                            canMove = false;
                            changeFacingDirection("Right");
                        }
                        break;
                    default:
                        break;
                }
            }//end try
            catch (IndexOutOfRangeException)
            {
                //check whether you can swap to an adjacent map
                if (GameScreen.world.isAdjacentTile(GameScreen.world.currentArea.globalX + nextTile.X, GameScreen.world.currentArea.globalY + nextTile.Y))
                {
                    //unoccupy current tile
                    GameScreen.world.currentArea.tile[tileCoords.X, tileCoords.Y].setOccupied(false);

                    //find global coords
                    nextTile.X += GameScreen.world.currentArea.globalX;
                    nextTile.Y += GameScreen.world.currentArea.globalY;
                    tileCoords.X += GameScreen.world.currentArea.globalX;
                    tileCoords.Y += GameScreen.world.currentArea.globalY; 
                    //change zone
                    GameScreen.world.moveToAdjZone(nextTile.X, nextTile.Y);
                    GameDraw.MakeAdjBuffers(GameScreen.world);
                    GameDraw.UpdateNPCSpritesheets(GameScreen.world);

                    //convert back to local coords in new zone
                    nextTile.X -= GameScreen.world.currentArea.globalX;
                    nextTile.Y -= GameScreen.world.currentArea.globalY;
                    tileCoords.X -= GameScreen.world.currentArea.globalX;
                    tileCoords.Y -= GameScreen.world.currentArea.globalY;

                    isMoving = true;
                    startMove(direction);
                }
                else
                {
                    nextTile = tileCoords;
                    switch (direction)
                    {
                        case "Up": changeFacingDirection("Up"); break;
                        case "Down": changeFacingDirection("Down"); break;
                        case "Left": changeFacingDirection("Left"); break;
                        case "Right": changeFacingDirection("Right"); break;
                        default: break;
                    }
                }
            }

            return canMove;
        }

        /// <summary>
        /// Called every x seconds for every NPC on the map
        /// It selects the next action on the list if the movement
        /// type is an action list, or a random action within the wander area
        /// if wandering, or nothing if not moving
        /// </summary>
        public void GetNextMove(Zone zone)
        {
            if (actionCoolDown <= 0)
            {
                //make a random class if there isn't already one in existence
                if (r == null)
                {
                    r = new Random();
                }
                //if we have a set path to loop
                if (movement == MovementType.PATH &&
                    actions != null &&
                    actions.Count >= 1)
                {
                    bool moved = true;

                    switch (actions[actionIndex])
                    {
                        case Action.UP:
                            changeFacingDirection("Up");
                            standCoolDown = 0;
                            moved = tryToMove("Up");
                            break;
                        case Action.DOWN:
                            changeFacingDirection("Down");
                            standCoolDown = 0;
                            moved = tryToMove("Down");
                            break;
                        case Action.LEFT:
                            changeFacingDirection("Left");
                            standCoolDown = 0;
                            moved = tryToMove("Left");
                            break;
                        case Action.RIGHT:
                            changeFacingDirection("Right");
                            standCoolDown = 0;
                            moved = tryToMove("Right");
                            break;
                        case Action.FACEUP:
                            changeFacingDirection("Up");
                            break;
                        case Action.FACEDOWN:
                            changeFacingDirection("Down");
                            break;
                        case Action.FACELEFT:
                            changeFacingDirection("Left");
                            break;
                        case Action.FACERIGHT:
                            changeFacingDirection("Right");
                            break;
                        case Action.PAUSE:
                            //do nothing
                            break;
                        case Action.STOP:
                            SetNoMovement();
                            break;
                        default:
                            break;
                    }

                    //only update the action index if we actually performed the action
                    if (moved && movement != MovementType.NONE)
                    {
                        actionIndex++;
                        //loop if end of action sequence
                        if (actionIndex >= actions.Count)
                            actionIndex = 0;
                    }

                    actionCoolDown = speed; //we want to wait 'speed' steps in between each movement
                }
                //if we have a wander rectangle to stay in
                else if (movement == MovementType.WANDER)
                {
                    int random = r.Next(9);

                    switch (random)
                    {
                        case 0: //move up
                            if (tileCoords.Y - 1 >= zone.globalY + wanderArea.Y)
                            {
                                changeFacingDirection("Up");
                                standCoolDown = 0;
                                tryToMove("Up");
                            }
                            break;
                        case 1: //move down
                            if (tileCoords.Y + 1 < zone.globalY + wanderArea.Y + wanderArea.Height)
                            {
                                changeFacingDirection("Down");
                                standCoolDown = 0;
                                tryToMove("Down");
                            }
                            break;
                        case 2: //move left
                            if (tileCoords.X - 1 >= zone.globalX + wanderArea.X)
                            {
                                changeFacingDirection("Left");
                                standCoolDown = 0;
                                tryToMove("Left");
                            }
                            break;
                        case 3: //move right
                            if (tileCoords.X + 1 < zone.globalX + wanderArea.X + wanderArea.Width)
                            {
                                changeFacingDirection("Right");
                                standCoolDown = 0;
                                tryToMove("Right");
                            }
                            break;
                        case 4: //face up
                            changeFacingDirection("Up");
                            break;
                        case 5: //face down
                            changeFacingDirection("Down");
                            break;
                        case 6: //face left
                            changeFacingDirection("Left");
                            break;
                        case 7: //face right
                            changeFacingDirection("Right");
                            break;
                        case 8: //pause
                            break;
                        default:
                            break;
                    }
                    actionCoolDown = 4 * speed;//we use 4*speed so that pauses are noticable
                }
            }
        }

        public void changeFacingDirection(string direction)
        {
            directionMoving = direction;
            switch (direction)
            {
                case "Up": facing = FacingDirection.North; break;
                case "Down": facing = FacingDirection.South; break;
                case "Left": facing = FacingDirection.West; break;
                case "Right": facing = FacingDirection.East; break;
                default: break;
            }

            standCoolDown = standCoolMax; //halts the player briefly before being able to start moving in that direction
        }

        private void startMove(String direction)
        {
            //bounds check for current tile (in case of zone change)
            bool boundsCheck = (tileCoords.X >= 0 && tileCoords.Y >= 0 &&
                                tileCoords.X < GameScreen.world.currentArea.mapWidth &&
                                tileCoords.Y < GameScreen.world.currentArea.mapHeight);

            //emulates the switching of direction before actually moving, so movement isnt always necessary
            if (directionMoving == direction)
            {
                isMoving = true;
                //set tile occupied, and old tile unoccupied
                switch (direction)
                {
                    case "Up":
                        {
                            if (tileCoords.Y - 1 >= 0 && tileCoords.Y < GameScreen.world.currentArea.mapHeight)
                            {
                                GameScreen.Map.tile[tileCoords.X, tileCoords.Y - 1].setOccupied(true);
                                if(boundsCheck)
                                    GameScreen.Map.tile[tileCoords.X, tileCoords.Y].setOccupied(false);
                            }
                            break;
                        }
                    case "Down":
                        {
                            if (tileCoords.Y + 1 >= 0 && tileCoords.Y < GameScreen.world.currentArea.mapHeight)
                            {
                                GameScreen.Map.tile[tileCoords.X, tileCoords.Y + 1].setOccupied(true);
                                if(boundsCheck)
                                    GameScreen.Map.tile[tileCoords.X, tileCoords.Y].setOccupied(false);
                            }
                            break;
                        }
                    case "Left":
                        {
                            if (tileCoords.X - 1 >= 0 && tileCoords.X < GameScreen.world.currentArea.mapWidth)
                            {
                                GameScreen.Map.tile[tileCoords.X - 1, tileCoords.Y].setOccupied(true);
                                if(boundsCheck)
                                    GameScreen.Map.tile[tileCoords.X, tileCoords.Y].setOccupied(false);
                            }
                            break;
                        }
                    case "Right":
                        {
                            if (tileCoords.X + 1 >= 0 && tileCoords.X < GameScreen.world.currentArea.mapWidth)
                            {
                                GameScreen.Map.tile[tileCoords.X + 1, tileCoords.Y].setOccupied(true);
                                if(boundsCheck)
                                    GameScreen.Map.tile[tileCoords.X, tileCoords.Y].setOccupied(false);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                changeFacingDirection(direction);
            }

            

            //switches feet after a move
            leftFoot = leftFoot ? false : true;
        }

        public void SetWanderArea(Rectangle area)
        {
            movement = MovementType.WANDER;
            actions = null;
            wanderArea = area;
        }

        /// <summary>
        /// Adds an action to the list of actions to perform on a loop
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(Action action)
        {
            if (movement != MovementType.PATH)
            {
                actionIndex = 0;
                movement = MovementType.PATH;
            }
            if (actions == null)
            {
                actions = new List<Action>();
            }
            actions.Add(action);
        }

        /// <summary>
        /// Removes the last action on the list of actions
        /// </summary>
        public void RemoveLastAction()
        {
            if (actions != null)
            {
                actions.RemoveAt(actions.Count - 1);
            }
        }

        public void SetNoMovement()
        {
            actions = null;
            movement = MovementType.NONE;
        }

    }
}


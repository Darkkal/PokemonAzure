using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PokeEngine.Map;
using PokeEngine.Trainers;
using PokeEngine.Menu;
using LuaInterface;
using System.Threading.Tasks;

namespace PokeEngine.Screens
{
    public class CutSceneScreen : Screen
    {
        public Zone map
        {
            get { return world.currentArea; }
        }

        public World world;

        private bool ended; //whether the scene has ended or not
        public int sceneTime; //time is in update cycles, increments each time update is called
        private List<TimedCommand> commands; //list of timed commands
        private int commandIndex; //the command we are up to (they are sorted)
        private int length; //the maximum number of update cycles that are allowed to pass before the scene ends
        private List<NPC> NPCs; //the list of NPCs, each with their respective action lists
        private Player player;
        private Vector3 viewLocation; //where we are pointing the camera during the cutscene
        private Lua lua;

        private bool hideNPCs;
        private bool hidePlayer;

        //  widescreen animation stuff
        private Rectangle topBarRect;
        private Rectangle bottomBarRect;
        private Texture2D barTexture;

        public CutSceneScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f, World inWorld, Player inPlayer, CutScene scene) : base (g,c,f)
        {
            world = inWorld;
            player = inPlayer;

            lua = new Lua();

            //  widescreen animation stuff
            Color[] color = { new Color(0, 0, 0) };
            barTexture = new Texture2D(g.GraphicsDevice, 1, 1);
            barTexture.SetData<Color>(color);
            topBarRect = new Rectangle(0, -40, 800, 40);
            bottomBarRect = new Rectangle(0, 480, 800, 40);

            NewCutScene(scene);
        }

        private void SortCommands()
        {
            commands.Sort();
        }

        public void NewCutScene(CutScene scene)
        {
            player.actions = scene.playerActions;
            player.movement = MovementType.PATH;
            NewCutScene(scene.commands, scene.NPCs, scene.length);
        }

        private void NewCutScene(List<TimedCommand> inCommands, List<NPC> inNPCs, int inLength)
        {
            hideNPCs = false;
            hideNPCs = false;
            commandIndex = 0;
            sceneTime = 0;
            commands = inCommands;
            length = inLength;
            ended = false;

            //find location to view the scene from initially (make it the player's position)
            float moveRatio = (float)player.movementIndex / (float)player.speed;
            viewLocation = new Vector3();
            viewLocation.X = map.globalX * 32 + player.tileCoords.X * 32 + moveRatio * 32 * (player.nextTile.X - player.tileCoords.X);
            viewLocation.Y = map.globalY * 32 + player.tileCoords.Y * 32 + moveRatio * 32 * (player.nextTile.Y - player.tileCoords.Y);
            viewLocation.Z = (float)player.currentZ + moveRatio * ((float)map.tile[player.nextTile.X, player.nextTile.Y].Z - (float)player.currentZ);

            if (inLength <= 0)
                length = Int32.MaxValue; //set value to max if not valid time

            if (commands == null)
            {
                commands = new List<TimedCommand>();
            }
            SortCommands();

            NPCs = inNPCs;
            if (NPCs == null)
            {
                NPCs = new List<NPC>();
            }
            map.trainerList = NPCs; //replace the npc list on the current area
                                    //when the scene ends we'll leave it as it is

            //set all NPCs movement cooldowns to zero
            //and reset action lists
            foreach (NPC npc in NPCs)
            {
                npc.actionCoolDown = 0;
                npc.actionIndex = 0;
            }

        }

        public void EndScene()
        {
            player.movement = MovementType.NONE;
            ended = true;
            topBarRect.Y = -40;
            bottomBarRect.Y = 480;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                GameDraw.DrawAdjacentGround(world, viewLocation);
                GameDraw.DrawScenery(world);
                //NPCs will be drawn with it's own method
                //NPCs from adjacent areas will NOT be drawn
                if (!hideNPCs)
                    GameDraw.DrawNPCs(NPCs, world);
                if (!hidePlayer)
                    GameDraw.DrawUncontroledPlayer(GameScreen.player, world);

                spriteBatch.Draw(barTexture, topBarRect, Color.White);
                spriteBatch.Draw(barTexture, bottomBarRect, Color.White);
            }
        }

        public override void Update(GameTime gametime)
        {
            if (!lua.IsExecuting) //block updates while the dialog box is visible
            {
                if(ended)
                {
                    ScreenHandler.PopScreen();
                }
                else
                {
                    if(sceneTime >= length)
                        ended = true;

                    //update all NPCs in the scene
                    updateNPCs(gametime);
                    updatePlayer(gametime);

                    //perform all timed commands with the current scene time
                    while(commandIndex < commands.Count && commands[commandIndex].time == sceneTime)
                    {
                        Task.Factory.StartNew(() => DoLuaScript(commands[commandIndex].command));
                        commandIndex++;
                    }

                    sceneTime++;
                }
            }

            //  widescreen animation stuff
            if (topBarRect.Bottom < 40)
                topBarRect.Y++;
            if (bottomBarRect.Y > 440)
                bottomBarRect.Y--;
        }

        public override void HandleInput(Microsoft.Xna.Framework.Input.GamePadState gamePadState, Microsoft.Xna.Framework.Input.KeyboardState keyState, Microsoft.Xna.Framework.Input.MouseState mouseState)
        {
            //none?
        }

        private void DoLuaScript(string s)
        {
            lua = new Lua();
            //put in the persistant variables
            TFSH.ScriptVariables.PutVariables(lua);
            //any variables that need to be included in the lua runtime go here
            lua["zone"] = world.currentArea;
            ///////
            LuaRegistrationHelper.TaggedStaticMethods(lua, typeof(TFSH.PokeEngineScriptHelper));
            lua.DoString(s);
            //store any changes to the persistant variables
            TFSH.ScriptVariables.TakeVariables(lua);
        }

        private void updatePlayer(GameTime gameTime)
        {
            if (!player.isMoving)
            {
                //do wander/routine/no movement, it happens every 'speed' steps
                player.GetNextMove(map);
            }

            //if player is moving update their position

            if (player.isMoving)
            {
                player.movementIndex++;
                if (player.movementIndex >= player.speed)
                {
                    player.movementIndex = 0;
                    player.isMoving = false;
                    player.tileCoords = player.nextTile;
                    player.currentZ = map.tile[player.tileCoords.X, player.tileCoords.Y].Z;
                }
            }

            player.Update();
        }

        private void updateNPCs(GameTime gameTime)
        {
            foreach (NPC npc in map.trainerList)
            {
                if (!npc.isMoving)
                {
                    //do wander/routine/no movement, it happens every 'speed' steps
                    npc.GetNextMove(map);
                }

                if (npc.isMoving)
                {
                    //increase the movment index, this is essentially the ratio of how far we are between one tile and another
                    npc.movementIndex++;
                    if (npc.movementIndex >= npc.speed)
                    {
                        //when we reach the max we reset
                        npc.movementIndex = 0;
                        npc.isMoving = false;
                        npc.tileCoords = npc.nextTile;
                        npc.currentZ = map.tile[npc.tileCoords.X, npc.tileCoords.Y].Z;
                    }
                }
                
                //basic update
                npc.Update();

            }
        }


    }

    /// <summary>
    /// A timed command is essentially a small (though it could be big) Lua script which happens at a certain time in the cutscene.
    /// An example would be to display dialog or to change the speed of an NPC
    /// </summary>
    public class TimedCommand : IComparable<TimedCommand>
    {
        internal int time; //time in 'update ticks' to run the command
        internal String command; //the lua script to run

        public TimedCommand()
        {
            time = 0;
            command = "";
        }

        public TimedCommand(int t, String s)
        {
            if (t >= 0)
                time = t;
            else
                time = 0;

            command = s;
        }

        public int CompareTo(TimedCommand inCom)
        {
            //they are equal if time is equal
            //the negative sign is to sort low to high
            return -inCom.time.CompareTo(this.time);
        }
    }

    /// <summary>
    /// Container class for all the information needed to do a cutscene
    /// </summary>
    public class CutScene
    {
        internal String name;
        internal int length;
        internal List<TimedCommand> commands;
        internal List<NPC> NPCs;
        internal List<PokeEngine.Trainers.Action> playerActions;

        public CutScene()
        {
            name = "DefaultName";
            length = 1800;
            commands = new List<TimedCommand>();
            NPCs = new List<NPC>();
            playerActions = new List<Trainers.Action>();
        }

        public CutScene(String name, int length, List<TimedCommand> commands, List<NPC> npcs, List<Trainers.Action> actions)
        {
            this.name = name;
            this.length = length;
            this.commands = commands;
            this.NPCs = npcs;
            this.playerActions = actions;
        }
    }
}
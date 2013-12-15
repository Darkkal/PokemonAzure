using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Map;
using PokeEngine.Input;
using PokeEngine.Menu;
using PC = PokeEngine.Trainers;
using System.Threading.Tasks;
using LuaInterface;
using PokeEngine.Trainers;
using PokeEngine.Pokemon;
using PokeEngine.Moves;
using PokeEngine.Tools;

namespace PokeEngine.Screens
{
    public class GameScreen : Screen
    {
        public static bool StopDrawing { get; set; } //Stops drawing while we are transitioning or something (otherwise nasty things will happen)
        //public enum ActiveMenu { Menu, PokeDex, Trainer, Pokemon, Save, Bag, Options, None }
        private MenuWindow menu;
        private Vector2 MENU_POS = new Vector2(5, 5);
        private const float MENU_PADDING = 5f;
        private bool inMenu;

        private int previousTime;
        private Lua lua;

        //TEST MAP
        public static Zone Map
        {
            get { return world.currentArea; }
        }
        public static World world = new World();
        //TEST PLAYER
        public static PC.Player player;

        public GameScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f) : base(g, c, f)
        {
            //initialize GameDraw
            GameDraw.Initialize(g, c, f);
            GameDraw.drawAdjObjects = true;
            KeyConfig.Initialize();
            StopDrawing = false;
        }

        public void Initialise()
        {
            StartNewGame();
            List<string> optionList = new List<string>();
            optionList.Add("Pokedex");
            optionList.Add(player.name);
            optionList.Add("Pokemon");
            optionList.Add("Save");
            optionList.Add("Bag");
            optionList.Add("Options");
            menu = new MenuWindow(MENU_POS, optionList, MENU_PADDING);
            inMenu = false;
            Name = "GameScreen";
        }

        public void StartNewGame()
        {
            //we want to load in the game initialization settings
            TFSH.PokeEngineScriptHelper.Initialize(graphics,content,font,world);
            lua = new Lua();

            previousTime = 0;

            //MAKE TEST MAP
            world.getBounds();
            world.changeZone("bedroom");
            //GameDraw.makeGroundBuffers(map);
            GameDraw.MakeAdjBuffers(world);
            GameDraw.UpdateNPCSpritesheets(world);
            //MAKE PLAYER            
            player = new PC.Player();
            player.name = "EncyKal";
            player.spriteSheet = "overworld.png";
            player.tileCoords = new Point(4, 4);
            loadPlayerTextureSheet();
            //TFSH.PokeEngineScriptHelper.setMap("test", 6, 6);
            for (int i = 0; i < player.IdentifiedPokemon.Length; i++)
                if (i % 3 != 0)
                    player.IdentifiedPokemon[i] = true;
            //world.currentArea.tile[6, 6].setOccupied(true);

            GameDraw.UpdateNPCSpritesheets(world);

            //PokedexScreen.SetUpPokedex(player);
        }

        private void loadPlayerTextureSheet()
        {
            player.textureSheet = SaveLoad.LoadTexture2D(@"Content\Sprites\Player\" + player.spriteSheet, graphics.GraphicsDevice);

            //Now we want to apply transparency
            //first get the raw data from the image
            Color[] gottenColour = new Color[player.textureSheet.Width * player.textureSheet.Height];
            player.textureSheet.GetData<Color>(gottenColour);
            //sample the pixel at 5,5 to get the transparent colour to use
            Color sample = new Color(gottenColour[player.textureSheet.Width * 5 + 5].R,
                                    gottenColour[player.textureSheet.Width * 5 + 5].G,
                                    gottenColour[player.textureSheet.Width * 5 + 5].B,
                                    gottenColour[player.textureSheet.Width * 5 + 5].A);
            for (int i = 0; i < player.textureSheet.Width * player.textureSheet.Height - 1; i++)
            {
                if (gottenColour[i].R == sample.R &&
                    gottenColour[i].G == sample.G &&
                    gottenColour[i].B == sample.B)
                {
                    gottenColour[i].R = gottenColour[i].G = gottenColour[i].B = gottenColour[i].A = 0;
                }
            }
            //get rid of black edges around sprite
            Color invisible = new Color(0f, 0f, 0f, 0f);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int i = 0;
                    //top
                    for (i = 0; i < 34; i++)
                        gottenColour[(y * 5 + y * 32 + 4) * player.textureSheet.Width + (x * 5 + x * 32 + 4) + i] = invisible;
                    //bottom
                    for (i = 0; i < 34; i++)
                        gottenColour[(y * 5 + (y + 1) * 32 + 5) * player.textureSheet.Width + (x * 5 + x * 32 + 4) + i] = invisible;
                    //left
                    for (i = 0; i < 34; i++)
                        gottenColour[(y * 5 + y * 32 + 4 + i) * player.textureSheet.Width + (x * 5 + x * 32 + 4)] = invisible;
                    //right
                    for (i = 0; i < 34; i++)
                        gottenColour[(y * 5 + y * 32 + 4 + i) * player.textureSheet.Width + (x * 5 + (x + 1) * 32 + 5)] = invisible;
                }
            }
            player.textureSheet.SetData<Color>(gottenColour);
        }

        /// <summary>
        /// This method is called once per frame (60fps is desired) to update logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
            UpdateNPCs(gameTime);

            previousTime = gameTime.TotalGameTime.Seconds;
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            //if player is moving update their position
            
            if (player.isMoving)
            {
                player.movementIndex++;
                if (player.movementIndex >= player.speed)
                {
                    player.movementIndex = 0;
                    player.isMoving = false;
                    player.tileCoords = player.nextTile;
                    player.currentZ = Map.tile[player.tileCoords.X, player.tileCoords.Y].Z;
                    //run the event on the tile, if any
                    runEvent(Map.tile[player.nextTile.X, player.nextTile.Y].eventScript);
                }
            }

            player.Update();
        }

        private void UpdateNPCs(GameTime gameTime)
        {
            foreach (NPC npc in Map.trainerList)
            {
                if (!npc.isMoving)
                {
                    //do wander/routine/no movement, it happens every 'speed' steps
                    npc.GetNextMove(Map);
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
                        npc.currentZ = Map.tile[npc.tileCoords.X, npc.tileCoords.Y].Z;
                    }
                }
                //basic update
                npc.Update();
            }
        }

        /// <summary>
        /// This method is called once per frame (60fps is desired) to draw.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (IsVisible && !StopDrawing) //StopDrawing is used while transitioning zones
            {
                //updateSpriteLocation(player);
                //graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
                //GameDraw.drawGround(map, player);            
                lock (TFSH.PokeEngineScriptHelper.lockObject)
                {
                    GameDraw.DrawAdjacentGround(world, player);
                    GameDraw.DrawScenery(world);
                    GameDraw.DrawPlayer(player, world, player.spritePosition, new Rectangle(0, 0, 116, 153));
                    GameDraw.DrawNPCs(world);
                }

                if (inMenu)
                    menu.Draw(spriteBatch, font, Color.White);
            }
        }

        private void updateSpriteLocation(PC.NPC inNPC)
        {
            
            Rectangle rect;
            switch (inNPC.facing)
            {
                case PC.FacingDirection.North:
                    rect = new Rectangle(5, 5, 32, 32);
                    break;
                case PC.FacingDirection.East:
                    rect = new Rectangle(5, 116, 32, 32);
                    break;
                case PC.FacingDirection.South:
                    rect = new Rectangle(5, 42, 32, 32);
                    break;
                case PC.FacingDirection.West:
                    rect = new Rectangle(5, 79, 32, 32);
                    break;
                default:
                    rect = new Rectangle(5, 116, 32, 32);
                    break;
            }
            inNPC.spritePosition = rect;
            
        }

        /// <summary>
        /// Handle the inputs
        /// </summary>
        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
           
            if (!player.isMoving && !lua.IsExecuting)
            {
                if (!inMenu)
                {
                    //DEBUG BATTLE ENGINE TESTING
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.B, 10))
                    {
                        //testing battle engine here
                        /////////////////////////////
                        //startBattle();
                        /////////////////////////////
                    }
                    //DEBUG BCUT SCENE TESTING
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.C, 10))
                    {
                        //testing cut scenes here
                        /////////////////////////////
                        StartCutScene();
                        /////////////////////////////
                    }
                    //DEBUG BCUT SCENE TESTING
                    if (Input.InputHandler.WasKeyPressed(keyState, Keys.M, 10))
                    {
                        //testing cut scenes here
                        /////////////////////////////
                        StartCinematic();
                        /////////////////////////////
                    }

                    //run!
                    if (keyState.IsKeyDown(KeyConfig.Cancel))
                        player.speed = 7;
                    else
                        player.speed = (int)MOVESPEED.NORMAL;
                    

                    if (keyState.IsKeyDown(KeyConfig.Left))
                        player.tryToMove("Left");
                    else if (keyState.IsKeyDown(KeyConfig.Right))
                        player.tryToMove("Right");
                    else if (keyState.IsKeyDown(KeyConfig.Up))
                        player.tryToMove("Up");
                    else if (keyState.IsKeyDown(KeyConfig.Down))
                        player.tryToMove("Down");

                    //if the action key is pressed while on the game world
                    if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.Action))
                    {
                        switch (player.facing)
                        {
                            case PC.FacingDirection.East:
                                interactWithTile(player.tileCoords.X + 1, player.tileCoords.Y);
                                interectWithNPC(player.tileCoords.X + 1, player.tileCoords.Y);
                                break;
                            case PC.FacingDirection.West:
                                interactWithTile(player.tileCoords.X - 1, player.tileCoords.Y);
                                interectWithNPC(player.tileCoords.X - 1, player.tileCoords.Y);
                                break;
                            case PC.FacingDirection.North:
                                interactWithTile(player.tileCoords.X, player.tileCoords.Y - 1);
                                interectWithNPC(player.tileCoords.X, player.tileCoords.Y - 1);
                                break;
                            case PC.FacingDirection.South:
                                interactWithTile(player.tileCoords.X, player.tileCoords.Y + 1);
                                interectWithNPC(player.tileCoords.X, player.tileCoords.Y + 1);
                                break;
                        }
                    }
                    //more key handles here


                    if (mouseState.LeftButton == ButtonState.Pressed) //zoom in
                        GameDraw.cameraOffset -= new Vector3(0, 0.6f, 0.3f);
                    if (mouseState.RightButton == ButtonState.Pressed) //zoom out
                        GameDraw.cameraOffset += new Vector3(0, 0.6f, 0.3f);
                    if (mouseState.MiddleButton == ButtonState.Pressed) //reset camera
                        GameDraw.cameraOffset = new Vector3(0.0f, 30.0f, 26.0f);

                    if(InputHandler.WasKeyPressed(keyState,KeyConfig.Menu, 10))
                        inMenu = true;

                }
                else
                {

                    if (InputHandler.WasKeyPressed(keyState, KeyConfig.Cancel, 10))
                        inMenu = false;
                    if (InputHandler.WasKeyPressed(keyState, KeyConfig.Down, 10))
                        menu.SelectionDown();
                    if (InputHandler.WasKeyPressed(keyState, KeyConfig.Up, 10))
                        menu.SelectionUp();
                    if (InputHandler.WasKeyPressed(keyState, KeyConfig.Action, 10))
                    {
                        switch (menu.GetSelection())
                        {
                            //  pokedex
                            case 0: ScreenHandler.PushScreen(new PokedexScreen(graphics, content, font, player)); break;
                            //  trainer
                            case 1: ScreenHandler.PushScreen(new Pokemon_InventoryScreen(graphics, content, font, player)); break;
                            case 2:
                            //  bag
                            case 3:
                            //  save
                            case 4: break;
                            //  options
                            case 5: ScreenHandler.PushScreen(new OptionScreen(graphics, content, font)); break;
                        }
                    }
                }
            }   
        }

        /// <summary>
        /// Runs the interact script for the object which is in the given tile
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        private void interactWithTile(int x, int y)
        {

            //if it's in the map's boundaries
            if ((x >= 0 && y >= 0) && (x < Map.mapWidth && y < Map.mapHeight))
            {
                //make sure there IS a scenery object to interact with
                if (Map.tile[x, y].sceneryObject != null)
                {
                    Task.Factory.StartNew(() => DoLuaScript(Map.tile[x, y].sceneryObject.interactScript)); //threading because I am too lazy to queue everything
                }
            }
        }

        /// <summary>
        /// Runs the NPC's script with the given tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void interectWithNPC(int x, int y)
        {
            int iX = Convert.ToInt32(x);
            int iY = Convert.ToInt32(y);

            //if it's in the map's boundaries
            if ((iX >= 0 && iY >= 0) && (iX < Map.mapWidth && iY < Map.mapHeight))
            {
                NPC activeNPC = Map.GetNPCAtLocation(x, y);
                //make sure there IS an NPC to interact with
                if (activeNPC != null)
                {
                    if (!activeNPC.isMoving)
                    {
                        //stop and turn to face the player
                        if (player.facing == FacingDirection.North)
                            activeNPC.facing = FacingDirection.South;
                        else if (player.facing == FacingDirection.South)
                            activeNPC.facing = FacingDirection.North;
                        else if (player.facing == FacingDirection.East)
                            activeNPC.facing = FacingDirection.West;
                        else if (player.facing == FacingDirection.West)
                            activeNPC.facing = FacingDirection.East;

                        Task.Factory.StartNew(() => DoLuaScript(Map.GetNPCAtLocation(x, y).interactScript));
                    }
                }
            }
        }

        private void DoLuaScript(string s)
        {
            lua = new Lua();
            //put in the persistant variables
            TFSH.ScriptVariables.PutVariables(lua);
            LuaRegistrationHelper.TaggedStaticMethods(lua, typeof(TFSH.PokeEngineScriptHelper));
            lua["player"] = player;
            lua.DoString(s);
            //store any changes to the persistant variables
            TFSH.ScriptVariables.TakeVariables(lua);
        }
        
        /// <summary>
        /// Runs an event on a tile
        /// </summary>
        /// <param name="script">script to run</param>
        private void runEvent(String script)
        {
            if (script != null)
            {
                if (script != "")
                {
                    Task.Factory.StartNew(() => DoLuaScript(script)); //threading because I am too lazy to queue everything
                }
            }
        }

        /// <summary>
        /// Runs the zone script, called when changing zones
        /// </summary>
        /// <param name="script"></param>
        internal void RunZoneScript(String script)
        {
            if (!String.IsNullOrWhiteSpace(script))
            {
                Task.Factory.StartNew(() => DoLuaScript(script)); //threading because I am too lazy to queue everything
            }
        }

        /// <summary>
        /// Testing battle engine
        /// </summary>
        private void startBattle()
        {
            Trainer trainer = new Trainer();

            //BaseStatsList.initialize();


            BaseMove tackle = new BaseMove("Tackle", "hits the opponent hard", 50, 100, "Normal", "Physical", 35);
            tackle.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove scratch = new BaseMove("Scratch", "hits the opponent hard", 10, 100, "Normal", "Physical", 35);
            scratch.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove bubble = new BaseMove("Bubble", "waters the opponent", 20, 100, "Water", "Special", 30);
            bubble.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";
            BaseMove ember = new BaseMove("Ember", "fires the opponent", 40, 100, "Fire", "Special", 25);
            ember.moveScript = @"  if user:hits(move, target) then
                                        user:doDamageTo(move, target)
                                    end
                                ";



            ActivePokemon charmander = new ActivePokemon(BaseStatsList.basestats);
            ActivePokemon squirtle = new ActivePokemon(BaseStatsList.basestats);
            charmander.baseStat = BaseStatsList.GetBaseStats("Charmander");
            charmander.level = 20;
            charmander.currentHP = charmander.HP;
            charmander.setNickname("Charmander");
            charmander.addExp(charmander.expAtLevel(charmander.level) - 1);
            squirtle.baseStat = BaseStatsList.GetBaseStats("Squirtle");
            squirtle.level = 20;
            squirtle.currentHP = squirtle.HP;
            squirtle.setNickname("Squirtle");
            squirtle.addExp(squirtle.expAtLevel(squirtle.level) - 1);


            charmander.move[0] = new ActiveMove(scratch);
            charmander.move[1] = new ActiveMove(ember);
            squirtle.move[0] = new ActiveMove(bubble);
            squirtle.move[1] = new ActiveMove(tackle);

            player.addPokemon(squirtle);
            trainer.addPokemon(charmander);

            ScreenHandler.TopScreen.IsVisible = false;
            ScreenHandler.PushScreen(new BattleScreen(graphics, content, font, player, trainer));
        }

        private void StartCutScene()
        {
            CutScene scene = new CutScene();
            scene.length = -1;
            scene.name = "Test Scene";

            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.LEFT);
            scene.playerActions.Add(PC.Action.RIGHT);
            scene.playerActions.Add(PC.Action.STOP);

            //set up some actions
            for (int i = 0; i < 11; i++)
            {
                Map.trainerList[0].AddAction(PC.Action.LEFT);
                Map.trainerList[1].AddAction(PC.Action.RIGHT);
            }
            Map.trainerList[0].AddAction(PC.Action.FACEUP);
            Map.trainerList[0].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[0].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[0].AddAction(PC.Action.FACELEFT);
            Map.trainerList[1].AddAction(PC.Action.FACEUP);
            Map.trainerList[1].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[1].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[1].AddAction(PC.Action.FACELEFT);
            for (int i = 0; i < 3; i++)
            {
                Map.trainerList[0].AddAction(PC.Action.DOWN);
                Map.trainerList[1].AddAction(PC.Action.UP);
            }
            Map.trainerList[0].AddAction(PC.Action.FACEUP);
            Map.trainerList[0].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[0].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[0].AddAction(PC.Action.FACELEFT);
            Map.trainerList[1].AddAction(PC.Action.FACEUP);
            Map.trainerList[1].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[1].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[1].AddAction(PC.Action.FACELEFT);
            for (int i = 0; i < 11; i++)
            {
                Map.trainerList[0].AddAction(PC.Action.RIGHT);
                Map.trainerList[1].AddAction(PC.Action.LEFT);
            }
            Map.trainerList[0].AddAction(PC.Action.FACEUP);
            Map.trainerList[0].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[0].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[0].AddAction(PC.Action.FACELEFT);
            Map.trainerList[1].AddAction(PC.Action.FACEUP);
            Map.trainerList[1].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[1].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[1].AddAction(PC.Action.FACELEFT);
            for (int i = 0; i < 3; i++)
            {
                Map.trainerList[0].AddAction(PC.Action.UP);
                Map.trainerList[1].AddAction(PC.Action.DOWN);
            }
            Map.trainerList[0].AddAction(PC.Action.FACEUP);
            Map.trainerList[0].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[0].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[0].AddAction(PC.Action.FACELEFT);
            Map.trainerList[1].AddAction(PC.Action.FACEUP);
            Map.trainerList[1].AddAction(PC.Action.FACERIGHT);
            Map.trainerList[1].AddAction(PC.Action.FACEDOWN);
            Map.trainerList[1].AddAction(PC.Action.FACELEFT);

            Map.trainerList[0].AddAction(PC.Action.STOP);
            Map.trainerList[1].AddAction(PC.Action.STOP);

            scene.NPCs = Map.trainerList;

            Trainer temp = new Trainer(player);

            //start up the cutscene
            List<TimedCommand> commands = new List<TimedCommand>();
            commands.Add(new TimedCommand(10 * Map.trainerList[0].speed, "ShowMessage(\"TESTING\")"));
            commands.Add(new TimedCommand(45 * Map.trainerList[0].speed, "EndCutScene()"));
            commands.Add(new TimedCommand(0, "WaitTill(\"Lady\", 150)"));
            commands.Add(new TimedCommand(140, "WaitTill(\"Boy\", 20)"));

            scene.commands = commands;
            ScreenHandler.PushScreen(new CutSceneScreen(graphics, content, font, world, player, scene));
        }

        private void StartCinematic()
        {
            List<CinematicAction> actions = new List<CinematicAction>();

            actions.Add(new CinematicAction(new Vector2(100, 256),
                                            new Vector2(300, 256),
                                            1f, 1.1f,
                                            600,
                                            null,
                                            "2j11yjd.jpg",
                                            false, true));
            actions.Add(new CinematicAction(new Vector2(20, 20),
                                            new Vector2(50, 400),
                                            2f, 1f,
                                            400,
                                            null,
                                            "281.jpg",
                                            false, true));
            actions.Add(new CinematicAction(new Vector2(50, 400),
                                            new Vector2(100, 400),
                                            1f, 1.5f,
                                            200,
                                            "test message",
                                            "281.jpg",
                                            true, true));

            ScreenHandler.PushScreen(new CinematicScreen(graphics, content, font, actions));
        }
    }
}

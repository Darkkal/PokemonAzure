using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
using PokeEngine.Screens;
using PokeEngine.Map;
using PokeEngine.Pokemon;
using LiveMapMaker.Tools;
using Pokemon_Base_Stats_Editor;

namespace LiveMapMaker
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public int selectedX, selectedY;
        GraphicsDeviceManager graphics;
        internal SpriteBatch spriteBatch;
        SpriteFont font;
        public World world { get { return GameScreen.world; } set { GameScreen.world = value; } }
        Effect simpleEffect;
        public bool screenShot;
        RenderTarget2D renderTarget;
        Editor form;

        public Game1(Editor form)
        {
            this.form = form;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            form.Show();
            form.game = this;
            IsMouseVisible = true;
            screenShot = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();
            //initialize the various tools we'll use
            EditorScreen.initialize(form, this);
            TileTool.InitializeTool(this, form);
            ModelTool.InitializeTool(this, form);
            NPCTool.InitializeTool(this, form);
            
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            simpleEffect = Content.Load<Effect>("simpleEffect");

            // initialize the screenhandler. need to do it here for the font.
            // ScreenHandler.Initialize(graphics, Content, font);

            if (!Directory.Exists(Application.StartupPath + "\\TileSets"))
                downloadTileSets();

            ScreenHandler.Initialize(graphics, Content, font);
            ScreenHandler.SwitchScreen(new GameScreen(graphics, Content, font));

            world = new World();
            Zone zone = new Zone(2,2);
            world.addZone(zone);
            world.changeZone(zone);


            DialogBox.Initialise(graphics, Content, font);
            GameDraw.Initialize(graphics, Content, font);
            GameDraw.drawAdjObjects = true;
            GameDraw.MakeAdjBuffers(world);

            BaseStatsList.initialize(); //  We need this so we can edit trainer pokemon in the map maker

            // TODO: use this.Content to load your game content here
        }

        #region update and draw

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //update the EditorScreen stuff if the window is focused
            Form xnaForm = (Form)Control.FromHandle(Window.Handle);
            if (xnaForm.Focused)
            {
                EditorScreen.update(gameTime, world);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (screenShot)
            {
                renderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                GraphicsDevice.SetRenderTarget(renderTarget);
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);
            EditorScreen.draw(world);

            //finds which tile is being hovered over
            findMouseoverTile();
            //draws border around hovered tile
            drawSelectedBorder();
            //draw accessibility guides if selected
            if (form.showAccessibilityToolStripMenuItem.Checked)
            {
                drawAccessibility();
            }
            if (form.showScriptsToolStripMenuItem.Checked)
            {
                drawScripts();
            }

            //update position of temporary selected model and draw at mouse position if we have one selected
            if (form.toolTab.SelectedTab == form.SceneryTab)
            {
                if (form.sceneryBox.SelectedItem != null && selectedX != -1 && selectedY != -1 && form.modelNewRadio.Checked)
                {
                    Scenery temp = form.sceneryList[(String)form.sceneryBox.SelectedItem];
                    temp.position = new Point(selectedX, selectedY);

                    drawModelBase(temp);
                    GameDraw.DrawScenery(temp, world.currentArea.globalX, world.currentArea.globalY, world.currentArea.tile[selectedX,selectedY].Z);
                }
                else if (!form.editingBaseScenery && selectedX != -1 && selectedY != -1 && form.modelEditRadio.Checked)
                {
                    Scenery temp = form.activeSceneryEdit;
                    if (temp != null && ModelTool.carryingScenery)
                    {
                        temp.position = new Point(selectedX, selectedY);
                        drawModelBase(temp);
                        GameDraw.DrawScenery(temp, world.currentArea.globalX, world.currentArea.globalY, world.currentArea.tile[selectedX, selectedY].Z);
                    }
                }
            }

            if (form.toolTab.SelectedTab == form.NPCTab)
            {
                DrawWanderArea();
            }

            if (form.toolTab.SelectedTab == form.TileSpritesTab)
            {
                DrawTileArea();
            }

            spriteBatch.End();
          
            base.Draw(gameTime);

            /*
            if (screenShot)
                takeScreenShot(); <--- it's not working =[
             */
        }

        

        #endregion

        /// <summary>
        /// Finds the square which you are mousing over and updates the selectedX and selectedY
        /// </summary>
        private void findMouseoverTile()
        {
            //defaults values
            selectedX = selectedY = -1;

            Form xnaForm = (Form)Control.FromHandle(Window.Handle);
            if(xnaForm.Focused)
            {
                //get mouse state
                MouseState mouseState = Mouse.GetState();            
                Vector3 near, direction;
            
                //calculate the ray
                near = GraphicsDevice.Viewport.Unproject(new Vector3(mouseState.X, mouseState.Y, 0f), GameDraw.projection, GameDraw.view, GameDraw.world);
                direction = GraphicsDevice.Viewport.Unproject(new Vector3(mouseState.X, mouseState.Y, 1f), GameDraw.projection, GameDraw.view, GameDraw.world) - near;
                direction.Normalize();

                Ray cursorRay = new Ray(near, direction);

                //using the ray find out which tile in the current map we are pointing to
                for (int x = 0; x < world.currentArea.mapWidth; x++)
                {
                    for (int y = 0; y < world.currentArea.mapHeight; y++)
                    {
                        float distance = (((float)world.currentArea.tile[x, y].Z/8) - cursorRay.Position.Y) / cursorRay.Direction.Y; //Y is "height" when drawing
                        Vector3 temp = cursorRay.Position + Vector3.Multiply(cursorRay.Direction, distance);
                        temp.Y = temp.Y * (-1);
                        //form.updateDebug(temp.X + ", " + temp.Z);

                        if    (temp.X < (world.currentArea.globalX + x) * 1f + 0.5f
                            && temp.X > (world.currentArea.globalX + x) * 1f - 0.5f
                            && temp.Z < (world.currentArea.globalY + y) * 1f + 0.5f
                            && temp.Z > (world.currentArea.globalY + y) * 1f - 0.5f)
                        {                       
                            selectedX = x;
                            selectedY = y;
                        }
                    }
                }
            }
            form.updateDebug(selectedX + ", " + selectedY + ", " + (selectedX != -1 ? world.currentArea.tile[selectedX, selectedY].Z : -1));
            form.updateGlobalDebug((world.currentArea.globalX + selectedX) + ", " + (world.currentArea.globalY + selectedY) + ", " + (selectedX != -1 ? world.currentArea.tile[selectedX, selectedY].Z : -1));
        }

        /// <summary>
        /// Draws the border around the tile that is currently selected
        /// </summary>
        private void drawSelectedBorder()
        {
            //don't draw if none is selected, obviously
            if (selectedX != -1 && selectedY != -1)
            {
                Vector3 position = new Vector3((world.currentArea.globalX + selectedX) , (float)world.currentArea.tile[selectedX, selectedY].Z/8, (world.currentArea.globalY + selectedY) );
                drawOnscreenRectangle(position, Color.White);

            }
        }

        /// <summary>
        /// draws outline around the squares that will be inhabited by the model
        /// </summary>
        private void drawModelBase(Scenery s)
        {
            //size must be bigger than zero, obviously
            if (s.size.X > 0 && s.size.Y > 0)
            {
                //loop through each tile in the scenery's size
                for (int x = 0; x < s.size.X; x++)
                {
                    for (int y = 0; y < s.size.Y; y++)
                    {
                        if (selectedX + x < world.currentArea.mapWidth && selectedY + y < world.currentArea.mapHeight)
                        {
                            Vector3 position = new Vector3((world.currentArea.globalX + selectedX + x) , world.currentArea.tile[selectedX + x, selectedY + y].Z, (world.currentArea.globalY + selectedY + y) );
                            //draw a rectangle on the tiles
                            drawOnscreenRectangle(position, Color.Wheat);
                        }

                    }
                }
            }
        }

        private void DrawTileArea()
        {
            if (form.selectedTiles.Count < 1)
                return;

            foreach (SelectedTile s in form.selectedTiles)
            {
                int currX = selectedX + s.PositionX - form.selectedTiles[0].PositionX;
                int currY = selectedY + s.PositionY - form.selectedTiles[0].PositionY;
                if (currX >= 0 && currX < world.currentArea.mapWidth &&
                    currY >= 0 && currY < world.currentArea.mapHeight)
                {
                    drawOnscreenRectangle(new Vector3((world.currentArea.globalX + currX), (float)world.currentArea.tile[ currX, currY].Z/8, (world.currentArea.globalY + currY)), Color.Teal);
                }

            }
        }

        /// <summary>
        /// Actually draws the outline around a selected tile
        /// </summary>
        private void drawOnscreenRectangle(Vector3 position, Color colour)
        {
            VertexPositionColor[] pointList = new VertexPositionColor[4];


            pointList[0] = new VertexPositionColor(position + new Vector3(-0.5f, 0.0f, -0.5f), colour);
            pointList[1] = new VertexPositionColor(position + new Vector3(0.5f, 0.0f, -0.5f), colour);
            pointList[2] = new VertexPositionColor(position + new Vector3(0.5f, 0f, 0.5f), colour);
            pointList[3] = new VertexPositionColor(position + new Vector3(-0.5f, 0f, 0.5f), colour);

            short[] lineStripIndices = new short[5];
            lineStripIndices[0] = 0;
            lineStripIndices[1] = 1;
            lineStripIndices[2] = 2;
            lineStripIndices[3] = 3;
            lineStripIndices[4] = 0;

            simpleEffect.Parameters["World"].SetValue(GameDraw.world);
            simpleEffect.Parameters["View"].SetValue(GameDraw.view);
            simpleEffect.Parameters["Projection"].SetValue(GameDraw.projection);

            simpleEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.LineStrip,
                pointList,
                0,   // vertex buffer offset to add to each element of the index buffer
                4,   // number of vertices to draw
                lineStripIndices,
                0,   // first index element to read
                4    // number of primitives to draw
            );
        }

        //Draws lines representing accesibility for each tile
        private void drawAccessibility()
        {
            //loop through each tile
            for(int x = 0; x < world.currentArea.mapWidth; x++)
            {
                for(int y = 0; y < world.currentArea.mapHeight; y++)
                {
                    VertexPositionColor[] pointList = new VertexPositionColor[8];

                    Vector3 position = new Vector3((world.currentArea.globalX + x), (float)world.currentArea.tile[x, y].Z/8, (world.currentArea.globalY + y));
                    pointList[0] = new VertexPositionColor(position + new Vector3(-0.46f, 0f, -0.46f), Color.Red);
                    pointList[1] = new VertexPositionColor(position + new Vector3(0.46f, 0f, -0.46f), Color.Red);
                    pointList[2] = new VertexPositionColor(position + new Vector3(0.46f, 0f, -0.46f), Color.Red);
                    pointList[3] = new VertexPositionColor(position + new Vector3(0.46f, 0f, 0.46f), Color.Red);
                    pointList[4] = new VertexPositionColor(position + new Vector3(0.46f, 0, 0.46f), Color.Red);
                    pointList[5] = new VertexPositionColor(position + new Vector3(-0.46f, 0, 0.46f), Color.Red);
                    pointList[6] = new VertexPositionColor(position + new Vector3(-0.46f, 0, 0.46f), Color.Red);
                    pointList[7] = new VertexPositionColor(position + new Vector3(-0.46f, 0, -0.46f), Color.Red);

                    short[] lineStripIndices = new short[8];
                    for (short i = 0; i < 8; i++)
                    {
                        lineStripIndices[i] = i;
                    }

                    //set colours for each side
                    if(world.currentArea.tile[x,y].isAccessibleFrom(Direction.North))
                        pointList[0].Color = pointList[1].Color = Color.Green;
                    if (world.currentArea.tile[x, y].isAccessibleFrom(Direction.East))
                        pointList[2].Color = pointList[3].Color = Color.Green;
                    if (world.currentArea.tile[x, y].isAccessibleFrom(Direction.South))
                        pointList[4].Color = pointList[5].Color = Color.Green;
                    if (world.currentArea.tile[x, y].isAccessibleFrom(Direction.West))
                        pointList[6].Color = pointList[7].Color = Color.Green;

                    simpleEffect.Parameters["World"].SetValue(GameDraw.world);
                    simpleEffect.Parameters["View"].SetValue(GameDraw.view);
                    simpleEffect.Parameters["Projection"].SetValue(GameDraw.projection);

                    //and draw
                    simpleEffect.CurrentTechnique.Passes[0].Apply();

                    GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                        PrimitiveType.LineList,
                        pointList,
                        0,   // vertex buffer offset to add to each element of the index buffer
                        8,   // number of vertices to draw
                        lineStripIndices,
                        0,   // first index element to read
                        4    // number of primitives to draw
                    );
                }
            }
        }

        //visibly shows scripts on the tiles
        private void drawScripts()
        {
            //loop through each tile
            for (int x = 0; x < world.currentArea.mapWidth; x++)
            {
                for (int y = 0; y < world.currentArea.mapHeight; y++)
                {
                    //only draw if there is a script on the tile
                    if (world.currentArea.tile[x, y].eventScript != null && world.currentArea.tile[x, y].eventScript != "")
                    {
                        VertexPositionColor[] pointList = new VertexPositionColor[4];

                        Vector3 position = new Vector3((world.currentArea.globalX + x) , world.currentArea.tile[x, y].Z, (world.currentArea.globalY + y) );
                        //this will be an X shape
                        pointList[0] = new VertexPositionColor(position + new Vector3(-0.46f, 0, -0.46f), Color.MintCream);
                        pointList[1] = new VertexPositionColor(position + new Vector3(0.46f, 0, 0.46f), Color.MintCream);
                        pointList[2] = new VertexPositionColor(position + new Vector3(0.46f, 0, -0.46f), Color.MintCream);
                        pointList[3] = new VertexPositionColor(position + new Vector3(-0.46f, 0, 0.46f), Color.MintCream);


                        short[] lineStripIndices = new short[4];
                        for (short i = 0; i < 4; i++)
                        {
                            lineStripIndices[i] = i;
                        }

                        simpleEffect.Parameters["World"].SetValue(GameDraw.world);
                        simpleEffect.Parameters["View"].SetValue(GameDraw.view);
                        simpleEffect.Parameters["Projection"].SetValue(GameDraw.projection);

                        //and draw
                        simpleEffect.CurrentTechnique.Passes[0].Apply();

                        GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                            PrimitiveType.LineList,
                            pointList,
                            0,   // vertex buffer offset to add to each element of the index buffer
                            4,   // number of vertices to draw
                            lineStripIndices,
                            0,   // first index element to read
                            2    // number of primitives to draw
                        );
                    }
                }
            }
        }

        private void DrawWanderArea()
        {
            
            if (form.NPCEditRadio.Checked && NPCTool.toolType == NPCToolType.EditNPC && form.activeNPCEdit != null)
            {
                if (form.activeNPCEdit.movement == PokeEngine.Trainers.MovementType.WANDER)
                {
                    Rectangle rect = form.activeNPCEdit.wanderArea;
                        
                    for (int x = rect.X; x < rect.X + rect.Width; x++)
                    {
                        for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                        {
                            Vector3 position = new Vector3((world.currentArea.globalX + x) , world.currentArea.tile[x, y].Z, (world.currentArea.globalY + y) );
                            drawOnscreenRectangle(position, Color.Cyan);
                        }
                    }
                }
            }
            
        }

        private void downloadTileSets()
        {
            MessageBox.Show("Hold on, downloading tilesets");
            string webDir = "http://www.tenfoldstudios.com/shared/mapmaker/TileSets/";
            string filePath = Application.StartupPath + "\\TileSets";
            Directory.CreateDirectory(filePath);

            System.Net.WebClient wc = new System.Net.WebClient();

            for (int x = 0; x < 43; x++)
                wc.DownloadFile(webDir + x + ".png", filePath + "\\" + x + ".png");
        }

        private void takeScreenShot()
        {
            
            string temp = Application.StartupPath + "\\Screenshots\\" + DateTime.Now.ToString() + ".png";

            FileStream stream = File.OpenWrite(temp);

            renderTarget.SaveAsPng(stream, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            stream.Close();
            stream.Dispose();

            temp = null;
            renderTarget = null;
            GraphicsDevice.SetRenderTarget(null);

            screenShot = false;
             
        }

    }
}

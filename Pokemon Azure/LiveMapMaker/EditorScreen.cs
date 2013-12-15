using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Map;
using PokeEngine.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LiveMapMaker.Tools;

namespace LiveMapMaker
{
    static class EditorScreen
    {
        private static Point previousTileHeightChange;
        public static Vector3 lookAtPosition = new Vector3(6, 6, 0);
        static MouseState prevMouseState;
        static MouseState currentMouseState;
        static KeyboardState prevKeyState;
        static KeyboardState currentKeyState;
        static Editor editor;
        static Game1 game;

        static public void initialize(Editor inEditor, Game1 inGame)
        {
            editor = inEditor;
            game = inGame;
            previousTileHeightChange = new Point(-1, -1);
        }

        static public void update(GameTime gameTime, World world)
        {
            //get current state of keyboard and mouse
            currentMouseState = Mouse.GetState();
            currentKeyState = Keyboard.GetState();

            //keyboard actions
            HandleArrowKeys();

            if (currentKeyState.IsKeyDown(Keys.A))
            {
                lookAtPosition -= new Vector3(GameDraw.cameraOffset.Length()/100, 0, 0);
            }
            if (currentKeyState.IsKeyDown(Keys.D))
            {
                lookAtPosition += new Vector3(GameDraw.cameraOffset.Length()/100, 0, 0);
            }
            if (currentKeyState.IsKeyDown(Keys.S))
            {
                lookAtPosition += new Vector3(0, GameDraw.cameraOffset.Length()/100, 0);
            }
            if (currentKeyState.IsKeyDown(Keys.W))
            {
                lookAtPosition -= new Vector3(0, GameDraw.cameraOffset.Length()/100, 0);
            }
            //undo action
            //check whether a ctrl is down, and the Z key has been pressed (was up, is now down)
            if ((currentKeyState.IsKeyDown(Keys.LeftControl) || currentKeyState.IsKeyDown(Keys.RightControl)) &&
               (currentKeyState.IsKeyDown(Keys.Z) && prevKeyState.IsKeyUp(Keys.Z)))
            {
                //make sure the next history isn't empty
                if (editor.zoneHistory[editor.historyIndex + 1] != null)
                {
                    //increase history index by one if possible
                    editor.historyIndex++;
                    if (editor.historyIndex >= editor.historySize)
                    {
                        editor.historyIndex = editor.historySize - 1;
                    }

                    //change to the historical zone
                    game.world.addZone(editor.zoneHistory[editor.historyIndex]);
                    game.world.changeZone(editor.zoneHistory[editor.historyIndex]);
                    GameDraw.MakeAdjBuffers(game.world);
                                        
                }
            }
            //redo action
            //check whether a ctrl is down, and the Y key has been pressed (was up, is now down)
            if ((currentKeyState.IsKeyDown(Keys.LeftControl) || currentKeyState.IsKeyDown(Keys.RightControl)) &&
               (currentKeyState.IsKeyDown(Keys.Y) && prevKeyState.IsKeyUp(Keys.Y)))
            {
                //make sure there is room for us to redo
                if (editor.historyIndex > 0)
                {
                    editor.historyIndex--;

                    //if so then restore the next zone state
                    game.world.addZone(editor.zoneHistory[editor.historyIndex]);
                    game.world.changeZone(editor.zoneHistory[editor.historyIndex]);
                    GameDraw.MakeAdjBuffers(game.world);
                }
            }
            
            //mouse actions
            {
                //if left mouse is down
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    //check if mouse has been pressed, and if so then save current history
                    //that way you can undo to whatever the state was before the mouse was pressed
                    if (prevMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        updateHistory();
                    }
                    

                    //if we have an actual tile selected do the different options
                    if (game.selectedX != -1 && game.selectedY != -1)
                    {
                        //if we are in the tile section
                        if (editor.toolTab.SelectedTab == editor.TileSpritesTab)
                        {
                            TileTool.ApplyImage();
                        }
                        //if we are in the tile settings tab
                        if (editor.toolTab.SelectedTab == editor.TilePropertiesTab)
                        {
                            TileTool.ApplySettings();
                        }
                        //if we are in the NPC tab
                        if (editor.toolTab.SelectedTab == editor.NPCTab)
                        {
                            if (NPCTool.toolType == NPCToolType.PlaceNPC)
                            {
                                NPCTool.PlaceNPC();
                            }
                            else if (NPCTool.toolType == NPCToolType.SetWanderArea)
                            {
                                //on start setting the wander area
                                if (prevMouseState.LeftButton == ButtonState.Released)
                                {
                                    NPCTool.StartSetWander();
                                }
                            }
                            else if (NPCTool.toolType == NPCToolType.EditNPC)
                            {
                                //on click pick up the NPC
                                if (prevMouseState.LeftButton == ButtonState.Released)
                                {
                                    NPCTool.PickUpNPC();
                                }
                                //update postion of NPC if we have one active
                                if (editor.activeNPCEdit != null)
                                {
                                    //if there are no obstables
                                    if (world.currentArea.tile[game.selectedX, game.selectedY].isClear())
                                        editor.activeNPCEdit.tileCoords = new Point(game.selectedX, game.selectedY);
                                }
                            }
                            else if (NPCTool.toolType == NPCToolType.DeleteNPC)
                            {
                                NPCTool.DeleteNPC();
                            }
                        }
                        //if we are in the model tab
                        if (editor.toolTab.SelectedTab == editor.SceneryTab)
                        {
                            //place a new model if new is selected
                            if (editor.modelNewRadio.Checked)
                            {
                                if (editor.sceneryBox.SelectedItem != null)
                                {
                                    //normal behaviour for restricted placement
                                    if(!editor.unrestrictedPlacementBox.Checked)
                                        ModelTool.PlaceModel();
                                    //only one model per mouse down for unrestricted placement
                                    else if (prevMouseState.LeftButton == ButtonState.Released)
                                    {
                                        ModelTool.PlaceModel();
                                    }
                                }
                            }
                            //if delete is selected then it should act as a delete tool
                            else if (editor.modelDeleteRadio.Checked)
                            {
                                ModelTool.DeleteModel();
                            }
                            //edit model if edit is selected
                            else if (editor.modelEditRadio.Checked)
                            {
                                //on "click" rather than whenever pressed
                                if (prevMouseState.LeftButton == ButtonState.Released)
                                {
                                    ModelTool.EditModel();
                                    //on mouse down we "pick up" the scenery object
                                    ModelTool.PickUpScenery();
                                }
                            }
                        }
                    }
                }
                //on left mouse release
                if (prevMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    //if we have an actual tile selected do the different options
                    if (game.selectedX != -1 && game.selectedY != -1)
                    {
                        //if we are in the model tab
                        if (editor.toolTab.SelectedTab == editor.SceneryTab && editor.modelEditRadio.Checked)
                        {
                            //on mouse up we "drop" the scenery object
                            ModelTool.DropScenery();
                        }
                        //if we are on the NPC tab
                        if (editor.toolTab.SelectedTab == editor.NPCTab)
                        {
                            if(NPCTool.toolType == NPCToolType.SetWanderArea && editor.NPCEditRadio.Checked)
                            {
                                NPCTool.EndSetWander();
                            }
                            else if (NPCTool.toolType == NPCToolType.EditNPC && editor.NPCEditRadio.Checked)
                            {
                                if (NPCTool.holdingNPC)
                                {
                                    NPCTool.DropNPC();
                                }
                            }
                        }
                    }
                }
                //on right mouse press
                if (currentMouseState.RightButton == ButtonState.Pressed)
                {
                    //if we have an actual tile selected do the different options
                    if (game.selectedX != -1 && game.selectedY != -1)
                    {
                        //if we are in the tile settings tab
                        if (editor.toolTab.SelectedTab == editor.TilePropertiesTab)
                        {
                            TileTool.SampleSettings();
                        }
                    }

                }

                //when we release the mouse we want to save the current state as the most recent history
                if (prevMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
                {
                    //if the history index is zero we want to save the current zone
                    if (editor.historyIndex == 0)
                    {
                        editor.zoneHistory[0] = new Zone(game.world.currentArea);
                    }
                }

                HandleScrollWheelActions();
            }

            //update previous mouse/keyboard state
            prevMouseState = currentMouseState;
            prevKeyState = currentKeyState;
        }

        private static void HandleArrowKeys()
        {            
            //arrow keys control the individual 'fine control' location of scenery models
            //ie, a translation matrix unique to each scenery object
            if (editor.toolTab.SelectedTab == editor.SceneryTab)
            {
                if ((editor.sceneryBox.SelectedItem != null || !editor.editingBaseScenery) && game.selectedX != -1 && game.selectedY != -1)
                {
                    Scenery temp = null;
                    if (editor.modelNewRadio.Checked && editor.sceneryBox.SelectedItem != null)
                        temp = editor.sceneryList[(String)editor.sceneryBox.SelectedItem]; //alter the template
                    else if(editor.modelEditRadio.Checked)
                        temp = ModelTool.currentModel;      //alter the placed object

                    //make sure we don't try to access a null reference
                    if (temp != null)
                    {
                        if (currentKeyState.IsKeyDown(Keys.Left))
                        {
                            temp.translation = temp.translation * Matrix.CreateTranslation(-0.03f, 0, 0);
                        }
                        if (currentKeyState.IsKeyDown(Keys.Right))
                        {
                            temp.translation = temp.translation * Matrix.CreateTranslation(0.03f, 0, 0);
                        }
                        if (currentKeyState.IsKeyDown(Keys.Down))
                        {
                            temp.translation = temp.translation * Matrix.CreateTranslation(0, 0, 0.03f);
                        }
                        if (currentKeyState.IsKeyDown(Keys.Up))
                        {
                            temp.translation = temp.translation * Matrix.CreateTranslation(0, 0, -0.03f);
                        }
                        if (currentKeyState.IsKeyDown(Keys.PageUp) && prevKeyState.IsKeyUp(Keys.PageUp))
                        {
                            temp.scale = temp.scale * Matrix.CreateScale(1.1f);
                        }
                        if (currentKeyState.IsKeyDown(Keys.PageDown) && prevKeyState.IsKeyUp(Keys.PageDown))
                        {
                            temp.scale = temp.scale * Matrix.CreateScale(1f / 1.1f);
                        }
                        if (currentKeyState.IsKeyDown(Keys.Home))
                        {
                            temp.translation = Matrix.CreateTranslation(0, 0, 0);
                        }
                    }
                }
            }
            else if (editor.toolTab.SelectedTab == editor.NPCTab)
            {
                if(NPCTool.toolType == NPCToolType.SetPath)
                    PathTool.HandleKeys(prevKeyState, currentKeyState);
            }
        }

        /// <summary>
        /// This is for handling actions with the scroll wheel
        /// </summary>
        private static void HandleScrollWheelActions()
        {
            //regular scrolling will zoom the camera in and out
            if (currentMouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue && currentKeyState.IsKeyUp(Keys.LeftControl) && currentKeyState.IsKeyUp(Keys.LeftShift))
            {
                Vector3 change = (currentMouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue) * new Vector3(0, 0.01f, 0.01f);
                GameDraw.cameraOffset -= change;
                if (GameDraw.cameraOffset.Y < 0.1f)
                    GameDraw.cameraOffset.Y = 0.1f;
                if (GameDraw.cameraOffset.Z < 0.1f)
                    GameDraw.cameraOffset.Z = 0.1f;
            }
            //scrolling with the shift key down will move the temporary model up and down (if applicable)
            //it will also change the height of the tile if the tile tab is selected
            else if (currentMouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue && currentKeyState.IsKeyUp(Keys.LeftControl) && currentKeyState.IsKeyDown(Keys.LeftShift))
            {
                if (editor.toolTab.SelectedTab == editor.SceneryTab)
                {
                    if ((editor.sceneryBox.SelectedItem != null || !editor.editingBaseScenery) && game.selectedX != -1 && game.selectedY != -1)
                    {
                        Scenery temp = null;
                        if (editor.modelNewRadio.Checked && editor.sceneryBox.SelectedItem != null)
                            temp = editor.sceneryList[(String)editor.sceneryBox.SelectedItem]; //alter the template
                        else if (editor.modelEditRadio.Checked)
                            temp = ModelTool.currentModel;      //alter the placed object

                        if (temp != null)
                        {
                            //make it 10% larger or smaller
                            if (currentMouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
                            {
                                if(currentKeyState.IsKeyDown(Keys.LeftShift) && currentKeyState.IsKeyDown(Keys.LeftAlt))
                                    temp.translation = temp.translation * Matrix.CreateTranslation(0, 1f, 0);
                                else
                                    temp.translation = temp.translation * Matrix.CreateTranslation(0, 1f / 8f, 0);
                            }
                            else
                            {
                                if(currentKeyState.IsKeyDown(Keys.LeftShift) && currentKeyState.IsKeyDown(Keys.LeftAlt))
                                    temp.translation = temp.translation * Matrix.CreateTranslation(0, -1f, 0);
                                else
                                    temp.translation = temp.translation * Matrix.CreateTranslation(0, -1f / 8f, 0);
                            }
                        }
                    }
                }
                else if ((editor.toolTab.SelectedTab == editor.TileSpritesTab || editor.toolTab.SelectedTab == editor.TilePropertiesTab))
                {
                    if (game.selectedX != -1 && game.selectedY != -1 && currentMouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue)
                    {
                        if (currentMouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
                        {
                            if (currentKeyState.IsKeyDown(Keys.LeftShift) && currentKeyState.IsKeyDown(Keys.LeftAlt))
                                game.world.currentArea.tile[game.selectedX, game.selectedY].Z += 8;
                            else
                                game.world.currentArea.tile[game.selectedX, game.selectedY].Z += 1;
                        }
                        else
                        {
                            if (currentKeyState.IsKeyDown(Keys.LeftShift) && currentKeyState.IsKeyDown(Keys.LeftAlt))
                                game.world.currentArea.tile[game.selectedX, game.selectedY].Z -= 8;
                            else
                                game.world.currentArea.tile[game.selectedX, game.selectedY].Z -= 1;
                        }
                            updateHistory();
                        previousTileHeightChange.X = game.selectedX;
                        previousTileHeightChange.Y = game.selectedY;
                        GameDraw.MakeAdjBuffers(game.world);
                    }
                }
            }
            //scrolling with the ctrl key down will rotate the temporary model if applicable
            else if (currentMouseState.ScrollWheelValue != prevMouseState.ScrollWheelValue && currentKeyState.IsKeyDown(Keys.LeftControl) && currentKeyState.IsKeyUp(Keys.LeftShift))
            {
                if (editor.toolTab.SelectedTab == editor.SceneryTab)
                {
                    if ((editor.sceneryBox.SelectedItem != null || !editor.editingBaseScenery) && game.selectedX != -1 && game.selectedY != -1)
                    {
                        Scenery temp = null;
                        if (editor.modelNewRadio.Checked && editor.sceneryBox.SelectedItem != null)
                            temp = editor.sceneryList[(String)editor.sceneryBox.SelectedItem]; //alter the template
                        else if (editor.modelEditRadio.Checked)
                            temp = ModelTool.currentModel;      //alter the placed object

                        if (temp != null)
                        {
                            //rotate it 27.5 degrees
                            if (currentMouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
                                temp.rotation = temp.rotation * Matrix.CreateRotationY(2f / 16f * (float)Math.PI);
                            else
                                temp.rotation = temp.rotation * Matrix.CreateRotationY(-2f / 16f * (float)Math.PI);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new "undo point" while retaining a moving window of old points
        /// Will also get rid of "redos" if an action is performed at an "undone" state
        /// </summary>
        public static void updateHistory()
        {
            //if history index is not zero then shift
            //the history to the correct locations
            //i.e clear the redos
            if (editor.historyIndex != 0)
            {
                //move all historical zones to the start of the array
                for (int i = editor.historyIndex; i < editor.historySize; i++)
                {
                    editor.zoneHistory[i - editor.historyIndex] = editor.zoneHistory[i];
                }
                //null the rest of the array
                for (int i = editor.historySize - editor.historyIndex; i < editor.historySize; i++)
                {
                    editor.zoneHistory[i] = null;
                }
                //finally set the history index to zero
                editor.historyIndex = 0;
            }

            for (int i = editor.historySize - 1; i > 0; i--)
            {
                editor.zoneHistory[i] = editor.zoneHistory[i - 1];
            }
            editor.zoneHistory[1] = new Zone(game.world.currentArea);
                       
        }

        static public void draw(World world)
        {
            //draw tiles
            GameDraw.DrawAdjacentGround(world, lookAtPosition);
            //draw scenery/models
            GameDraw.DrawScenery(world);
            //draw npc's
            //GameDraw.updateNPCSpritesheets(world);
            GameDraw.DrawNPCs(world);

            //draw the temporary NPC if we are setting a path
            if (editor.toolTab.SelectedTab == editor.NPCTab)
            {
                NPCTool.Draw();
            }
            else if (editor.toolTab.SelectedTab == editor.SceneryTab)
            {
                ModelTool.Draw(currentKeyState);
            }
        }
    }
}

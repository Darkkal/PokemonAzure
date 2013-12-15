using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Screens;
using PokeEngine.Map;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;

namespace LiveMapMaker.Tools
{
    public static class ModelTool
    {
        public static Game1 game;    //where the tool is applied to
        public static Editor editor; //stores the settings for the tool to be used
        public static Scenery currentModel; //the scenery to be placed or edited
        public static bool carryingScenery; //whether we are holding a scenery object to be edited or not

        private static Texture2D adjustmentsHud;
        private static Rectangle adjustmentsHudLocation;

        public static void InitializeTool(Game1 inGame, Editor inEditor)
        {
            game = inGame;
            editor = inEditor;

            adjustmentsHud = game.Content.Load<Texture2D>("Textures\\scenery_hud");
            adjustmentsHudLocation = new Rectangle(0, 0, adjustmentsHud.Width, adjustmentsHud.Height);
        }

        public static void PlaceModel()
        {
            //make sure some scenery is selected
            if (editor.sceneryList[(String)editor.sceneryBox.SelectedItem] == null)
            {
                return;
            }
            //make sure there is not already a duplicate scenery there (doesn't apply if unrestricted placement)
            if (!editor.unrestrictedPlacementBox.Checked && game.world.currentArea.tile[game.selectedX, game.selectedY].sceneryObject == editor.sceneryList[(String)editor.sceneryBox.SelectedItem])
            {
                return;
            }

            int selX = game.selectedX;
            int selY = game.selectedY;
            int xSize = editor.sceneryList[(String)editor.sceneryBox.SelectedItem].size.X;
            int ySize = editor.sceneryList[(String)editor.sceneryBox.SelectedItem].size.Y;

            Point location = new Point(selX, selY);

            currentModel = editor.sceneryList[(String)editor.sceneryBox.SelectedItem];
            Scenery scenery = new Scenery(currentModel.name, currentModel.interactScript, currentModel.modelName, location, currentModel.size);
            scenery.size = new Point(xSize, ySize);
            scenery.rotation = currentModel.rotation;
            scenery.scale = currentModel.scale;
            scenery.translation = currentModel.translation;

            //check if we are using restricted placement
            if (editor.unrestrictedPlacementBox.Checked == false)
            {
                //if not then we should exit the method if there is already
                //scenery associated with the selected tile area
                //(so we don't make orphans and don't overlap models)
                for(int x = 0; x < scenery.size.X; x++)
                {
                    for(int y = 0; y < scenery.size.Y; y++)
                    {
                        //bounds checking
                        if (selX+x < game.world.currentArea.mapWidth && selY+y < game.world.currentArea.mapHeight)
                        {
                            if (game.world.currentArea.tile[selX + x, selY + y].sceneryObject != null)
                                return;
                        }
                    }
                }
            }

            //add the scenery to the zone's list
            if(!game.world.currentArea.scenery.Contains(scenery) || editor.unrestrictedPlacementBox.Checked)
            {
                game.world.currentArea.scenery.Add(scenery);
            }

            //then link the tiles to that model
            //also set tiles non-accessable
            if (editor.sceneryBlockCheckbox.Checked == true)
            {
                for (int x = selX; x < selX + xSize; x++)
                {
                    for (int y = selY; y < selY + ySize; y++)
                    {
                        //do bounds checking
                        if (x < game.world.currentArea.mapWidth && y < game.world.currentArea.mapHeight)
                        {
                            game.world.currentArea.tile[x, y].sceneryObject = scenery;
                            game.world.currentArea.tile[x, y].setAccessible(false, false, false, false);
                        }
                    }
                }
            }
        }

        public static void EditModel()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //make the current model the object of the selected tile
            if (game.world.currentArea.tile[selX, selY].sceneryObject != null)
            {
                currentModel = game.world.currentArea.tile[selX, selY].sceneryObject;
                editor.activeSceneryEdit = currentModel;
                editor.editingBaseScenery = false; //we are editing a specific scenery instance rather than the template

                editor.modelNameBox.Text = currentModel.name;
                editor.modelWidthBox.Text = Convert.ToString(currentModel.size.X);
                editor.modelHeightBox.Text = Convert.ToString(currentModel.size.Y);
                editor.modelScriptBox.Text = currentModel.interactScript;
            }
            //otherwise make the current model null
            else
            {
                currentModel = null;
                editor.activeSceneryEdit = null;
                editor.modelNameBox.Text = "";
                editor.modelWidthBox.Text = "";
                editor.modelHeightBox.Text = "";
                editor.modelScriptBox.Text = "";
                editor.editingBaseScenery = false;
            }

        }

        public static void PickUpScenery()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //make the current model the object of the selected tile
            if (game.world.currentArea.tile[selX, selY].sceneryObject != null && !editor.editingBaseScenery) 
            {
                currentModel = game.world.currentArea.tile[selX, selY].sceneryObject;
                carryingScenery = true;
                //clear all references to the object on the map
                for (int x = currentModel.position.X; x < Math.Min(currentModel.position.X + currentModel.size.X, game.world.currentArea.mapWidth); x++)
                {
                    for (int y = currentModel.position.Y; y < Math.Min(currentModel.position.Y + currentModel.size.Y, game.world.currentArea.mapHeight); y++)
                    {
                        if (game.world.currentArea.tile[x, y].sceneryObject == currentModel)
                        {
                            game.world.currentArea.tile[x, y].sceneryObject = null;
                        }
                    }
                }
                    
            }
        }

        public static void DropScenery()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //place the current model in the selected place
            if (currentModel != null && !editor.editingBaseScenery)
            {
                currentModel.position.X = selX;
                currentModel.position.Y = selY;
                carryingScenery = false;
                //set references on the map according to the scenery's size
                for (int x = currentModel.position.X; x < Math.Min(currentModel.position.X + currentModel.size.X, game.world.currentArea.mapWidth); x++)
                {
                    for (int y = currentModel.position.Y; y < Math.Min(currentModel.position.Y + currentModel.size.Y, game.world.currentArea.mapHeight); y++)
                    {
                           game.world.currentArea.tile[x, y].sceneryObject = currentModel;
                    }
                }
            }
        }

        public static void DeleteModel()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //do nothing if already empty
            if (game.world.currentArea.tile[selX, selY].sceneryObject == null)
            {
                return;
            }

            Scenery temp = game.world.currentArea.tile[selX, selY].sceneryObject;
            int xSize = temp.size.X;
            int ySize = temp.size.Y;

            //first clear all tiles that link to that particular scenery object
            //also set them accessable

            for (int x = temp.position.X; x < temp.position.X + temp.size.X; x++)
            {
                for (int y = temp.position.Y; y < temp.position.Y + temp.size.Y; y++)
                {
                    //do bounds checking
                    if (x < game.world.currentArea.mapWidth && y < game.world.currentArea.mapHeight)
                    {
                        //then set scenery to null if it contains the scenery we want to delete
                        if (game.world.currentArea.tile[x, y].sceneryObject == temp)
                        {
                            game.world.currentArea.tile[x, y].sceneryObject = null;
                            game.world.currentArea.tile[x, y].setAccessible(true, true, true, true);
                        }
                    }
                }
            }

            //then remove the scenery from the list of scenery on the zone
            game.world.currentArea.scenery.Remove(temp);
            temp = null;

        }

        internal static void Draw(Microsoft.Xna.Framework.Input.KeyboardState currentKeyState)
        {
            if (currentKeyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F1))
            {
                game.spriteBatch.Draw(adjustmentsHud, adjustmentsHudLocation, Color.White);
            }
        }
    }
}

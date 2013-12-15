using System;
using PokeEngine.Trainers;
using System.IO;
using PokeEngine.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LiveMapMaker.Tools
{
    static class NPCTool
    {
        public static Game1 game;    //where the tool is applied to
        public static Editor editor; //stores the settings for the tool to be used
        public static NPCToolType toolType;
        private static Point wanderLoc1;
        private static Point wanderLoc2;
        public static bool holdingNPC; //whether an NPC is picked up or not

        private static Texture2D wanderHud;
        private static Rectangle wanderHudLocation;

        public static void InitializeTool(Game1 inGame, Editor inEditor)
        {
            game = inGame;
            editor = inEditor;
            toolType = NPCToolType.PlaceNPC;

            PathTool.InitializeTool(inGame, inEditor);

            wanderHud = game.Content.Load<Texture2D>("Textures\\wander_hud");
            wanderHudLocation = new Rectangle(0, 0, wanderHud.Width, wanderHud.Height);
        }

        public static void PlaceNPC()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            if (game.world.currentArea.GetNPCAtLocation(selX, selY) != null)
            {
                //do not place if there is already a NPC there
            }
            else
            {
                NPC tempNPC = editor.CreateNPC();
                //check if the sprite is already there
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Content\\Sprites\\NPCs\\Overworlds\\" + tempNPC.spriteSheet))
                {
                    tempNPC.tileCoords = new Microsoft.Xna.Framework.Point(selX, selY);
                    game.world.currentArea.tile[selX, selY].setOccupied(true);

                    game.world.currentArea.trainerList.Add(tempNPC);

                    tempNPC = null;

                    GameDraw.UpdateNPCSpritesheets(game.world);
                    editor.ResetNPCTab();
                }
            }
        }

        public static void DeleteNPC()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //do nothing if already empty
            if (game.world.currentArea.GetNPCAtLocation(selX, selY) == null)
            {
            }
            else
            {
                //remove the NPC from the trainer list
                NPC temp = game.world.currentArea.GetNPCAtLocation(selX, selY);

                game.world.currentArea.trainerList.Remove(temp);

                //set the tile as unoccupied
                game.world.currentArea.tile[selX, selY].setOccupied(false);
                temp = null;
            }
        }

        public static void PickUpNPC()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //do nothing if empty
            if (game.world.currentArea.GetNPCAtLocation(selX, selY) == null)
            {
                game.world.currentArea.tile[selX, selY].setOccupied(false);
                editor.activeNPCEdit = null;
                editor.ResetNPCTab();
            }
            else
            {
                holdingNPC = true;
                NPC temp = game.world.currentArea.GetNPCAtLocation(selX, selY);
                //unoccupy the tile 
                game.world.currentArea.tile[selX, selY].setOccupied(false);

                //uncheck NPC edit radio so that the change events won't trigger
                editor.NPCEditRadio.Checked = false;
                //fill the editor with current informatino about the NPC
                editor.tbox_NPCName.Text = temp.name;
                switch (temp.facing)
                {
                    case FacingDirection.North:
                        editor.rbtn_NPCFacingDirection_UP.Checked = true;
                        break;
                    case FacingDirection.South:
                        editor.rbtn_NPCFacingDirection_DOWN.Checked = true;
                        break;
                    case FacingDirection.West:
                        editor.rbtn_NPCFacingDirection_LEFT.Checked = true;
                        break;
                    case FacingDirection.East:
                        editor.rbtn_NPCFacingDirection_RIGHT.Checked = true;
                        break;
                    default:
                        break;
                }
                editor.cbox_IsTrainer.Checked = temp.GetType() == typeof(Trainer);
                editor.cbox_IsMale.Checked = temp.isMale;
                editor.NPCScriptBox.Text = temp.interactScript;

                editor.NPCImageBox.Image = new System.Drawing.Bitmap(Directory.GetCurrentDirectory() + "\\Content\\Sprites\\NPCs\\Overworlds\\" + temp.spriteSheet);
                editor.NPCImageToUse = temp.spriteSheet;
                switch (temp.movement)
                {
                    case MovementType.NONE:
                        editor.movementNoneRadio.Checked = true;
                        break;
                    case MovementType.PATH:
                        editor.movementPathRadio.Checked = true;
                        break;
                    case MovementType.WANDER:
                        editor.movementWanderRadio.Checked = true;
                        break;
                    default:
                        break;
                }

                //if it is a trainer
                if (temp.GetType() == typeof(Trainer))
                {
                    editor.tbox_TrainerPayout.Text = Convert.ToString(((Trainer)temp).money);
                }

                //check NPC edit radio again
                editor.NPCEditRadio.Checked = true;
                //set the NPC as the active NPC
                editor.activeNPCEdit = temp;
            }
        }

        public static void DropNPC()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            //The Editor Screen will handle dragging NPCs around
            //when the mouse is released the NPC will stop moving around
            //and the active npc may be edited

            //set tile to be occupied
            game.world.currentArea.tile[selX, selY].setOccupied(true);

            holdingNPC = false;
        }

        public static void StartPath()
        {
            toolType = NPCToolType.SetPath;
            PathTool.StartPath();
        }

        public static void StartSetWander()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            wanderLoc1 = new Point(selX, selY);
        }

        public static void EndSetWander()
        {
            int selX = game.selectedX;
            int selY = game.selectedY;

            wanderLoc2 = new Point(selX, selY);

            Microsoft.Xna.Framework.Rectangle wanderArea = new Microsoft.Xna.Framework.Rectangle(
                                                 Math.Min(wanderLoc1.X, wanderLoc2.X),
                                                 Math.Min(wanderLoc1.Y, wanderLoc2.Y),
                                                 Math.Abs(wanderLoc1.X - wanderLoc2.X) + 1,
                                                 Math.Abs(wanderLoc1.Y - wanderLoc2.Y) + 1);

            //if we are editing an NPC then set it's wander area
            if (editor.NPCEditRadio.Checked && editor.activeNPCEdit != null)
            {
                editor.activeNPCEdit.SetWanderArea(wanderArea);

                //if the npc is not in the wander area
                if (!wanderArea.Contains(editor.activeNPCEdit.tileCoords))
                {
                    //set the NPC to the top left corner of the area
                    game.world.currentArea.tile[editor.activeNPCEdit.tileCoords.X, editor.activeNPCEdit.tileCoords.Y].setOccupied(false);
                    for (int y = wanderArea.Y; y < wanderArea.Y + wanderArea.Height; y++)
                    {
                        for (int x = wanderArea.X; x < wanderArea.X + wanderArea.Width; x++)
                        {
                            if (game.world.currentArea.tile[x, y].isClear())
                            {
                                editor.activeNPCEdit.tileCoords = new Microsoft.Xna.Framework.Point(x, y);
                                game.world.currentArea.tile[x, y].setOccupied(true);
                                //exit the loop
                                y = wanderArea.Y + wanderArea.Height;
                                break;
                            }
                        }
                    }
                }
                toolType = NPCToolType.EditNPC;
            }
            else
            {
                toolType = NPCToolType.EditNPC;
            }


        }

        internal static void Draw()
        {
            if (toolType == NPCToolType.SetPath)
            {
                PathTool.Draw();
            }
            else if (toolType == NPCToolType.SetWanderArea)
            {
                game.spriteBatch.Draw(wanderHud, wanderHudLocation, Color.White);
            }
        }
    }

    public enum NPCToolType : byte
    {
        PlaceNPC,
        EditNPC,
        SetWanderArea,
        SetPath,
        DeleteNPC,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Trainers;
using PokeEngine.Map;
using PokeEngine.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LiveMapMaker.Tools
{
    static class PathTool
    {
        public static Game1 game;    //where the tool is applied to
        public static Editor editor; //stores the settings for the tool to be used
        private static List<PokeEngine.Trainers.Action> actions;
        private static NPC tempNPC;
        private static Texture2D hud;
        private static Rectangle hudLocation;

        private static Zone map
        {
            get { return game.world.currentArea; }
        }

        public static void InitializeTool(Game1 inGame, Editor inEditor)
        {
            game = inGame;
            editor = inEditor;

            hud = game.Content.Load<Texture2D>("Textures\\path_hud");
            hudLocation = new Rectangle(0, 0, hud.Width, hud.Height);
        }

        public static void StartPath()
        {
            if (editor.activeNPCEdit != null)
            {
                //make the original NPC walk-through
                map.tile[editor.activeNPCEdit.tileCoords.X, editor.activeNPCEdit.tileCoords.Y].setOccupied(false);

                tempNPC = new NPC(editor.activeNPCEdit);
                //start a new path for the selected NPC
                actions = new List<PokeEngine.Trainers.Action>();
                tempNPC.actions = actions;
            }
            else
                NPCTool.toolType = NPCToolType.EditNPC;

        }

        public static void EndPath()
        {
            if (editor.activeNPCEdit != null)
            {
                //make the original NPC solid
                map.tile[editor.activeNPCEdit.tileCoords.X, editor.activeNPCEdit.tileCoords.Y].setOccupied(true);

                //set the actions
                editor.activeNPCEdit.actions = actions;
                //set the movement type (a full list with a NONE movement type won't move)
                editor.activeNPCEdit.movement = MovementType.PATH;

                NPCTool.toolType = NPCToolType.EditNPC;
            }
        }

        /// <summary>
        /// Adds an action to the list
        /// Given that only face commands, pause, and stop will be given any double-face commands will
        /// be transmuted into a walk
        /// </summary>
        /// <param name="a">incoming action</param>
        public static void AddAction(PokeEngine.Trainers.Action a)
        {
                actions.Add(a);
        }

        public static void RemoveLastAction()
        {
            actions.RemoveAt(actions.Count-1);
        }



        internal static void HandleKeys(KeyboardState prev, KeyboardState curr)
        {

            if (NPCTool.toolType == NPCToolType.SetPath)
            {
                if (tempNPC != null)
                {
                    //Direction Keys will face the NPC in that direction if not facing that direction
                    //or it will make the NPC face that direction if facing that direction
                    if (prev.IsKeyUp(Keys.Left) && curr.IsKeyDown(Keys.Left))
                    {
                        if (tempNPC.facing == FacingDirection.West)
                        {
                            AddAction(PokeEngine.Trainers.Action.LEFT);
                            AlterTempNPC(PokeEngine.Trainers.Action.LEFT);
                        }
                        else
                        {
                            AddAction(PokeEngine.Trainers.Action.FACELEFT);
                            AlterTempNPC(PokeEngine.Trainers.Action.FACELEFT);
                        }
                    }
                    else if (prev.IsKeyUp(Keys.Down) && curr.IsKeyDown(Keys.Down))
                    {
                        if (tempNPC.facing == FacingDirection.South)
                        {
                            AddAction(PokeEngine.Trainers.Action.DOWN);
                            AlterTempNPC(PokeEngine.Trainers.Action.DOWN);
                        }
                        else
                        {
                            AddAction(PokeEngine.Trainers.Action.FACEDOWN);
                            AlterTempNPC(PokeEngine.Trainers.Action.FACEDOWN);
                        }
                    }
                    else if (prev.IsKeyUp(Keys.Right) && curr.IsKeyDown(Keys.Right))
                    {
                        if (tempNPC.facing == FacingDirection.East)
                        {
                            AddAction(PokeEngine.Trainers.Action.RIGHT);
                            AlterTempNPC(PokeEngine.Trainers.Action.RIGHT);
                        }
                        else
                        {
                            AddAction(PokeEngine.Trainers.Action.FACERIGHT);
                            AlterTempNPC(PokeEngine.Trainers.Action.FACERIGHT);
                        }
                    }
                    else if (prev.IsKeyUp(Keys.Up) && curr.IsKeyDown(Keys.Up))
                    {
                        if (tempNPC.facing == FacingDirection.North)
                        {
                            AddAction(PokeEngine.Trainers.Action.UP);
                            AlterTempNPC(PokeEngine.Trainers.Action.UP);
                        }
                        else
                        {
                            AddAction(PokeEngine.Trainers.Action.FACEUP);
                            AlterTempNPC(PokeEngine.Trainers.Action.FACEUP);
                        }
                    }
                    //spacebar key makes the NPC pause
                    else if (prev.IsKeyUp(Keys.Space) && curr.IsKeyDown(Keys.Space))
                    {
                        AddAction(PokeEngine.Trainers.Action.PAUSE);
                    }
                    //backspace key adds a stop command to the list (NPC will change it's movement type to NONE)
                    else if (prev.IsKeyUp(Keys.Back) && curr.IsKeyDown(Keys.Back))
                    {
                        AddAction(PokeEngine.Trainers.Action.STOP);
                    }
                    //esc key quickly finishes the path
                    else if (prev.IsKeyUp(Keys.Escape) && curr.IsKeyDown(Keys.Escape))
                    {
                        EndPath();
                    }
                }
            }
        }

        private static void AlterTempNPC(PokeEngine.Trainers.Action action)
        {
            int x = tempNPC.tileCoords.X;
            int y = tempNPC.tileCoords.Y;

            switch (action)
            {
                case PokeEngine.Trainers.Action.LEFT:
                    if (x - 1 >= 0)
                    {
                        if (map.tile[x - 1, y].isAccessibleFrom(Direction.East))
                        {
                            tempNPC.tileCoords.X -= 1;
                        }
                    }
                    break;
                case PokeEngine.Trainers.Action.RIGHT:
                    if (x + 1 < map.mapWidth)
                    {
                        if (map.tile[x + 1, y].isAccessibleFrom(Direction.West))
                        {
                            tempNPC.tileCoords.X += 1;
                        }
                    }
                    break;
                case PokeEngine.Trainers.Action.UP:
                    if (y - 1 >= 0)
                    {
                        if (map.tile[x, y - 1].isAccessibleFrom(Direction.South))
                        {
                            tempNPC.tileCoords.Y -= 1;
                        }
                    }
                    break;
                case PokeEngine.Trainers.Action.DOWN:
                    if (y + 1 < map.mapHeight)
                    {
                        if (map.tile[x, y + 1].isAccessibleFrom(Direction.North))
                        {
                            tempNPC.tileCoords.Y += 1;
                        }
                    }
                    break;
                case PokeEngine.Trainers.Action.FACELEFT:
                    tempNPC.facing = FacingDirection.West;
                    tempNPC.Update();
                    break;
                case PokeEngine.Trainers.Action.FACERIGHT:
                    tempNPC.facing = FacingDirection.East;
                    tempNPC.Update();
                    break;
                case PokeEngine.Trainers.Action.FACEUP:
                    tempNPC.facing = FacingDirection.North;
                    tempNPC.Update();
                    break;
                case PokeEngine.Trainers.Action.FACEDOWN:
                    tempNPC.facing = FacingDirection.South;
                    tempNPC.Update();
                    break;
            }
        }

        internal static void Draw()
        {
            GameDraw.DrawUncontroledPlayer(tempNPC, game.world);
            game.spriteBatch.Draw(hud, hudLocation, Color.White);
        }
    }
}

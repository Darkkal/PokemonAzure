using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Screens;
using PokeEngine.Map;

namespace LiveMapMaker.Tools
{
    public static class TileTool
    {
        public static Game1 game;    //where the tool is applied to
        public static Editor editor; //stores the settings for the tool to be used
        

        public static void InitializeTool(Game1 inGame, Editor inEditor)
        {
            game = inGame;
            editor = inEditor;
        }

        public static void ApplyImage()
        {
            if (editor.selectedTiles.Count < 1)
                return;

            int x = game.selectedX;
            int y = game.selectedY;

            foreach (SelectedTile s in editor.selectedTiles)
            {
                //first tile in list is under the mouse
                int currX = x + s.PositionX - editor.selectedTiles[0].PositionX;
                int currY = y + s.PositionY - editor.selectedTiles[0].PositionY;
                if (currX >= 0 && currX < game.world.currentArea.mapWidth &&
                    currY >= 0 && currY < game.world.currentArea.mapHeight &&
                    !String.IsNullOrWhiteSpace(s.TileType))
                {
                    //apply the changes to the properties of the tile
                    game.world.currentArea.tile[currX, currY].setAccessible(editor.northBox.Checked, editor.eastBox.Checked, editor.southBox.Checked, editor.westBox.Checked);
                    game.world.currentArea.tile[currX, currY].setRamp(editor.rampBox.Checked);
                    game.world.currentArea.tile[currX, currY].setJumpable(editor.jumpBox.Checked);
                    game.world.currentArea.tile[currX, currY].setRandomEncounter(editor.randomEncounterBox.Checked);
                    game.world.currentArea.tile[currX, currY].setWater(editor.waterBox.Checked);
                    game.world.currentArea.tile[currX, currY].tileType = s.TileType;
                }
            }
            //check if the tile is already of the selected type
            //if not then change it
            game.world.changeZone(game.world.currentArea);
            GameDraw.MakeAdjBuffers(game.world);
        }

        public static void ApplySettings()
        {
            int x = game.selectedX;
            int y = game.selectedY;

            //apply the changes to the properties of the tile
            if (editor.controlPropertiesBox.Checked == true)
            {
                game.world.currentArea.tile[x, y].setAccessible(editor.controlNorthBox.Checked, editor.controlEastBox.Checked, editor.controlSouthBox.Checked, editor.controlWestBox.Checked);
                game.world.currentArea.tile[x, y].setRamp(editor.controlBikeBox.Checked);
                game.world.currentArea.tile[x, y].setJumpable(editor.controlJumpBox.Checked);
                game.world.currentArea.tile[x, y].setRandomEncounter(editor.controlREBox.Checked);
                game.world.currentArea.tile[x, y].setWater(editor.controlWaterBox.Checked);
            }

            //apply a script if it is enabled and there is stuff in the box
            if (editor.tileScriptBox.Checked && !String.IsNullOrEmpty(editor.tileScriptTextBox.Text))
            {
                game.world.currentArea.tile[x, y].eventScript = editor.tileScriptTextBox.Text;
            }
                //otherwise remove the script
            else
            {
                game.world.currentArea.tile[x, y].eventScript = null;
            }
        }

        internal static void SetAll(String tileType)
        {
            EditorScreen.updateHistory();

            for (int x = 0; x < game.world.currentArea.mapWidth; x++)
            {
                for (int y = 0; y < game.world.currentArea.mapHeight; y++)
                {
                    //apply properties
                    game.world.currentArea.tile[x, y].setAccessible(editor.northBox.Checked, editor.eastBox.Checked, editor.southBox.Checked, editor.westBox.Checked);
                    game.world.currentArea.tile[x, y].setRamp(editor.rampBox.Checked);
                    game.world.currentArea.tile[x, y].setJumpable(editor.jumpBox.Checked);
                    game.world.currentArea.tile[x, y].setRandomEncounter(editor.randomEncounterBox.Checked);
                    game.world.currentArea.tile[x, y].setWater(editor.waterBox.Checked);

                    //apply tile image
                    game.world.currentArea.tile[x, y].tileType = tileType;
                    //game.world.changeZone(game.world.currentArea);                    
                }
            }

            //if the history index is zero we want to save the current zone
            if (editor.historyIndex == 0)
            {
                editor.zoneHistory[0] = new Zone(game.world.currentArea);
            }

            //then update buffers all in one fell swoop
            GameDraw.MakeAdjBuffers(game.world);
        }

        //Sample the settings from the currently selected tile.
        internal static void SampleSettings()
        {
            int x = game.selectedX;
            int y = game.selectedY;

            //sample the properties of the tile
                editor.controlNorthBox.Checked = game.world.currentArea.tile[x, y].isAccessibleFrom(Direction.North);
                editor.controlSouthBox.Checked = game.world.currentArea.tile[x, y].isAccessibleFrom(Direction.South);
                editor.controlEastBox.Checked = game.world.currentArea.tile[x, y].isAccessibleFrom(Direction.East);
                editor.controlWestBox.Checked = game.world.currentArea.tile[x, y].isAccessibleFrom(Direction.West);

                editor.controlBikeBox.Checked = game.world.currentArea.tile[x, y].isRamp();
                editor.controlJumpBox.Checked = game.world.currentArea.tile[x, y].isJumpable();
                editor.controlREBox.Checked = game.world.currentArea.tile[x, y].hasRandomEncounter();
                editor.controlWaterBox.Checked = game.world.currentArea.tile[x, y].isWater;

            //sample a script on the tile
            if (editor.tileScriptBox.Text != null)
            {
                editor.tileScriptTextBox.Text = game.world.currentArea.tile[x, y].eventScript;
            }
        }
    }
}

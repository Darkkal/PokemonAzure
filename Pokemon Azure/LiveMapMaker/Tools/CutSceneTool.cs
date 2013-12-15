using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PokeEngine.Screens;

namespace LiveMapMaker.Tools
{
    static class CutSceneTool
    {
        public static Game1 game;    //where the tool is applied to
        public static Editor editor; //stores the settings for the tool to be used
        internal static CutScene scene;

        public static void InitializeTool(Game1 inGame, Editor inEditor)
        {
            game = inGame;
            editor = inEditor;
        }

        public static void NewCutScene()
        {
            scene = new CutScene();
        }

        //saves to current zone
        public static void SaveCutScene()
        {

        }

        //brings up a list to select from
        public static void LoadCutScene()
        {

        }
    }
}

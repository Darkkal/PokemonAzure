using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Menu;
using PokeEngine.Input;
using PokeEngine.Tools;

namespace PokeEngine.Screens
{
    public static class ScreenHandler
    {
        
        public static GraphicsDeviceManager graphics;
        public static ContentManager content;
        public static SpriteFont font;
        private static List<Screen> screenStack;

        public static Screen TopScreen { get { return screenStack[screenStack.Count-1]; } }
                
        public static Options GameOptions;
        public static int SCREEN_WIDTH, SCREEN_HEIGHT;
        public static Texture2D WindowMarker, WindowBackground, WindowBackgroundCorner, WindowBackgroundSide, WindowCorner, WindowSide;
        public static SoundEffect MarkerUp, MarkerDown, MenuSelect;
        public static bool Exit;
        public static float FontWidth, FontHeight;
        public static string debug;

        public static void Initialize(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
        {
            graphics = g;
            content = c;
            font = f;
            screenStack = new List<Screen>();
            SwitchScreen(new TitleScreen(graphics, content, font));
            
            GameOptions = new Options();
            SCREEN_WIDTH = graphics.PreferredBackBufferWidth;
            SCREEN_HEIGHT = graphics.PreferredBackBufferHeight;

            WindowMarker =              SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\marker.png", g.GraphicsDevice);
            WindowBackground =          SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\background.png", g.GraphicsDevice);
            WindowBackgroundCorner =    SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\backgroundCorner.png", g.GraphicsDevice);
            WindowBackgroundSide =      SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\backgroundSide.png", g.GraphicsDevice);
            WindowCorner =              SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\corner.png", g.GraphicsDevice);
            WindowSide =                SaveLoad.LoadTexture2D(@"Content\Textures\Menu\" + GameOptions.Frame.ToString() + "\\side.png", g.GraphicsDevice);

            MarkerUp =      SaveLoad.LoadSoundEffect(@"Content\SoundEffects\Menu\markerUp.wav");
            MarkerDown =    SaveLoad.LoadSoundEffect(@"Content\SoundEffects\Menu\markerUp.wav");
            MenuSelect =    SaveLoad.LoadSoundEffect(@"Content\SoundEffects\Menu\markerUp.wav");

            FontWidth = font.MeasureString("E").X;
            FontHeight = font.MeasureString("E").Y;

            Exit = false;
            debug = "";
        }

        public static void HandleInput(GamePadState g, KeyboardState k, MouseState m)
        {
            debug = "Inputting: ";
            if (screenStack.Count > 0)
            {
                TopScreen.HandleInput(g, k, m);
                debug += " " + screenStack.Last<Screen>().Name;

                debug += "\n";
                InputHandler.UpdateCooling();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (screenStack.Count > 0)
            {
                debug += "Updating:";
                if (TopScreen.IsAlive)
                {
                    TopScreen.Update(gameTime);
                    debug += " " + screenStack.Last<Screen>().Name;
                }
                else
                    screenStack.Remove(TopScreen);

                debug += "\n";
            }

            if (screenStack.Count <= 0)
                Exit = true;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (screenStack.Count > 0)
            {
                debug += "Drawing:\n";
                for (int i = 0; i < screenStack.Count; i++)
                {
                    if (screenStack[i].IsVisible)
                    {
                        screenStack[i].Draw(spriteBatch);
                        debug += " " + screenStack[i].Name;
                        debug += "\n";
                    }
                }

                /*
                spriteBatch.DrawString(
                    font, 
                    debug, 
                    new Vector2(
                        SCREEN_WIDTH - font.MeasureString(debug).X, 
                        0), 
                    Color.White);
                 */ 
            }
        }

        public static void PopScreen()
        {
            screenStack.Remove(TopScreen);

            if (screenStack.Count <= 0)
                Exit = true;
            else
            {
                TopScreen.IsActive = true;
                TopScreen.IsVisible = true;
            }
        }

        public static void SwitchScreen(Screen screen)
        {
            screenStack.Clear();

            screen.IsVisible = true;
            screenStack.Add(screen);
        }

        public static void PushScreen(Screen screen)
        {
            TopScreen.IsActive = false;
            screenStack.Add(screen);
        }
    }
}

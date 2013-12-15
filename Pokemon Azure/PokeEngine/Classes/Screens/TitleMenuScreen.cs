using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PokeEngine.Input;
using PokeEngine.Menu;

namespace PokeEngine.Screens
{

	class TitleMenuScreen : Screen
	{

        private MenuWindow menu;
        private List<string> optionList;
        private const float MENU_PADDING = 32f;
	
		public TitleMenuScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
			:base(g, c, f)
		{
            optionList = new List<string>();
            optionList.Add("New Game");
            optionList.Add("Load Game");
            optionList.Add("Multiplayer");
            optionList.Add("Options");
            optionList.Add("Mystery Gift");

            Name = "TitleMenuScreen";
            menu = new MenuWindow(new Vector2(5, 5), optionList, MENU_PADDING);
            IsVisible = true;
        }
		
		public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
			
			if(InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[0], 10))
			{
				menu.SelectionUp();
			}
			if(InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[1], 10))
			{
				menu.SelectionDown();
			}
			if(InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[4], 10))
			{
				switch(menu.GetSelection())
				{
                    case 0:
                        {
                            GameScreen newScreen = new GameScreen(graphics, content, font);
                            newScreen.Initialise();
                            ScreenHandler.SwitchScreen(newScreen);

                            
                            break;
                        }
                    case 1: break;
                    case 2: break;
                    case 3: ScreenHandler.PushScreen(new OptionScreen(graphics, content, font)); break;
                    case 4: break;
				}
			}
			if(InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[5], 10))
			{
				ScreenHandler.SwitchScreen(new TitleScreen(graphics, content, font));
			}
			
        }

        public override void Update(GameTime gameTime)
        {
			
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch, font, Color.White);
            spriteBatch.DrawString(
                font,
                KeyConfig.KeyList[4].ToString() + " to Select | " + KeyConfig.KeyList[5] + " to go back",
                new Vector2(
                    5,
                    ScreenHandler.SCREEN_HEIGHT - ScreenHandler.FontHeight - 5),
                Color.White);
        }
	}	
	
}

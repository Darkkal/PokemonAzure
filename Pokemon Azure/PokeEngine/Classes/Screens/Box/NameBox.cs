using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PokeEngine.Menu;
using PokeEngine.Input;

namespace PokeEngine.Screens
{

    public class NameBox : Screen
    {

        private MenuWindow menuWindow;
        private KeyboardInput textBox;

        public NameBox(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
            : base(g, c, f)
        {
            textBox = new KeyboardInput("Enter Name");

            List<string> stringList = new List<string>();
            stringList.Add(textBox.GetText());

            menuWindow = new MenuWindow(Vector2.Zero, stringList, 5);

            menuWindow.Position = new Vector2(
                (ScreenHandler.SCREEN_WIDTH / 2) - (menuWindow.size.x * 16),
                (ScreenHandler.SCREEN_HEIGHT / 2) - (menuWindow.size.y * 16));
            menuWindow.SetMarkerEnabled(false);

            Name = "NameBox";

        }


        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
            textBox.Update(keyState);
        }

        public override void Update(GameTime gameTime)
        {
            menuWindow.ChangeOption(0, textBox.GetText());

            menuWindow.Position = new Vector2(
                (ScreenHandler.SCREEN_WIDTH / 2) - (menuWindow.size.x * 16),
                (ScreenHandler.SCREEN_HEIGHT / 2) - (menuWindow.size.y * 16));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuWindow.Draw(spriteBatch, Color.White);
        }

        public string GetText()
        {
            return textBox.GetText();
        }

        public bool IsDone()
        {
            return textBox.IsDone;
        }

        public bool WasCancelled()
        {
            return textBox.Cancelled;
        }

    }
}

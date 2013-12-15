using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PokeEngine.Input;
using PokeEngine.Menu;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PokeEngine.Screens
{
    class OptionScreen : Screen
    {

        private const float OPTIONSMENU_PADDING = 5f;
        private const float KEYCONFIGMENU_PADDING = 1f;
        private Vector2 OPTIONSMENU_POS = new Vector2(5, 5);
        private Vector2 KEYCONFIGMENU_POS = new Vector2(5, 5);

        private bool inKeyMenu;
        private bool isSelectingKey;

        private MenuWindow optionsMenu, keyconfigMenu;
        private List<string> optionsOptionList, keyconfigOptionList;
        

        public OptionScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
            : base(g, c, f)
        {
            optionsOptionList = new List<string>();
            keyconfigOptionList = new List<string>();

            populateOptionLists();

            optionsMenu = new MenuWindow(OPTIONSMENU_POS, optionsOptionList, OPTIONSMENU_PADDING);
            for (int i = 0; i < 6; i++)
                UpdateBaseOption(i);
            keyconfigMenu = new MenuWindow(KEYCONFIGMENU_POS, keyconfigOptionList, KEYCONFIGMENU_PADDING);
            for (int i = 0; i < 8; i++)
                UpdateKeyOption(i);

            inKeyMenu = false;
            isSelectingKey = false;

            Name = "OptionScreen";

        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyboardState, MouseState mouseState)
        {
            if (!isSelectingKey)
            {
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Down, 10))
                {
                    if (!inKeyMenu)
                    {
                        optionsMenu.SelectionDown();
                    }
                    else
                    {
                        if (!isSelectingKey)
                        {
                            keyconfigMenu.SelectionDown();
                        }
                    }
                }
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Left, 10))
                {
                    if (!inKeyMenu)
                    {
                        switch (optionsMenu.GetSelection())
                        {
                            case 0: ScreenHandler.GameOptions.TextSpeed = ScreenHandler.GameOptions.TextSpeed > 0 ? (byte)(ScreenHandler.GameOptions.TextSpeed - 1) : (byte)2; break;
                            case 1: ScreenHandler.GameOptions.BattleScene = ScreenHandler.GameOptions.BattleScene ? false : true; break;
                            case 2: ScreenHandler.GameOptions.BattleStyle = ScreenHandler.GameOptions.BattleStyle ? false : true; break;
                            case 3: ScreenHandler.GameOptions.Sound = ScreenHandler.GameOptions.Sound ? false : true; break;
                        }
                    }
                    UpdateBaseOption(optionsMenu.GetSelection());
                }
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Right, 10))
                {
                    if (!inKeyMenu)
                    {
                        switch (optionsMenu.GetSelection())
                        {
                            case 0: ScreenHandler.GameOptions.TextSpeed = ScreenHandler.GameOptions.TextSpeed < 2 ? (byte)(ScreenHandler.GameOptions.TextSpeed + 1): (byte)0; break;
                            case 1: ScreenHandler.GameOptions.BattleScene = ScreenHandler.GameOptions.BattleScene ? false : true; break;
                            case 2: ScreenHandler.GameOptions.BattleStyle = ScreenHandler.GameOptions.BattleStyle ? false : true; break;
                            case 3: ScreenHandler.GameOptions.Sound = ScreenHandler.GameOptions.Sound ? false : true; break;
                        }
                        UpdateBaseOption(optionsMenu.GetSelection());
                    }
                }
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Up, 10))
                {
                    if (!inKeyMenu)
                    {
                        optionsMenu.SelectionUp();
                    }
                    else
                    {
                        if (!isSelectingKey)
                        {
                            keyconfigMenu.SelectionUp();
                        }
                    }
                }
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Cancel, 10))
                {
                    if (!inKeyMenu)
                    {
                        Close();
                    }
                    else
                    {
                        if (!isSelectingKey)
                            inKeyMenu = false;
                    }
                }
                if (InputHandler.WasKeyPressed(keyboardState, KeyConfig.Action, 10))
                {
                    if (!inKeyMenu)
                    {
                        switch (optionsMenu.GetSelection())
                        {
                            case 4: inKeyMenu = true; break;
                            case 6: ScreenHandler.GameOptions.Save(); break;
                            case 7: Close(); break;
                        }
                    }
                    else
                    {
                        if (!isSelectingKey)
                        {
                            int i = keyconfigMenu.GetSelection();
                            if (i <= keyconfigMenu.GetOptionList().Count - 3)
                                isSelectingKey = true;
                            else if (i == keyconfigMenu.GetOptionList().Count - 2)
                                KeyConfig.Save();
                            else
                                inKeyMenu = false;
                        }
                    }
                }
            }
            else
            {
                int i = keyconfigMenu.GetSelection();

                Keys[] selectedKeys = InputHandler.GetSelectedKeys(keyboardState, 10);
                if (selectedKeys.Length > 0 && !selectedKeys[0].Equals(Keys.None) && !checkIfKeyExists(selectedKeys[0]))
                {
                    KeyConfig.KeyList[i] = selectedKeys[0];
                    UpdateKeyOption(i);
                    isSelectingKey = false;
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch s)
        {
            if (!inKeyMenu)
                optionsMenu.Draw(s, font, Color.White);
            else
                keyconfigMenu.Draw(s, font, Color.White);
        }

        private void populateOptionLists()
        {

            //  populate optionsOptionList
            {

                for (int i = 0; i < 6; i++)
                    optionsOptionList.Add(" ");

                optionsOptionList.Add("Save");
                optionsOptionList.Add("Cancel");
            }

            //  populate keyconfigOptionList
            {

                for(int i = 0; i < 8; i++)
                    keyconfigOptionList.Add(" ");
                keyconfigOptionList.Add("SAVE");
                keyconfigOptionList.Add("CANCEL");

            }

        }

        private bool checkIfKeyExists(Keys key)
        {
            bool check = false;

            for (int i = 0; i < KeyConfig.KeyList.Length; i++)
                if (key.Equals(KeyConfig.KeyList[i]))
                    check = true;

            return check;
        }

        private void UpdateKeyOption(int index)
        {
            switch (index)
            {
                case 0: keyconfigMenu.ChangeOption(index, String.Format("Up: {0}", KeyConfig.Up.ToString())); break;
                case 1: keyconfigMenu.ChangeOption(index, String.Format("Down: {0}", KeyConfig.Down.ToString())); break;
                case 2: keyconfigMenu.ChangeOption(index, String.Format("Left: {0}", KeyConfig.Left.ToString())); break;
                case 3: keyconfigMenu.ChangeOption(index, String.Format("Right: {0}", KeyConfig.Right.ToString())); break;
                case 4: keyconfigMenu.ChangeOption(index, String.Format("Action (Select): {0}", KeyConfig.Action.ToString())); break;
                case 5: keyconfigMenu.ChangeOption(index, String.Format("Cancel (Back): {0}", KeyConfig.Cancel.ToString())); break;
                case 6: keyconfigMenu.ChangeOption(index, String.Format("Menu: {0}", KeyConfig.Menu.ToString())); break;
                case 7: keyconfigMenu.ChangeOption(index, String.Format("Item: {0}", KeyConfig.Item.ToString())); break;
            }
        }

        private void UpdateBaseOption(int index)
        {
            string textspeed =  "Text Speed: ";

            switch (ScreenHandler.GameOptions.TextSpeed)
            {
                case 0: textspeed += "Slow"; break;
                case 1: textspeed += "Medium"; break;
                case 2: textspeed += "Fast"; break;
                default: textspeed += "oh god an error what the fuck"; break;
            }

            
            switch(index)
            {
                case 0: optionsMenu.ChangeOption(index, (textspeed)); break;
                case 1: optionsMenu.ChangeOption(index, String.Format("Battle Scene: {0}", ScreenHandler.GameOptions.BattleScene ? "Animate" : "Don't Animate")); break;
                case 2: optionsMenu.ChangeOption(index, String.Format("Battle Style: {0}", ScreenHandler.GameOptions.BattleStyle ? "Shift" : "Set")); break;
                case 3: optionsMenu.ChangeOption(index, String.Format("Sound: {0}", ScreenHandler.GameOptions.Sound ? "On" : "Off")); break;
                case 4: optionsMenu.ChangeOption(index, "Key Config"); break;
                case 5: optionsMenu.ChangeOption(index, String.Format("Frame: {0}", ScreenHandler.GameOptions.Frame.ToString())); break;
            }
        }

    }
}

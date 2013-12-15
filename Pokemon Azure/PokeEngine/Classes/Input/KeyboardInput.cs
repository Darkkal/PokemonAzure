using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PokeEngine.Input
{
    class KeyboardInput
    {

        private const int KEY_COOLDOWN_MAX = 3;
        
        private string buffer;
        private int keyCoolDown;
        private bool keyIsCooling;

        public bool IsDone, Cancelled;

        public KeyboardInput()
        {
            initialize();
        }

        public KeyboardInput(string defaultText)
        {

            initialize();
            buffer = defaultText;

        }

        private void initialize()
        {

            buffer = "";
            keyCoolDown = KEY_COOLDOWN_MAX;
            keyIsCooling = false;
            IsDone = false;
            Cancelled = false;

        }

        private char getCharFromKeyboard(KeyboardState keyState)
        {
            Keys[] pressedKeys = keyState.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {

                int keyNumber = (int)pressedKeys[0];

                if (keyNumber >= 65 && keyNumber <= 90)
                    if ((char)keyNumber != '\0')
                        return (char)keyNumber;

            }

            return (char)0;
        }

        

        public void Update(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Escape))
            {

                Cancelled = true;
                IsDone = true;

            }
            else if (keyState.IsKeyDown(Keys.Enter))
            {

                IsDone = true;

            }
            else if (keyState.IsKeyDown(Keys.Back))
            {

                if (!keyIsCooling)
                {

                    buffer.Remove(buffer.Length - 1);

                    keyIsCooling = true;
                    keyCoolDown = keyCoolDown = KEY_COOLDOWN_MAX;

                }
                else
                {

                    if (keyCoolDown > 0)
                        keyCoolDown--;
                    else
                        keyIsCooling = false;

                }

            }
            else
            {

                if (!IsDone)
                {
                    if (keyState.GetPressedKeys().Length > 0)
                    {
                        if (!keyIsCooling)
                        {

                            string key = getCharFromKeyboard(keyState).ToString();
                            buffer += key;

                            keyIsCooling = true;
                            keyCoolDown = KEY_COOLDOWN_MAX;

                        }
                        else
                        {

                            if (keyCoolDown > 0)
                                keyCoolDown--;
                            else
                                keyIsCooling = false;

                        }
                    }
                }

            }
            
        }

        public string GetText() { return buffer; }
        

    }
}

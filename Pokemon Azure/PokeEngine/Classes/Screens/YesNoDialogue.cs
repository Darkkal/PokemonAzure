using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Screens;

namespace PokeEngine.Menu
{
    public static class YesNoDialogue
    {

        static GraphicsDeviceManager graphics;
        static SpriteBatch spriteBatch;
        static ContentManager content;
        static SpriteFont font;
        public static bool isVisible = false; //whether the dialog box is showing, essentially means whether it is active or not
        static String lineOne = ""; //first line to display
        static String lineTwo = ""; //second line to display
        static String remainingText = ""; //text that can not fit on the two visible lines
        static bool isAnimating = false;
        static List<String> messageBuffer = new List<string>();
        public static List<String> options = new List<string>();
        public static string choice = "";

        static public void Initialize(GraphicsDeviceManager g, SpriteBatch s, ContentManager c, SpriteFont f)
        {
            graphics = g;
            spriteBatch = s;
            content = c;
            font = f;
        }

        /// <summary>
        /// Shows a message in the dialog box
        /// </summary>
        /// <param name="text">text to show</param>
        private static void showDialog(String text, string[] inOptions)
        {
            isVisible = true;
            isAnimating = true;
            //clear these variables
            remainingText = "";
            lineOne = "";
            lineTwo = "";
            //split the text into lines that can fit in the box
            //first split the text into a bunch of words
            String[] words = text.Split(' ');
            String buffer = "";
            //add each word to a new string, when that string exceeds a certain length add it to line 1 if not null, else line 2 if not null, else remaining text
            foreach (String word in words)
            {
                //if the current word is not empty or null
                if (word != null && word != "")
                {
                    //then if the width of the buffer plus the new word exceedes the desired width
                    if (font.MeasureString(buffer + " " + word).X > 560)
                    {
                        //add the buffer to the correct location then clear the buffer
                        if (lineOne == "" || lineOne == null)
                        {
                            lineOne = buffer;
                            buffer = word;
                        }
                        else if (lineTwo == "" || lineTwo == null)
                        {
                            lineTwo = buffer;
                            buffer = word;
                        }
                        else
                        {
                            remainingText = remainingText + " " + buffer; //yes, += is correct
                            buffer = word;
                        }

                    }
                    //else then add the line to the buffer
                    else
                    {
                        buffer = buffer + " " + word;
                        buffer = buffer.Trim();
                    }
                }
            }
            //if the buffer is not empty then add it to the highest priority clear String
            if (lineOne == "" || lineOne == null)
            {
                lineOne = buffer;
                buffer = "";
            }
            else if (lineTwo == "" || lineTwo == null)
            {
                lineTwo = buffer;
                buffer = "";
            }
            else
            {
                remainingText = remainingText + " " + buffer; //yes, += is correct
                buffer = "";
                remainingText = remainingText.Trim();
            }

        }

        /// <summary>
        /// Add a new message to be displayed
        /// </summary>
        /// <param name="text"></param>
        static public void newMessage(String text)
        {
            messageBuffer.Add(text);

            if (DialogBox.isVisible == false)
            {
                showDialog(messageBuffer[0]);
                messageBuffer.RemoveAt(0);
            }
        }

        /// <summary>
        /// This method is called once per frame (60fps is desired) to update logic.
        /// </summary>
        static public void Update()
        {

        }

        /// <summary>
        /// This method is called once per frame (60fps is desired) to draw.
        /// </summary>
        static public void Draw()
        {
            if (isVisible)
            {
                spriteBatch.Draw(ScreenHandler.Textures.DialogueBox, ScreenHandler.Rectangles.DialogueBox, Color.White);
                Vector2 L1 = new Vector2((float)ScreenHandler.Rectangles.DialogueBox.X + 50f, (float)ScreenHandler.Rectangles.DialogueBox.Y + 10f);
                spriteBatch.DrawString(font, lineOne, L1, Color.Blue);
                Vector2 L2 = new Vector2((float)ScreenHandler.Rectangles.DialogueBox.X + 50f, (float)ScreenHandler.Rectangles.DialogueBox.Y + 40f);
                spriteBatch.DrawString(font, lineTwo, L2, Color.Blue);
            }
        }

        public static void HandleInput()
        {
            if (Input.Input.isKeyPress(Keys.Z))
            {
                Input.Input.coolDown = Input.Input.cooldownMax;
                if (remainingText != null && remainingText != "")
                {
                    showDialog(remainingText);
                }
                else
                {
                    if (messageBuffer.Count > 0)
                    {
                        showDialog(messageBuffer[0]);
                        messageBuffer.RemoveAt(0);
                    }
                    else
                    {
                        DialogBox.isVisible = false;
                    }
                }

            }
        }

    }
}

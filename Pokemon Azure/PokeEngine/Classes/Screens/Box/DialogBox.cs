using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Menu;
using PokeEngine.Input;
using System.Threading;

namespace PokeEngine.Screens
{
    public class DialogBox : Screen
    {

        private MenuWindow menuWindow;
        private static GraphicsDeviceManager stgraphics;
        private static ContentManager stcontent;
        private static SpriteFont stfont;
        private string lineOne, lineTwo, remainingText;
        private Queue<Message> messageQueue;
        private bool isAnimating = false;
        private Message currentMessage;
        public static int choice { get; set; }

        public static void Initialise(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
        {
            stgraphics = g;
            stcontent = c;
            stfont = f;
        }

        public DialogBox(GraphicsDeviceManager g, ContentManager c, SpriteFont f, Message s)
            : base(g, c, f)
        {
            Name = "DialogBox";

            messageQueue = new Queue<Message>();
            
            ShowDialog(s);
        }

        public DialogBox(GraphicsDeviceManager g, ContentManager c, SpriteFont f, String s)
            : base(g, c, f)
        {
            Name = "DialogBox";

            messageQueue = new Queue<Message>();

            ShowDialog(new Message(s));

            List<string> stringList = new List<string>();
            stringList.Add(lineOne);
            stringList.Add(lineTwo);

            menuWindow = new MenuWindow(
                Vector2.Zero,
                stringList,
                5f);

            menuWindow.SetMarkerEnabled(false);

            setPosition();
        }
    

        /// <summary>
        /// Add a new message to be displayed
        /// </summary>
        /// <param name="text"></param>
        static public void newMessage(GraphicsDeviceManager g, ContentManager c, SpriteFont f, Message msg)
        {
            //check that dialog box is the top screen, if not then open it
            if (ScreenHandler.TopScreen.GetType() != typeof(DialogBox))
            {
                ScreenHandler.PushScreen(new DialogBox(g, c, f, msg));
                OptionBox.isActive = false;
            }
            //otherwise add the message to the queue
            else
            {
                ((DialogBox)ScreenHandler.TopScreen).QueueMessage(msg);
                OptionBox.isActive = false;
            }
        }

        static public void newMessage(GraphicsDeviceManager g, ContentManager c, SpriteFont f, string msg)
        {
            newMessage(g, c, f, new Message(msg));
        }

        static public void newMessage(string msg)
        {
            newMessage(stgraphics, stcontent, stfont, new Message(msg));
        }

        public void ShowDialog(Message msg)
        {
            currentMessage = msg;
            isAnimating = true;
            //clear these variables
            lineOne = ""; lineTwo = ""; remainingText = "";

            //split the text into lines that can fit in the box
            //first split the text into a bunch of words
            string[] words = msg.text.Split(' ');

            bool lineOneDone = false, lineTwoDone = false;
            float maxWidth = ScreenHandler.SCREEN_WIDTH - (32 * 2) - (5 * 2);

            //loop through words in message text
            for (int i = 0; i < words.Length; i++)
            {
                words[i] += ' ';
                //add to line one until it exceeds the max length
                if (!lineOneDone)
                {
                    string newLine = lineOne + words[i];
                    float newLineLength = font.MeasureString(newLine + " " + words[i]).X;
                    if (newLineLength < maxWidth)
                    {
                        lineOne = newLine;
                        continue;
                    }
                    else
                    {
                        lineOneDone = true;
                        i--;
                    }
                }
                //then add to line two until it exceeds the max length
                else if (!lineTwoDone)
                {
                    string newLine = lineTwo + words[i];
                    float newLineLength = font.MeasureString(newLine + " " + words[i]).X;
                    if (newLineLength < maxWidth)
                    {
                        lineTwo = newLine;
                        continue;
                    }
                    else
                    {
                        lineTwoDone = true;
                        i--;
                    }
                }
                //collect the remaining words
                else
                {
                    remainingText = remainingText + words[i];
                }
            }
            //show the actual box
            List<string> stringList = new List<string>();
            stringList.Add(lineOne);
            stringList.Add(lineTwo);

            menuWindow = new MenuWindow(
                Vector2.Zero,
                stringList,
                5f);

            menuWindow.SetMarkerEnabled(false);

            setPosition();
        }

        private void setPosition()
        {
            menuWindow.Position = new Vector2(
                (ScreenHandler.SCREEN_WIDTH / 2) - ((menuWindow.size.x * 32) / 2),
                ScreenHandler.SCREEN_HEIGHT - (menuWindow.size.y * 32));
        }

        public void QueueMessage(Message msg)
        {
            messageQueue.Enqueue(msg);
        }

        public void QueueMessage(string msg)
        {
            messageQueue.Enqueue(new Message(msg));
        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
            if(InputHandler.WasKeyPressed(keyState, KeyConfig.Action))
            {
                //handle options
                if (OptionBox.isVisible && OptionBox.isActive)
                {
                    //finalize the choice the user makes
                    OptionBox.isVisible = false;
                    lock (TFSH.PokeEngineScriptHelper.lockObject)
                    {
                        //send pulse to method to continue execution
                        Monitor.PulseAll(TFSH.PokeEngineScriptHelper.lockObject);
                    }
                    Close();
                }
                //handle regular text
                else
                {
                    if (remainingText.Replace(" ", "") == "")
                    {
                        if (messageQueue.Count > 0)
                        {
                            ShowDialog(messageQueue.Dequeue());
                            setPosition();
                        }
                        else if (!currentMessage.hasOptions)
                        {
                            Close();
                        }
                    }
                    else
                    {
                        ShowDialog(new Message(remainingText));
                        menuWindow.ChangeOption(0, lineOne);
                        if (lineTwo.Replace(" ", "") == "")
                        {
                            menuWindow.RemoveOption(1);
                            setPosition();
                        }
                        else
                        {
                            menuWindow.ChangeOption(1, lineTwo);
                            setPosition();
                        }
                    }
                }
            }
            //press up to decrease option choice by 1, minimum is zero
            if (InputHandler.WasKeyPressed(keyState, KeyConfig.Up, 10))
            {
                if (OptionBox.isVisible)
                {
                    choice--;
                    if (choice < 0)
                    {
                        choice = 0;
                    }
                    if (choice < OptionBox.topChoice)
                    {
                        OptionBox.topChoice--;
                    }

                }
            }
            //press down to increase option choice by 1, maximum of options.length-1 (cause zero index)
            if (InputHandler.WasKeyPressed(keyState, KeyConfig.Down, 10))
            {
                if (OptionBox.isVisible)
                {
                    choice++;
                    if (choice >= OptionBox.options.Length)
                    {
                        choice = OptionBox.options.Length - 1;
                    }
                    if (choice > OptionBox.topChoice + 3)
                    {
                        OptionBox.topChoice++;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsVisible && !OptionBox.isVisible)
            {
                if (remainingText == "" && currentMessage.hasOptions)
                {
                    //show options box
                    OptionBox.newOption(currentMessage.option);
                    OptionBox.isActive = true;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuWindow.Draw(spriteBatch, font, Color.White);
            if (OptionBox.isVisible)
            {
                OptionBox.Draw(spriteBatch, choice, font);
            }
        }
    }

    /// <summary>
    /// Option box sits over the top of a dialog box
    /// options will appear at the end of the message
    /// </summary>
    static public class OptionBox
    {
        static public bool isActive;
        static public bool isVisible;
        static public String[] options;
        static private Window menu;
        static public int topChoice; //the choice that will be at the top of the drawn options when number of options > 4

        static Vector2 menuPosition = new Vector2(390, 150);

        static public void newOption(String[] inOptions)
        {
            DialogBox.choice = 0;
            topChoice = 0;
            isVisible = true;
            options = inOptions;
            //size increases to fit a maximum of four options at a time (take the min of 6 and 1.5*number of options)
            menu = new Window(8, Convert.ToInt32(Math.Min(6, Math.Ceiling(1.5 * options.Length))), menuPosition);
        }

        static public void Draw(SpriteBatch sb, int choice, SpriteFont font)
        {
            //draw up to 4 of the options
            menu.Draw(sb, Color.White);
            //if number of options is 4 or fewer then draw all options
            if (options.Length <= 4)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (i != choice)
                        sb.DrawString(font, options[i], new Vector2(menuPosition.X + 40, menuPosition.Y + (i + 1) * 30), Color.LightSlateGray);
                    else
                        sb.DrawString(font, options[i], new Vector2(menuPosition.X + 40, menuPosition.Y + (i + 1) * 30), Color.White);
                }
            }
            //if number of options is greater than 4 draw four, starting from topChoice
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i + topChoice != choice)
                        sb.DrawString(font, options[topChoice + i], new Vector2(menuPosition.X + 40, menuPosition.Y + (i + 1) * 30), Color.LightSlateGray);
                    else
                        sb.DrawString(font, options[topChoice + i], new Vector2(menuPosition.X + 40, menuPosition.Y + (i + 1) * 30), Color.White);
                }
            }
        }
    }

    public class Message
    {
        public String text;
        public bool hasOptions;
        public String[] option;

        public Message()
        {
            text = "DEFAULT MESSAGE";
            hasOptions = false;
            option = null;
        }

        public Message(String text)
        {
            this.text = text;
            hasOptions = false;
            this.option = null;
        }

        public Message(String text, String[] options)
        {
            this.text = text;
            this.option = options;
            hasOptions = true;
        }
    }

}

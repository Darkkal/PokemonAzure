using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using PokeEngine.Menu;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using PokeEngine.Input;

namespace PokeEngine.Screens
{
    class CinematicScreen : Screen
    {
        private Object lockObject;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public ContentManager content;
        private SpriteFont font;

        private bool requiresInput;
        private bool canSkip;
        private Texture2D tex;
        private Texture2D nextTex;
        private Rectangle drawLoc;
        private Vector2 startPoint;
        private Vector2 endPoint;
        private Vector2 drawPoint;
        private float startScale;
        private float endScale;
        private int maxTime;
        private int time;
        private int actionIndex; //the action we are up to in the list
        private List<CinematicAction> actions;

        public CinematicScreen(GraphicsDeviceManager g, ContentManager c, SpriteFont f, List<CinematicAction> inActions) : base(g, c, f)
        {
            graphics = g;
            content = c;
            font = f;
            lockObject = new Object();

            NewCinematic(inActions);

            drawLoc = new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2, 1, 1);
            Name = "CinematicScreen";
        }

        /// <summary>
        /// Starts up a new cinematic with the list of actions.
        /// Once the list is depleted it ends.
        /// </summary>
        /// <param name="actions"></param>
        public void NewCinematic(List<CinematicAction> inActions)
        {
            if(inActions != null && inActions.Count >= 1)
            {
                actions = inActions;
                actionIndex = 0;
                time = 0;
                maxTime = actions[actionIndex].maxTime;
                startPoint = actions[actionIndex].startPoint;
                endPoint = actions[actionIndex].endPoint;
                startScale = actions[actionIndex].startScale;
                endScale = actions[actionIndex].endScale;
                requiresInput = actions[actionIndex].requiresInput;
                canSkip = actions[actionIndex].canSkip;

                LoadNextImage(actions[actionIndex]); //load up the first texture (goes into nextTex)
                tex = nextTex; //set it as the current texture
                nextTex = null;

                //if there is a second action start loading that up
                if (actions.Count >= 2)
                {
                    if (actions[actionIndex].image == actions[actionIndex + 1].image)
                    {
                        nextTex = tex;
                    }
                    else
                    {
                        Task.Factory.StartNew(() => LoadNextImage(actions[actionIndex + 1]));
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
           
            //if the current action has run it's course and we are out of actions
            if (time >= maxTime && actionIndex + 1 >= actions.Count)
            {
                //automatically finish only if we don't require input
                if (!requiresInput)
                {
                    FinishCinematic();
                }
            }
            //if the current action has run it's course and we still have remaining actions
            else if (time >= maxTime && actionIndex + 1 < actions.Count)
            {
                //automatically move to next one only if we don't require input
                if (!requiresInput)
                {
                    NextAction();
                }
            }
            //otherwise update as normal
            else
            {
                //update positon and scale of the image
                drawPoint = new Vector2(startPoint.X + ((float)time / (float)maxTime) * (endPoint.X - startPoint.X),
                                        startPoint.Y + ((float)time / (float)maxTime) * (endPoint.Y - startPoint.Y));

                float currentScale = startScale + ((float)time / (float)maxTime) * (endScale - startScale);
                drawLoc.Width = (int)((float)tex.Width * currentScale);
                drawLoc.Height = (int)((float)tex.Height * currentScale);
                time++;
            }
            
        }

        private void NextAction()
        {
            //swap to the next action
            actionIndex++;
            time = 0;
            maxTime = actions[actionIndex].maxTime;
            startPoint = actions[actionIndex].startPoint;
            endPoint = actions[actionIndex].endPoint;
            startScale = actions[actionIndex].startScale;
            endScale = actions[actionIndex].endScale;
            requiresInput = actions[actionIndex].requiresInput;
            canSkip = actions[actionIndex].canSkip;
            if (!String.IsNullOrEmpty(actions[actionIndex].message))
            {
                ScreenHandler.PushScreen(new DialogBox(graphics, content, font, actions[actionIndex].message));
                //DialogBox.newMessage(actions[actionIndex].message);
            }

            //swap to the next texture
            lock (lockObject)
            {
                while (nextTex == null)
                {
                    Monitor.Wait(lockObject);
                }
                tex = nextTex;
                nextTex = null;
            }

            //begin loading next texture if applicable
            if (actionIndex + 1 < actions.Count)
            {
                if (actions[actionIndex].image == actions[actionIndex + 1].image)
                {
                    nextTex = tex;
                }
                else
                    Task.Factory.StartNew(() => LoadNextImage(actions[actionIndex + 1]));
            }
        }

        private void FinishCinematic()
        {
            ScreenHandler.PopScreen();
        }

        public override void HandleInput(GamePadState gamePadState, KeyboardState keyState, MouseState mouseState)
        {
            if (canSkip)
            {
                if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[4], 10))
                {
                    //if we are out of actions
                    if (actionIndex + 1 >= actions.Count)
                    {
                        FinishCinematic();
                    }
                    //if there are remaning actions
                    else if (actionIndex + 1 < actions.Count)
                    {
                        NextAction();
                    }

                }
            }
            else if (requiresInput)
            {
                if (Input.InputHandler.WasKeyPressed(keyState, KeyConfig.KeyList[4], 10))
                {
                    //if the current action has run it's course and we are out of actions
                    if (time >= maxTime && actionIndex + 1 >= actions.Count)
                    {
                        //finish on input
                        FinishCinematic();
                    }
                    //if the current action has run it's course and we still have remaining actions
                    else if (time >= maxTime && actionIndex + 1 < actions.Count)
                    {
                        //move to next action on input
                        NextAction();
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(tex, drawLoc, null, Color.White, 0f, drawPoint, SpriteEffects.None, 0);
            }
        }

        private void LoadNextImage(CinematicAction action)
        {
            lock (lockObject)
            {
                string rootDir = Directory.GetCurrentDirectory();
                string path = rootDir + "\\Content\\Sprites\\Cinematics\\" + action.image;
                nextTex = Texture2D.FromStream(graphics.GraphicsDevice, new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));

                Monitor.Pulse(lockObject);
            }
        }
    }

    public class CinematicAction
    {
        internal Vector2 startPoint;
        internal Vector2 endPoint;
        internal float startScale;
        internal float endScale;
        internal int maxTime;
        internal String image;
        internal bool requiresInput; //whether you must press a key to advance to the next action
        internal bool canSkip; //whether we can skip to the next action
        internal String message; //message to display in dialogue box at beginning of action

        public CinematicAction(Vector2 startPoint, Vector2 endPoint, float startScale, float endScale, int maxTime, String message, String image, bool input, bool skip)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.startScale = startScale;
            this.endScale = endScale;
            this.maxTime = maxTime;
            this.image = image;
            this.requiresInput = input;
            this.canSkip = skip;
            this.message = message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PokeEngine.Screens;

namespace PokeEngine.Menu
{
    class MenuWindow : Window
    {

        protected List<string> optionList;
        protected int selection;
        protected float padding;
        protected bool isMarkerEnabled;

        public MenuWindow(Vector2 position, List<string> listOfOptions, float menuPadding)
            :base(position)
        {
            isMarkerEnabled = true;
            optionList = listOfOptions;
            padding = menuPadding;
            selection = 0;
            SetSize();
        }

        protected string getLongestOption()
        {
            string option = optionList[0];


            for (int i = 1; i < optionList.Count; i++)
            {
                if (option.Length < optionList[i].Length)
                    option = optionList[i];
            }

            return option;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Color color)
        {

            base.Draw(spriteBatch, color);

            for (int i = 0; i < optionList.Count; i++)
            {
                spriteBatch.DrawString(font, optionList[i], GetOptionPosition(i), color);

                if (isMarkerEnabled && selection == i)
                {
                    Rectangle markerRect = new Rectangle(
                        (int)GetMarkerPos().X,
                        (int)GetMarkerPos().Y,
                        ScreenHandler.WindowMarker.Width,
                        ScreenHandler.WindowMarker.Height);

                    spriteBatch.Draw(ScreenHandler.WindowMarker, markerRect, color);
                }
            }

        }

        public void SelectionDown()
        {
            selection = selection < optionList.Count - 1 ? selection + 1 : 0;
        }

        public void SelectionUp()
        {
            selection = selection > 0 ? selection - 1 : optionList.Count - 1;
        }

        public int GetSelection() { return selection; }

        public void SetSize()
        {
            /// fontwidth * longestOptionLength gives us the width of the string
            /// 2 * padding gives us the extra space asked for 
            /// divide all by 32 will give us the size - 1 of the box
            /// add one to make up for the lost change in the conversion from float to int
            size.x = (int)Math.Ceiling(((ScreenHandler.FontWidth * getLongestOption().Length) + padding) / 32) + 2;

            /// (optionList.Count + 1) * padding gives us the total padding space including the top and bottom
            /// (optionList.Count * fontHeight) gives us the total space taken up by the options
            /// divide all by 32 gives us size - 1 of the box
            /// add one to make up for the lost change in the conversion from float to int
            size.y = (int)Math.Ceiling((((optionList.Count + 1) * padding) + (optionList.Count * ScreenHandler.FontHeight)) / 32) + 2;
        }

        public void SetSize(int width, int height) { size.x = width; size.y = height; }

        public void SetWidth(int width) { size.x = width; }

        public void SetHeight(int height) { size.y = height; }

        public List<string> GetOptionList() { return optionList; }
        
        public void ChangeOption(int index, string newOption)
        {
            if (index > -1 && index < optionList.Count)
            {
                optionList[index] = newOption;
                SetSize();
            }
        }

        public void RemoveOption(int index)
        {
            if (index > -1 && index < optionList.Count)
            {
                optionList.RemoveAt(index);
                SetSize();
            }
        }

        public Vector2 GetOptionPosition(int index)
        {
            Vector2 pos = Vector2.Zero;

            if (index < optionList.Count && index > -1)
            {
                pos = new Vector2(
                    Position.X + padding + ScreenHandler.WindowCorner.Width,
                    Position.Y + (padding * (index + 1)) + (ScreenHandler.FontHeight * index) + ScreenHandler.WindowCorner.Height);
            }

            return pos;
        }

        public Vector2 GetMarkerPos()
        {
            Vector2 pos = new Vector2(
                (Position.X + (padding / 2) + (ScreenHandler.WindowMarker.Width / 2)),
                GetOptionPosition(selection).Y + (ScreenHandler.WindowMarker.Height / 2));

            return pos;
        }

        public void SetPadding(float p) { padding = p; }

        public void SetMarkerEnabled(bool state) { isMarkerEnabled = state; }

    }
}

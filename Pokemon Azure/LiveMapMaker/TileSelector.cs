using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveMapMaker
{
    public partial class TileSelector : PictureBox
    {
        Editor editor;

        public TileSelector(int x, int y, Editor inEditor)
        {
            InitializeComponent();
            editor = inEditor;
            Size = new System.Drawing.Size(24, 24);
            Location = new Point(x, y);
        }

        private void TileSelector_Click(object sender, EventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Shift)
                {
                    Console.WriteLine("tile selected with shift key pressed");
                }
                else
                {
                    editor.pbox_TilePreview.Load(ImageLocation);
                    editor.pbox_TilePreview.Refresh();
                    editor.pbox_TilePreview.Tag = Tag;
                }


            }
            catch (ArgumentException)
            { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokeEngine.Screens;
using System.Collections;

namespace LiveMapMaker
{
    public partial class NewTCForm : Form
    {
        private Editor editor;
        ListViewItem existingItem;

        public NewTCForm(Editor inEditor)
        {
            InitializeComponent();
            editor = inEditor;
            existingItem = null;
        }

        public NewTCForm(Editor inEditor, ListViewItem existingItem)
        {
            InitializeComponent();
            editor = inEditor;
            this.existingItem = existingItem;
            timeBox.Text = existingItem.Text;
            commandBox.Text = existingItem.SubItems[1].Text;
            button1.Text = "Edit";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int time = Convert.ToInt32(timeBox.Text);
                String comTex = commandBox.Text;

                if (existingItem == null)
                {
                    ListViewItem item = new ListViewItem(timeBox.Text);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, comTex));
                    editor.commandList.Items.Add(item);
                }
                else
                {
                    existingItem.Text = timeBox.Text;
                    existingItem.SubItems[1].Text = comTex;
                    editor.commandList.Sort();
                }
                Close();
                Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

    public class TCComparer : IComparer
    {
        public TCComparer()
        {

        }

        public int Compare(object x, object y)
        {
            try
            {
                return Convert.ToInt32(((ListViewItem)x).Text) - Convert.ToInt32(((ListViewItem)y).Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}

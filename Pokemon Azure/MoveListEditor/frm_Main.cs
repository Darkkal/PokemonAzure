using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PokeEngine.Moves;

namespace MoveListEditor
{
    public partial class frm_Main : Form
    {
        string FileName;

        public frm_Main()
        {
            InitializeComponent();
        }

        private void LoadData(string fileName)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                MoveList.move = (List<BaseMove>)formatter.Deserialize(stream);
                RefreshListBox();

            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RefreshListBox()
        {
            lbox_MoveList.Items.Clear();
            for (int i = 0; i < MoveList.move.Count; i++)
            {
                lbox_MoveList.Items.Add(MoveList.move[i]);
            }

        }

        private void SaveData(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            formatter.Serialize(stream, MoveList.move);
            stream.Close();
        }

        private void SaveMoveData(string moveName)
        {
            BaseMove temp = new BaseMove(
                moveName,
                tbox_Description.Text,
                Int32.Parse(tbox_Power.Text),
                Int32.Parse(tbox_Accuracy.Text),
                (string)lbox_MoveType.SelectedItem,
                (string)lbox_MoveKind.SelectedItem,
                Int32.Parse(tbox_PP.Text));

            if (MoveList.getMove(moveName) == null)
            {
                if (MessageBox.Show(moveName + "does not exist in the Move List.\nWould you like to add it to the list?", "Input Required", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    MoveList.addMove(temp);
                else if (MessageBox.Show("Would you like to change the move in the MoveList, including it's name?", "Input Requred", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    MoveList.move[MoveList.getIndex((string)lbox_MoveList.SelectedItem)] = temp;
                    lbox_MoveList.Items.Add(temp);
                }
            }
            else if (MessageBox.Show("Would you like to overwrite the move in the MoveList?", "Input Required", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                MoveList.addMove(temp);

            RefreshListBox();
        }

        private void RefreshFormData(string moveName)
        {
            BaseMove temp = MoveList.getMove(moveName);

            tbox_Name.Text = temp.name;
            tbox_Description.Text = temp.description;
            tbox_Power.Text = temp.power.ToString();
            tbox_Accuracy.Text = temp.accuracy.ToString();

            for (int i = 0; i < lbox_MoveType.Items.Count; i++)
            {
                if (temp.moveType == (string)lbox_MoveType.Items[i])
                {
                    lbox_MoveType.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < lbox_MoveKind.Items.Count; i++)
            {
                if (temp.moveKind == (string)lbox_MoveKind.Items[i])
                {
                    lbox_MoveKind.SelectedIndex = i;
                    break;
                }
            }

            tbox_PP.Text = temp.basePP.ToString();

            tbox_MoveScript.Text = temp.moveScript;
            tbox_EffectScript.Text = temp.effectScript;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PokeEngine MoveList binary (movelist.dat)|movelist.dat";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadData(ofd.FileName);
                FileName = ofd.FileName;
            }
        }

        private void lbox_MoveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!gbox_Editor.Enabled)
                gbox_Editor.Enabled = true;
            if (!moveListToolStripMenuItem.Enabled)
                moveListToolStripMenuItem.Enabled = true;

            RefreshFormData(((BaseMove)lbox_MoveList.SelectedItem).name);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshFormData(((BaseMove)lbox_MoveList.SelectedItem).name);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveMoveData(tbox_Name.Text);
            RefreshListBox();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData(FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".dat";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveData(sfd.FileName);
                FileName = sfd.FileName;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_AddMove addMove = new frm_AddMove();

            if (addMove.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MoveList.addMove(addMove.baseMove);
            }
        }
    }
}
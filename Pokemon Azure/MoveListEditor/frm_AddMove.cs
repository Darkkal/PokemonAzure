using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokeEngine.Moves;

namespace MoveListEditor
{
    public partial class frm_AddMove : Form
    {

        public BaseMove baseMove = new BaseMove();

        public frm_AddMove()
        {
            InitializeComponent();

            lbox_MoveKind.SelectedIndex = 0;
            lbox_MoveType.SelectedIndex = 0;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                baseMove.accuracy = Int32.Parse(tbox_Accuracy.Text);
                baseMove.basePP = Int32.Parse(tbox_PP.Text);
                baseMove.description = tbox_Description.Text;
                baseMove.effectScript = tbox_EffectScript.Text;
                baseMove.moveKind = (string)lbox_MoveKind.SelectedItem;
                baseMove.moveScript = tbox_MoveScript.Text;
                baseMove.moveType = (string)lbox_MoveType.SelectedItem;
                baseMove.name = tbox_Name.Text;
                baseMove.power = Int32.Parse(tbox_Power.Text);

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("You didn't finish the form!");
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}

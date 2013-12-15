using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokeEngine.Map;
using Microsoft.Xna.Framework;
using System.IO;

namespace LiveMapMaker
{
    public partial class NewSceneryForm : Form
    {
        Editor editor;

        public NewSceneryForm(Editor inEditor)
        {
            InitializeComponent();
            editor = inEditor;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Scenery newScenery = new Scenery();
            int x = editor.game.selectedX;
            int y = editor.game.selectedY;
            int xSize = 0;
            int ySize = 0;

            //get the size of the model if possible
            try
            {
                xSize = Convert.ToInt32(modelXSize.Text);
                ySize = Convert.ToInt32(modelYSize.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("This is not a valid size for the model");
                return;
            }

            //if we have a model selected then we can procede
            if (!String.IsNullOrWhiteSpace((String)modelBox.SelectedItem))
            {
                //add scenery to the selected tile
                newScenery = new Scenery(sceneryNameBox.Text,
                                         sceneryScriptBox.Text,
                                         modelBox.SelectedItem.ToString(),
                                         new Point(x, y),
                                         new Point(xSize, ySize));

                //move model to centre of selected size area
                newScenery.translation = Matrix.CreateTranslation((float)xSize / 2f, 0, (float)ySize / 2f);
            }
            //otherwise we tell the user to select a model
            else
            {
                MessageBox.Show("Select a Model first");
                return;
            }

            //try
            {
                editor.sceneryBox.Items.Add(newScenery.name);
                editor.sceneryList.Add(newScenery.name, newScenery);
            }
            //catch
            
            //hide rather than close to preserve list of models
            Hide();
            //select newly created scenery
            editor.sceneryBox.SelectedItem = newScenery.name;
        }

        private void loadModelButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Model File (*.x, *.fbx)|*.x;*.fbx";


            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //use the modelbuilder to create a compiled version of the model
                ModelBuilder.addModel(ofd.FileName);

                //add that model to the list of available models
                modelBox.Items.Add(Path.GetFileNameWithoutExtension(ofd.FileName));
            }

            //select newly added model
            modelBox.SelectedItem = Path.GetFileNameWithoutExtension(ofd.FileName);
        }
    }
}

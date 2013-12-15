using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PokeEngine.Map;
using PokeEngine.Screens;
using Microsoft.Xna.Framework;

namespace LiveMapMaker
{
    //Is also used to resize a zone
    public partial class newZoneForm : Form
    {
        private Game1 game;
        private Editor editor;
        private bool edit;

        public newZoneForm(Game1 inGame, Editor inEditor, bool inEdit)
        {
            InitializeComponent();
            game = inGame;
            editor = inEditor;
            edit = inEdit;
            if (edit) 
            {
                this.Text = "Edit Zone";
                nameTextBox.Text = game.world.currentArea.zoneName;
                zoneWidthBox.Text = game.world.currentArea.mapWidth.ToString();
                zoneHeightBox.Text = game.world.currentArea.mapHeight.ToString();
                zoneXBox.Text = game.world.currentArea.globalX.ToString();
                zoneYBox.Text = game.world.currentArea.globalY.ToString();
                isRoomBox.Checked = game.world.currentArea.isRoom;
            }
            else this.Text = "New Zone";
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!edit) //we are making a new zone
            {
                makeNewZone();
                //clear all history
                for (int i = 0; i < editor.historySize; i++)
                {
                    editor.zoneHistory[i] = null;
                }
            }
            else //we are editing the zone
            {
                editCurrentZone();
            }
        }

        private void makeNewZone()
        {
            //read all the boxes to get the information necessary
            int x, y;
            int width, height;
            try
            {
                x = Convert.ToInt32(zoneXBox.Text);
                y = Convert.ToInt32(zoneYBox.Text);
                width = Convert.ToInt32(zoneWidthBox.Text);
                height = Convert.ToInt32(zoneHeightBox.Text);

                //make the zone
                Zone zone = new Zone(width, height, x, y);
                zone.zoneName = nameTextBox.Text;
                zone.isRoom = isRoomBox.Checked;

                game.world.addZone(zone);
                
                game.world.changeZone(zone);
                GameDraw.MakeAdjBuffers(game.world);
                EditorScreen.lookAtPosition = new Vector3(x, y, 0);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //close the form
                Close();
            }
            catch (FormatException) { /*do nothing*/ }
            catch (ArgumentException)
            {
                MessageBox.Show("Zone with same name already exists");
            }
        }

        private void editCurrentZone()
        {
            //read all the boxes to get the information necessary
            int x, y;
            int width, height;
            try
            {
                x = Convert.ToInt32(zoneXBox.Text);
                y = Convert.ToInt32(zoneYBox.Text);
                width = Convert.ToInt32(zoneWidthBox.Text);
                height = Convert.ToInt32(zoneHeightBox.Text);

                //make the zone
                Zone zone = new Zone(width, height, x, y);
                zone.zoneName = nameTextBox.Text;
                zone.isRoom = isRoomBox.Checked;
                zone.scenery = game.world.currentArea.scenery;
                zone.tileSheetLocation = game.world.currentArea.tileSheetLocation;
                zone.trainerList = game.world.currentArea.trainerList;
                zone.randomPokemon = game.world.currentArea.randomPokemon;

                for (int i = 0; i < Math.Min(zone.mapWidth, game.world.currentArea.mapWidth); i++)
                {
                    for (int j = 0; j < Math.Min(zone.mapHeight, game.world.currentArea.mapHeight); j++)
                    {
                        zone.tile[i, j] = new Tile(game.world.currentArea.tile[i, j]);
                    }
                }

                game.world.addZone(zone);

                game.world.changeZone(zone);
                GameDraw.MakeAdjBuffers(game.world);
                EditorScreen.lookAtPosition = new Vector3(x, y, 0);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //close the form
                Close();
            }
            catch (FormatException) { /*do nothing*/ }
            catch (ArgumentException)
            {
                MessageBox.Show("Zone with same name already exists");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}

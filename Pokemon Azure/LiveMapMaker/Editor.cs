using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LiveMapMaker.Tools;
using System.IO;
using PokeEngine.Screens;
using PokeEngine.Map;
using PokeEngine.Pokemon;
using PokeEngine.Trainers;
using Microsoft.Xna.Framework;
using PokeEngine.Tools;

namespace LiveMapMaker
{
    public partial class Editor : Form
    {
        public Game1 game;
        public string tileDirectory;
        private NewSceneryForm sceneryForm = null;
        public SortedList<String, Scenery> sceneryList = new SortedList<string, Scenery>();
        public PokeEngine.Trainers.NPC tempNPC;
        public int historySize = 25;// size of the history
        public int historyIndex = 0; //location where we are 'undo-wise'
        //can be up to a maximum of historySize - 1
        public Zone[] zoneHistory;
        public Graphics g;
        public Bitmap bmp;
        public System.Drawing.Point selectedTile;
        public System.Drawing.Point oldSelectedTile;
        public Scenery activeSceneryEdit; //scenery we are currently editing/placing
        public NPC activeNPCEdit; //NPC we are editing when editing an NPC
        public bool editingBaseScenery = true;
        public String NPCImageToUse;
        public List<SelectedTile> selectedTiles = new List<SelectedTile>();

        public bool ShowModels;
        public bool ShowNPCS;

        public Editor()
        {
            InitializeComponent();
            selectedTile = System.Drawing.Point.Empty;
            oldSelectedTile = System.Drawing.Point.Empty;
            ShowModels = true;
            ShowNPCS = true;
            commandList.ListViewItemSorter = new TCComparer();
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.Exit();
        }

        public void updateDebug(string text)
        {
            debugLabel.Text = text;
        }

        public void updateGlobalDebug(string text)
        {
            globalDebugLabel.Text = text;
        }

        private void newZoneButton_Click(object sender, EventArgs e)
        {
            newZoneForm zoneForm = new newZoneForm(game, this, false); //false means make new zone
            zoneForm.Show();
            toolTab.Enabled = true;
        }

        private void editCurrentZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newZoneForm zoneForm = new newZoneForm(game, this, true); //false means make new zone
            zoneForm.Show();
            toolTab.Enabled = true;
        }

        private void btn_BrowseTileDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Image tempImage;
            ofd.Multiselect = false;
            //use default zone path if one exists
            if (Directory.Exists(DefaultFileLocations.tileLocation))
            {
                ofd.InitialDirectory = DefaultFileLocations.tileLocation;
            }
            ofd.ShowDialog();
            Image spriteSheet = Bitmap.FromFile(ofd.FileName);
            pbar_ExtractionProgress.Enabled = true;
            pbar_ExtractionProgress.Minimum = 0;
            pbar_ExtractionProgress.Maximum = ((spriteSheet.Height / 24) * (spriteSheet.Width / 24));
            pbar_ExtractionProgress.Step = 1;
            string sheetName = ofd.FileName.Split('\\')[ofd.FileName.Split('\\').GetUpperBound(0)];
            string ofdDirectory = ofd.InitialDirectory + "\\" + sheetName.Replace(".png", "");
            string s;
            Directory.CreateDirectory(ofdDirectory);

            for (int y = 0; y < (spriteSheet.Height / 24); y++)
            {
                for (int x = 0; x < (spriteSheet.Width / 24); x++)
                {
                    s = (ofdDirectory + "\\" + sheetName.Replace(".png", "") + "_" + y + "_" + x + ".png");

                    tempImage = CopyImage(spriteSheet, new System.Drawing.Rectangle((x * 24), (y * 24), 24, 24));
                    tempImage.Save(s);
                    pbar_ExtractionProgress.Increment(pbar_ExtractionProgress.Step);
                    g.Dispose();
                    tempImage.Dispose();
                    bmp.Dispose();
                }
            }

            ImportTileFolder(ofdDirectory);
            game.world.currentArea.tileSheetLocation = ofdDirectory;
            ofd.Dispose();
            lbox_Tiles.Enabled = true;

        }

        private Bitmap CopyImage(Image source, System.Drawing.Rectangle part)
        {
            Bitmap sc = new Bitmap(source);

            bmp = new Bitmap(part.Width, part.Height);
            g = Graphics.FromImage(bmp);
            g.DrawImage(sc, 0, 0, part, GraphicsUnit.Pixel);
            g.Dispose();
            sc.Dispose();
            return bmp;
        }

        private void lbox_Tiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pbox_TilePreview.Load(tileDirectory + "\\" + lbox_Tiles.SelectedItem);
                pbox_TilePreview.Tag = lbox_Tiles.SelectedItem;
            }
            catch (ArgumentException)
            { }
        }

        private void ImportTileFolder(string tileDirectory)
        {
            try
            {
                string[] tiles = Directory.GetFiles(tileDirectory);

                lbox_Tiles.Items.Clear();

                //Clear the directory before using
                foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Content\\Tiles\\"))
                    File.Delete(file);

                foreach (string tile in tiles)
                {
                    if (tile.EndsWith(".png") || tile.EndsWith(".jpg"))
                    {
                        string s = tile.Split('\\')[tile.Split('\\').Length - 1];
                        File.Copy(tile, Directory.GetCurrentDirectory() + "\\Content\\Tiles\\" + s, true);


                        lbox_Tiles.Items.Add(s);
                    }
                }

                lbox_Tiles.Enabled = true;

                //clear our display box
                tilesPanel.Controls.Clear();

                //then display the tiles in our tile display box
                int x = 0;
                int y = 0;

                foreach (String s in lbox_Tiles.Items)
                {
                    TileSelector ts = new TileSelector(x, y, this);
                    ts.Load(tileDirectory + "\\" + s);
                    ts.Parent = tilesPanel;
                    ts.Tag = s;
                    ts.Click += new EventHandler(tileClick);

                    //increment location counters
                    x += 25;
                    if (x >= 240)
                    {
                        x = 0;
                        y += 25;
                    }
                }
            }
            catch (ArgumentException)
            {

            }

            //update the tiles used by gamedraw
            GameDraw.LoadTileTextures();

        }

        private void importDefaultTiles()
        {
            //default directory is \Content\Tiles\
            tileDirectory = Directory.GetCurrentDirectory() + "\\Content\\Tiles\\";

            try
            {
                string[] tiles = Directory.GetFiles(tileDirectory);

                lbox_Tiles.Items.Clear();

                foreach (string tile in tiles)
                {
                    if (tile.EndsWith(".png") || tile.EndsWith(".jpg"))
                    {
                        string s = tile.Split('\\')[tile.Split('\\').Length - 1];
                        //File.Copy(tile, Directory.GetCurrentDirectory() + "\\Content\\Tiles\\" + s, true);


                        lbox_Tiles.Items.Add(s);
                    }
                }

                lbox_Tiles.Enabled = true;

                //clear our display box
                tilesPanel.Controls.Clear();

                //then display the tiles in our tile display box
                int x = 0;
                int y = 0;

                foreach (String s in lbox_Tiles.Items)
                {
                    TileSelector ts = new TileSelector(x, y, this);
                    ts.Load(tileDirectory + s);
                    ts.Parent = tilesPanel;
                    ts.Tag = s;
                    ts.Click += new EventHandler(tileClick);

                    //increment location counters
                    x += 25;
                    if (x >= 240)
                    {
                        x = 0;
                        y += 25;
                    }
                }
            }
            catch (ArgumentException)
            {

            }
        }

        private void tileClick(object sender, EventArgs e)
        {
            TileSelector sel = (TileSelector)sender;
            if (Control.ModifierKeys == Keys.Shift)
            {
                selectedTiles.Add(new SelectedTile((string)sel.Tag, sel.Location.X / 25, sel.Location.Y / 25));
            }
            //if no key is selected make sure the clear out the old selected tiles
            else
            {
                //refresh previously selected tiles
                foreach (TileSelector t in tilesPanel.Controls)
                {
                    foreach (SelectedTile s in selectedTiles)
                    {
                        if (t.Location.X == s.PositionX * 25 && t.Location.Y == s.PositionY * 25)
                        {
                            t.Image.Dispose();
                            t.Load(tileDirectory + t.Tag);
                            Console.WriteLine("oldTile reloaded from {0}", tileDirectory + t.Tag);
                        }
                    }
                }
                //empty the selected list
                selectedTiles.Clear();
                //add the newly select tile to the list
                selectedTiles.Add(new SelectedTile((string)sel.Tag, sel.Location.X / 25, sel.Location.Y / 25));
            }            
            
            Bitmap temp = (Bitmap)sel.Image;
            for (int x = 0; x < temp.Width; x += 2)
                for (int y = 0; y < temp.Height; y += 2)
                    temp.SetPixel(x, y, System.Drawing.Color.Blue);

            sel.Image = temp;
            sel.Refresh();
        }

        private void setAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Set all to Currently Selected Tile Type?", "Confirm Set All", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TileTool.SetAll((String)pbox_TilePreview.Tag);
            }
        }

        private void saveZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save the zone
            //choose the directory
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose directory to save map file...";
            //use default zone path if one exists
            if (Directory.Exists(DefaultFileLocations.zoneLocation))
            {
                fbd.SelectedPath = DefaultFileLocations.zoneLocation;
            }

            fbd.ShowNewFolderButton = true;
            DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK) //make sure "okay" was pressed
            {
                saveZone(fbd.SelectedPath);

                //save selected directory as default
                DefaultFileLocations.zoneLocation = fbd.SelectedPath;
            }
        }

        /// <summary>
        /// Saves the Current Zone in the world
        /// </summary>
        private void saveZone(String location)
        {

            //file will have the zone name as the filename
            String fileLocation = location + "\\" + game.world.currentArea.zoneName + ".zon";

            try
            {
                using (FileStream stream = new FileStream(fileLocation, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        SaveLoad.SaveZone(game.world.currentArea, writer);
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Something went wrong!\nYour map was probably not saved correctly - " + e.Message);
            }

            

        }

        private void loadZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadZone();
        }

        private void loadZone()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pokemon Zone File (*.zon)|*.zon";
            //use default zone path if one exists
            if (Directory.Exists(DefaultFileLocations.zoneLocation))
            {
                ofd.InitialDirectory = DefaultFileLocations.zoneLocation;
            }
            ofd.ShowDialog();
            Zone zone = null;

            try
            {
                using (FileStream stream = new FileStream(ofd.FileName, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        zone = SaveLoad.LoadZone(reader);
                    }
                }                

                ImportTileFolder(zone.tileSheetLocation); //for now its just the location of where the tiles are

                game.world.getBounds();
                game.world.addZone(zone);

                game.world.changeZone(zone);
                GameDraw.MakeAdjBuffers(game.world);
                GameDraw.UpdateNPCSpritesheets(game.world);
                EditorScreen.lookAtPosition = new Vector3(zone.globalX, zone.globalY, 0);

                toolTab.Enabled = true;

                //save selected directory as default
                String[] temp = ofd.FileName.Split('\\');
                DefaultFileLocations.zoneLocation = ofd.FileName.Substring(0, ofd.FileName.Length - temp[temp.Length - 1].Length);
            }
            catch (ArgumentException) { }
        }

        private void newZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newZoneButton_Click(sender, e);
        }

        private void screenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //game.screenShot = true; see -> Game1.Update()
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gbox_TrainerEditor.Enabled = cbox_IsTrainer.Checked;

            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                //make the default payout zero
                NPCEditRadio.Checked = false;
                tbox_TrainerPayout.Text = "0";

                //create new NPC
                NPC temp = CreateNPC();
                temp.tileCoords = activeNPCEdit.tileCoords;
                //remove old NPC
                game.world.currentArea.trainerList.Remove(activeNPCEdit);
                //add new NPC
                activeNPCEdit = temp;
                game.world.currentArea.trainerList.Add(activeNPCEdit);

                NPCEditRadio.Checked = true;
            }
        }

        private void exportTrainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //The image is copied into the Overworlds Sprites directory.
                //Will overwrite old file
                String pathToSprite = Directory.GetCurrentDirectory() + "\\Content\\Sprites\\NPCs\\Overworlds\\" + Path.GetFileName(NPCImageToUse);
                try
                {
                    File.Copy(NPCImageToUse, pathToSprite, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                NPC tempNPC = CreateNPC();

                game.world.currentArea.trainerList.Add(tempNPC);

                tempNPC = null;

                ResetNPCTab();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public NPC CreateNPC()
        {
            NPC tempNPC = new NPC();

            if (cbox_IsTrainer.Checked)
            {
                tempNPC = new Trainer();

                ((Trainer)tempNPC).money = Int32.Parse(tbox_TrainerPayout.Text);

                foreach (ListViewItem lvi in lview_TrainerActivePokemon.Items)
                    if (lvi.Tag != null && (lvi.Tag.GetType() == typeof(ActivePokemon))) //if the object is not null and is an ActivePokemon
                        ((ActivePokemon)(lvi.Tag)).trainer = (Trainer)tempNPC; //set the activepokemon's trainer to the one currently being used
            }

            //dump the rest of the npc tab's info into the tempNPC
            tempNPC.name = tbox_NPCName.Text;
            tempNPC.isMale = cbox_IsMale.Checked;

            if (rbtn_NPCFacingDirection_DOWN.Checked)
                tempNPC.facing = FacingDirection.South;
            if (rbtn_NPCFacingDirection_UP.Checked)
                tempNPC.facing = FacingDirection.North;
            if (rbtn_NPCFacingDirection_LEFT.Checked)
                tempNPC.facing = FacingDirection.West;
            if (rbtn_NPCFacingDirection_RIGHT.Checked)
                tempNPC.facing = FacingDirection.East;

            tempNPC.spriteSheet = Path.GetFileName(NPCImageToUse); //the tag will be a string that contains the image to use
            //copy the sprite over to the content folder
            if (!String.IsNullOrWhiteSpace(NPCImageToUse))
            {
                String destination = Directory.GetCurrentDirectory() + "\\Content\\Sprites\\NPCs\\Overworlds\\" + tempNPC.spriteSheet;
                try
                {
                    File.Copy(NPCImageToUse, destination, true);
                }
                catch (IOException e) { }
            }

            tempNPC.spritePosition = new Microsoft.Xna.Framework.Rectangle(5, 5, 32, 32);
            tempNPC.spritesheetSize = new Microsoft.Xna.Framework.Rectangle(0, 0, 116, 153);
            tempNPC.zoneLocation = game.world.currentArea.zoneName;

            tempNPC.interactScript = NPCScriptBox.Text;
            tempNPC.Update();

            return tempNPC;
        }

        private void SaveSceneryList()
        {
            try
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\MapmakerData\\"))
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\MapmakerData\\");

                FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "\\MapmakerData\\scenery.dat", FileMode.Create);
                BinaryWriter writer = new BinaryWriter(stream);
                SaveLoad.SaveSceneryList(sceneryList, writer);
                writer.Dispose();
                stream.Close();
                stream.Dispose();
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot save scenery list at this time");
            }
        }

        private void LoadSceneryList()
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\MapmakerData\\") && File.Exists(Directory.GetCurrentDirectory() + "\\MapmakerData\\scenery.dat"))
            {
                FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "\\MapmakerData\\scenery.dat", FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                sceneryList = SaveLoad.LoadSceneryList(reader);
                foreach (KeyValuePair<string, Scenery> kvp in sceneryList)
                {
                    sceneryBox.Items.Add(kvp.Key);
                }
            }
        }

        private void newSceneryButton_Click(object sender, EventArgs e)
        {
            if (sceneryForm == null || sceneryForm.IsDisposed)
            {
                sceneryForm = new NewSceneryForm(this);
            }

            DialogResult res = sceneryForm.ShowDialog();
            SaveSceneryList();
        }

        private void saveToProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveZone(Directory.GetCurrentDirectory() + "\\Content\\Zones");
        }

        private void deleteSceneryButton_Click(object sender, EventArgs e)
        {
            if (sceneryBox.SelectedItem != null)
            {
                sceneryList.Remove((string)sceneryBox.SelectedItem);
                sceneryBox.Items.Remove(sceneryBox.SelectedItem);
                SaveSceneryList();
            }
        }

        private void cleanOrphanedSceneryButton_Click(object sender, EventArgs e)
        {
            //create an array of booleans
            bool[] check = new bool[game.world.currentArea.scenery.Count];
            //set all to false
            for (int i = 0; i < check.Length; i++)
            {
                check[i] = false;
            }
            //go through each tile and check off each scenery object as we find them on the map
            for (int x = 0; x < game.world.currentArea.mapWidth; x++)
            {
                for (int y = 0; y < game.world.currentArea.mapHeight; y++)
                {
                    if (game.world.currentArea.tile[x, y].sceneryObject != null)
                    {
                        int index = game.world.currentArea.scenery.IndexOf(game.world.currentArea.tile[x, y].sceneryObject);
                        check[index] = true;
                    }
                }
            }

            List<Scenery> toRemove = new List<Scenery>();
            for (int i = 0; i < game.world.currentArea.scenery.Count; i++)
            {
                if (check[i] == false)
                {
                    toRemove.Add(game.world.currentArea.scenery[i]);
                }
            }

            //remove any scenery that isn't checked
            for (int i = 0; i < toRemove.Count; i++)
            {
                game.world.currentArea.scenery.Remove(toRemove[i]);
            }

            toRemove.Clear();
        }

        private void lview_TrainerActivePokemon_DoubleClick(object sender, EventArgs e)
        {

            if (lview_TrainerActivePokemon.SelectedItems.Count != 0)
            {
                ListViewItem lvi = lview_TrainerActivePokemon.SelectedItems[0];

                if (lvi.Tag == null)
                    lvi.Tag = new ActivePokemon(BaseStatsList.basestats);

                ActivePokemonEditor ape = new ActivePokemonEditor() { activePokemon = (ActivePokemon)lvi.Tag };

                if (ape.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lview_TrainerActivePokemon.SelectedItems[0].Tag = ape.activePokemon;
                    lview_TrainerActivePokemon.SelectedItems[0].Text = ape.activePokemon.ToString();
                }
            }

        }

        public void ResetNPCTab()
        {
            tbox_NPCName.Text = "NPC Name";
            cbox_IsTrainer.Checked = false;

            //default facing is down
            rbtn_NPCFacingDirection_DOWN.Checked = true;

            cbox_IsMale.Checked = false;
            gbox_TrainerEditor.Enabled = false;
            tbox_TrainerPayout.Text = "Trainer Payout";

            //default movement type is NONE
            movementNoneRadio.Checked = true;
            NPCImageToUse = "";

            //reset sprites
            NPCImageBox.Image = null;


            for (int i = 0; i < lview_TrainerActivePokemon.Items.Count; i++)
            {
                lview_TrainerActivePokemon.Items[i].Tag = null;
                lview_TrainerActivePokemon.Items[i].Text = i.ToString();
            }
        }

        private void btn_ImportNPCScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File (*.txt)|*.txt";
            ofd.Multiselect = false;
            ofd.ShowDialog();

            try
            {
                NPCScriptBox.Text = File.ReadAllText(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ofd.Dispose();
        }

        private void btn_LoadNPCSprites_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //use default zone path if one exists
            if (Directory.Exists(DefaultFileLocations.npcSpriteLocation))
            {
                ofd.InitialDirectory = DefaultFileLocations.npcSpriteLocation;
            }
            ofd.Multiselect = false;
            ofd.ShowDialog();

            Image tempImage = null;
            ImageList tempImageList = new ImageList();
            tempImageList.ImageSize = new System.Drawing.Size(116, 153);

            try
            {
                NPCImageToUse = ofd.FileName;
                tempImage = Bitmap.FromFile(ofd.FileName);
                NPCImageBox.Image = tempImage;

                //save selected directory as default
                String[] temp = ofd.FileName.Split('\\');
                DefaultFileLocations.npcSpriteLocation = ofd.FileName.Substring(0, ofd.FileName.Length - temp[temp.Length - 1].Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                //copy the sprite over to the content folder
                if (!String.IsNullOrWhiteSpace(NPCImageToUse))
                {
                    String destination = Directory.GetCurrentDirectory() + "\\Content\\Sprites\\NPCs\\Overworlds\\" + Path.GetFileName(NPCImageToUse);
                    File.Copy(NPCImageToUse, destination, true);
                }
                activeNPCEdit.spriteSheet = Path.GetFileName(NPCImageToUse);
                GameDraw.UpdateNPCSpritesheets(game.world);
            }
        }

        private void tilesPanel_Paint(object sender, PaintEventArgs e)
        {
            /*
             * Draw the box around the selected tile
             * g.DrawLine(pen, selectedtile.x, selectedtile.y, selectedtile.x + 32, selectedtile.y );
             * g.DrawLine(pen, selectedtile.x + 32, selectedtile.y, selectedtile.x + 32, selectedtile.y + 32 );
             * g.DrawLine(pen, selectedtile.x + 32, selectedtile.y + 32, selectedtile.x, selectedtile.y + 32 );
             * g.DrawLine(pen, selectedtile.x, selectedtile.y + 32, selectedtile.x , selectedtile.y );
             */
            
            //g.Dispose();
        }

        private void modelDeleteRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modelDeleteRadio.Checked == true)
            {
                sceneryBox.SelectedItem = null;
                modelNameBox.Enabled = false;
                modelNameBox.Text = "";
                modelWidthBox.Enabled = false;
                modelWidthBox.Text = "";
                modelHeightBox.Enabled = false;
                modelHeightBox.Text = "";
                modelScriptBox.Enabled = false;
                modelScriptBox.Text = "";
                sceneryBox.Enabled = false;
            }
        }

        private void modelEditRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modelEditRadio.Checked == true)
            {
                sceneryBox.SelectedItem = null;
                modelNameBox.Enabled = true;
                modelNameBox.Text = "";
                modelWidthBox.Enabled = true;
                modelWidthBox.Text = "";
                modelHeightBox.Enabled = true;
                modelHeightBox.Text = "";
                modelScriptBox.Enabled = true;
                modelScriptBox.Text = "";
                sceneryBox.Enabled = true;
            }
        }

        private void modelNewRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modelNewRadio.Checked == true)
            {
                sceneryBox.SelectedItem = null;
                modelNameBox.Enabled = false;
                modelNameBox.Text = "";
                modelWidthBox.Enabled = false;
                modelWidthBox.Text = "";
                modelHeightBox.Enabled = false;
                modelHeightBox.Text = "";
                modelScriptBox.Enabled = false;
                modelScriptBox.Text = "";
                sceneryBox.Enabled = true;
            }
        }

        private void sceneryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sceneryBox.SelectedItem != null)
            {
                if (modelNewRadio.Checked)
                {
                    String name = (String)sceneryBox.SelectedItem;
                    modelNameBox.Text = sceneryList[name].name;
                    modelWidthBox.Text = Convert.ToString(sceneryList[name].size.X);
                    modelHeightBox.Text = Convert.ToString(sceneryList[name].size.Y);
                    modelScriptBox.Text = sceneryList[name].interactScript;
                }
                else if (modelEditRadio.Checked)
                {
                    editingBaseScenery = true; //we are editing the template, not scenery on the map
                    String name = (String)sceneryBox.SelectedItem;
                    modelNameBox.Text = sceneryList[name].name;
                    modelWidthBox.Text = Convert.ToString(sceneryList[name].size.X);
                    modelHeightBox.Text = Convert.ToString(sceneryList[name].size.Y);
                    modelScriptBox.Text = sceneryList[name].interactScript;
                    activeSceneryEdit = sceneryList[name];
                }
            }
        }

        private void modelNameBox_TextChanged(object sender, EventArgs e)
        {
            if (modelEditRadio.Checked && sceneryBox.SelectedItem != null && editingBaseScenery == true)
            {
                Scenery temp = sceneryList[(String)sceneryBox.SelectedItem];


                sceneryList.Remove((String)sceneryBox.SelectedItem);
                temp.name = modelNameBox.Text;
                sceneryList.Add(modelNameBox.Text, temp);
                sceneryBox.Items.Add(modelNameBox.Text);
                sceneryBox.Items.Remove(sceneryBox.SelectedItem);
                sceneryBox.SelectedItem = modelNameBox.Text;
                SaveSceneryList();
            }
            else if (modelEditRadio.Checked && editingBaseScenery == false && activeSceneryEdit != null)
            {
                Scenery temp = activeSceneryEdit;

                temp.name = modelNameBox.Text;
            }
        }

        private void modelWidthBox_TextChanged(object sender, EventArgs e)
        {
            if (modelEditRadio.Checked && sceneryBox.SelectedItem != null)
            {
                Scenery temp = sceneryList[(String)sceneryBox.SelectedItem];

                try
                {
                    int newSize = Convert.ToInt32(modelWidthBox.Text);
                    if (newSize >= 0)
                        temp.size.X = newSize;
                }
                catch (Exception) { }
                SaveSceneryList();
            }
            else if (modelEditRadio.Checked && editingBaseScenery == false && activeSceneryEdit != null)
            {
                //expand or shrink to cover any non-occupied tile within the new size
                //will only apply to empty tiles or tiles that have the object to be edited
                Scenery temp = activeSceneryEdit;
                try
                {
                    //get new size from box (will throw exception if it fails)
                    int newSize = Convert.ToInt32(modelWidthBox.Text);
                    
                    //remove all references to the object in the old size range
                    for (int x = temp.position.X; x < Math.Min(temp.position.X + temp.size.X, game.world.currentArea.mapWidth); x++)
                    {
                        for (int y = temp.position.Y; y < Math.Min(temp.position.Y + temp.size.Y, game.world.currentArea.mapHeight); y++)
                        {
                            if (game.world.currentArea.tile[x, y].sceneryObject == temp)
                            {
                                game.world.currentArea.tile[x, y].sceneryObject = null;
                            }
                        }
                    }
                    //update size
                    if (newSize >= 0)
                        temp.size.X = newSize;
                    //add references to the object in the new size range
                    for (int x = temp.position.X; x < Math.Min(temp.position.X + temp.size.X, game.world.currentArea.mapWidth); x++)
                    {
                        for (int y = temp.position.Y; y < Math.Min(temp.position.Y + temp.size.Y, game.world.currentArea.mapHeight); y++)
                        {
                            if (game.world.currentArea.tile[x, y].sceneryObject == null)
                            {
                                game.world.currentArea.tile[x, y].sceneryObject = temp;
                            }
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void modelHeightBox_TextChanged(object sender, EventArgs e)
        {
            if (modelEditRadio.Checked && sceneryBox.SelectedItem != null)
            {
                Scenery temp = sceneryList[(String)sceneryBox.SelectedItem];

                try
                {
                    int newSize = Convert.ToInt32(modelHeightBox.Text);
                    if (newSize >= 0)
                        temp.size.Y = newSize;
                }
                catch (Exception) { }
                SaveSceneryList();
            }
            else if (modelEditRadio.Checked && editingBaseScenery == false && activeSceneryEdit != null)
            {
                //expand or shrink to cover any non-occupied tile within the new size
                //will only apply to empty tiles or tiles that have the object to be edited
                Scenery temp = activeSceneryEdit;
                try
                {
                    //get new size from box (will throw exception if it fails)
                    int newSize = Convert.ToInt32(modelHeightBox.Text);

                    //remove all references to the object in the old size range
                    for (int x = temp.position.X; x < Math.Min(temp.position.X + temp.size.X, game.world.currentArea.mapWidth); x++)
                    {
                        for (int y = temp.position.Y; y < Math.Min(temp.position.Y + temp.size.Y, game.world.currentArea.mapHeight); y++)
                        {
                            if (game.world.currentArea.tile[x, y].sceneryObject == temp)
                            {
                                game.world.currentArea.tile[x, y].sceneryObject = null;
                            }
                        }
                    }
                    //update size
                    if (newSize >= 0)
                        temp.size.Y = newSize;
                    //add references to the object in the new size range
                    for (int x = temp.position.X; x < Math.Min(temp.position.X + temp.size.X, game.world.currentArea.mapWidth); x++)
                    {
                        for (int y = temp.position.Y; y < Math.Min(temp.position.Y + temp.size.Y, game.world.currentArea.mapHeight); y++)
                        {
                            if (game.world.currentArea.tile[x, y].sceneryObject == null)
                            {
                                game.world.currentArea.tile[x, y].sceneryObject = temp;
                            }
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void modelScriptBox_TextChanged(object sender, EventArgs e)
        {
            if (modelEditRadio.Checked && sceneryBox.SelectedItem != null && editingBaseScenery == true)
            {
                Scenery temp = sceneryList[(String)sceneryBox.SelectedItem];

                temp.interactScript = modelScriptBox.Text;
                SaveSceneryList();
            }
            else if (modelEditRadio.Checked && editingBaseScenery == false && activeSceneryEdit != null)
            {
                Scenery temp = activeSceneryEdit;

                temp.interactScript = modelScriptBox.Text;
            }
        }

        private void tbox_NPCName_TextChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                activeNPCEdit.name = tbox_NPCName.Text;
            }
        }

        private void cbox_IsMale_CheckedChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                activeNPCEdit.isMale = cbox_IsMale.Checked;
            }
        }

        private void rbtn_NPCFacingDirection_UP_CheckedChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                if (rbtn_NPCFacingDirection_UP.Checked)
                {
                    activeNPCEdit.facing = FacingDirection.North;
                    activeNPCEdit.Update();
                }
            }
        }

        private void rbtn_NPCFacingDirection_RIGHT_CheckedChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                if (rbtn_NPCFacingDirection_RIGHT.Checked)
                {
                    activeNPCEdit.facing = FacingDirection.East;
                    activeNPCEdit.Update();
                }
            }
        }

        private void rbtn_NPCFacingDirection_LEFT_CheckedChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                if (rbtn_NPCFacingDirection_LEFT.Checked)
                {
                    activeNPCEdit.facing = FacingDirection.West;
                    activeNPCEdit.Update();
                }
            }
        }

        private void rbtn_NPCFacingDirection_DOWN_CheckedChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                if (rbtn_NPCFacingDirection_DOWN.Checked)
                {
                    activeNPCEdit.facing = FacingDirection.South;
                    activeNPCEdit.Update();
                }
            }
        }

        private void NPCScriptBox_TextChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                activeNPCEdit.interactScript = NPCScriptBox.Text;
            }
        }

        private void tbox_TrainerPayout_TextChanged(object sender, EventArgs e)
        {
            //only fires when we are editing an existing NPC
            if (NPCEditRadio.Checked && activeNPCEdit != null)
            {
                try
                {
                    ((Trainer)activeNPCEdit).money = Convert.ToInt32(tbox_TrainerPayout.Text);
                }
                catch (Exception)
                {
                }
            }
        }

        private void NPCNewRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (NPCNewRadio.Checked)
            {
                ResetNPCTab();
                NPCTool.toolType = NPCToolType.PlaceNPC;
            }
        }

        private void NPCEditRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (NPCEditRadio.Checked)
            {
                NPCTool.toolType = NPCToolType.EditNPC;
                gbox_Movement_NPC.Enabled = true;
            }
            else
            {
                gbox_Movement_NPC.Enabled = false;
            }
        }

        private void NPCMovementButton_Click_1(object sender, EventArgs e)
        {
            if (movementWanderRadio.Checked)
            {
                NPCTool.toolType = NPCToolType.SetWanderArea;
            }
            else if (movementNoneRadio.Checked)
            {
                activeNPCEdit.SetNoMovement();
            }
            else if (movementPathRadio.Checked)
            {
                NPCTool.StartPath();
            }
        }

        private void NPCDeleteRadio_CheckedChanged_1(object sender, EventArgs e)
        {
            if (NPCDeleteRadio.Checked)
                NPCTool.toolType = NPCToolType.DeleteNPC;
        }

        private void NPCScriptBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 1)
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTCForm tcForm = new NewTCForm(this);
            tcForm.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (commandList.SelectedItems.Count == 1)
            {
                NewTCForm tcForm = new NewTCForm(this, commandList.SelectedItems[0]);
                tcForm.Show();
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (commandList.SelectedItems.Count == 1)
            {
                commandList.Items.Remove(commandList.SelectedItems[0]);
            }
        }

        private void importScenery_Click(object sender, EventArgs e)
        {
            foreach (Scenery s in game.world.currentArea.scenery)
            {
                Scenery tmp = null;
                if (!sceneryList.TryGetValue(s.name, out tmp))
                {
                    sceneryList.Add(s.name, s);
                    sceneryBox.Items.Add(s.name);
                }
            }

            SaveSceneryList();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            //check for tiles in tiles directory, load them if there are any
            importDefaultTiles();
            zoneHistory = new Zone[historySize];
            g = CreateGraphics();
            LoadSceneryList();
        }
        
    }

    public class SelectedTile
    {
        public string TileType { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public SelectedTile(string type, int x, int y)
        {
            TileType = type;
            PositionX = x;
            PositionY = y;
        }
    }
}
namespace LiveMapMaker
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("0");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("1");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("2");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("3");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("4");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("5");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "ShowMessage(\"Hello\")"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToProductionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCurrentZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAccessibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTab = new System.Windows.Forms.TabControl();
            this.TileSpritesTab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.pbar_ExtractionProgress = new System.Windows.Forms.ProgressBar();
            this.waterBox = new System.Windows.Forms.CheckBox();
            this.rampBox = new System.Windows.Forms.CheckBox();
            this.randomEncounterBox = new System.Windows.Forms.CheckBox();
            this.jumpBox = new System.Windows.Forms.CheckBox();
            this.southBox = new System.Windows.Forms.CheckBox();
            this.eastBox = new System.Windows.Forms.CheckBox();
            this.westBox = new System.Windows.Forms.CheckBox();
            this.northBox = new System.Windows.Forms.CheckBox();
            this.setAllButton = new System.Windows.Forms.Button();
            this.lbox_Tiles = new System.Windows.Forms.ListBox();
            this.tilesPanel = new System.Windows.Forms.Panel();
            this.pbox_TilePreview = new System.Windows.Forms.PictureBox();
            this.btn_BrowseTileDirectory = new System.Windows.Forms.Button();
            this.TilePropertiesTab = new System.Windows.Forms.TabPage();
            this.controlPropertiesBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.controlNorthBox = new System.Windows.Forms.CheckBox();
            this.controlJumpBox = new System.Windows.Forms.CheckBox();
            this.controlREBox = new System.Windows.Forms.CheckBox();
            this.controlSouthBox = new System.Windows.Forms.CheckBox();
            this.controlBikeBox = new System.Windows.Forms.CheckBox();
            this.controlEastBox = new System.Windows.Forms.CheckBox();
            this.controlWaterBox = new System.Windows.Forms.CheckBox();
            this.controlWestBox = new System.Windows.Forms.CheckBox();
            this.tileScriptBox = new System.Windows.Forms.CheckBox();
            this.tileScriptTextBox = new System.Windows.Forms.TextBox();
            this.NPCTab = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NPCDeleteRadio = new System.Windows.Forms.RadioButton();
            this.NPCEditRadio = new System.Windows.Forms.RadioButton();
            this.NPCNewRadio = new System.Windows.Forms.RadioButton();
            this.gbox_Movement_NPC = new System.Windows.Forms.GroupBox();
            this.NPCMovementButton = new System.Windows.Forms.Button();
            this.movementPathRadio = new System.Windows.Forms.RadioButton();
            this.movementWanderRadio = new System.Windows.Forms.RadioButton();
            this.movementNoneRadio = new System.Windows.Forms.RadioButton();
            this.NPCScriptBox = new System.Windows.Forms.TextBox();
            this.cbox_IsTrainer = new System.Windows.Forms.CheckBox();
            this.NPCImageBox = new System.Windows.Forms.PictureBox();
            this.btn_LoadNPCSprites = new System.Windows.Forms.Button();
            this.cbox_IsMale = new System.Windows.Forms.CheckBox();
            this.gbox_NPCFacingDirection = new System.Windows.Forms.GroupBox();
            this.rbtn_NPCFacingDirection_RIGHT = new System.Windows.Forms.RadioButton();
            this.rbtn_NPCFacingDirection_LEFT = new System.Windows.Forms.RadioButton();
            this.rbtn_NPCFacingDirection_DOWN = new System.Windows.Forms.RadioButton();
            this.rbtn_NPCFacingDirection_UP = new System.Windows.Forms.RadioButton();
            this.gbox_TrainerEditor = new System.Windows.Forms.GroupBox();
            this.lview_TrainerActivePokemon = new System.Windows.Forms.ListView();
            this.tbox_TrainerPayout = new System.Windows.Forms.TextBox();
            this.btn_ImportNPCScript = new System.Windows.Forms.Button();
            this.tbox_NPCName = new System.Windows.Forms.TextBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SceneryTab = new System.Windows.Forms.TabPage();
            this.importScenery = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.modelScriptBox = new System.Windows.Forms.TextBox();
            this.modelNameBox = new System.Windows.Forms.TextBox();
            this.modelHeightBox = new System.Windows.Forms.TextBox();
            this.modelWidthBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.modelDeleteRadio = new System.Windows.Forms.RadioButton();
            this.modelNewRadio = new System.Windows.Forms.RadioButton();
            this.modelEditRadio = new System.Windows.Forms.RadioButton();
            this.unrestrictedPlacementBox = new System.Windows.Forms.CheckBox();
            this.cleanOrphanedSceneryButton = new System.Windows.Forms.Button();
            this.deleteSceneryButton = new System.Windows.Forms.Button();
            this.newSceneryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sceneryBox = new System.Windows.Forms.ListBox();
            this.CutSceneTab = new System.Windows.Forms.TabPage();
            this.commandList = new System.Windows.Forms.ListView();
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timedCommandContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.newDiscardCSButton = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.globalDebugLabel = new System.Windows.Forms.Label();
            this.sceneryBlockCheckbox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.toolTab.SuspendLayout();
            this.TileSpritesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_TilePreview)).BeginInit();
            this.TilePropertiesTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NPCTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbox_Movement_NPC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NPCImageBox)).BeginInit();
            this.gbox_NPCFacingDirection.SuspendLayout();
            this.gbox_TrainerEditor.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SceneryTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.CutSceneTab.SuspendLayout();
            this.timedCommandContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(297, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newZoneToolStripMenuItem,
            this.saveZoneToolStripMenuItem,
            this.saveToProductionToolStripMenuItem,
            this.loadZoneToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newZoneToolStripMenuItem
            // 
            this.newZoneToolStripMenuItem.Name = "newZoneToolStripMenuItem";
            this.newZoneToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.newZoneToolStripMenuItem.Text = "New Zone";
            this.newZoneToolStripMenuItem.Click += new System.EventHandler(this.newZoneToolStripMenuItem_Click);
            // 
            // saveZoneToolStripMenuItem
            // 
            this.saveZoneToolStripMenuItem.Name = "saveZoneToolStripMenuItem";
            this.saveZoneToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveZoneToolStripMenuItem.Text = "Save Zone";
            this.saveZoneToolStripMenuItem.Click += new System.EventHandler(this.saveZoneToolStripMenuItem_Click);
            // 
            // saveToProductionToolStripMenuItem
            // 
            this.saveToProductionToolStripMenuItem.Name = "saveToProductionToolStripMenuItem";
            this.saveToProductionToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.saveToProductionToolStripMenuItem.Text = "Save to Production";
            this.saveToProductionToolStripMenuItem.Click += new System.EventHandler(this.saveToProductionToolStripMenuItem_Click);
            // 
            // loadZoneToolStripMenuItem
            // 
            this.loadZoneToolStripMenuItem.Name = "loadZoneToolStripMenuItem";
            this.loadZoneToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadZoneToolStripMenuItem.Text = "Load Zone";
            this.loadZoneToolStripMenuItem.Click += new System.EventHandler(this.loadZoneToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCurrentZoneToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // editCurrentZoneToolStripMenuItem
            // 
            this.editCurrentZoneToolStripMenuItem.Name = "editCurrentZoneToolStripMenuItem";
            this.editCurrentZoneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editCurrentZoneToolStripMenuItem.Text = "Edit Zone Properties";
            this.editCurrentZoneToolStripMenuItem.Click += new System.EventHandler(this.editCurrentZoneToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAccessibilityToolStripMenuItem,
            this.showScriptsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showAccessibilityToolStripMenuItem
            // 
            this.showAccessibilityToolStripMenuItem.Checked = true;
            this.showAccessibilityToolStripMenuItem.CheckOnClick = true;
            this.showAccessibilityToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAccessibilityToolStripMenuItem.Name = "showAccessibilityToolStripMenuItem";
            this.showAccessibilityToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showAccessibilityToolStripMenuItem.Text = "Show Accessibility";
            // 
            // showScriptsToolStripMenuItem
            // 
            this.showScriptsToolStripMenuItem.Checked = true;
            this.showScriptsToolStripMenuItem.CheckOnClick = true;
            this.showScriptsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showScriptsToolStripMenuItem.Name = "showScriptsToolStripMenuItem";
            this.showScriptsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showScriptsToolStripMenuItem.Text = "Show Scripts";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screenShotToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // screenShotToolStripMenuItem
            // 
            this.screenShotToolStripMenuItem.Name = "screenShotToolStripMenuItem";
            this.screenShotToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.screenShotToolStripMenuItem.Text = "Screen Shot";
            this.screenShotToolStripMenuItem.Click += new System.EventHandler(this.screenShotToolStripMenuItem_Click);
            // 
            // toolTab
            // 
            this.toolTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolTab.Controls.Add(this.TileSpritesTab);
            this.toolTab.Controls.Add(this.TilePropertiesTab);
            this.toolTab.Controls.Add(this.NPCTab);
            this.toolTab.Controls.Add(this.SceneryTab);
            this.toolTab.Controls.Add(this.CutSceneTab);
            this.toolTab.Location = new System.Drawing.Point(0, 28);
            this.toolTab.Name = "toolTab";
            this.toolTab.SelectedIndex = 0;
            this.toolTab.Size = new System.Drawing.Size(297, 593);
            this.toolTab.TabIndex = 2;
            // 
            // TileSpritesTab
            // 
            this.TileSpritesTab.Controls.Add(this.label6);
            this.TileSpritesTab.Controls.Add(this.pbar_ExtractionProgress);
            this.TileSpritesTab.Controls.Add(this.waterBox);
            this.TileSpritesTab.Controls.Add(this.rampBox);
            this.TileSpritesTab.Controls.Add(this.randomEncounterBox);
            this.TileSpritesTab.Controls.Add(this.jumpBox);
            this.TileSpritesTab.Controls.Add(this.southBox);
            this.TileSpritesTab.Controls.Add(this.eastBox);
            this.TileSpritesTab.Controls.Add(this.westBox);
            this.TileSpritesTab.Controls.Add(this.northBox);
            this.TileSpritesTab.Controls.Add(this.setAllButton);
            this.TileSpritesTab.Controls.Add(this.lbox_Tiles);
            this.TileSpritesTab.Controls.Add(this.tilesPanel);
            this.TileSpritesTab.Controls.Add(this.pbox_TilePreview);
            this.TileSpritesTab.Controls.Add(this.btn_BrowseTileDirectory);
            this.TileSpritesTab.Location = new System.Drawing.Point(4, 22);
            this.TileSpritesTab.Name = "TileSpritesTab";
            this.TileSpritesTab.Padding = new System.Windows.Forms.Padding(3);
            this.TileSpritesTab.Size = new System.Drawing.Size(289, 567);
            this.TileSpritesTab.TabIndex = 0;
            this.TileSpritesTab.Text = "Tile Sprites";
            this.TileSpritesTab.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 521);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "label6";
            // 
            // pbar_ExtractionProgress
            // 
            this.pbar_ExtractionProgress.Enabled = false;
            this.pbar_ExtractionProgress.Location = new System.Drawing.Point(172, 45);
            this.pbar_ExtractionProgress.Name = "pbar_ExtractionProgress";
            this.pbar_ExtractionProgress.Size = new System.Drawing.Size(105, 64);
            this.pbar_ExtractionProgress.TabIndex = 49;
            // 
            // waterBox
            // 
            this.waterBox.AutoSize = true;
            this.waterBox.Location = new System.Drawing.Point(159, 488);
            this.waterBox.Name = "waterBox";
            this.waterBox.Size = new System.Drawing.Size(55, 17);
            this.waterBox.TabIndex = 48;
            this.waterBox.Text = "Water";
            this.waterBox.UseVisualStyleBackColor = true;
            // 
            // rampBox
            // 
            this.rampBox.AutoSize = true;
            this.rampBox.Location = new System.Drawing.Point(159, 442);
            this.rampBox.Name = "rampBox";
            this.rampBox.Size = new System.Drawing.Size(78, 17);
            this.rampBox.TabIndex = 47;
            this.rampBox.Text = "Bike Ramp";
            this.rampBox.UseVisualStyleBackColor = true;
            // 
            // randomEncounterBox
            // 
            this.randomEncounterBox.AutoSize = true;
            this.randomEncounterBox.Location = new System.Drawing.Point(159, 465);
            this.randomEncounterBox.Name = "randomEncounterBox";
            this.randomEncounterBox.Size = new System.Drawing.Size(118, 17);
            this.randomEncounterBox.TabIndex = 46;
            this.randomEncounterBox.Text = "Random Encounter";
            this.randomEncounterBox.UseVisualStyleBackColor = true;
            // 
            // jumpBox
            // 
            this.jumpBox.AutoSize = true;
            this.jumpBox.Location = new System.Drawing.Point(159, 419);
            this.jumpBox.Name = "jumpBox";
            this.jumpBox.Size = new System.Drawing.Size(71, 17);
            this.jumpBox.TabIndex = 45;
            this.jumpBox.Text = "Jumpable";
            this.jumpBox.UseVisualStyleBackColor = true;
            // 
            // southBox
            // 
            this.southBox.AutoSize = true;
            this.southBox.Checked = true;
            this.southBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.southBox.Location = new System.Drawing.Point(35, 465);
            this.southBox.Name = "southBox";
            this.southBox.Size = new System.Drawing.Size(54, 17);
            this.southBox.TabIndex = 44;
            this.southBox.Text = "South";
            this.southBox.UseVisualStyleBackColor = true;
            // 
            // eastBox
            // 
            this.eastBox.AutoSize = true;
            this.eastBox.Checked = true;
            this.eastBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eastBox.Location = new System.Drawing.Point(63, 442);
            this.eastBox.Name = "eastBox";
            this.eastBox.Size = new System.Drawing.Size(47, 17);
            this.eastBox.TabIndex = 43;
            this.eastBox.Text = "East";
            this.eastBox.UseVisualStyleBackColor = true;
            // 
            // westBox
            // 
            this.westBox.AutoSize = true;
            this.westBox.Checked = true;
            this.westBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.westBox.Location = new System.Drawing.Point(6, 442);
            this.westBox.Name = "westBox";
            this.westBox.Size = new System.Drawing.Size(51, 17);
            this.westBox.TabIndex = 42;
            this.westBox.Text = "West";
            this.westBox.UseVisualStyleBackColor = true;
            // 
            // northBox
            // 
            this.northBox.AutoSize = true;
            this.northBox.Checked = true;
            this.northBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.northBox.Location = new System.Drawing.Point(35, 419);
            this.northBox.Name = "northBox";
            this.northBox.Size = new System.Drawing.Size(52, 17);
            this.northBox.TabIndex = 41;
            this.northBox.Text = "North";
            this.northBox.UseVisualStyleBackColor = true;
            // 
            // setAllButton
            // 
            this.setAllButton.Location = new System.Drawing.Point(3, 513);
            this.setAllButton.Name = "setAllButton";
            this.setAllButton.Size = new System.Drawing.Size(276, 51);
            this.setAllButton.TabIndex = 40;
            this.setAllButton.Text = "Set All";
            this.setAllButton.UseVisualStyleBackColor = true;
            this.setAllButton.Click += new System.EventHandler(this.setAllButton_Click);
            // 
            // lbox_Tiles
            // 
            this.lbox_Tiles.Enabled = false;
            this.lbox_Tiles.FormattingEnabled = true;
            this.lbox_Tiles.Location = new System.Drawing.Point(101, 6);
            this.lbox_Tiles.Name = "lbox_Tiles";
            this.lbox_Tiles.Size = new System.Drawing.Size(178, 30);
            this.lbox_Tiles.TabIndex = 39;
            this.lbox_Tiles.SelectedIndexChanged += new System.EventHandler(this.lbox_Tiles_SelectedIndexChanged);
            // 
            // tilesPanel
            // 
            this.tilesPanel.AutoScroll = true;
            this.tilesPanel.Location = new System.Drawing.Point(3, 115);
            this.tilesPanel.Name = "tilesPanel";
            this.tilesPanel.Size = new System.Drawing.Size(276, 298);
            this.tilesPanel.TabIndex = 38;
            this.tilesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tilesPanel_Paint);
            // 
            // pbox_TilePreview
            // 
            this.pbox_TilePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbox_TilePreview.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pbox_TilePreview.Location = new System.Drawing.Point(101, 45);
            this.pbox_TilePreview.Name = "pbox_TilePreview";
            this.pbox_TilePreview.Size = new System.Drawing.Size(64, 64);
            this.pbox_TilePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbox_TilePreview.TabIndex = 37;
            this.pbox_TilePreview.TabStop = false;
            this.pbox_TilePreview.WaitOnLoad = true;
            // 
            // btn_BrowseTileDirectory
            // 
            this.btn_BrowseTileDirectory.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_BrowseTileDirectory.Location = new System.Drawing.Point(6, 6);
            this.btn_BrowseTileDirectory.Name = "btn_BrowseTileDirectory";
            this.btn_BrowseTileDirectory.Size = new System.Drawing.Size(89, 103);
            this.btn_BrowseTileDirectory.TabIndex = 36;
            this.btn_BrowseTileDirectory.Text = "Browse...";
            this.btn_BrowseTileDirectory.UseVisualStyleBackColor = true;
            this.btn_BrowseTileDirectory.Click += new System.EventHandler(this.btn_BrowseTileDirectory_Click);
            // 
            // TilePropertiesTab
            // 
            this.TilePropertiesTab.Controls.Add(this.controlPropertiesBox);
            this.TilePropertiesTab.Controls.Add(this.groupBox1);
            this.TilePropertiesTab.Controls.Add(this.tileScriptBox);
            this.TilePropertiesTab.Controls.Add(this.tileScriptTextBox);
            this.TilePropertiesTab.Location = new System.Drawing.Point(4, 22);
            this.TilePropertiesTab.Name = "TilePropertiesTab";
            this.TilePropertiesTab.Padding = new System.Windows.Forms.Padding(3);
            this.TilePropertiesTab.Size = new System.Drawing.Size(289, 567);
            this.TilePropertiesTab.TabIndex = 1;
            this.TilePropertiesTab.Text = "Tile Controls";
            this.TilePropertiesTab.UseVisualStyleBackColor = true;
            // 
            // controlPropertiesBox
            // 
            this.controlPropertiesBox.AutoSize = true;
            this.controlPropertiesBox.Location = new System.Drawing.Point(11, 409);
            this.controlPropertiesBox.Name = "controlPropertiesBox";
            this.controlPropertiesBox.Size = new System.Drawing.Size(113, 17);
            this.controlPropertiesBox.TabIndex = 61;
            this.controlPropertiesBox.Text = "Change Properties";
            this.controlPropertiesBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.controlNorthBox);
            this.groupBox1.Controls.Add(this.controlJumpBox);
            this.groupBox1.Controls.Add(this.controlREBox);
            this.groupBox1.Controls.Add(this.controlSouthBox);
            this.groupBox1.Controls.Add(this.controlBikeBox);
            this.groupBox1.Controls.Add(this.controlEastBox);
            this.groupBox1.Controls.Add(this.controlWaterBox);
            this.groupBox1.Controls.Add(this.controlWestBox);
            this.groupBox1.Location = new System.Drawing.Point(4, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 138);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            // 
            // controlNorthBox
            // 
            this.controlNorthBox.AutoSize = true;
            this.controlNorthBox.Checked = true;
            this.controlNorthBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlNorthBox.Location = new System.Drawing.Point(31, 23);
            this.controlNorthBox.Name = "controlNorthBox";
            this.controlNorthBox.Size = new System.Drawing.Size(52, 17);
            this.controlNorthBox.TabIndex = 53;
            this.controlNorthBox.Text = "North";
            this.controlNorthBox.UseVisualStyleBackColor = true;
            // 
            // controlJumpBox
            // 
            this.controlJumpBox.AutoSize = true;
            this.controlJumpBox.Location = new System.Drawing.Point(155, 23);
            this.controlJumpBox.Name = "controlJumpBox";
            this.controlJumpBox.Size = new System.Drawing.Size(71, 17);
            this.controlJumpBox.TabIndex = 49;
            this.controlJumpBox.Text = "Jumpable";
            this.controlJumpBox.UseVisualStyleBackColor = true;
            // 
            // controlREBox
            // 
            this.controlREBox.AutoSize = true;
            this.controlREBox.Location = new System.Drawing.Point(155, 69);
            this.controlREBox.Name = "controlREBox";
            this.controlREBox.Size = new System.Drawing.Size(118, 17);
            this.controlREBox.TabIndex = 50;
            this.controlREBox.Text = "Random Encounter";
            this.controlREBox.UseVisualStyleBackColor = true;
            // 
            // controlSouthBox
            // 
            this.controlSouthBox.AutoSize = true;
            this.controlSouthBox.Checked = true;
            this.controlSouthBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlSouthBox.Location = new System.Drawing.Point(31, 69);
            this.controlSouthBox.Name = "controlSouthBox";
            this.controlSouthBox.Size = new System.Drawing.Size(54, 17);
            this.controlSouthBox.TabIndex = 56;
            this.controlSouthBox.Text = "South";
            this.controlSouthBox.UseVisualStyleBackColor = true;
            // 
            // controlBikeBox
            // 
            this.controlBikeBox.AutoSize = true;
            this.controlBikeBox.Location = new System.Drawing.Point(155, 46);
            this.controlBikeBox.Name = "controlBikeBox";
            this.controlBikeBox.Size = new System.Drawing.Size(78, 17);
            this.controlBikeBox.TabIndex = 51;
            this.controlBikeBox.Text = "Bike Ramp";
            this.controlBikeBox.UseVisualStyleBackColor = true;
            // 
            // controlEastBox
            // 
            this.controlEastBox.AutoSize = true;
            this.controlEastBox.Checked = true;
            this.controlEastBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlEastBox.Location = new System.Drawing.Point(59, 46);
            this.controlEastBox.Name = "controlEastBox";
            this.controlEastBox.Size = new System.Drawing.Size(47, 17);
            this.controlEastBox.TabIndex = 55;
            this.controlEastBox.Text = "East";
            this.controlEastBox.UseVisualStyleBackColor = true;
            // 
            // controlWaterBox
            // 
            this.controlWaterBox.AutoSize = true;
            this.controlWaterBox.Location = new System.Drawing.Point(155, 92);
            this.controlWaterBox.Name = "controlWaterBox";
            this.controlWaterBox.Size = new System.Drawing.Size(55, 17);
            this.controlWaterBox.TabIndex = 52;
            this.controlWaterBox.Text = "Water";
            this.controlWaterBox.UseVisualStyleBackColor = true;
            // 
            // controlWestBox
            // 
            this.controlWestBox.AutoSize = true;
            this.controlWestBox.Checked = true;
            this.controlWestBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlWestBox.Location = new System.Drawing.Point(2, 46);
            this.controlWestBox.Name = "controlWestBox";
            this.controlWestBox.Size = new System.Drawing.Size(51, 17);
            this.controlWestBox.TabIndex = 54;
            this.controlWestBox.Text = "West";
            this.controlWestBox.UseVisualStyleBackColor = true;
            // 
            // tileScriptBox
            // 
            this.tileScriptBox.AutoSize = true;
            this.tileScriptBox.Location = new System.Drawing.Point(6, 155);
            this.tileScriptBox.Name = "tileScriptBox";
            this.tileScriptBox.Size = new System.Drawing.Size(75, 17);
            this.tileScriptBox.TabIndex = 59;
            this.tileScriptBox.Text = "Add Script";
            this.tileScriptBox.UseVisualStyleBackColor = true;
            // 
            // tileScriptTextBox
            // 
            this.tileScriptTextBox.AcceptsReturn = true;
            this.tileScriptTextBox.AcceptsTab = true;
            this.tileScriptTextBox.Location = new System.Drawing.Point(6, 178);
            this.tileScriptTextBox.Multiline = true;
            this.tileScriptTextBox.Name = "tileScriptTextBox";
            this.tileScriptTextBox.Size = new System.Drawing.Size(271, 212);
            this.tileScriptTextBox.TabIndex = 57;
            // 
            // NPCTab
            // 
            this.NPCTab.Controls.Add(this.groupBox4);
            this.NPCTab.Controls.Add(this.gbox_Movement_NPC);
            this.NPCTab.Controls.Add(this.NPCScriptBox);
            this.NPCTab.Controls.Add(this.cbox_IsTrainer);
            this.NPCTab.Controls.Add(this.NPCImageBox);
            this.NPCTab.Controls.Add(this.btn_LoadNPCSprites);
            this.NPCTab.Controls.Add(this.cbox_IsMale);
            this.NPCTab.Controls.Add(this.gbox_NPCFacingDirection);
            this.NPCTab.Controls.Add(this.gbox_TrainerEditor);
            this.NPCTab.Controls.Add(this.btn_ImportNPCScript);
            this.NPCTab.Controls.Add(this.tbox_NPCName);
            this.NPCTab.Controls.Add(this.menuStrip2);
            this.NPCTab.Location = new System.Drawing.Point(4, 22);
            this.NPCTab.Name = "NPCTab";
            this.NPCTab.Padding = new System.Windows.Forms.Padding(3);
            this.NPCTab.Size = new System.Drawing.Size(289, 567);
            this.NPCTab.TabIndex = 2;
            this.NPCTab.Text = "NPCs";
            this.NPCTab.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.NPCDeleteRadio);
            this.groupBox4.Controls.Add(this.NPCEditRadio);
            this.groupBox4.Controls.Add(this.NPCNewRadio);
            this.groupBox4.Location = new System.Drawing.Point(188, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(89, 83);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            // 
            // NPCDeleteRadio
            // 
            this.NPCDeleteRadio.AutoSize = true;
            this.NPCDeleteRadio.Location = new System.Drawing.Point(17, 59);
            this.NPCDeleteRadio.Name = "NPCDeleteRadio";
            this.NPCDeleteRadio.Size = new System.Drawing.Size(56, 17);
            this.NPCDeleteRadio.TabIndex = 2;
            this.NPCDeleteRadio.TabStop = true;
            this.NPCDeleteRadio.Text = "Delete";
            this.NPCDeleteRadio.UseVisualStyleBackColor = true;
            this.NPCDeleteRadio.CheckedChanged += new System.EventHandler(this.NPCDeleteRadio_CheckedChanged_1);
            // 
            // NPCEditRadio
            // 
            this.NPCEditRadio.AutoSize = true;
            this.NPCEditRadio.Location = new System.Drawing.Point(17, 35);
            this.NPCEditRadio.Name = "NPCEditRadio";
            this.NPCEditRadio.Size = new System.Drawing.Size(43, 17);
            this.NPCEditRadio.TabIndex = 1;
            this.NPCEditRadio.TabStop = true;
            this.NPCEditRadio.Text = "Edit";
            this.NPCEditRadio.UseVisualStyleBackColor = true;
            this.NPCEditRadio.CheckedChanged += new System.EventHandler(this.NPCEditRadio_CheckedChanged);
            // 
            // NPCNewRadio
            // 
            this.NPCNewRadio.AutoSize = true;
            this.NPCNewRadio.Checked = true;
            this.NPCNewRadio.Location = new System.Drawing.Point(17, 11);
            this.NPCNewRadio.Name = "NPCNewRadio";
            this.NPCNewRadio.Size = new System.Drawing.Size(47, 17);
            this.NPCNewRadio.TabIndex = 0;
            this.NPCNewRadio.TabStop = true;
            this.NPCNewRadio.Text = "New";
            this.NPCNewRadio.UseVisualStyleBackColor = true;
            this.NPCNewRadio.CheckedChanged += new System.EventHandler(this.NPCNewRadio_CheckedChanged);
            // 
            // gbox_Movement_NPC
            // 
            this.gbox_Movement_NPC.Controls.Add(this.NPCMovementButton);
            this.gbox_Movement_NPC.Controls.Add(this.movementPathRadio);
            this.gbox_Movement_NPC.Controls.Add(this.movementWanderRadio);
            this.gbox_Movement_NPC.Controls.Add(this.movementNoneRadio);
            this.gbox_Movement_NPC.Enabled = false;
            this.gbox_Movement_NPC.Location = new System.Drawing.Point(6, 171);
            this.gbox_Movement_NPC.Name = "gbox_Movement_NPC";
            this.gbox_Movement_NPC.Size = new System.Drawing.Size(107, 140);
            this.gbox_Movement_NPC.TabIndex = 17;
            this.gbox_Movement_NPC.TabStop = false;
            this.gbox_Movement_NPC.Text = "Movement Type";
            // 
            // NPCMovementButton
            // 
            this.NPCMovementButton.Location = new System.Drawing.Point(7, 88);
            this.NPCMovementButton.Name = "NPCMovementButton";
            this.NPCMovementButton.Size = new System.Drawing.Size(86, 39);
            this.NPCMovementButton.TabIndex = 3;
            this.NPCMovementButton.Text = "Set";
            this.NPCMovementButton.UseVisualStyleBackColor = true;
            this.NPCMovementButton.Click += new System.EventHandler(this.NPCMovementButton_Click_1);
            // 
            // movementPathRadio
            // 
            this.movementPathRadio.AutoSize = true;
            this.movementPathRadio.Location = new System.Drawing.Point(6, 65);
            this.movementPathRadio.Name = "movementPathRadio";
            this.movementPathRadio.Size = new System.Drawing.Size(47, 17);
            this.movementPathRadio.TabIndex = 2;
            this.movementPathRadio.Text = "Path";
            this.movementPathRadio.UseVisualStyleBackColor = true;
            // 
            // movementWanderRadio
            // 
            this.movementWanderRadio.AutoSize = true;
            this.movementWanderRadio.Location = new System.Drawing.Point(6, 43);
            this.movementWanderRadio.Name = "movementWanderRadio";
            this.movementWanderRadio.Size = new System.Drawing.Size(63, 17);
            this.movementWanderRadio.TabIndex = 1;
            this.movementWanderRadio.Text = "Wander";
            this.movementWanderRadio.UseVisualStyleBackColor = true;
            // 
            // movementNoneRadio
            // 
            this.movementNoneRadio.AutoSize = true;
            this.movementNoneRadio.Checked = true;
            this.movementNoneRadio.Location = new System.Drawing.Point(6, 19);
            this.movementNoneRadio.Name = "movementNoneRadio";
            this.movementNoneRadio.Size = new System.Drawing.Size(51, 17);
            this.movementNoneRadio.TabIndex = 0;
            this.movementNoneRadio.TabStop = true;
            this.movementNoneRadio.Text = "None";
            this.movementNoneRadio.UseVisualStyleBackColor = true;
            // 
            // NPCScriptBox
            // 
            this.NPCScriptBox.Location = new System.Drawing.Point(6, 333);
            this.NPCScriptBox.Multiline = true;
            this.NPCScriptBox.Name = "NPCScriptBox";
            this.NPCScriptBox.Size = new System.Drawing.Size(268, 114);
            this.NPCScriptBox.TabIndex = 16;
            this.NPCScriptBox.TextChanged += new System.EventHandler(this.NPCScriptBox_TextChanged);
            this.NPCScriptBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NPCScriptBox_KeyPress);
            // 
            // cbox_IsTrainer
            // 
            this.cbox_IsTrainer.AutoSize = true;
            this.cbox_IsTrainer.Location = new System.Drawing.Point(78, 54);
            this.cbox_IsTrainer.Name = "cbox_IsTrainer";
            this.cbox_IsTrainer.Size = new System.Drawing.Size(59, 17);
            this.cbox_IsTrainer.TabIndex = 2;
            this.cbox_IsTrainer.Text = "Trainer";
            this.cbox_IsTrainer.UseVisualStyleBackColor = true;
            this.cbox_IsTrainer.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // NPCImageBox
            // 
            this.NPCImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NPCImageBox.Location = new System.Drawing.Point(156, 127);
            this.NPCImageBox.Name = "NPCImageBox";
            this.NPCImageBox.Size = new System.Drawing.Size(118, 155);
            this.NPCImageBox.TabIndex = 15;
            this.NPCImageBox.TabStop = false;
            // 
            // btn_LoadNPCSprites
            // 
            this.btn_LoadNPCSprites.Location = new System.Drawing.Point(156, 96);
            this.btn_LoadNPCSprites.Name = "btn_LoadNPCSprites";
            this.btn_LoadNPCSprites.Size = new System.Drawing.Size(116, 25);
            this.btn_LoadNPCSprites.TabIndex = 14;
            this.btn_LoadNPCSprites.Text = "Load NPC Sprite";
            this.btn_LoadNPCSprites.UseVisualStyleBackColor = true;
            this.btn_LoadNPCSprites.Click += new System.EventHandler(this.btn_LoadNPCSprites_Click);
            // 
            // cbox_IsMale
            // 
            this.cbox_IsMale.AutoSize = true;
            this.cbox_IsMale.Location = new System.Drawing.Point(6, 54);
            this.cbox_IsMale.Name = "cbox_IsMale";
            this.cbox_IsMale.Size = new System.Drawing.Size(66, 17);
            this.cbox_IsMale.TabIndex = 13;
            this.cbox_IsMale.Text = "Is Male?";
            this.cbox_IsMale.UseVisualStyleBackColor = true;
            this.cbox_IsMale.CheckedChanged += new System.EventHandler(this.cbox_IsMale_CheckedChanged);
            // 
            // gbox_NPCFacingDirection
            // 
            this.gbox_NPCFacingDirection.Controls.Add(this.rbtn_NPCFacingDirection_RIGHT);
            this.gbox_NPCFacingDirection.Controls.Add(this.rbtn_NPCFacingDirection_LEFT);
            this.gbox_NPCFacingDirection.Controls.Add(this.rbtn_NPCFacingDirection_DOWN);
            this.gbox_NPCFacingDirection.Controls.Add(this.rbtn_NPCFacingDirection_UP);
            this.gbox_NPCFacingDirection.Location = new System.Drawing.Point(6, 77);
            this.gbox_NPCFacingDirection.Name = "gbox_NPCFacingDirection";
            this.gbox_NPCFacingDirection.Size = new System.Drawing.Size(103, 88);
            this.gbox_NPCFacingDirection.TabIndex = 11;
            this.gbox_NPCFacingDirection.TabStop = false;
            this.gbox_NPCFacingDirection.Text = "Facing Direction";
            // 
            // rbtn_NPCFacingDirection_RIGHT
            // 
            this.rbtn_NPCFacingDirection_RIGHT.AutoSize = true;
            this.rbtn_NPCFacingDirection_RIGHT.Location = new System.Drawing.Point(62, 42);
            this.rbtn_NPCFacingDirection_RIGHT.Name = "rbtn_NPCFacingDirection_RIGHT";
            this.rbtn_NPCFacingDirection_RIGHT.Size = new System.Drawing.Size(31, 17);
            this.rbtn_NPCFacingDirection_RIGHT.TabIndex = 3;
            this.rbtn_NPCFacingDirection_RIGHT.Text = "˃";
            this.rbtn_NPCFacingDirection_RIGHT.UseVisualStyleBackColor = true;
            this.rbtn_NPCFacingDirection_RIGHT.CheckedChanged += new System.EventHandler(this.rbtn_NPCFacingDirection_RIGHT_CheckedChanged);
            // 
            // rbtn_NPCFacingDirection_LEFT
            // 
            this.rbtn_NPCFacingDirection_LEFT.AutoSize = true;
            this.rbtn_NPCFacingDirection_LEFT.Location = new System.Drawing.Point(6, 42);
            this.rbtn_NPCFacingDirection_LEFT.Name = "rbtn_NPCFacingDirection_LEFT";
            this.rbtn_NPCFacingDirection_LEFT.Size = new System.Drawing.Size(31, 17);
            this.rbtn_NPCFacingDirection_LEFT.TabIndex = 2;
            this.rbtn_NPCFacingDirection_LEFT.Text = "˂";
            this.rbtn_NPCFacingDirection_LEFT.UseVisualStyleBackColor = true;
            this.rbtn_NPCFacingDirection_LEFT.CheckedChanged += new System.EventHandler(this.rbtn_NPCFacingDirection_LEFT_CheckedChanged);
            // 
            // rbtn_NPCFacingDirection_DOWN
            // 
            this.rbtn_NPCFacingDirection_DOWN.AutoSize = true;
            this.rbtn_NPCFacingDirection_DOWN.Checked = true;
            this.rbtn_NPCFacingDirection_DOWN.Location = new System.Drawing.Point(35, 65);
            this.rbtn_NPCFacingDirection_DOWN.Name = "rbtn_NPCFacingDirection_DOWN";
            this.rbtn_NPCFacingDirection_DOWN.Size = new System.Drawing.Size(31, 17);
            this.rbtn_NPCFacingDirection_DOWN.TabIndex = 1;
            this.rbtn_NPCFacingDirection_DOWN.TabStop = true;
            this.rbtn_NPCFacingDirection_DOWN.Text = "˅";
            this.rbtn_NPCFacingDirection_DOWN.UseVisualStyleBackColor = true;
            this.rbtn_NPCFacingDirection_DOWN.CheckedChanged += new System.EventHandler(this.rbtn_NPCFacingDirection_DOWN_CheckedChanged);
            // 
            // rbtn_NPCFacingDirection_UP
            // 
            this.rbtn_NPCFacingDirection_UP.AutoSize = true;
            this.rbtn_NPCFacingDirection_UP.Location = new System.Drawing.Point(35, 19);
            this.rbtn_NPCFacingDirection_UP.Name = "rbtn_NPCFacingDirection_UP";
            this.rbtn_NPCFacingDirection_UP.Size = new System.Drawing.Size(31, 17);
            this.rbtn_NPCFacingDirection_UP.TabIndex = 0;
            this.rbtn_NPCFacingDirection_UP.Text = "˄";
            this.rbtn_NPCFacingDirection_UP.UseVisualStyleBackColor = true;
            this.rbtn_NPCFacingDirection_UP.CheckedChanged += new System.EventHandler(this.rbtn_NPCFacingDirection_UP_CheckedChanged);
            // 
            // gbox_TrainerEditor
            // 
            this.gbox_TrainerEditor.Controls.Add(this.lview_TrainerActivePokemon);
            this.gbox_TrainerEditor.Controls.Add(this.tbox_TrainerPayout);
            this.gbox_TrainerEditor.Enabled = false;
            this.gbox_TrainerEditor.Location = new System.Drawing.Point(6, 453);
            this.gbox_TrainerEditor.Name = "gbox_TrainerEditor";
            this.gbox_TrainerEditor.Size = new System.Drawing.Size(268, 108);
            this.gbox_TrainerEditor.TabIndex = 5;
            this.gbox_TrainerEditor.TabStop = false;
            this.gbox_TrainerEditor.Text = "Trainer Pokemon Editor";
            // 
            // lview_TrainerActivePokemon
            // 
            this.lview_TrainerActivePokemon.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lview_TrainerActivePokemon.Location = new System.Drawing.Point(6, 46);
            this.lview_TrainerActivePokemon.MultiSelect = false;
            this.lview_TrainerActivePokemon.Name = "lview_TrainerActivePokemon";
            this.lview_TrainerActivePokemon.Size = new System.Drawing.Size(262, 56);
            this.lview_TrainerActivePokemon.TabIndex = 1;
            this.lview_TrainerActivePokemon.UseCompatibleStateImageBehavior = false;
            this.lview_TrainerActivePokemon.DoubleClick += new System.EventHandler(this.lview_TrainerActivePokemon_DoubleClick);
            // 
            // tbox_TrainerPayout
            // 
            this.tbox_TrainerPayout.Location = new System.Drawing.Point(7, 20);
            this.tbox_TrainerPayout.Name = "tbox_TrainerPayout";
            this.tbox_TrainerPayout.Size = new System.Drawing.Size(100, 20);
            this.tbox_TrainerPayout.TabIndex = 0;
            this.tbox_TrainerPayout.Text = "Trainer Payout";
            this.tbox_TrainerPayout.TextChanged += new System.EventHandler(this.tbox_TrainerPayout_TextChanged);
            // 
            // btn_ImportNPCScript
            // 
            this.btn_ImportNPCScript.Location = new System.Drawing.Point(196, 288);
            this.btn_ImportNPCScript.Name = "btn_ImportNPCScript";
            this.btn_ImportNPCScript.Size = new System.Drawing.Size(78, 39);
            this.btn_ImportNPCScript.TabIndex = 4;
            this.btn_ImportNPCScript.Text = "Import NPC Script";
            this.btn_ImportNPCScript.UseVisualStyleBackColor = true;
            this.btn_ImportNPCScript.Click += new System.EventHandler(this.btn_ImportNPCScript_Click);
            // 
            // tbox_NPCName
            // 
            this.tbox_NPCName.Location = new System.Drawing.Point(6, 30);
            this.tbox_NPCName.Name = "tbox_NPCName";
            this.tbox_NPCName.Size = new System.Drawing.Size(145, 20);
            this.tbox_NPCName.TabIndex = 1;
            this.tbox_NPCName.Text = "NPC Name";
            this.tbox_NPCName.TextChanged += new System.EventHandler(this.tbox_NPCName_TextChanged);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.viewToolStripMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(3, 3);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(277, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.Visible = false;
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTrainerToolStripMenuItem,
            this.exportTrainerToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // newTrainerToolStripMenuItem
            // 
            this.newTrainerToolStripMenuItem.Name = "newTrainerToolStripMenuItem";
            this.newTrainerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.newTrainerToolStripMenuItem.Text = "New Trainer";
            // 
            // exportTrainerToolStripMenuItem
            // 
            this.exportTrainerToolStripMenuItem.Name = "exportTrainerToolStripMenuItem";
            this.exportTrainerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exportTrainerToolStripMenuItem.Text = "Export Trainer";
            this.exportTrainerToolStripMenuItem.Click += new System.EventHandler(this.exportTrainerToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // SceneryTab
            // 
            this.SceneryTab.Controls.Add(this.sceneryBlockCheckbox);
            this.SceneryTab.Controls.Add(this.importScenery);
            this.SceneryTab.Controls.Add(this.label5);
            this.SceneryTab.Controls.Add(this.label4);
            this.SceneryTab.Controls.Add(this.label3);
            this.SceneryTab.Controls.Add(this.label2);
            this.SceneryTab.Controls.Add(this.modelScriptBox);
            this.SceneryTab.Controls.Add(this.modelNameBox);
            this.SceneryTab.Controls.Add(this.modelHeightBox);
            this.SceneryTab.Controls.Add(this.modelWidthBox);
            this.SceneryTab.Controls.Add(this.groupBox2);
            this.SceneryTab.Controls.Add(this.unrestrictedPlacementBox);
            this.SceneryTab.Controls.Add(this.cleanOrphanedSceneryButton);
            this.SceneryTab.Controls.Add(this.deleteSceneryButton);
            this.SceneryTab.Controls.Add(this.newSceneryButton);
            this.SceneryTab.Controls.Add(this.label1);
            this.SceneryTab.Controls.Add(this.sceneryBox);
            this.SceneryTab.Location = new System.Drawing.Point(4, 22);
            this.SceneryTab.Name = "SceneryTab";
            this.SceneryTab.Padding = new System.Windows.Forms.Padding(3);
            this.SceneryTab.Size = new System.Drawing.Size(289, 567);
            this.SceneryTab.TabIndex = 4;
            this.SceneryTab.Text = "Scenery";
            this.SceneryTab.UseVisualStyleBackColor = true;
            // 
            // importScenery
            // 
            this.importScenery.Location = new System.Drawing.Point(180, 156);
            this.importScenery.Name = "importScenery";
            this.importScenery.Size = new System.Drawing.Size(97, 23);
            this.importScenery.TabIndex = 18;
            this.importScenery.Text = "Import from Map";
            this.importScenery.UseVisualStyleBackColor = true;
            this.importScenery.Click += new System.EventHandler(this.importScenery_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 424);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Script";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Name";
            // 
            // modelScriptBox
            // 
            this.modelScriptBox.Location = new System.Drawing.Point(47, 421);
            this.modelScriptBox.Multiline = true;
            this.modelScriptBox.Name = "modelScriptBox";
            this.modelScriptBox.Size = new System.Drawing.Size(218, 109);
            this.modelScriptBox.TabIndex = 13;
            this.modelScriptBox.TextChanged += new System.EventHandler(this.modelScriptBox_TextChanged);
            // 
            // modelNameBox
            // 
            this.modelNameBox.Enabled = false;
            this.modelNameBox.Location = new System.Drawing.Point(165, 319);
            this.modelNameBox.Name = "modelNameBox";
            this.modelNameBox.Size = new System.Drawing.Size(100, 20);
            this.modelNameBox.TabIndex = 12;
            this.modelNameBox.TextChanged += new System.EventHandler(this.modelNameBox_TextChanged);
            // 
            // modelHeightBox
            // 
            this.modelHeightBox.Enabled = false;
            this.modelHeightBox.Location = new System.Drawing.Point(165, 392);
            this.modelHeightBox.Name = "modelHeightBox";
            this.modelHeightBox.Size = new System.Drawing.Size(100, 20);
            this.modelHeightBox.TabIndex = 11;
            this.modelHeightBox.TextChanged += new System.EventHandler(this.modelHeightBox_TextChanged);
            // 
            // modelWidthBox
            // 
            this.modelWidthBox.Enabled = false;
            this.modelWidthBox.Location = new System.Drawing.Point(165, 356);
            this.modelWidthBox.Name = "modelWidthBox";
            this.modelWidthBox.Size = new System.Drawing.Size(100, 20);
            this.modelWidthBox.TabIndex = 10;
            this.modelWidthBox.TextChanged += new System.EventHandler(this.modelWidthBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.modelDeleteRadio);
            this.groupBox2.Controls.Add(this.modelNewRadio);
            this.groupBox2.Controls.Add(this.modelEditRadio);
            this.groupBox2.Location = new System.Drawing.Point(180, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(85, 82);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // modelDeleteRadio
            // 
            this.modelDeleteRadio.AutoSize = true;
            this.modelDeleteRadio.Location = new System.Drawing.Point(18, 55);
            this.modelDeleteRadio.Name = "modelDeleteRadio";
            this.modelDeleteRadio.Size = new System.Drawing.Size(56, 17);
            this.modelDeleteRadio.TabIndex = 9;
            this.modelDeleteRadio.Text = "Delete";
            this.modelDeleteRadio.UseVisualStyleBackColor = true;
            this.modelDeleteRadio.CheckedChanged += new System.EventHandler(this.modelDeleteRadio_CheckedChanged);
            // 
            // modelNewRadio
            // 
            this.modelNewRadio.AutoSize = true;
            this.modelNewRadio.Checked = true;
            this.modelNewRadio.Location = new System.Drawing.Point(18, 8);
            this.modelNewRadio.Name = "modelNewRadio";
            this.modelNewRadio.Size = new System.Drawing.Size(47, 17);
            this.modelNewRadio.TabIndex = 7;
            this.modelNewRadio.TabStop = true;
            this.modelNewRadio.Text = "New";
            this.modelNewRadio.UseVisualStyleBackColor = true;
            this.modelNewRadio.CheckedChanged += new System.EventHandler(this.modelNewRadio_CheckedChanged);
            // 
            // modelEditRadio
            // 
            this.modelEditRadio.AutoSize = true;
            this.modelEditRadio.Location = new System.Drawing.Point(18, 31);
            this.modelEditRadio.Name = "modelEditRadio";
            this.modelEditRadio.Size = new System.Drawing.Size(43, 17);
            this.modelEditRadio.TabIndex = 8;
            this.modelEditRadio.Text = "Edit";
            this.modelEditRadio.UseVisualStyleBackColor = true;
            this.modelEditRadio.CheckedChanged += new System.EventHandler(this.modelEditRadio_CheckedChanged);
            // 
            // unrestrictedPlacementBox
            // 
            this.unrestrictedPlacementBox.AutoSize = true;
            this.unrestrictedPlacementBox.Location = new System.Drawing.Point(10, 281);
            this.unrestrictedPlacementBox.Name = "unrestrictedPlacementBox";
            this.unrestrictedPlacementBox.Size = new System.Drawing.Size(164, 17);
            this.unrestrictedPlacementBox.TabIndex = 6;
            this.unrestrictedPlacementBox.Text = "Allow Unrestricted Placement";
            this.unrestrictedPlacementBox.UseVisualStyleBackColor = true;
            // 
            // cleanOrphanedSceneryButton
            // 
            this.cleanOrphanedSceneryButton.Location = new System.Drawing.Point(167, 536);
            this.cleanOrphanedSceneryButton.Name = "cleanOrphanedSceneryButton";
            this.cleanOrphanedSceneryButton.Size = new System.Drawing.Size(110, 23);
            this.cleanOrphanedSceneryButton.TabIndex = 4;
            this.cleanOrphanedSceneryButton.Text = "Remove Orphans";
            this.cleanOrphanedSceneryButton.UseVisualStyleBackColor = true;
            this.cleanOrphanedSceneryButton.Click += new System.EventHandler(this.cleanOrphanedSceneryButton_Click);
            // 
            // deleteSceneryButton
            // 
            this.deleteSceneryButton.Location = new System.Drawing.Point(180, 109);
            this.deleteSceneryButton.Name = "deleteSceneryButton";
            this.deleteSceneryButton.Size = new System.Drawing.Size(97, 23);
            this.deleteSceneryButton.TabIndex = 3;
            this.deleteSceneryButton.Text = "Delete";
            this.deleteSceneryButton.UseVisualStyleBackColor = true;
            this.deleteSceneryButton.Click += new System.EventHandler(this.deleteSceneryButton_Click);
            // 
            // newSceneryButton
            // 
            this.newSceneryButton.Location = new System.Drawing.Point(10, 252);
            this.newSceneryButton.Name = "newSceneryButton";
            this.newSceneryButton.Size = new System.Drawing.Size(75, 23);
            this.newSceneryButton.TabIndex = 2;
            this.newSceneryButton.Text = "Add New";
            this.newSceneryButton.UseVisualStyleBackColor = true;
            this.newSceneryButton.Click += new System.EventHandler(this.newSceneryButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scenery Objects";
            // 
            // sceneryBox
            // 
            this.sceneryBox.FormattingEnabled = true;
            this.sceneryBox.Location = new System.Drawing.Point(6, 62);
            this.sceneryBox.Name = "sceneryBox";
            this.sceneryBox.Size = new System.Drawing.Size(167, 173);
            this.sceneryBox.TabIndex = 0;
            this.sceneryBox.SelectedIndexChanged += new System.EventHandler(this.sceneryBox_SelectedIndexChanged);
            // 
            // CutSceneTab
            // 
            this.CutSceneTab.Controls.Add(this.commandList);
            this.CutSceneTab.Controls.Add(this.numericUpDown1);
            this.CutSceneTab.Controls.Add(this.textBox1);
            this.CutSceneTab.Controls.Add(this.button3);
            this.CutSceneTab.Controls.Add(this.button2);
            this.CutSceneTab.Controls.Add(this.newDiscardCSButton);
            this.CutSceneTab.Location = new System.Drawing.Point(4, 22);
            this.CutSceneTab.Name = "CutSceneTab";
            this.CutSceneTab.Size = new System.Drawing.Size(289, 567);
            this.CutSceneTab.TabIndex = 5;
            this.CutSceneTab.Text = "CutScenes";
            this.CutSceneTab.UseVisualStyleBackColor = true;
            // 
            // commandList
            // 
            this.commandList.AllowColumnReorder = true;
            this.commandList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Time,
            this.Command});
            this.commandList.ContextMenuStrip = this.timedCommandContextMenu;
            this.commandList.FullRowSelect = true;
            this.commandList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7});
            this.commandList.Location = new System.Drawing.Point(8, 115);
            this.commandList.MultiSelect = false;
            this.commandList.Name = "commandList";
            this.commandList.Size = new System.Drawing.Size(251, 165);
            this.commandList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.commandList.TabIndex = 5;
            this.commandList.UseCompatibleStateImageBehavior = false;
            this.commandList.View = System.Windows.Forms.View.Details;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 70;
            // 
            // Command
            // 
            this.Command.Text = "Command";
            this.Command.Width = 166;
            // 
            // timedCommandContextMenu
            // 
            this.timedCommandContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.deleteToolStripMenuItem1});
            this.timedCommandContextMenu.Name = "timedCommandContextMenu";
            this.timedCommandContextMenu.Size = new System.Drawing.Size(108, 70);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.addNewToolStripMenuItem.Text = "Add";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Edit";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(192, 64);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(89, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 61);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Load from Zone";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add To Zone";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // newDiscardCSButton
            // 
            this.newDiscardCSButton.Location = new System.Drawing.Point(8, 3);
            this.newDiscardCSButton.Name = "newDiscardCSButton";
            this.newDiscardCSButton.Size = new System.Drawing.Size(94, 23);
            this.newDiscardCSButton.TabIndex = 0;
            this.newDiscardCSButton.Text = "New Cut Scene";
            this.newDiscardCSButton.UseVisualStyleBackColor = true;
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(20, 622);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(35, 13);
            this.debugLabel.TabIndex = 3;
            this.debugLabel.Text = "label1";
            // 
            // globalDebugLabel
            // 
            this.globalDebugLabel.AutoSize = true;
            this.globalDebugLabel.Location = new System.Drawing.Point(221, 622);
            this.globalDebugLabel.Name = "globalDebugLabel";
            this.globalDebugLabel.Size = new System.Drawing.Size(35, 13);
            this.globalDebugLabel.TabIndex = 4;
            this.globalDebugLabel.Text = "label7";
            // 
            // sceneryBlockCheckbox
            // 
            this.sceneryBlockCheckbox.AutoSize = true;
            this.sceneryBlockCheckbox.Checked = true;
            this.sceneryBlockCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sceneryBlockCheckbox.Location = new System.Drawing.Point(10, 304);
            this.sceneryBlockCheckbox.Name = "sceneryBlockCheckbox";
            this.sceneryBlockCheckbox.Size = new System.Drawing.Size(136, 17);
            this.sceneryBlockCheckbox.TabIndex = 19;
            this.sceneryBlockCheckbox.Text = "Block tiles under object";
            this.sceneryBlockCheckbox.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 646);
            this.Controls.Add(this.globalDebugLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Editor";
            this.Text = " TenFold Live Mapmaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolTab.ResumeLayout(false);
            this.TileSpritesTab.ResumeLayout(false);
            this.TileSpritesTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_TilePreview)).EndInit();
            this.TilePropertiesTab.ResumeLayout(false);
            this.TilePropertiesTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NPCTab.ResumeLayout(false);
            this.NPCTab.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbox_Movement_NPC.ResumeLayout(false);
            this.gbox_Movement_NPC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NPCImageBox)).EndInit();
            this.gbox_NPCFacingDirection.ResumeLayout(false);
            this.gbox_NPCFacingDirection.PerformLayout();
            this.gbox_TrainerEditor.ResumeLayout(false);
            this.gbox_TrainerEditor.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.SceneryTab.ResumeLayout(false);
            this.SceneryTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.CutSceneTab.ResumeLayout(false);
            this.CutSceneTab.PerformLayout();
            this.timedCommandContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Panel tilesPanel;
        public System.Windows.Forms.PictureBox pbox_TilePreview;
        private System.Windows.Forms.Button btn_BrowseTileDirectory;
        private System.Windows.Forms.ListBox lbox_Tiles;
        public System.Windows.Forms.TabControl toolTab;
        public System.Windows.Forms.TabPage TileSpritesTab;
        private System.Windows.Forms.Button setAllButton;
        private System.Windows.Forms.ToolStripMenuItem loadZoneToolStripMenuItem;
        public System.Windows.Forms.CheckBox southBox;
        public System.Windows.Forms.CheckBox eastBox;
        public System.Windows.Forms.CheckBox westBox;
        public System.Windows.Forms.CheckBox northBox;
        public System.Windows.Forms.CheckBox waterBox;
        public System.Windows.Forms.CheckBox rampBox;
        public System.Windows.Forms.CheckBox randomEncounterBox;
        public System.Windows.Forms.CheckBox jumpBox;
        private System.Windows.Forms.ToolStripMenuItem newZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem showAccessibilityToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem showScriptsToolStripMenuItem;
        public System.Windows.Forms.CheckBox controlWaterBox;
        public System.Windows.Forms.CheckBox controlBikeBox;
        public System.Windows.Forms.CheckBox controlREBox;
        public System.Windows.Forms.CheckBox controlJumpBox;
        public System.Windows.Forms.CheckBox controlSouthBox;
        public System.Windows.Forms.CheckBox controlEastBox;
        public System.Windows.Forms.CheckBox controlWestBox;
        public System.Windows.Forms.CheckBox controlNorthBox;
        public System.Windows.Forms.CheckBox tileScriptBox;
        public System.Windows.Forms.TextBox tileScriptTextBox;
        public System.Windows.Forms.TabPage TilePropertiesTab;
        public System.Windows.Forms.TabPage NPCTab;
        public System.Windows.Forms.TabPage SceneryTab;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox controlPropertiesBox;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenShotToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newTrainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTrainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.Button btn_ImportNPCScript;
        private System.Windows.Forms.GroupBox gbox_TrainerEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newSceneryButton;
        public System.Windows.Forms.ListBox sceneryBox;
        private System.Windows.Forms.ToolStripMenuItem saveToProductionToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbox_NPCFacingDirection;
        private System.Windows.Forms.Button deleteSceneryButton;
        private System.Windows.Forms.Button cleanOrphanedSceneryButton;
        public System.Windows.Forms.ListView lview_TrainerActivePokemon;
        public System.Windows.Forms.TextBox tbox_NPCName;
        public System.Windows.Forms.CheckBox unrestrictedPlacementBox;
        private System.Windows.Forms.Button btn_LoadNPCSprites;
        private System.Windows.Forms.ProgressBar pbar_ExtractionProgress;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCurrentZoneToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton modelEditRadio;
        public System.Windows.Forms.RadioButton modelNewRadio;
        public System.Windows.Forms.RadioButton modelDeleteRadio;
        public System.Windows.Forms.TextBox modelHeightBox;
        public System.Windows.Forms.TextBox modelWidthBox;
        public System.Windows.Forms.TextBox modelNameBox;
        public System.Windows.Forms.TextBox modelScriptBox;
        private System.Windows.Forms.GroupBox gbox_Movement_NPC;
        private System.Windows.Forms.Button NPCMovementButton;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.RadioButton NPCDeleteRadio;
        public System.Windows.Forms.RadioButton NPCEditRadio;
        public System.Windows.Forms.RadioButton NPCNewRadio;
        public System.Windows.Forms.CheckBox cbox_IsTrainer;
        public System.Windows.Forms.TextBox tbox_TrainerPayout;
        public System.Windows.Forms.RadioButton rbtn_NPCFacingDirection_RIGHT;
        public System.Windows.Forms.RadioButton rbtn_NPCFacingDirection_LEFT;
        public System.Windows.Forms.RadioButton rbtn_NPCFacingDirection_DOWN;
        public System.Windows.Forms.RadioButton rbtn_NPCFacingDirection_UP;
        public System.Windows.Forms.CheckBox cbox_IsMale;
        public System.Windows.Forms.PictureBox NPCImageBox;
        public System.Windows.Forms.TextBox NPCScriptBox;
        public System.Windows.Forms.RadioButton movementPathRadio;
        public System.Windows.Forms.RadioButton movementWanderRadio;
        public System.Windows.Forms.RadioButton movementNoneRadio;
        private System.Windows.Forms.TabPage CutSceneTab;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button newDiscardCSButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Command;
        private System.Windows.Forms.ContextMenuStrip timedCommandContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        internal System.Windows.Forms.ListView commandList;
        private System.Windows.Forms.Button importScenery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label globalDebugLabel;
        public System.Windows.Forms.CheckBox sceneryBlockCheckbox;
    }
}
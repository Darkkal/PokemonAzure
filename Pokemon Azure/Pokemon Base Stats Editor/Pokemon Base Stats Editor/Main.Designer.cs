namespace Pokemon_Base_Stats_Editor
{
    partial class Main
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
            this.cmbPokemon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbType2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numDex = new System.Windows.Forms.NumericUpDown();
            this.numForme = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numHP = new System.Windows.Forms.NumericUpDown();
            this.numATK = new System.Windows.Forms.NumericUpDown();
            this.numDEF = new System.Windows.Forms.NumericUpDown();
            this.numSPATK = new System.Windows.Forms.NumericUpDown();
            this.numSPDEF = new System.Windows.Forms.NumericUpDown();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lstSelectedMoves = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.numMoveLevel = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbAbility1 = new System.Windows.Forms.ComboBox();
            this.cmbAbility2 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numGrowth = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numGenderRatio = new System.Windows.Forms.NumericUpDown();
            this.cmbAvailableMoves = new System.Windows.Forms.ComboBox();
            this.txtDexEntry = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.importDexEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numDex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numATK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDEF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSPATK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSPDEF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveLevel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGrowth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGenderRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPokemon
            // 
            this.cmbPokemon.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPokemon.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPokemon.FormattingEnabled = true;
            this.cmbPokemon.Location = new System.Drawing.Point(9, 43);
            this.cmbPokemon.Name = "cmbPokemon";
            this.cmbPokemon.Size = new System.Drawing.Size(147, 21);
            this.cmbPokemon.TabIndex = 0;
            this.cmbPokemon.SelectedIndexChanged += new System.EventHandler(this.cmbPokemon_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Pokemon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dex #";
            // 
            // cmbType1
            // 
            this.cmbType1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType1.FormattingEnabled = true;
            this.cmbType1.Location = new System.Drawing.Point(76, 88);
            this.cmbType1.Name = "cmbType1";
            this.cmbType1.Size = new System.Drawing.Size(235, 21);
            this.cmbType1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Primary Elemental Type";
            // 
            // cmbType2
            // 
            this.cmbType2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbType2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType2.FormattingEnabled = true;
            this.cmbType2.Location = new System.Drawing.Point(77, 128);
            this.cmbType2.Name = "cmbType2";
            this.cmbType2.Size = new System.Drawing.Size(234, 21);
            this.cmbType2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Secondary Elemental Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Forme";
            // 
            // numDex
            // 
            this.numDex.Location = new System.Drawing.Point(11, 89);
            this.numDex.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numDex.Name = "numDex";
            this.numDex.Size = new System.Drawing.Size(46, 20);
            this.numDex.TabIndex = 10;
            // 
            // numForme
            // 
            this.numForme.Location = new System.Drawing.Point(11, 129);
            this.numForme.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numForme.Name = "numForme";
            this.numForme.Size = new System.Drawing.Size(46, 20);
            this.numForme.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "HP";
            // 
            // numHP
            // 
            this.numHP.Location = new System.Drawing.Point(11, 170);
            this.numHP.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numHP.Name = "numHP";
            this.numHP.Size = new System.Drawing.Size(46, 20);
            this.numHP.TabIndex = 13;
            // 
            // numATK
            // 
            this.numATK.Location = new System.Drawing.Point(58, 170);
            this.numATK.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numATK.Name = "numATK";
            this.numATK.Size = new System.Drawing.Size(46, 20);
            this.numATK.TabIndex = 14;
            // 
            // numDEF
            // 
            this.numDEF.Location = new System.Drawing.Point(110, 170);
            this.numDEF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numDEF.Name = "numDEF";
            this.numDEF.Size = new System.Drawing.Size(46, 20);
            this.numDEF.TabIndex = 15;
            // 
            // numSPATK
            // 
            this.numSPATK.Location = new System.Drawing.Point(162, 170);
            this.numSPATK.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSPATK.Name = "numSPATK";
            this.numSPATK.Size = new System.Drawing.Size(46, 20);
            this.numSPATK.TabIndex = 16;
            // 
            // numSPDEF
            // 
            this.numSPDEF.Location = new System.Drawing.Point(214, 170);
            this.numSPDEF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSPDEF.Name = "numSPDEF";
            this.numSPDEF.Size = new System.Drawing.Size(46, 20);
            this.numSPDEF.TabIndex = 17;
            // 
            // numSpeed
            // 
            this.numSpeed.Location = new System.Drawing.Point(265, 170);
            this.numSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(46, 20);
            this.numSpeed.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "ATK";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "DEF";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(159, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "SPATK";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(214, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "SPDEF";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(266, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Speed";
            // 
            // lstSelectedMoves
            // 
            this.lstSelectedMoves.FormattingEnabled = true;
            this.lstSelectedMoves.Location = new System.Drawing.Point(317, 43);
            this.lstSelectedMoves.Name = "lstSelectedMoves";
            this.lstSelectedMoves.Size = new System.Drawing.Size(300, 134);
            this.lstSelectedMoves.TabIndex = 24;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(495, 208);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(58, 23);
            this.btnAdd.TabIndex = 26;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(559, 208);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(58, 23);
            this.btnRemove.TabIndex = 27;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // numMoveLevel
            // 
            this.numMoveLevel.Location = new System.Drawing.Point(395, 210);
            this.numMoveLevel.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.numMoveLevel.Name = "numMoveLevel";
            this.numMoveLevel.Size = new System.Drawing.Size(94, 20);
            this.numMoveLevel.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(316, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Level Learned";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.importDexEntriesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(622, 24);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Abilities";
            // 
            // cmbAbility1
            // 
            this.cmbAbility1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAbility1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAbility1.FormattingEnabled = true;
            this.cmbAbility1.Location = new System.Drawing.Point(10, 210);
            this.cmbAbility1.Name = "cmbAbility1";
            this.cmbAbility1.Size = new System.Drawing.Size(112, 21);
            this.cmbAbility1.TabIndex = 32;
            // 
            // cmbAbility2
            // 
            this.cmbAbility2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAbility2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAbility2.FormattingEnabled = true;
            this.cmbAbility2.Location = new System.Drawing.Point(128, 210);
            this.cmbAbility2.Name = "cmbAbility2";
            this.cmbAbility2.Size = new System.Drawing.Size(114, 21);
            this.cmbAbility2.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(243, 193);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Growth Rate";
            // 
            // numGrowth
            // 
            this.numGrowth.Location = new System.Drawing.Point(246, 210);
            this.numGrowth.Name = "numGrowth";
            this.numGrowth.Size = new System.Drawing.Size(64, 20);
            this.numGrowth.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(172, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Gender Ratio";
            // 
            // numGenderRatio
            // 
            this.numGenderRatio.Location = new System.Drawing.Point(175, 43);
            this.numGenderRatio.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numGenderRatio.Name = "numGenderRatio";
            this.numGenderRatio.Size = new System.Drawing.Size(67, 20);
            this.numGenderRatio.TabIndex = 37;
            // 
            // cmbAvailableMoves
            // 
            this.cmbAvailableMoves.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAvailableMoves.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAvailableMoves.FormattingEnabled = true;
            this.cmbAvailableMoves.Location = new System.Drawing.Point(317, 183);
            this.cmbAvailableMoves.Name = "cmbAvailableMoves";
            this.cmbAvailableMoves.Size = new System.Drawing.Size(300, 21);
            this.cmbAvailableMoves.TabIndex = 38;
            // 
            // txtDexEntry
            // 
            this.txtDexEntry.Location = new System.Drawing.Point(68, 239);
            this.txtDexEntry.Name = "txtDexEntry";
            this.txtDexEntry.Size = new System.Drawing.Size(549, 20);
            this.txtDexEntry.TabIndex = 39;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 239);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 40;
            this.label16.Text = "Dex Entry";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(317, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 13);
            this.label17.TabIndex = 41;
            this.label17.Text = "Moves:";
            // 
            // importDexEntriesToolStripMenuItem
            // 
            this.importDexEntriesToolStripMenuItem.Name = "importDexEntriesToolStripMenuItem";
            this.importDexEntriesToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.importDexEntriesToolStripMenuItem.Text = "Import Dex Entries";
            this.importDexEntriesToolStripMenuItem.Click += new System.EventHandler(this.importDexEntriesToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 266);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtDexEntry);
            this.Controls.Add(this.cmbAvailableMoves);
            this.Controls.Add(this.numGenderRatio);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.numGrowth);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbAbility2);
            this.Controls.Add(this.cmbAbility1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.numMoveLevel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstSelectedMoves);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numSpeed);
            this.Controls.Add(this.numSPDEF);
            this.Controls.Add(this.numSPATK);
            this.Controls.Add(this.numDEF);
            this.Controls.Add(this.numATK);
            this.Controls.Add(this.numHP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numForme);
            this.Controls.Add(this.numDex);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbType2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbType1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPokemon);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Pokemon Base Stats Editor";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numDex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numForme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numATK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDEF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSPATK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSPDEF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveLevel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGrowth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGenderRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPokemon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbType1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numDex;
        private System.Windows.Forms.NumericUpDown numForme;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numHP;
        private System.Windows.Forms.NumericUpDown numATK;
        private System.Windows.Forms.NumericUpDown numDEF;
        private System.Windows.Forms.NumericUpDown numSPATK;
        private System.Windows.Forms.NumericUpDown numSPDEF;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lstSelectedMoves;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.NumericUpDown numMoveLevel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbAbility1;
        private System.Windows.Forms.ComboBox cmbAbility2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numGrowth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numGenderRatio;
        private System.Windows.Forms.ComboBox cmbAvailableMoves;
        private System.Windows.Forms.TextBox txtDexEntry;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolStripMenuItem importDexEntriesToolStripMenuItem;
    }
}


namespace MoveListEditor
{
    partial class frm_Main
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
            this.lbox_MoveList = new System.Windows.Forms.ListBox();
            this.mStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.lbl_Power = new System.Windows.Forms.Label();
            this.lbl_Accuracy = new System.Windows.Forms.Label();
            this.lbl_MoveType = new System.Windows.Forms.Label();
            this.lbl_MoveKind = new System.Windows.Forms.Label();
            this.lbl_PP = new System.Windows.Forms.Label();
            this.tbox_Name = new System.Windows.Forms.TextBox();
            this.tbox_Description = new System.Windows.Forms.TextBox();
            this.tbox_Power = new System.Windows.Forms.TextBox();
            this.tbox_Accuracy = new System.Windows.Forms.TextBox();
            this.tbox_PP = new System.Windows.Forms.TextBox();
            this.lbox_MoveType = new System.Windows.Forms.ListBox();
            this.lbox_MoveKind = new System.Windows.Forms.ListBox();
            this.gbox_Editor = new System.Windows.Forms.GroupBox();
            this.tbox_MoveScript = new System.Windows.Forms.TextBox();
            this.tbox_EffectScript = new System.Windows.Forms.TextBox();
            this.lbl_MoveScript = new System.Windows.Forms.Label();
            this.lbl_EffectScript = new System.Windows.Forms.Label();
            this.mStrip_Editor = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mStrip_Main.SuspendLayout();
            this.gbox_Editor.SuspendLayout();
            this.mStrip_Editor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbox_MoveList
            // 
            this.lbox_MoveList.FormattingEnabled = true;
            this.lbox_MoveList.Location = new System.Drawing.Point(12, 27);
            this.lbox_MoveList.Name = "lbox_MoveList";
            this.lbox_MoveList.Size = new System.Drawing.Size(126, 407);
            this.lbox_MoveList.TabIndex = 0;
            this.lbox_MoveList.SelectedIndexChanged += new System.EventHandler(this.lbox_MoveList_SelectedIndexChanged);
            // 
            // mStrip_Main
            // 
            this.mStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.moveListToolStripMenuItem});
            this.mStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.mStrip_Main.Name = "mStrip_Main";
            this.mStrip_Main.Size = new System.Drawing.Size(624, 24);
            this.mStrip_Main.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(6, 46);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(35, 13);
            this.lbl_Name.TabIndex = 2;
            this.lbl_Name.Text = "Name";
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(6, 72);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(60, 13);
            this.lbl_Description.TabIndex = 3;
            this.lbl_Description.Text = "Description";
            // 
            // lbl_Power
            // 
            this.lbl_Power.AutoSize = true;
            this.lbl_Power.Location = new System.Drawing.Point(7, 100);
            this.lbl_Power.Name = "lbl_Power";
            this.lbl_Power.Size = new System.Drawing.Size(37, 13);
            this.lbl_Power.TabIndex = 4;
            this.lbl_Power.Text = "Power";
            // 
            // lbl_Accuracy
            // 
            this.lbl_Accuracy.AutoSize = true;
            this.lbl_Accuracy.Location = new System.Drawing.Point(6, 122);
            this.lbl_Accuracy.Name = "lbl_Accuracy";
            this.lbl_Accuracy.Size = new System.Drawing.Size(52, 13);
            this.lbl_Accuracy.TabIndex = 5;
            this.lbl_Accuracy.Text = "Accuracy";
            // 
            // lbl_MoveType
            // 
            this.lbl_MoveType.AutoSize = true;
            this.lbl_MoveType.Location = new System.Drawing.Point(6, 145);
            this.lbl_MoveType.Name = "lbl_MoveType";
            this.lbl_MoveType.Size = new System.Drawing.Size(61, 13);
            this.lbl_MoveType.TabIndex = 6;
            this.lbl_MoveType.Text = "Move Type";
            // 
            // lbl_MoveKind
            // 
            this.lbl_MoveKind.AutoSize = true;
            this.lbl_MoveKind.Location = new System.Drawing.Point(6, 194);
            this.lbl_MoveKind.Name = "lbl_MoveKind";
            this.lbl_MoveKind.Size = new System.Drawing.Size(58, 13);
            this.lbl_MoveKind.TabIndex = 7;
            this.lbl_MoveKind.Text = "Move Kind";
            // 
            // lbl_PP
            // 
            this.lbl_PP.AutoSize = true;
            this.lbl_PP.Location = new System.Drawing.Point(8, 246);
            this.lbl_PP.Name = "lbl_PP";
            this.lbl_PP.Size = new System.Drawing.Size(21, 13);
            this.lbl_PP.TabIndex = 8;
            this.lbl_PP.Text = "PP";
            // 
            // tbox_Name
            // 
            this.tbox_Name.Location = new System.Drawing.Point(72, 43);
            this.tbox_Name.Name = "tbox_Name";
            this.tbox_Name.Size = new System.Drawing.Size(390, 20);
            this.tbox_Name.TabIndex = 9;
            // 
            // tbox_Description
            // 
            this.tbox_Description.Location = new System.Drawing.Point(72, 69);
            this.tbox_Description.Name = "tbox_Description";
            this.tbox_Description.Size = new System.Drawing.Size(390, 20);
            this.tbox_Description.TabIndex = 10;
            // 
            // tbox_Power
            // 
            this.tbox_Power.Location = new System.Drawing.Point(72, 93);
            this.tbox_Power.Name = "tbox_Power";
            this.tbox_Power.Size = new System.Drawing.Size(390, 20);
            this.tbox_Power.TabIndex = 11;
            // 
            // tbox_Accuracy
            // 
            this.tbox_Accuracy.Location = new System.Drawing.Point(72, 119);
            this.tbox_Accuracy.Name = "tbox_Accuracy";
            this.tbox_Accuracy.Size = new System.Drawing.Size(390, 20);
            this.tbox_Accuracy.TabIndex = 12;
            // 
            // tbox_PP
            // 
            this.tbox_PP.Location = new System.Drawing.Point(72, 243);
            this.tbox_PP.Name = "tbox_PP";
            this.tbox_PP.Size = new System.Drawing.Size(390, 20);
            this.tbox_PP.TabIndex = 13;
            // 
            // lbox_MoveType
            // 
            this.lbox_MoveType.FormattingEnabled = true;
            this.lbox_MoveType.Items.AddRange(new object[] {
            "Bug",
            "Dark",
            "Dragon",
            "Electric",
            "Fighting",
            "Fire",
            "Flying",
            "Ghost",
            "Grass",
            "Ground",
            "Ice",
            "Normal",
            "Poison",
            "Psychic",
            "Rock",
            "Steel",
            "Water"});
            this.lbox_MoveType.Location = new System.Drawing.Point(72, 145);
            this.lbox_MoveType.Name = "lbox_MoveType";
            this.lbox_MoveType.Size = new System.Drawing.Size(390, 43);
            this.lbox_MoveType.TabIndex = 14;
            // 
            // lbox_MoveKind
            // 
            this.lbox_MoveKind.FormattingEnabled = true;
            this.lbox_MoveKind.Items.AddRange(new object[] {
            "Physical",
            "Special",
            "Status"});
            this.lbox_MoveKind.Location = new System.Drawing.Point(72, 194);
            this.lbox_MoveKind.Name = "lbox_MoveKind";
            this.lbox_MoveKind.Size = new System.Drawing.Size(390, 43);
            this.lbox_MoveKind.TabIndex = 15;
            // 
            // gbox_Editor
            // 
            this.gbox_Editor.Controls.Add(this.lbl_EffectScript);
            this.gbox_Editor.Controls.Add(this.lbl_MoveScript);
            this.gbox_Editor.Controls.Add(this.tbox_EffectScript);
            this.gbox_Editor.Controls.Add(this.tbox_MoveScript);
            this.gbox_Editor.Controls.Add(this.lbl_Name);
            this.gbox_Editor.Controls.Add(this.lbox_MoveKind);
            this.gbox_Editor.Controls.Add(this.lbl_Description);
            this.gbox_Editor.Controls.Add(this.lbox_MoveType);
            this.gbox_Editor.Controls.Add(this.lbl_Power);
            this.gbox_Editor.Controls.Add(this.tbox_PP);
            this.gbox_Editor.Controls.Add(this.lbl_Accuracy);
            this.gbox_Editor.Controls.Add(this.tbox_Accuracy);
            this.gbox_Editor.Controls.Add(this.lbl_MoveType);
            this.gbox_Editor.Controls.Add(this.tbox_Power);
            this.gbox_Editor.Controls.Add(this.lbl_MoveKind);
            this.gbox_Editor.Controls.Add(this.tbox_Description);
            this.gbox_Editor.Controls.Add(this.lbl_PP);
            this.gbox_Editor.Controls.Add(this.tbox_Name);
            this.gbox_Editor.Controls.Add(this.mStrip_Editor);
            this.gbox_Editor.Enabled = false;
            this.gbox_Editor.Location = new System.Drawing.Point(144, 27);
            this.gbox_Editor.Name = "gbox_Editor";
            this.gbox_Editor.Size = new System.Drawing.Size(468, 403);
            this.gbox_Editor.TabIndex = 16;
            this.gbox_Editor.TabStop = false;
            this.gbox_Editor.Text = "Editor";
            // 
            // tbox_MoveScript
            // 
            this.tbox_MoveScript.Location = new System.Drawing.Point(6, 282);
            this.tbox_MoveScript.Multiline = true;
            this.tbox_MoveScript.Name = "tbox_MoveScript";
            this.tbox_MoveScript.Size = new System.Drawing.Size(222, 115);
            this.tbox_MoveScript.TabIndex = 16;
            // 
            // tbox_EffectScript
            // 
            this.tbox_EffectScript.Location = new System.Drawing.Point(240, 282);
            this.tbox_EffectScript.Multiline = true;
            this.tbox_EffectScript.Name = "tbox_EffectScript";
            this.tbox_EffectScript.Size = new System.Drawing.Size(222, 115);
            this.tbox_EffectScript.TabIndex = 17;
            // 
            // lbl_MoveScript
            // 
            this.lbl_MoveScript.AutoSize = true;
            this.lbl_MoveScript.Location = new System.Drawing.Point(6, 266);
            this.lbl_MoveScript.Name = "lbl_MoveScript";
            this.lbl_MoveScript.Size = new System.Drawing.Size(64, 13);
            this.lbl_MoveScript.TabIndex = 18;
            this.lbl_MoveScript.Text = "Move Script";
            // 
            // lbl_EffectScript
            // 
            this.lbl_EffectScript.AutoSize = true;
            this.lbl_EffectScript.Location = new System.Drawing.Point(237, 266);
            this.lbl_EffectScript.Name = "lbl_EffectScript";
            this.lbl_EffectScript.Size = new System.Drawing.Size(65, 13);
            this.lbl_EffectScript.TabIndex = 19;
            this.lbl_EffectScript.Text = "Effect Script";
            // 
            // mStrip_Editor
            // 
            this.mStrip_Editor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.mStrip_Editor.Location = new System.Drawing.Point(3, 16);
            this.mStrip_Editor.Name = "mStrip_Editor";
            this.mStrip_Editor.Size = new System.Drawing.Size(462, 24);
            this.mStrip_Editor.TabIndex = 20;
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1,
            this.reloadToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templatesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveScriptToolStripMenuItem,
            this.effectScriptToolStripMenuItem});
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            this.templatesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.templatesToolStripMenuItem.Text = "Templates";
            // 
            // moveScriptToolStripMenuItem
            // 
            this.moveScriptToolStripMenuItem.Name = "moveScriptToolStripMenuItem";
            this.moveScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.moveScriptToolStripMenuItem.Text = "Move Script";
            // 
            // effectScriptToolStripMenuItem
            // 
            this.effectScriptToolStripMenuItem.Name = "effectScriptToolStripMenuItem";
            this.effectScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.effectScriptToolStripMenuItem.Text = "Effect Script";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // moveListToolStripMenuItem
            // 
            this.moveListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.moveListToolStripMenuItem.Enabled = false;
            this.moveListToolStripMenuItem.Name = "moveListToolStripMenuItem";
            this.moveListToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.moveListToolStripMenuItem.Text = "Move List";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.gbox_Editor);
            this.Controls.Add(this.lbox_MoveList);
            this.Controls.Add(this.mStrip_Main);
            this.MainMenuStrip = this.mStrip_Main;
            this.Name = "frm_Main";
            this.Text = "MoveList Editor";
            this.mStrip_Main.ResumeLayout(false);
            this.mStrip_Main.PerformLayout();
            this.gbox_Editor.ResumeLayout(false);
            this.gbox_Editor.PerformLayout();
            this.mStrip_Editor.ResumeLayout(false);
            this.mStrip_Editor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbox_MoveList;
        private System.Windows.Forms.MenuStrip mStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.Label lbl_Power;
        private System.Windows.Forms.Label lbl_Accuracy;
        private System.Windows.Forms.Label lbl_MoveType;
        private System.Windows.Forms.Label lbl_MoveKind;
        private System.Windows.Forms.Label lbl_PP;
        private System.Windows.Forms.TextBox tbox_Name;
        private System.Windows.Forms.TextBox tbox_Description;
        private System.Windows.Forms.TextBox tbox_Power;
        private System.Windows.Forms.TextBox tbox_Accuracy;
        private System.Windows.Forms.TextBox tbox_PP;
        private System.Windows.Forms.ListBox lbox_MoveType;
        private System.Windows.Forms.ListBox lbox_MoveKind;
        private System.Windows.Forms.GroupBox gbox_Editor;
        private System.Windows.Forms.Label lbl_EffectScript;
        private System.Windows.Forms.Label lbl_MoveScript;
        private System.Windows.Forms.TextBox tbox_EffectScript;
        private System.Windows.Forms.TextBox tbox_MoveScript;
        private System.Windows.Forms.MenuStrip mStrip_Editor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}

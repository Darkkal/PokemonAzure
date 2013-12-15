namespace LiveMapMaker
{
    partial class ActivePokemonEditor
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
            this.lbox_PokemonList = new System.Windows.Forms.ListBox();
            this.tbox_Level = new System.Windows.Forms.TextBox();
            this.clbox_MoveList = new System.Windows.Forms.CheckedListBox();
            this.btn_SaveExit = new System.Windows.Forms.Button();
            this.lbl_Level = new System.Windows.Forms.Label();
            this.lbox_Gender = new System.Windows.Forms.ListBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbox_PokemonList
            // 
            this.lbox_PokemonList.FormattingEnabled = true;
            this.lbox_PokemonList.Location = new System.Drawing.Point(13, 10);
            this.lbox_PokemonList.Name = "lbox_PokemonList";
            this.lbox_PokemonList.Size = new System.Drawing.Size(120, 95);
            this.lbox_PokemonList.TabIndex = 0;
            this.lbox_PokemonList.SelectedIndexChanged += new System.EventHandler(this.lbox_PokemonList_SelectedIndexChanged);
            // 
            // tbox_Level
            // 
            this.tbox_Level.Location = new System.Drawing.Point(51, 111);
            this.tbox_Level.Name = "tbox_Level";
            this.tbox_Level.Size = new System.Drawing.Size(26, 20);
            this.tbox_Level.TabIndex = 1;
            this.tbox_Level.Text = "1";
            // 
            // clbox_MoveList
            // 
            this.clbox_MoveList.Enabled = false;
            this.clbox_MoveList.FormattingEnabled = true;
            this.clbox_MoveList.Location = new System.Drawing.Point(13, 183);
            this.clbox_MoveList.Name = "clbox_MoveList";
            this.clbox_MoveList.Size = new System.Drawing.Size(120, 79);
            this.clbox_MoveList.TabIndex = 2;
            // 
            // btn_SaveExit
            // 
            this.btn_SaveExit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_SaveExit.Location = new System.Drawing.Point(13, 268);
            this.btn_SaveExit.Name = "btn_SaveExit";
            this.btn_SaveExit.Size = new System.Drawing.Size(116, 23);
            this.btn_SaveExit.TabIndex = 3;
            this.btn_SaveExit.Text = "Save / Exit";
            this.btn_SaveExit.UseVisualStyleBackColor = true;
            this.btn_SaveExit.Click += new System.EventHandler(this.btn_SaveExit_Click);
            // 
            // lbl_Level
            // 
            this.lbl_Level.AutoSize = true;
            this.lbl_Level.Location = new System.Drawing.Point(12, 118);
            this.lbl_Level.Name = "lbl_Level";
            this.lbl_Level.Size = new System.Drawing.Size(33, 13);
            this.lbl_Level.TabIndex = 4;
            this.lbl_Level.Text = "Level";
            // 
            // lbox_Gender
            // 
            this.lbox_Gender.Enabled = false;
            this.lbox_Gender.FormattingEnabled = true;
            this.lbox_Gender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "None (N/A)"});
            this.lbox_Gender.Location = new System.Drawing.Point(13, 134);
            this.lbox_Gender.Name = "lbox_Gender";
            this.lbox_Gender.Size = new System.Drawing.Size(120, 43);
            this.lbox_Gender.TabIndex = 5;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(15, 298);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(114, 23);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // ActivePokemonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(147, 327);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lbox_Gender);
            this.Controls.Add(this.lbl_Level);
            this.Controls.Add(this.btn_SaveExit);
            this.Controls.Add(this.clbox_MoveList);
            this.Controls.Add(this.tbox_Level);
            this.Controls.Add(this.lbox_PokemonList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActivePokemonEditor";
            this.Text = "Active Pokemon Editor";
            this.Load += new System.EventHandler(this.ActivePokemonEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbox_PokemonList;
        private System.Windows.Forms.TextBox tbox_Level;
        private System.Windows.Forms.CheckedListBox clbox_MoveList;
        private System.Windows.Forms.Button btn_SaveExit;
        private System.Windows.Forms.Label lbl_Level;
        private System.Windows.Forms.ListBox lbox_Gender;
        private System.Windows.Forms.Button btn_Cancel;
    }
}
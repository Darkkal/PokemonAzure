namespace MoveListEditor
{
    partial class frm_AddMove
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_EffectScript = new System.Windows.Forms.Label();
            this.lbl_MoveScript = new System.Windows.Forms.Label();
            this.tbox_EffectScript = new System.Windows.Forms.TextBox();
            this.tbox_MoveScript = new System.Windows.Forms.TextBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbox_MoveKind = new System.Windows.Forms.ListBox();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.lbox_MoveType = new System.Windows.Forms.ListBox();
            this.lbl_Power = new System.Windows.Forms.Label();
            this.tbox_PP = new System.Windows.Forms.TextBox();
            this.lbl_Accuracy = new System.Windows.Forms.Label();
            this.tbox_Accuracy = new System.Windows.Forms.TextBox();
            this.lbl_MoveType = new System.Windows.Forms.Label();
            this.tbox_Power = new System.Windows.Forms.TextBox();
            this.lbl_MoveKind = new System.Windows.Forms.Label();
            this.tbox_Description = new System.Windows.Forms.TextBox();
            this.lbl_PP = new System.Windows.Forms.Label();
            this.tbox_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(8, 372);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(222, 23);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(242, 372);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(222, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lbl_EffectScript
            // 
            this.lbl_EffectScript.AutoSize = true;
            this.lbl_EffectScript.Location = new System.Drawing.Point(239, 235);
            this.lbl_EffectScript.Name = "lbl_EffectScript";
            this.lbl_EffectScript.Size = new System.Drawing.Size(65, 13);
            this.lbl_EffectScript.TabIndex = 19;
            this.lbl_EffectScript.Text = "Effect Script";
            // 
            // lbl_MoveScript
            // 
            this.lbl_MoveScript.AutoSize = true;
            this.lbl_MoveScript.Location = new System.Drawing.Point(8, 235);
            this.lbl_MoveScript.Name = "lbl_MoveScript";
            this.lbl_MoveScript.Size = new System.Drawing.Size(64, 13);
            this.lbl_MoveScript.TabIndex = 18;
            this.lbl_MoveScript.Text = "Move Script";
            // 
            // tbox_EffectScript
            // 
            this.tbox_EffectScript.Location = new System.Drawing.Point(242, 251);
            this.tbox_EffectScript.Multiline = true;
            this.tbox_EffectScript.Name = "tbox_EffectScript";
            this.tbox_EffectScript.Size = new System.Drawing.Size(222, 115);
            this.tbox_EffectScript.TabIndex = 17;
            // 
            // tbox_MoveScript
            // 
            this.tbox_MoveScript.Location = new System.Drawing.Point(8, 251);
            this.tbox_MoveScript.Multiline = true;
            this.tbox_MoveScript.Name = "tbox_MoveScript";
            this.tbox_MoveScript.Size = new System.Drawing.Size(222, 115);
            this.tbox_MoveScript.TabIndex = 16;
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Location = new System.Drawing.Point(8, 15);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(35, 13);
            this.lbl_Name.TabIndex = 2;
            this.lbl_Name.Text = "Name";
            // 
            // lbox_MoveKind
            // 
            this.lbox_MoveKind.FormattingEnabled = true;
            this.lbox_MoveKind.Items.AddRange(new object[] {
            "Physical",
            "Special",
            "Status"});
            this.lbox_MoveKind.Location = new System.Drawing.Point(74, 163);
            this.lbox_MoveKind.Name = "lbox_MoveKind";
            this.lbox_MoveKind.Size = new System.Drawing.Size(390, 43);
            this.lbox_MoveKind.TabIndex = 15;
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(8, 41);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(60, 13);
            this.lbl_Description.TabIndex = 3;
            this.lbl_Description.Text = "Description";
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
            this.lbox_MoveType.Location = new System.Drawing.Point(74, 114);
            this.lbox_MoveType.Name = "lbox_MoveType";
            this.lbox_MoveType.Size = new System.Drawing.Size(390, 43);
            this.lbox_MoveType.TabIndex = 14;
            // 
            // lbl_Power
            // 
            this.lbl_Power.AutoSize = true;
            this.lbl_Power.Location = new System.Drawing.Point(9, 69);
            this.lbl_Power.Name = "lbl_Power";
            this.lbl_Power.Size = new System.Drawing.Size(37, 13);
            this.lbl_Power.TabIndex = 4;
            this.lbl_Power.Text = "Power";
            // 
            // tbox_PP
            // 
            this.tbox_PP.Location = new System.Drawing.Point(74, 212);
            this.tbox_PP.Name = "tbox_PP";
            this.tbox_PP.Size = new System.Drawing.Size(390, 20);
            this.tbox_PP.TabIndex = 13;
            // 
            // lbl_Accuracy
            // 
            this.lbl_Accuracy.AutoSize = true;
            this.lbl_Accuracy.Location = new System.Drawing.Point(8, 91);
            this.lbl_Accuracy.Name = "lbl_Accuracy";
            this.lbl_Accuracy.Size = new System.Drawing.Size(52, 13);
            this.lbl_Accuracy.TabIndex = 5;
            this.lbl_Accuracy.Text = "Accuracy";
            // 
            // tbox_Accuracy
            // 
            this.tbox_Accuracy.Location = new System.Drawing.Point(74, 88);
            this.tbox_Accuracy.Name = "tbox_Accuracy";
            this.tbox_Accuracy.Size = new System.Drawing.Size(390, 20);
            this.tbox_Accuracy.TabIndex = 12;
            // 
            // lbl_MoveType
            // 
            this.lbl_MoveType.AutoSize = true;
            this.lbl_MoveType.Location = new System.Drawing.Point(8, 114);
            this.lbl_MoveType.Name = "lbl_MoveType";
            this.lbl_MoveType.Size = new System.Drawing.Size(61, 13);
            this.lbl_MoveType.TabIndex = 6;
            this.lbl_MoveType.Text = "Move Type";
            // 
            // tbox_Power
            // 
            this.tbox_Power.Location = new System.Drawing.Point(74, 62);
            this.tbox_Power.Name = "tbox_Power";
            this.tbox_Power.Size = new System.Drawing.Size(390, 20);
            this.tbox_Power.TabIndex = 11;
            // 
            // lbl_MoveKind
            // 
            this.lbl_MoveKind.AutoSize = true;
            this.lbl_MoveKind.Location = new System.Drawing.Point(8, 163);
            this.lbl_MoveKind.Name = "lbl_MoveKind";
            this.lbl_MoveKind.Size = new System.Drawing.Size(58, 13);
            this.lbl_MoveKind.TabIndex = 7;
            this.lbl_MoveKind.Text = "Move Kind";
            // 
            // tbox_Description
            // 
            this.tbox_Description.Location = new System.Drawing.Point(74, 38);
            this.tbox_Description.Name = "tbox_Description";
            this.tbox_Description.Size = new System.Drawing.Size(390, 20);
            this.tbox_Description.TabIndex = 10;
            // 
            // lbl_PP
            // 
            this.lbl_PP.AutoSize = true;
            this.lbl_PP.Location = new System.Drawing.Point(10, 215);
            this.lbl_PP.Name = "lbl_PP";
            this.lbl_PP.Size = new System.Drawing.Size(21, 13);
            this.lbl_PP.TabIndex = 8;
            this.lbl_PP.Text = "PP";
            // 
            // tbox_Name
            // 
            this.tbox_Name.Location = new System.Drawing.Point(74, 12);
            this.tbox_Name.Name = "tbox_Name";
            this.tbox_Name.Size = new System.Drawing.Size(390, 20);
            this.tbox_Name.TabIndex = 9;
            // 
            // frm_AddMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 407);
            this.Controls.Add(this.lbl_EffectScript);
            this.Controls.Add(this.lbl_MoveScript);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.tbox_EffectScript);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tbox_MoveScript);
            this.Controls.Add(this.tbox_Name);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.lbl_PP);
            this.Controls.Add(this.lbox_MoveKind);
            this.Controls.Add(this.tbox_Description);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.lbl_MoveKind);
            this.Controls.Add(this.lbox_MoveType);
            this.Controls.Add(this.tbox_Power);
            this.Controls.Add(this.lbl_Power);
            this.Controls.Add(this.lbl_MoveType);
            this.Controls.Add(this.tbox_PP);
            this.Controls.Add(this.tbox_Accuracy);
            this.Controls.Add(this.lbl_Accuracy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_AddMove";
            this.Text = "Add a Move to the MoveList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_EffectScript;
        private System.Windows.Forms.Label lbl_MoveScript;
        private System.Windows.Forms.TextBox tbox_EffectScript;
        private System.Windows.Forms.TextBox tbox_MoveScript;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.ListBox lbox_MoveKind;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.ListBox lbox_MoveType;
        private System.Windows.Forms.Label lbl_Power;
        private System.Windows.Forms.TextBox tbox_PP;
        private System.Windows.Forms.Label lbl_Accuracy;
        private System.Windows.Forms.TextBox tbox_Accuracy;
        private System.Windows.Forms.Label lbl_MoveType;
        private System.Windows.Forms.TextBox tbox_Power;
        private System.Windows.Forms.Label lbl_MoveKind;
        private System.Windows.Forms.TextBox tbox_Description;
        private System.Windows.Forms.Label lbl_PP;
        private System.Windows.Forms.TextBox tbox_Name;
    }
}
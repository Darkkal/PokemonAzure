namespace LiveMapMaker
{
    partial class newZoneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newZoneForm));
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.zoneWidthBox = new System.Windows.Forms.MaskedTextBox();
            this.zoneHeightBox = new System.Windows.Forms.MaskedTextBox();
            this.zoneXBox = new System.Windows.Forms.MaskedTextBox();
            this.zoneYBox = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.isRoomBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(81, 27);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(186, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zone Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zone Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Zone Height";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Zone Position  X:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(25, 273);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Okay";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(155, 273);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // zoneWidthBox
            // 
            this.zoneWidthBox.Location = new System.Drawing.Point(81, 69);
            this.zoneWidthBox.Name = "zoneWidthBox";
            this.zoneWidthBox.Size = new System.Drawing.Size(66, 20);
            this.zoneWidthBox.TabIndex = 1;
            // 
            // zoneHeightBox
            // 
            this.zoneHeightBox.Location = new System.Drawing.Point(81, 96);
            this.zoneHeightBox.Name = "zoneHeightBox";
            this.zoneHeightBox.Size = new System.Drawing.Size(66, 20);
            this.zoneHeightBox.TabIndex = 2;
            this.zoneHeightBox.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox2_MaskInputRejected);
            // 
            // zoneXBox
            // 
            this.zoneXBox.Location = new System.Drawing.Point(106, 157);
            this.zoneXBox.Name = "zoneXBox";
            this.zoneXBox.Size = new System.Drawing.Size(66, 20);
            this.zoneXBox.TabIndex = 3;
            // 
            // zoneYBox
            // 
            this.zoneYBox.Location = new System.Drawing.Point(201, 157);
            this.zoneYBox.Name = "zoneYBox";
            this.zoneYBox.Size = new System.Drawing.Size(66, 20);
            this.zoneYBox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Y:";
            // 
            // isRoomBox
            // 
            this.isRoomBox.AutoSize = true;
            this.isRoomBox.Location = new System.Drawing.Point(81, 218);
            this.isRoomBox.Name = "isRoomBox";
            this.isRoomBox.Size = new System.Drawing.Size(74, 17);
            this.isRoomBox.TabIndex = 17;
            this.isRoomBox.Text = "Is a Room";
            this.isRoomBox.UseVisualStyleBackColor = true;
            // 
            // newZoneForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 316);
            this.Controls.Add(this.isRoomBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.zoneYBox);
            this.Controls.Add(this.zoneXBox);
            this.Controls.Add(this.zoneHeightBox);
            this.Controls.Add(this.zoneWidthBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "newZoneForm";
            this.Text = "New Zone";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.MaskedTextBox zoneWidthBox;
        private System.Windows.Forms.MaskedTextBox zoneHeightBox;
        private System.Windows.Forms.MaskedTextBox zoneXBox;
        private System.Windows.Forms.MaskedTextBox zoneYBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox isRoomBox;
    }
}
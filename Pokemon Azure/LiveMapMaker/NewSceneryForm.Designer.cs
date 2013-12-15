namespace LiveMapMaker
{
    partial class NewSceneryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSceneryForm));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.modelYSize = new System.Windows.Forms.TextBox();
            this.modelXSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sceneryScriptBox = new System.Windows.Forms.TextBox();
            this.loadModelButton = new System.Windows.Forms.Button();
            this.modelBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.sceneryNameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Size Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Size X:";
            // 
            // modelYSize
            // 
            this.modelYSize.Location = new System.Drawing.Point(181, 357);
            this.modelYSize.Name = "modelYSize";
            this.modelYSize.Size = new System.Drawing.Size(64, 20);
            this.modelYSize.TabIndex = 14;
            // 
            // modelXSize
            // 
            this.modelXSize.Location = new System.Drawing.Point(65, 357);
            this.modelXSize.Name = "modelXSize";
            this.modelXSize.Size = new System.Drawing.Size(64, 20);
            this.modelXSize.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Interact Script";
            // 
            // sceneryScriptBox
            // 
            this.sceneryScriptBox.Location = new System.Drawing.Point(12, 231);
            this.sceneryScriptBox.Multiline = true;
            this.sceneryScriptBox.Name = "sceneryScriptBox";
            this.sceneryScriptBox.Size = new System.Drawing.Size(271, 120);
            this.sceneryScriptBox.TabIndex = 11;
            // 
            // loadModelButton
            // 
            this.loadModelButton.Location = new System.Drawing.Point(12, 179);
            this.loadModelButton.Name = "loadModelButton";
            this.loadModelButton.Size = new System.Drawing.Size(75, 23);
            this.loadModelButton.TabIndex = 10;
            this.loadModelButton.Text = "Load Model";
            this.loadModelButton.UseVisualStyleBackColor = true;
            this.loadModelButton.Click += new System.EventHandler(this.loadModelButton_Click);
            // 
            // modelBox
            // 
            this.modelBox.FormattingEnabled = true;
            this.modelBox.Location = new System.Drawing.Point(9, 52);
            this.modelBox.Name = "modelBox";
            this.modelBox.Size = new System.Drawing.Size(212, 121);
            this.modelBox.TabIndex = 9;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(15, 384);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 17;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(222, 384);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // sceneryNameBox
            // 
            this.sceneryNameBox.Location = new System.Drawing.Point(47, 13);
            this.sceneryNameBox.Name = "sceneryNameBox";
            this.sceneryNameBox.Size = new System.Drawing.Size(153, 20);
            this.sceneryNameBox.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Name";
            // 
            // NewSceneryForm
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 419);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sceneryNameBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modelYSize);
            this.Controls.Add(this.modelXSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sceneryScriptBox);
            this.Controls.Add(this.loadModelButton);
            this.Controls.Add(this.modelBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewSceneryForm";
            this.Text = "New Scenery Object";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox modelYSize;
        public System.Windows.Forms.TextBox modelXSize;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox sceneryScriptBox;
        private System.Windows.Forms.Button loadModelButton;
        public System.Windows.Forms.ListBox modelBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox sceneryNameBox;
        private System.Windows.Forms.Label label4;
    }
}
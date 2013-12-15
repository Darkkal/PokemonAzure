namespace LiveMapMaker
{
    partial class CinematicForm
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
            this.startXBox = new System.Windows.Forms.TextBox();
            this.startYBox = new System.Windows.Forms.TextBox();
            this.endXBox = new System.Windows.Forms.TextBox();
            this.endYBox = new System.Windows.Forms.TextBox();
            this.startScaleBox = new System.Windows.Forms.TextBox();
            this.endScaleBox = new System.Windows.Forms.TextBox();
            this.timeBox = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.inputBox = new System.Windows.Forms.CheckBox();
            this.skipBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startXBox
            // 
            this.startXBox.Location = new System.Drawing.Point(88, 16);
            this.startXBox.Name = "startXBox";
            this.startXBox.Size = new System.Drawing.Size(100, 20);
            this.startXBox.TabIndex = 0;
            // 
            // startYBox
            // 
            this.startYBox.Location = new System.Drawing.Point(210, 15);
            this.startYBox.Name = "startYBox";
            this.startYBox.Size = new System.Drawing.Size(100, 20);
            this.startYBox.TabIndex = 1;
            // 
            // endXBox
            // 
            this.endXBox.Location = new System.Drawing.Point(88, 56);
            this.endXBox.Name = "endXBox";
            this.endXBox.Size = new System.Drawing.Size(100, 20);
            this.endXBox.TabIndex = 2;
            // 
            // endYBox
            // 
            this.endYBox.Location = new System.Drawing.Point(198, 56);
            this.endYBox.Name = "endYBox";
            this.endYBox.Size = new System.Drawing.Size(100, 20);
            this.endYBox.TabIndex = 3;
            // 
            // startScaleBox
            // 
            this.startScaleBox.Location = new System.Drawing.Point(73, 101);
            this.startScaleBox.Name = "startScaleBox";
            this.startScaleBox.Size = new System.Drawing.Size(100, 20);
            this.startScaleBox.TabIndex = 4;
            // 
            // endScaleBox
            // 
            this.endScaleBox.Location = new System.Drawing.Point(245, 101);
            this.endScaleBox.Name = "endScaleBox";
            this.endScaleBox.Size = new System.Drawing.Size(100, 20);
            this.endScaleBox.TabIndex = 5;
            // 
            // timeBox
            // 
            this.timeBox.Location = new System.Drawing.Point(134, 142);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(100, 20);
            this.timeBox.TabIndex = 6;
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(29, 195);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(316, 53);
            this.messageBox.TabIndex = 7;
            // 
            // inputBox
            // 
            this.inputBox.AutoSize = true;
            this.inputBox.Location = new System.Drawing.Point(358, 15);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(90, 17);
            this.inputBox.TabIndex = 8;
            this.inputBox.Text = "Require Input";
            this.inputBox.UseVisualStyleBackColor = true;
            // 
            // skipBox
            // 
            this.skipBox.AutoSize = true;
            this.skipBox.Location = new System.Drawing.Point(358, 39);
            this.skipBox.Name = "skipBox";
            this.skipBox.Size = new System.Drawing.Size(75, 17);
            this.skipBox.TabIndex = 9;
            this.skipBox.Text = "Allow Skip";
            this.skipBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Start Point X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "End Point X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Start Scale";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "End Scale";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Time (in update cycles)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Message";
            // 
            // CinematicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 285);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skipBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.timeBox);
            this.Controls.Add(this.endScaleBox);
            this.Controls.Add(this.startScaleBox);
            this.Controls.Add(this.endYBox);
            this.Controls.Add(this.endXBox);
            this.Controls.Add(this.startYBox);
            this.Controls.Add(this.startXBox);
            this.Name = "CinematicForm";
            this.Text = "CinematicForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox startXBox;
        private System.Windows.Forms.TextBox startYBox;
        private System.Windows.Forms.TextBox endXBox;
        private System.Windows.Forms.TextBox endYBox;
        private System.Windows.Forms.TextBox startScaleBox;
        private System.Windows.Forms.TextBox endScaleBox;
        private System.Windows.Forms.TextBox timeBox;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.CheckBox inputBox;
        private System.Windows.Forms.CheckBox skipBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
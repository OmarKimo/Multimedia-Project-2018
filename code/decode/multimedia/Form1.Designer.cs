namespace multimedia
{
    partial class Form1
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
            this.unCompress = new System.Windows.Forms.Button();
            this.chooseFile = new System.Windows.Forms.Button();
            this.NameOfFile = new System.Windows.Forms.TextBox();
            this.check = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // unCompress
            // 
            this.unCompress.Location = new System.Drawing.Point(272, 31);
            this.unCompress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.unCompress.Name = "unCompress";
            this.unCompress.Size = new System.Drawing.Size(118, 78);
            this.unCompress.TabIndex = 1;
            this.unCompress.Text = "unCompress";
            this.unCompress.UseVisualStyleBackColor = true;
            this.unCompress.Click += new System.EventHandler(this.unCompress_Click);
            // 
            // chooseFile
            // 
            this.chooseFile.Location = new System.Drawing.Point(78, 24);
            this.chooseFile.Name = "chooseFile";
            this.chooseFile.Size = new System.Drawing.Size(92, 34);
            this.chooseFile.TabIndex = 2;
            this.chooseFile.Text = "Choose file";
            this.chooseFile.UseVisualStyleBackColor = true;
            this.chooseFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // NameOfFile
            // 
            this.NameOfFile.Location = new System.Drawing.Point(40, 75);
            this.NameOfFile.Multiline = true;
            this.NameOfFile.Name = "NameOfFile";
            this.NameOfFile.ReadOnly = true;
            this.NameOfFile.Size = new System.Drawing.Size(168, 34);
            this.NameOfFile.TabIndex = 3;
            // 
            // check
            // 
            this.check.Location = new System.Drawing.Point(199, 157);
            this.check.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(57, 44);
            this.check.TabIndex = 5;
            this.check.Text = "Check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 225);
            this.Controls.Add(this.check);
            this.Controls.Add(this.NameOfFile);
            this.Controls.Add(this.chooseFile);
            this.Controls.Add(this.unCompress);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Decoding";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button unCompress;
        private System.Windows.Forms.Button chooseFile;
        private System.Windows.Forms.TextBox NameOfFile;
        private System.Windows.Forms.Button check;
    }
}


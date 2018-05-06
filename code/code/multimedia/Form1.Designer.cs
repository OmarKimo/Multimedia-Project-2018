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
            this.Compress = new System.Windows.Forms.Button();
            this.chooseFile = new System.Windows.Forms.Button();
            this.NameOfFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Compress
            // 
            this.Compress.Location = new System.Drawing.Point(140, 122);
            this.Compress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Compress.Name = "Compress";
            this.Compress.Size = new System.Drawing.Size(118, 78);
            this.Compress.TabIndex = 0;
            this.Compress.Text = "Compress";
            this.Compress.UseVisualStyleBackColor = true;
            this.Compress.Click += new System.EventHandler(this.Compress_Click);
            // 
            // chooseFile
            // 
            this.chooseFile.Location = new System.Drawing.Point(45, 50);
            this.chooseFile.Name = "chooseFile";
            this.chooseFile.Size = new System.Drawing.Size(92, 34);
            this.chooseFile.TabIndex = 2;
            this.chooseFile.Text = "Choose file";
            this.chooseFile.UseVisualStyleBackColor = true;
            this.chooseFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // NameOfFile
            // 
            this.NameOfFile.Location = new System.Drawing.Point(159, 50);
            this.NameOfFile.Multiline = true;
            this.NameOfFile.Name = "NameOfFile";
            this.NameOfFile.ReadOnly = true;
            this.NameOfFile.Size = new System.Drawing.Size(168, 34);
            this.NameOfFile.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 225);
            this.Controls.Add(this.NameOfFile);
            this.Controls.Add(this.chooseFile);
            this.Controls.Add(this.Compress);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Encoding";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Compress;
        private System.Windows.Forms.Button chooseFile;
        private System.Windows.Forms.TextBox NameOfFile;
    }
}


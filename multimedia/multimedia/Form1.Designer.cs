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
            this.unCompress = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.NameOfFile = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Compress
            // 
            this.Compress.Location = new System.Drawing.Point(79, 115);
            this.Compress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Compress.Name = "Compress";
            this.Compress.Size = new System.Drawing.Size(157, 96);
            this.Compress.TabIndex = 0;
            this.Compress.Text = "Compress";
            this.Compress.UseVisualStyleBackColor = true;
            this.Compress.Click += new System.EventHandler(this.Compress_Click);
            // 
            // unCompress
            // 
            this.unCompress.Location = new System.Drawing.Point(384, 115);
            this.unCompress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.unCompress.Name = "unCompress";
            this.unCompress.Size = new System.Drawing.Size(157, 96);
            this.unCompress.TabIndex = 1;
            this.unCompress.Text = "unCompress";
            this.unCompress.UseVisualStyleBackColor = true;
            this.unCompress.Click += new System.EventHandler(this.unCompress_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 52);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "Choose file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NameOfFile
            // 
            this.NameOfFile.Location = new System.Drawing.Point(345, 52);
            this.NameOfFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NameOfFile.Multiline = true;
            this.NameOfFile.Name = "NameOfFile";
            this.NameOfFile.ReadOnly = true;
            this.NameOfFile.Size = new System.Drawing.Size(223, 41);
            this.NameOfFile.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 13);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 60);
            this.button2.TabIndex = 4;
            this.button2.Text = "test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // check
            // 
            this.check.Location = new System.Drawing.Point(79, 242);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(75, 23);
            this.check.TabIndex = 5;
            this.check.Text = "check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 277);
            this.Controls.Add(this.check);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.NameOfFile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.unCompress);
            this.Controls.Add(this.Compress);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "check";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Compress;
        private System.Windows.Forms.Button unCompress;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox NameOfFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button check;
    }
}


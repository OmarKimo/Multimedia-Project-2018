using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multimedia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.ShowDialog();
                od.InitialDirectory = Directory.GetCurrentDirectory();
                od.RestoreDirectory = true;
                string fileName = od.FileName;
                if (fileName == "")
                {
                    //MessageBox.Show("Choose a file to compress!");
                    return;
                }
                string fileNameWithoutPath = fileName.Split('\\').Last();
                FileStream fr = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fr);
                string text = sr.ReadToEnd();
                fr.Close();
                sr.Close();
                NameOfFile.Text = fileNameWithoutPath;

                string EncodedText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text));
                Process(EncodedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void Process(string text)
        {
            string uniqueChars = String.Join("",text.Distinct());
            MessageBox.Show(uniqueChars);
        }
    }
}

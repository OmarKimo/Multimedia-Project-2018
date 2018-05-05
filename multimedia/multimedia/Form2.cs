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
    public partial class Form2 : Form
    {
        private string fileNameWithPath1;
        private string fileNameWithPath2;
        private string fileNameWithoutPath1;
        private string fileNameWithoutPath2;

        public Form2()
        {
            InitializeComponent();
            fileNameWithPath1 = "";
            fileNameWithPath2 = "";
            fileNameWithoutPath1 = "";
            fileNameWithoutPath2 = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.ShowDialog();
                od.InitialDirectory = Directory.GetCurrentDirectory();
                od.RestoreDirectory = true;
                fileNameWithPath1 = od.FileName;
                fileNameWithoutPath1 = fileNameWithPath1.Split('\\').Last();
                textBox3.Text = fileNameWithoutPath1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.ShowDialog();
                od.InitialDirectory = Directory.GetCurrentDirectory();
                od.RestoreDirectory = true;
                fileNameWithPath2 = od.FileName;
                fileNameWithoutPath2 = fileNameWithPath2.Split('\\').Last();
                textBox2.Text = fileNameWithoutPath1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fileNameWithPath1 == "" || fileNameWithPath2 == "")
            {
                MessageBox.Show("Choose 2 proper files to check!");
                return;
            }


            FileStream fr1 = new FileStream(fileNameWithPath1, FileMode.Open, FileAccess.Read);
            StreamReader sr1 = new StreamReader(fr1, Encoding.UTF8);

            FileStream fr2 = new FileStream(fileNameWithPath2, FileMode.Open, FileAccess.Read);
            StreamReader sr2 = new StreamReader(fr2, Encoding.UTF8);

            string text1 = sr1.ReadToEnd();
            string text2 = sr2.ReadToEnd();

            if (text1.Length != text2.Length)
            {
                textBox1.ForeColor = Color.Red;
                textBox1.Text = "The files aren't identical!";
                return;
            }

            for (int i = 0; i < text1.Length; i++)
            {
                if (text1[i] != text2[i])
                {
                    textBox1.ForeColor = Color.Red;
                    textBox1.Text = "The files aren't identical!";
                    return;
                }
            }

            textBox1.ForeColor = Color.Green;
            textBox1.Text = "The files are identical.";

            sr1.Close();
            fr1.Close();
            sr2.Close();
            fr2.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

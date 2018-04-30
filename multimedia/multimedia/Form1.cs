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
        public IList<string> generalChars;
        public IList<int> cntChars;
        private string fileNameWithPath;
        private string fileNameWithoutPath;
        //public string tmpText;
        public Form1()
        {
            InitializeComponent();
            generalChars = new List<string>();
            cntChars = new List<int>();
            //tmpText = "";
            fileNameWithPath = "";
            fileNameWithoutPath = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.ShowDialog();
                od.InitialDirectory = Directory.GetCurrentDirectory();
                od.RestoreDirectory = true;
                fileNameWithPath = od.FileName;
                fileNameWithoutPath = fileNameWithPath.Split('\\').Last();
                NameOfFile.Text = fileNameWithoutPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] x ={"a","b","c","d","e","f"};
            int[] arr={12,3,400,200,1,30};
            //test huffman 
            Huffman.build(x, arr);
            IList<Node> list = Huffman.Gethuffman(); //for debug
            string y = Huffman.codeData("abcd"); //000100001101
            y = Huffman.DecodeData(y); //check if return same value abcd
            string z = Huffman.DecodeData("010000101");//bdb
            Huffman.extendhuffman();
            list = Huffman.Gethuffman();
            bool test = list[0] == null;
            //test arthmitic
            //arthmitc.Main(x, arr);
            //letter y=arthmitc.arthmitclist[0];
            //IList<Double> c=arthmitc.codeData("abc");
            //string f = arthmitc.decodeData(c[0], "c");
            //f=arthmitc.doubletobinary(arthmitc.Binarytodouble("01010001"));
            //double r=arthmitc.Binarytodouble("10010");
            //f = arthmitc.doubletobinary(0.5);
            
        }

        private void Compress_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileNameWithPath == "")
                {
                    MessageBox.Show("Choose a file to compress!");
                    return;
                }
                FileStream fr = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fr);
                string textToBeCompressed = sr.ReadToEnd();
                fr.Close();
                sr.Close();

                Process(textToBeCompressed);

                Huffman.build(generalChars,cntChars);
                IList<string> binarizedChars = Huffman.codeData(textToBeCompressed);

                FileStream file = new FileStream(fileNameWithoutPath.Split('.').First() + ".bin", FileMode.Create);
                BinaryWriter binaryFile = new BinaryWriter(file);
                foreach (string str in binarizedChars)
                {
                    binaryFile.Write(Convert.ToByte(str));
                }
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void Process(string text)
        {
            string EncodedText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text));
            string uniqueChars = String.Join("", EncodedText.Distinct());
            //tmpText += uniqueChars;
            StreamWriter of = new StreamWriter("testFile.txt"); //Just for test

            //Dictionary<char, int> dict = new Dictionary<char,int>();
            foreach (char ch in uniqueChars)
            {
                int count = text.Count(f => f == ch);
                cntChars.Add(count);
                generalChars.Add(ch.ToString());
                of.Write(ch.ToString() + "\n");
            }
            of.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < 20; i++)
            {
                button1.PerformClick();
            }
            tmpText = String.Join("", tmpText.Distinct());
            StreamWriter of = new StreamWriter("lzw Dictionary.txt"); //Just for test

            //Dictionary<char, int> dict = new Dictionary<char,int>();
            foreach (char ch in tmpText)
            {
                of.Write(ch.ToString() + "\n");
            }
            of.Close();*/
        }
    }
}

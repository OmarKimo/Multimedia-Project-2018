﻿using System;
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
        private string fileNameWithPath;
        private string fileNameWithoutPath;
        private string EncodedText;
        private IList<string> paths;
        private Dictionary<char, int> allCharsDict;
        private string uniqueCharSet;
        public Form1()
        {
            InitializeComponent();
            generalChars = new List<string>();
            fileNameWithPath = "";
            fileNameWithoutPath = "";
            paths = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                //paths.Add("D:\\Major & Interests\\Github Repositories & My Projects\\Multimedia-Project-2018\\DataSet\\DataSet_" + (i + 1).ToString() + ".tsv");
                //paths.Add("C:\\Multimedia-Project-2018\\DataSet\\DataSet_" + (i + 1).ToString() + ".tsv");
                //paths.Add("D:\\Newfolder\\Multimedia-Project-2018\\DataSet\\DataSet_" + (i + 1).ToString() + ".tsv");
                paths.Add("D:\\Computer department\\cairo university\\Assembly game\\Multimedia-Project-2018\\DataSet\\DataSet_" + (i + 1).ToString() + ".tsv");

            }
            allCharsDict = new Dictionary<char, int>();
            uniqueCharSet = "";
            init(allCharsDict);
            EncodedText = "";
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
        }

        private void init(Dictionary<char, int> chars)
        {
            FileStream file = new FileStream("all Unique Chars.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file, Encoding.UTF8);

            uniqueCharSet = sr.ReadToEnd();

            foreach (char ch in uniqueCharSet)
            {
                allCharsDict.Add(ch, 0);
            }

            sr.Close();
            file.Close();
        }

        private void Compress_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileNameWithPath == "" || fileNameWithPath.Split('.').Last() == "bin")
                {
                    MessageBox.Show("Choose a file to compress!");
                    return;
                }

                FileStream fr = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fr, Encoding.UTF8);

                string textToBeCompressed = sr.ReadToEnd();
                sr.Close();
                fr.Close();

                Process(textToBeCompressed);

                lzw.Main(allCharsDict.Keys.ToList());
                IList<int> binarized = lzw.Coding(textToBeCompressed);
                IList<char> binarizedChars = lzw.convertbinary(binarized);

                #region comment
                //Huffman.build(allCharsDict);
                //string binarizedChars = Huffman.codeData(textToBeCompressed);

                //binarizedChars = runlength.main(binarizedChars);
                //binarizedChars= optmize.main(binarizedChars);

                //arthmitc.Main(allCharsDict.Keys.ToList(), allCharsDict.Values.ToList());
                //string binarizedChars = arthmitc.buildbinary(textToBeCompressed, allCharsDict.Values.ToList());
                #endregion

                FileStream file = new FileStream(fileNameWithPath.Split('.').First() + ".bin", FileMode.Create);
                BinaryWriter binaryFile = new BinaryWriter(file, Encoding.UTF8);

                string s = "";
                for (int i = 1; i <= binarizedChars.Count; i++)
                {
                    s += binarizedChars[i - 1];
                    if (i % 8 == 0)
                    {
                        binaryFile.Write(Convert.ToByte(s, 2));
                        s = "";
                    }
                }
                if (s != "")
                {
                    binaryFile.Write(Convert.ToByte(s, 2));
                    s = "";
                }

                file.Close();
                binaryFile.Close();
                MessageBox.Show("Compression is done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }

        private void Process(string text)
        {
            EncodedText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(text));
            string uniqueChars = String.Join("", EncodedText.Distinct());

            foreach (char ch in uniqueChars)
            {
                int count = text.Count(f => f == ch);
                allCharsDict[ch] = count;
            }
        }
    }
}
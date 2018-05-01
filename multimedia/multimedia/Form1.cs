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
        private string fileNameWithPath;
        private string fileNameWithoutPath;
        private string EncodedText;
        private string tmpText;
        private IList<string> paths;
        private Dictionary<string, int> allCharsDict;
        public Form1()
        {
            InitializeComponent();
            generalChars = new List<string>();
            tmpText = "";
            fileNameWithPath = "";
            fileNameWithoutPath = "";
            paths = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                paths.Add("D:\\Major & Interests\\Github Repositories & My Projects\\Multimedia-Project-2018\\DataSet\\DataSet_" + (i + 1).ToString() + ".tsv");
            }
            allCharsDict = new Dictionary<string, int>();
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
            //string[] x ={"a","b","c","d","e","f"};
            //int[] arr={12,3,400,200,1,30};
            ////test huffman 
            //Huffman.build(x, arr);
            //IList<Node> list = Huffman.Gethuffman(); //for debug
            //string y = Huffman.codeData("abcd"); //000100001101
            //y = "001110000000000000010000101010101010010101010100101010101010101010101010101010111110101011101001010101";
            //y = Huffman.DecodeData(y,generalChars); //check if return same value abcd
            ////string z = Huffman.DecodeData("010000101",);//bdb
            //Huffman.extendhuffman();
            //list = Huffman.Gethuffman();
            //bool test = list[0] == null;
            //test arthmitic
            //arthmitc.Main(x, arr);
            //letter y=arthmitc.arthmitclist[0];
            //IList<Double> c=arthmitc.codeData("abc");
            //string f = arthmitc.decodeData(c[0], "c");
            //f=arthmitc.doubletobinary(arthmitc.Binarytodouble("01010001"));
            //double r=arthmitc.Binarytodouble("10010");
            //f = arthmitc.doubletobinary(0.5);
           
        }

        private void init(Dictionary<string, int> chars)
        {

            string txt = "";
            for (int i = 0; i < 20; i++)
            {
                FileStream fr = new FileStream(paths[i], FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fr);
                txt += sr.ReadToEnd();
                txt = String.Join("", txt.Distinct());
                sr.Close();
                fr.Close();
            }
            FileStream file = new FileStream("all Unique Chars.txt", FileMode.Create);
            StreamWriter of = new StreamWriter(file);
            of.Write(txt);
            foreach (char ch in txt)
            {
                allCharsDict.Add(ch.ToString(), 0);
            }
            of.Close();
            file.Close();
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
                sr.Close();
                fr.Close();

                Process(textToBeCompressed);

                //Huffman.build(allCharsDict);
                //Huffman.extendhuffman();
                //string binarizedChars = Huffman.codeData(EncodedText);

                //lzw.Main(allCharsDict.Keys.ToList());
                //IList<int> binarized = lzw.Coding(EncodedText);
                //string binarizedChars = lzw.convertbinary(binarized);

                arthmitc.Main(allCharsDict.Keys.ToList(), allCharsDict.Values.ToList());
                string binarizedChars = arthmitc.buildbinary(EncodedText, allCharsDict.Values.ToList());

                byte[] bytesFile = GetBytes(binarizedChars);

                FileStream file = new FileStream(fileNameWithPath.Split('.').First() + ".bin", FileMode.Create);
                BinaryWriter binaryFile = new BinaryWriter(file);
                foreach (byte by in bytesFile)
                {
                    binaryFile.Write(by);
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
            tmpText += uniqueChars;
            //StreamWriter of = new StreamWriter("testFile.txt"); //Just for test

            Dictionary<char, int> dict = new Dictionary<char,int>();
            foreach (char ch in uniqueChars)
            {
                int count = text.Count(f => f == ch);
                allCharsDict[ch.ToString()] = count;
                //generalChars.Add(ch.ToString());
                /*if (dict.ContainsKey(ch))
                {
                    dict[ch] += count;
                }
                else
                {
                    dict[ch] = count;
                }
                mxCnt += (dict[ch] > mxCnt) ? dict[ch] - mxCnt : 0;*/
                //of.Write(ch.ToString() + "\n");
            }
            //of.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < 20; i++)
            {
                FileStream fr = new FileStream(paths[i], FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fr);
                string textToBeCompressed = sr.ReadToEnd();
                fr.Close();
                sr.Close();

                Process(textToBeCompressed);
            }
            MessageBox.Show(mxCnt.ToString());
            tmpText = String.Join("", tmpText.Distinct());
            StreamWriter of = new StreamWriter("lzw Dictionary.txt"); //Just for test

            //Dictionary<char, int> dict = new Dictionary<char,int>();
            foreach (char ch in tmpText)
            {
                of.Write(ch.ToString() + "\n");
            }
            of.Close();*/
        }

        private byte[] GetBytes(string bitString)
        {
            return Enumerable.Range(0, bitString.Length / 8).
                Select(pos => Convert.ToByte(
                    bitString.Substring(pos * 8, 8),
                    2)
                ).ToArray();
        }

        private void unCompress_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileNameWithPath == "" || fileNameWithPath.Split('.').Last() != "bin")
                {
                    MessageBox.Show("Choose a proper binary file (.bin) to uncompress!");
                    return;
                }

                FileStream fr = new FileStream(fileNameWithPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fr);
                byte[] binText = br.ReadBytes(Convert.ToInt32(fr.Length));
                string textToBeUnCompressed = Convert.ToBase64String(binText);
                br.Close();
                fr.Close();

                //string DecodedText = Huffman.DecodeData(textToBeUnCompressed,generalChars);

                arthmitc.Main(allCharsDict.Keys.ToList(), allCharsDict.Values.ToList());
                string DecodedText = arthmitc.buildstring(textToBeUnCompressed, allCharsDict.Keys.ToList());

                FileStream file = new FileStream(fileNameWithPath.Split('.').First() + "_1.txt", FileMode.Create);
                StreamWriter DecodedFile = new StreamWriter(file);
                DecodedFile.Write(DecodedText);

                DecodedFile.Close();
                file.Close();
                MessageBox.Show("Uncompression is done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!!" + ex);
            }
        }
    }
}

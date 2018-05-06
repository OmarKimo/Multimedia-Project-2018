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
        private string fileNameWithPath;
        private string fileNameWithoutPath;
        private IList<string> paths;
        private Dictionary<char, int> allCharsDict;
        public Form1()
        {
            InitializeComponent();
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
            init(allCharsDict);
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
                allCharsDict.Add(ch, 0);
            }
            of.Close();
            file.Close();
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
                BinaryReader br = new BinaryReader(fr,Encoding.UTF8);
                IList<byte> binText = new List<byte>();
                while(true){
                    try
                    {
                        binText.Add(br.ReadByte());
                    }
                    catch (Exception ex)
                    {
                        br.Close();
                        fr.Close();
                        break;
                    }
                }
                IList<char> Text = new List<char>();
                for (int i = 0; i < binText.Count; i++)
                {
                    string s = Convert.ToString(binText[i],2);
                    string add = "";
                    for (int j = 0; j < s.Length; j++)
                    {
                        add += s[j];
                    }
                    for (int j = 0; j < 8 - s.Length; j++)
                    {
                        Text.Add('0');
                    }
                    for (int j = 0; j <add.Length; j++)
                    {
                        Text.Add(add[j]);
                    }
                }

                lzw.Main(allCharsDict.Keys.ToList());
                string DecodedText = lzw.deCoding(lzw.convertint(Text));
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
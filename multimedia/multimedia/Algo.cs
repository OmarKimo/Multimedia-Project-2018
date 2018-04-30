using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace multimedia
{
    class Node
    {
        public int number;
        public string data;
        public string code;
        public Node leftChild, rightChild;

        public Node(string data,int number)
        {
            this.data = data;
            this.number = number;
            this.code = "";
            this.leftChild = null;
            this.rightChild = null;

        }

        public Node(Node leftChild, Node rightChild)
        {
            this.leftChild = leftChild;
            this.rightChild = rightChild;
            this.data = leftChild.data + rightChild.data; //for debug >>can remove it
            this.number = leftChild.number + rightChild.number;
        }
    }

    class Huffman
    {
        static public int numberofextend;
        static public int codelength;
        static private IList<Node> huffmanlist; //include all letter with thier code
        static private IList<int> dict;
        static public IList<Node> Gethuffman() //tested
        {//get copy of list
            IList<Node> newlist = new List<Node>();

            for (int i = 0; i < huffmanlist.Count; i++)
            {
                Node tempNode = huffmanlist[i];
                newlist.Add(tempNode);
            } 
            return newlist;
        }
        static public void build(IList<string> input, IList<int> array) //tested
        {//give them array of string & array of number of each string
            IList<Node> list = new List<Node>();
            numberofextend = 0;
            dict = new List<int>();
            huffmanlist = new List<Node>();
            for (int i = 0; i < array.Count; i++)
            {
                dict.Add(array[i]);
                list.Add(new Node(input[i], array[i]));
            }

            //build the tree
            Stack<Node> stack = GetSortedStack(list);
            while (stack.Count > 1)
            {
                //move last 2 least number
                Node leftChild = stack.Pop();
                Node rightChild = stack.Pop();
                //repalce them with the sum of them 
                Node parentNode = new Node(leftChild, rightChild);
                stack.Push(parentNode);
                //sort again
                stack = GetSortedStack(stack.ToList<Node>());
            }

            Node parentNode1 = stack.Pop();

            GenerateCode(parentNode1);

        }

        public static Stack<Node> GetSortedStack(IList<Node> list) //tested
        {//sort thd probability
            //sort the nodes
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i+1; j < list.Count; j++)
                {
                    if (list[i].number < list[j].number)
                    {
                        Node tempNode = list[j];
                        list[j] = list[i];
                        list[i] = tempNode;
                    }
                }
            }
            //make new array for store the new value 
            Stack<Node> stack = new Stack<Node>();
            for (int j = 0; j < list.Count; j++)
                stack.Push(list[j]);
            return stack;
        }

        public static void GenerateCode(Node parentNode) //tested
        { //after build tree , apply this function on the parent node to get the code for each symple
            if (parentNode == null)
                return;
            else if (parentNode.leftChild == null)
            {
                huffmanlist.Add(parentNode);
            }
            else
            {
                parentNode.leftChild.code = parentNode.code + "0";
                parentNode.rightChild.code = parentNode.code + "1";
                GenerateCode(parentNode.leftChild);
                GenerateCode(parentNode.rightChild);
            }
            
        }

        public static long callength() //if this more than 7*length of code bad compresstion 
        { //this number will increase if the gab between the 7 first number & another letter decrease
            long res=24;
            res+=(18*dict.Count());
            for(int i=0;i<huffmanlist.Count;i++)
            {
                res += (huffmanlist[i].number * huffmanlist[i].code.Length);
            }
            return res;
        }

        public static string codeData(string input) //tested
        {
            //given data ,output code
            string res = "";
            int y = input.Length;
            for (int i = 20; i > -1; i--)
            {
                if ((i << 1) < y)
                {
                    res += '1';
                    y -= (i << 1);
                }
                else 
                {
                    res += '0';
                }
            }
            for (int i = 0; i < dict.Count; i++)
            {
                int x = dict[i];
                for (int j = 17; j > -1; j--)
                {
                    if ((j<< 1) < y)
                    {
                        res += '1';
                        x -= (j<< 1);
                    }
                    else
                    {
                        res += '0';
                    }
                }
            }
            
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < huffmanlist.Count; j++)
                {
                    if (input[i] == huffmanlist[j].data[0])
                    {
                        res+=huffmanlist[j].code;
                        break;
                    }
                }
            }
            return res;
        }

        public static string DecodeData(string input,IList<string> GeneralDict) //tested
        {//given code ,output data
            int x=0,length=0;
            for (int i = 0; i < 21; i++)
            {
                length += ((21 - i) << (input[x] - '0'));
                x++;
            }
            IList<int> mynum = new List<int>();
            for (int i = 0; i < GeneralDict.Count; i++)
            {
                int y = 0;
                for (int j = 0; j < 18; j++)
                {
                    y += ((17 - i) << (input[x] - '0'));
                    x++;
                }
                mynum.Add(y);
            }
            Huffman.build(GeneralDict, mynum);
            string res = "";
            string curr = "";
            while(res.Length<length )
            {
                if (x > input.Length)
                    return res;
                //add current code
                curr += input[x++];
                //search on current code on the list 
                for (int j = 0; j < huffmanlist.Count; j++)
                {
                    if (curr == huffmanlist[j].code) //if found then add data to result and search on the next symple 
                    {
                        res += huffmanlist[j].data;
                        curr = "";
                        break;
                    }
                }

            }
            return res;
        }

        public static void extendhuffman()//extend huffman code 
        {
            if (numberofextend == null)
                return;
            numberofextend++;
            IList<Node> newlist = new List<Node>();
            //build new list
            for (int i = 0; i < huffmanlist.Count; i++)
            {
                for (int j = i; j < huffmanlist.Count; j++)
                {
                    newlist.Add(new Node(huffmanlist[i].data + huffmanlist[j].data, huffmanlist[i].number * huffmanlist[j].number));
                }
            }


            //build the tree
            Stack<Node> stack = GetSortedStack(newlist);
            while (stack.Count > 1)
            {
                //move last 2 least number
                Node leftChild = stack.Pop();
                Node rightChild = stack.Pop();
                //repalce them with the sum of them 
                Node parentNode = new Node(leftChild, rightChild);
                stack.Push(parentNode);
                //sort again
                stack = GetSortedStack(stack.ToList<Node>());
            }

            Node parentNode1 = stack.Pop();
            huffmanlist = new List<Node>();
            GenerateCode(parentNode1);


            //for debug
            //for (int i = 0; i < huffmanlist.Count; i++)
            //{
            //    for (int j = i + 1; j < huffmanlist.Count; j++)
            //    {
            //        if (huffmanlist[i].number < huffmanlist[j].number)
            //        {
            //            Node tempNode = huffmanlist[j];
            //            huffmanlist[j] = huffmanlist[i];
            //            huffmanlist[i] = tempNode;
            //        }
            //    }
            //}

        }
    }

    class letter
    {
        public double upper;
        public double lower;
        public string data;
        public letter(string data,double up,double low)
        {
            this.data = data;
            this.upper = up;
            this.lower = low;

        }
    }

    class arthmitc
    {
        static public IList<letter> arthmitclist;
        public static void Main(string[] input, int[] array) //given array of string & array of number of each string
        {
            arthmitclist = new List<letter>();
            long sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            double curr = 0.0;
            for (int i = 0; i < array.Length; i++)
            {
                arthmitclist.Add(new letter(input[i] ,(curr + array[i]) / sum, curr / sum));
                curr += array[i];

            }
        }
        public static IList<double> codeData(string input) //given data ,output code
        {
            IList<double> list = new List<double>();
            IList<letter> artlist = new List<letter>();
            for (int i = 0; i < arthmitclist.Count; i++)
            { 
                artlist.Add(new letter(arthmitclist[i].data,arthmitclist[i].upper,arthmitclist[i].lower));
            }
            double up=1, down=0;
            string end = input[input.Length - 1]+""; //need to change
            string curr = "";
            for (int i = 0; i < input.Length; i++)
            {
                curr += input[i];
                for (int j = 0; j < artlist.Count; j++)
                {
                    if (artlist[j].data == curr)
                    {
                        curr = "";
                        up = artlist[j].upper;
                        down = artlist[j].lower;
                        double ratio = up - down;
                        if ((artlist[j].data+"")==end) //end of coding  need to change
                        {
                            list.Add((up + down) / 2.0);
                            up = 1;
                            down = 0;
                            artlist.Clear();
                            for (int k = 0; k < arthmitclist.Count; k++)
                            {
                                artlist.Add(new letter(arthmitclist[k].data, arthmitclist[k].upper, arthmitclist[k].lower));
                            }
                            break;
                        }
                        for (int k = 0; k < arthmitclist.Count; k++)
                        {
                            artlist[k].lower = arthmitclist[k].lower * ratio + down;
                            artlist[k].upper = arthmitclist[k].upper * ratio + down;
                        }
                        break;
                    }
                }
            }
            return list;
        }

        public static string decodeData(double input,string end) //given double & the last symple in the text ,output data  for each double
        {
            if (input == null || end == null)
                return null;

            string res = "";
            IList<letter> artlist = new List<letter>();
            for (int i = 0; i < arthmitclist.Count; i++)
            {
                artlist.Add(new letter(arthmitclist[i].data, arthmitclist[i].upper, arthmitclist[i].lower));
            }
  
            while(true)
            {

                for (int j = 0; j < artlist.Count; j++)
                {
                    if (input < artlist[j].upper && input > artlist[j].lower)
                    {
                        res += artlist[j].data;
                        if (artlist[j].data == end) //end of coding  need to change 
                        {
                            return res;
                        }
                        double up = artlist[j].upper;
                        double down = artlist[j].lower;
                        double ratio = up - down;
                        for (int k = 0; k < arthmitclist.Count; k++)
                        {
                            artlist[k].lower = arthmitclist[k].lower * ratio + down;
                            artlist[k].upper = arthmitclist[k].upper * ratio + down;
                        }
                    }
                }
            }
        }

        public static string  doubletobinary(double x)
        {
            long m = BitConverter.DoubleToInt64Bits(x); 
            string str = Convert.ToString(m, 2); 
            return str;
        }

        public static double Binarytodouble(string str)
        {
            long n = Convert.ToInt64(str, 2); 
            double x = BitConverter.Int64BitsToDouble(n); 
            return x;
        }

    }


    class lzw
    {
        static public IList<string> LetterDict;
        //static private int Max; handle in input

        static void Main(IList<string> input) //give them array of string & array of number of each string
        {
            //fill letterdict with all letter in the data set
            LetterDict = new List<string>();
            for (int i = 0; i < input.Count; i++)
            {
                LetterDict.Add(input[i]);
            }
            
        }



        static IList<int> Coding(string input) //given string , output code
        {
            IList<int> mylist=new List<int>();
            IList<string> Dict=new List<string>();
            for (int i = 0; i < LetterDict.Count; i++)
            {
                Dict.Add(LetterDict[i]);
            }
            string curr="";
            int last = 0;
            for (int i = 0; i < input.Length; i++)
            {
                bool test = true;
                curr += input[i];
                for (int j = 0; j < Dict.Count; j++)
                {
                    if (curr == Dict[j])
                    {
                        last = j;
                        test = false;
                        break;
                    }
                }
                if (test) 
                {
                    mylist.Add(last);
                    Dict.Add(curr);
                    string temp = "";
                    for (int j = 1; j < curr.Length; j++)
                        temp += curr[j];
                }
                
            }
            return mylist;
        }
        static string convertbinary(IList<int> input) //convert int list to binary code
        { 
            string res="";
            for (int i = 0; i < input.Count; i++)
            {

                for (int j = 31; j >-1;j--)
                {
                    if ((input[i]&(i<<1))!=0)
                        res+='1';
                    else
                        res+='0';
                }
            }

            return res;
        }

        static string deCoding(int[] input) //given code , output string
        {
            string res = "";
            IList<string> Dict = new List<string>();
            for (int i = 0; i < LetterDict.Count; i++)
            {
                string x = "" + LetterDict[i];
                Dict.Add(x);
            }
            string last = Dict[input[0]];
            res+=last;
            for (int i = 1; i < input.Length; i++)
            {
                res += Dict[input[i]];
                Dict.Add(last + Dict[input[i]][0]);
                last = Dict[input[i]];
            }
            return res;
        }

        static IList<int> convertint(string input) //convert code binary to int
        {
            IList<int> res = new List<int>();
            int curr = 0,test=0;
            for (int i = 0; i < input.Length; i++)
            {
                if (test == 32)
                {
                    test = 0;
                    res.Add(curr);
                    curr = 0;
                }
                if (input[i] == '1')
                    curr = curr | 1;
                curr = 1 << curr;
            }
            return res;
        }

    }




}

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

        public Node(string data, int number)
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
        static public int numberofextend; //no of extend huffman
        static private IList<Node> huffmanlist; //include all letter with thier code
        static private IList<int> dict;
        public static int len;
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
        static public void build(Dictionary<string, int> input/*IList<string> input, IList<int> array*/) //tested
        {//give them array of string & array of number of each string
            IList<Node> list = new List<Node>();
            numberofextend = 0;
            dict = new List<int>();
            huffmanlist = new List<Node>();
            for (int i = 0; i < input.Count; i++)
            {
                int value = input.ToList()[i].Value; //array[i]
                string key = input.ToList()[i].Key; //input[i]
                dict.Add(value);
                if (value != 0)
                    list.Add(new Node(key, value));
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
            if (stack.Count == 1)
            {
                Node parentNode1 = stack.Pop();
                GenerateCode(parentNode1);
            }
            huffmanlist = huffmanlist.OrderByDescending(o => o.number).ToList();
        }

        public static Stack<Node> GetSortedStack(IList<Node> list) //sort thd probability
        {
            //sort the nodes
            List<Node> SortedList = list.OrderByDescending(o => o.number).ToList();
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

        public static long callength() //must be less than the original size
        { //this number will increase if the gab between the no of rebeat letter decrease
            long res = (18 * dict.Count());//size of dict
            for (int i = 0; i < huffmanlist.Count; i++)
            {
                res += (huffmanlist[i].number * huffmanlist[i].code.Length);
            }
            return res;
        }

        public static IList<char> codeData(IList<char> input)  //given data ,output code
        {
            IList<char> res = new List<char>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < huffmanlist.Count; i++)
                dict.Add(huffmanlist[i].data, huffmanlist[i].code);
            string curr = "";
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (curr.Length == len)
                {
                    string newres = dict[curr];
                    for (int k = 0; k < newres.Length; k++)
                        res.Add(newres[k]);
                    curr = "";
                }
            }
            return res;
        }

        public static IList<char> DecodeData(IList<char> input) //given code ,output data
        {
            int x = 0;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < huffmanlist.Count; i++)
                dict.Add(huffmanlist[i].code, huffmanlist[i].data);
            IList<char> res = new List<char>();
            string curr = "",newres="";
            while (x >= input.Count)
            {
                //add current code
                curr += input[x++];
                //search on current code on the list 
                if (dict.TryGetValue(curr, out newres))
                {
                    for (int k = 0; k < newres.Length; k++)
                        res.Add(newres[k]);
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
            //huffmanlist = huffmanlist.OrderByDescending(o => o.number).ToList();

        }
    }


    class lzw
    {
        public static IList<char> LetterDict;
        public static int mymax = 1<<20; //65536 16 // 1048576 20
        public static int maxbit = 20;

        public static void Main(IList<char> input) //give them array of char to initil dict
        {
            //fill letterdict with all letter in the data set
            LetterDict = new List<char>();
            for (int i = 0; i < input.Count; i++)
            {
                LetterDict.Add(input[i]);
            }

        }

        public static IList<int> Coding(string input) //given string , output code
        {

            mymax = 1 << maxbit;
            IList<int> mylist = new List<int>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int index = 0, last = 0, x = 1, slast = 0;
            while (index < LetterDict.Count)
                dict.Add(LetterDict[index].ToString(), index++);
            string curr = input[0] + "";
            while (curr.Length > 0)
            {
                while (true)
                {
                    if (dict.ContainsKey(curr))
                    {
                        last = dict[curr];
                        slast = curr.Length;
                    }
                    else
                        break;
                    if (x < input.Length)
                        curr += input[x++];
                    else
                        break;
                }
                mylist.Add(last);
                if (index < mymax && !(dict.ContainsKey(curr))) //max for bit
                    dict.Add(curr, index++);
                curr = curr.Remove(0, slast);
            }

            return mylist;

        }


        public static string deCoding(IList<int> input) //given code , output string
        {
            string res = "";
            IList<string> Dict = new List<string>();
            for (int i = 0; i < LetterDict.Count; i++)
                Dict.Add(LetterDict[i].ToString());
            string last = Dict[input[0]];

            for (int i = 1; i < input.Count; i++)
            {
                res += last;
                if (input[i] == Dict.Count)
                    Dict.Add(last + last[0]);
                else
                    Dict.Add(last + Dict[input[i]][0]);

                last = Dict[input[i]];
            }
            res += last;
            int mysize = res.Length;
            return res;
        }




        public static IList<char> convertbinary(IList<int> input) //convert int list to binary code
        {
            IList<char> res = new List<char>();
            int maxcurr = Convert.ToString(LetterDict.Count, 2).Length;
            int x = LetterDict.Count;
            for (int i = 0; i < input.Count; i++)
            {
                string binary = Convert.ToString(input[i], 2).PadLeft(maxcurr, '0');
                for (int j = 0; j < binary.Length; j++)
                    res.Add(binary[j]);
                maxcurr = Convert.ToString(++x, 2).Length < maxbit ? Convert.ToString(x, 2).Length : maxbit;
            }
            while (res.Count % 8 != 0)
                res.Add('0');
            return res;
        }

        public static IList<int> convertint(IList<char> input) //convert code binary to int
        {
            IList<int> res = new List<int>();
            int maxcurr = Convert.ToString(LetterDict.Count, 2).Length;
            int x = LetterDict.Count;
            int curr = 0;
            string mynum = "";
            for (; curr < input.Count; curr++)
            {
                mynum += input[curr];
                if (mynum.Length == maxcurr)
                {
                    res.Add(Convert.ToInt32(mynum, 2));
                    mynum = "";
                    maxcurr = Convert.ToString(++x, 2).Length < maxbit ? Convert.ToString(x, 2).Length : maxbit;
                }
                
            }
            return res;
        }

    }




    class letter
    {
        public double upper;
        public double lower;
        public string data;
        public letter(string data, double up, double low)
        {
            this.data = data;
            this.upper = up;
            this.lower = low;

        }
    }

    class arthmitc
    {
        static public IList<letter> arthmitclist;
        static public int number;

        public static void Main(IList<string> input, IList<int> array) //given array of string & array of number of each string
        {
            arthmitclist = new List<letter>();
            int sum = 0;
            for (int i = 0; i < array.Count; i++)
                sum += array[i];
            number = sum;
            double curr = 0.0;
            for (int i = 0; i < array.Count; i++)
            {
                arthmitclist.Add(new letter(input[i], (curr + array[i]) / sum, curr / sum));
                curr += array[i];

            }
        }

        public static IList<char> buildbinary(IList<char> input)
        {
            IList<char> res = new List<char>();
            IList<Double> x = new List<Double>();
            x = codeData(input);
            for (int i = 0; i < x.Count; i++)
            {
                string curr = arthmitc.doubletobinary(x[i]);
                for(int j=0;j<curr.Length;j++)
                    res.Add(curr[j]);
            }
            return res;
        }

        public static IList<char> buildstring(IList<char> input)
        {
            IList<char> res =new List<char>();
            int x = 0;
            while (x < input.Count)
            {
                string curr = "";
                for (int i = 0; i < 64; i++)
                    curr += input[x++];
                string s=arthmitc.decodeData(arthmitc.Binarytodouble(curr));
                for (int j = 0; j < s.Length; j++)
                    res.Add(s[j]);
            }
            return res;
        }

        public static IList<double> codeData(IList<char> input) //given data ,output code
        {
            IList<double> list = new List<double>();
            IList<letter> mylist = new List<letter>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i < arthmitclist.Count; i++)
            {
               mylist.Add( new letter(arthmitclist[i].data, arthmitclist[i].upper, arthmitclist[i].lower));
                dict.Add(mylist[i].data, i);
            }
            double up = 1, down = 0;
            string curr = "";
            int x = 0,last=0;
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (dict.TryGetValue(curr, out last))
                {
                        curr = "";
                        up = mylist[last].upper;
                        down = mylist[last].lower;
                        double ratio = up - down;
                        if (x++ >= 8) //end of coding  need to change
                        {
                            list.Add((up + down) / 2.0);
                            up = 1;
                            down = 0;
                            mylist.Clear();
                            for (int k = 0; k < arthmitclist.Count; k++)
                                mylist.Add(new letter(arthmitclist[k].data, arthmitclist[k].upper, arthmitclist[k].lower));
                        }
                        for (int k = 0; k < mylist.Count; k++)
                        {
                            mylist[k].lower = arthmitclist[k].lower * ratio + down;
                            mylist[k].upper = arthmitclist[k].upper * ratio + down;
                        }
                    }
            }
            return list;
        }

        public static string decodeData(double input) //given double & the last symple in the text ,output data  for each double
        {
            if (input == null /*|| end == null*/)
                return null;

            string res = "";
            IList<letter> artlist = new List<letter>();
            for (int i = 0; i < arthmitclist.Count; i++)
            {
                artlist.Add(new letter(arthmitclist[i].data, arthmitclist[i].upper, arthmitclist[i].lower));
            }
            int x = 0;
            while (true)
            {

                for (int j = 0; j < artlist.Count; j++)
                {
                    if (input < artlist[j].upper && input > artlist[j].lower)
                    {
                        res += artlist[j].data;
                        double up = artlist[j].upper;
                        double down = artlist[j].lower;
                        double ratio = up - down;
                        if (x++ >=8)  //end of coding  need to change 
                            return res;
                        for (int k = 0; k < arthmitclist.Count; k++)
                        {
                            artlist[k].lower = arthmitclist[k].lower * ratio + down;
                            artlist[k].upper = arthmitclist[k].upper * ratio + down;
                        }
                    }
                }
            }
        }

        public static string doubletobinary(double x)
        {
            if (x < 0)
                return null;
            long m = BitConverter.DoubleToInt64Bits(x);
            string str = Convert.ToString(m, 2);
            return str;
        }

        public static double Binarytodouble(string str)
        {

            long n = Convert.ToInt64(str, 2);
            double x = BitConverter.Int64BitsToDouble(n);
            return Math.Abs(x);
        }

    }


    class runlength
    {
        public static int maxbit = 3;

        public static IList<char> main(IList<char> input)
        {
            IList<char> res = new List<char>();
            bool test = false;
            int curr = 0,num=0;
            for (int i = 0; i < input.Count; i++)
            {
                if (test)
                {
                    if (input[i] == '1')
                        num++;
                    if (++curr == (1<<maxbit)-1|| input[i]!='1')
                    {
                        string str = Convert.ToString(num,2).PadLeft(maxbit, '0');
                        for (int j = 0; j < maxbit; j++)
                            res.Add(str[j]);
                        test = !(test);
                        curr = 0;
                        num = 0;
                    }
                }
                else
                {
                    if (input[i] == '0')
                        num++;
                    if (++curr == (1 << maxbit) - 1 || input[i] != '0')
                    {
                        string str = Convert.ToString(num,2).PadLeft(maxbit, '0');
                        for (int j = 0; j < maxbit; j++)
                            res.Add(str[j]);
                        test = !(test);
                        curr = 0;
                        num = 0;
                    }
                }
 
            }
            return res;
 

        }


        public static IList<char> back(IList<char> input)
        {
            IList<char> res = new List<char>();
            bool test = false;
            int len = (1 << maxbit);
            string curr = "";
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (curr.Length == maxbit)
                {
                    int y = Convert.ToInt16(curr);
                    char s;
                    if (test)
                        s = '1';
                    else
                        s = '0';
                    for (int j = 0; j < len; j++)
                        res.Add(s);
                    test = !(test);
                    curr = "";
                }
            }
            return res;
        }

    }

    class optmize
    {
        static public IList<string> mychar;
        static public int length = 8;
        static public IList<char> main(IList<char> input)
        {
            IList<char> res = new List<char>();
            mychar = new List<string>();
            optmize g = new optmize();
            g.generate("", length);
            Dictionary<string, int> currlist = new Dictionary<string, int>();
            for (int i = 0; i < mychar.Count; i++)
                currlist.Add(mychar[i], 0);
            string curr = "";
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (curr.Length == length) 
                {
                    currlist[curr]++;
                    curr = "";
                }
            }
            Huffman.build(currlist);
            IList<int> dict = new List<int>();
            for (int i = 0; i < currlist.Count; i++)
            {
                int x = currlist.ToList()[i].Value;
                dict.Add(x);
                string binary = Convert.ToString(x, 2).PadLeft(20, '0');
                for (int j = 0; j < binary.Length; j++)
                    res.Add(binary[j]);
            }
            //long lengthhuffman = Huffman.callength();
            /*arthmitc.Main(mychar, dict);
            IList<char> newcurr = arthmitc.buildbinary(input);
            for (int i = 0; i < newcurr.Count; i++)
                res.Add(newcurr[i]);*/



            Huffman.len = length;
            IList<char> newres = Huffman.codeData(input);
            for (int i = 0; i < newres.Count; i++)
                res.Add(newres[i]);

            while (res.Count % 8 != 0)
                res.Add('0');
            return res;
        }



        static public IList<char> back(IList<char> input)
        {
            IList<char> res = new List<char>();
            mychar = new List<string>();
            optmize g = new optmize();
            g.generate("", length);
            Dictionary<string, int> currlist = new Dictionary<string, int>();

            IList<int> dict = new List<int>();
            int currx = 0;
            string mynum = "";
            for (; currx < length*20; currx++)
            {
                mynum += input[currx];
                if (mynum.Length == 20)
                {
                    dict.Add(Convert.ToInt32(mynum, 2));
                    mynum = "";
                }
            }
            arthmitc.Main(mychar, dict);
            res = arthmitc.buildstring(input);

            return res;
        }


        private void generate(string x, int y)
        {
            if (y == 0)
            {
                mychar.Add(x);
                return;
            }
            y--;
            generate(x + '0', y);
            generate(x + '1', y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace multimedia
{
    class lzw
    {
        //this class are implemnted for LZW algorithm 
        #region variable
        //base dictionary include all letter
        private static IList<char> LetterDict; 
        //max size for each integer
        private static int mymax = 1 << 20;
        //the max limit of our dictionary 
        private static int maxbit = 20;
        #endregion

        #region function
        //given array of char to initil dictionary
        public static void Main(IList<char> input) 
        {
            if (input == null)
                return;
            //fill letterdict with all letter in the data set
            LetterDict = new List<char>();
            for (int i = 0; i < input.Count; i++)
                LetterDict.Add(input[i]);

        }

        //given code , output the decompressed string
        public static string deCoding(IList<int> input) 
        {
            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (maxbit < 0 || maxbit > 32)
                maxbit = 20;
            if (LetterDict.Count <= 1)
                return null;
            #endregion

            #region initial all needed variable
            string res = "";//to store the result
            // intial current dictionary
            IList<string> Dict = new List<string>(); 
            for (int i = 0; i < LetterDict.Count; i++)
                Dict.Add(LetterDict[i].ToString());
            string last = Dict[input[0]];
            #endregion

            #region main algorithm
            for (int i = 1; i < input.Count; i++)
            {
                //add last block
                res += last;
                //add new block to dictionary
                if (input[i] == Dict.Count)
                    Dict.Add(last + last[0]);
                else
                    Dict.Add(last + Dict[input[i]][0]);

                last = Dict[input[i]];
            }
            //add last block
            res += last;
            return res;
            #endregion
        }

        //convert binary to int list
        public static IList<int> convertint(IList<char> input) 
        {
            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (maxbit < 0 || maxbit > 32)
                maxbit = 20;
            if (LetterDict.Count <= 1)
                return null;
            #endregion

            #region intial variable 
            IList<int> res = new List<int>(); // final result array of integer
            int maxcurr = Convert.ToString(LetterDict.Count, 2).Length;//size of integer
            int x = LetterDict.Count; //count for know max length for current integer
            string mynum = ""; //current string
            #endregion 

            #region main algorithm
            for (int curr = 0; curr < input.Count; curr++)
            {
                mynum += input[curr];
                if (mynum.Length == maxcurr)
                {
                    //add to fianl result
                    res.Add(Convert.ToInt32(mynum, 2)); 
                    mynum = "";
                    //update max length of integer
                    maxcurr = Convert.ToString(++x, 2).Length < maxbit ? Convert.ToString(x, 2).Length : maxbit;
                }

            }
            return res;
            #endregion 
        }
        #endregion
    }

    class Node
    {
        //for save probrities of symple at huffman coding
        #region variable
        public int number;     //number of times that symple appear in the text
        public string data;     //original symple 
        public string code;     //huffman code for this symple
        public Node leftChild, rightChild; //for build huffman tree
        #endregion

        #region function
        //intial build 
        public Node(string data, int number)
        {
            this.data = data;
            this.number = number;
            this.code = "";
            this.leftChild = null;
            this.rightChild = null;

        }

        //advance build
        public Node(Node leftChild, Node rightChild)
        {
            this.leftChild = leftChild;
            this.rightChild = rightChild;
            this.data = leftChild.data + rightChild.data; 
            this.number = leftChild.number + rightChild.number;
        }
        #endregion
    }

    class Huffman
    {
        #region variable
        static public int numberofextend; //number of extended huffman
        static private IList<Node> huffmanlist; //include all letter with thier code
        #endregion

        #region function

        //get copy of the list
        static public IList<Node> Gethuffman() 
        {
            if(huffmanlist==null)
                return null;
            IList<Node> newlist = new List<Node>();
            for (int i = 0; i < huffmanlist.Count; i++)
            {
                Node tempNode = huffmanlist[i];
                newlist.Add(tempNode);
            }
            return newlist;
        }

        //build huffman tree giveen array of string & array of number of each string
        static public void build(Dictionary<string, int> input) 
        {

            if (input == null|| input.Count<=1)
                return;

            #region intial variable 
            IList<Node> list = new List<Node>(); //list for build huffman tree 
            numberofextend = 0;
            huffmanlist = new List<Node>();
            for (int i = 0; i < input.Count; i++)
            {
                int value = input.ToList()[i].Value; //array[i]
                string key = input.ToList()[i].Key; //input[i]
                if (value != 0)
                    list.Add(new Node(key, value));
            }
            #endregion

            #region main algorithm 
            //build the huffman tree
            Stack<Node> stack = SortStack(list);
            while (stack.Count > 1)
            {
                //move last 2 least number (less number)
                Node leftChild = stack.Pop();
                Node rightChild = stack.Pop();
                //repalce them with the sum of them 
                Node parentNode = new Node(leftChild, rightChild);
                stack.Push(parentNode);
                //sort again
                stack = SortStack(stack.ToList<Node>());
            }
            if (stack.Count == 1)
                GenerateCode(stack.Pop()); //give all of them thier code
            #endregion
        }

        //sort the list Order By number
        public static Stack<Node> SortStack(IList<Node> list) 
        {
            if (list == null)
                return null;
            //sort the nodes
            List<Node> SortedList = list.OrderByDescending(o => o.number).ToList();
            //make new array for store the new value 
            Stack<Node> stack = new Stack<Node>();
            for (int j = 0; j < list.Count; j++)
                stack.Push(list[j]);
            return stack;
        }

        //after build tree , apply this function on the parent node to get the code for each childern
        public static void GenerateCode(Node parentNode) 
        {
            if (parentNode == null)
                return;
            else if (parentNode.leftChild == null)
                huffmanlist.Add(parentNode);
            else
            {
                parentNode.leftChild.code = parentNode.code + "0";
                parentNode.rightChild.code = parentNode.code + "1";
                GenerateCode(parentNode.leftChild);
                GenerateCode(parentNode.rightChild);
            }

        }
        
        //this number will increase if the gab between the nomber of rebeat letter decrease
        public static long callength() 
        {
            if (huffmanlist == null)
                return 0;
            long res = (18 * huffmanlist.Count()); //size of dict
            for (int i = 0; i < huffmanlist.Count; i++)
                res += (huffmanlist[i].number * huffmanlist[i].code.Length);

            //should be less than the original size
            return res;
        }

        //given code ,output data
        public static IList<char> DecodeData(IList<char> input) 
        {

            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (huffmanlist == null || huffmanlist.Count == 0)
                return null;
            #endregion 

            #region intial variable
            int x = 0;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < huffmanlist.Count; i++)
                dict.Add(huffmanlist[i].code, huffmanlist[i].data);
            IList<char> res = new List<char>();
            string curr = "",newres="";
            #endregion

            #region main algorithm
            while (x >= input.Count)
            {
                //add current code
                curr += input[x++];
                //search on current code on the list 
                if (dict.TryGetValue(curr, out newres))
                {
                    //if found add them to the result
                    for (int k = 0; k < newres.Length; k++)
                        res.Add(newres[k]);
                    curr = "";
                }
            }
            return res;
            #endregion 

        }

        // build extend huffman code
        public static void extendhuffman() 
        {
            if (huffmanlist == null || huffmanlist.Count < 2)
                return;

            #region intial variable
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
            #endregion

            #region main algorithm
            //build the tree
            Stack<Node> stack = SortStack(newlist);
            while (stack.Count > 1)
            {
                //move last 2 least number
                Node leftChild = stack.Pop();
                Node rightChild = stack.Pop();
                //repalce them with the sum of them 
                Node parentNode = new Node(leftChild, rightChild);
                stack.Push(parentNode);
                //sort again
                stack = SortStack(stack.ToList<Node>());
            }
            huffmanlist.Clear();
            huffmanlist = new List<Node>();
            GenerateCode(stack.Pop());
            #endregion 
        }
        #endregion
    }

    class letter
    {
        //for represent symple in arithmetic coding
        public double upper; //upper range
        public double lower; //lower range
        public string data; //the original data
        public letter(string data, double up, double low)
        {
            this.data = data;
            this.upper = up;
            this.lower = low;

        }
    }

    class arithmetic
    {
        //for arithmetic coding technique
        #region variable
        static public IList<letter> arthmitclist; //include all letter 
        #endregion

        #region function

        //given array of string & array of number of each string
        public static void build(IList<string> input, IList<int> array)
        {
            #region validtion
            if (input == null || array == null)
                return;
            if (input.Count < 2 || array.Count < 2 || array.Count != input.Count)
                return;
            #endregion 

            #region intial variable 
            arthmitclist = new List<letter>();
            int sum = 0;
            for (int i = 0; i < array.Count; i++)
                sum += array[i];
            double curr = 0.0;
            #endregion

            for (int i = 0; i < array.Count; i++)
            {
                arthmitclist.Add(new letter(input[i], (curr + array[i]) / sum, curr / sum));
                curr += array[i];
            }
        }

        //given compressed binary code , return string 
        public static IList<char> buildstring(IList<char> input)
        {
            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (arthmitclist == null || arthmitclist.Count < 2)
                return null;
            #endregion

            IList<char> res =new List<char>();
            int x = 0;

            while (x < input.Count)
            {
                string curr = "";
                for (int i = 0; i < 64; i++)
                    curr += input[x++];
                string s = arithmetic.decodeData(arithmetic.Binarytodouble(curr));
                for (int j = 0; j < s.Length; j++)
                    res.Add(s[j]);
            }
            return res;
        }

        //given double ,output data  for each double
        public static string decodeData(double input) 
        {

            #region validtion
            if (input < 0)
                return null;
            if (arthmitclist == null || arthmitclist.Count < 2)
                return null;
            #endregion

            #region intial 
            string res = "";
            IList<letter> artlist = new List<letter>();
            for (int i = 0; i < arthmitclist.Count; i++)
            {
                artlist.Add(new letter(arthmitclist[i].data, arthmitclist[i].upper, arthmitclist[i].lower));
            }
            int x = 0;
            #endregion

            #region main algorithm
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
            #endregion

        }

        //convert binary to double
        public static double Binarytodouble(string str)
        {

            long n = Convert.ToInt64(str, 2);
            double x = BitConverter.Int64BitsToDouble(n);
            return Math.Abs(x);
        }
        #endregion
    }

    class runlength
    {
        //convert binary seqance to number of 1 and 0 in sequence 
        #region variable
        public static int maxbit = 3;
        #endregion

        #region function

        public static IList<char> back(IList<char> input)
        {
            #region validtion
            if (maxbit < 1)
                return null;
            if (input == null || input.Count < maxbit)
                return null;
            #endregion

            #region intial variable 
            IList<char> res = new List<char>();
            bool test = false;
            int len = (1 << maxbit);
            string curr = "";
            #endregion

            #region main algorithm 
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (curr.Length == maxbit)
                {
                    //number found
                    int y = Convert.ToInt16(curr);
                    //decide which char will be represent
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
            #endregion

        }

        #endregion 
    }


}

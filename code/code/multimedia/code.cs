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
        public static IList<char> LetterDict; 
        //max size for each integer
        public static int mymax = 1 << 20;
        //the max limit of our dictionary 
        public static int maxbit = 20;
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
        
        //given string , output compressed code
        public static IList<int> Coding(string input) 
        {
            #region validtion
            if (input == null || input.Length == 0)
                return null;
            if (maxbit < 0|| maxbit>32)
                maxbit = 20;
            if (LetterDict.Count <= 1)
                return null;
            #endregion

            #region initial all needed variable
            mymax = 1 << maxbit;
            IList<int> res = new List<int>(); //array to save result
            Dictionary<string, int> dict = new Dictionary<string, int>(); //current dictionary ,change typefor quickly search in dictionary
            int index = 0; //count for Dictionary
            int x = 1; //pointer for current element to add
            int last = 0, slast = 0; //to save the information about last block found for remove it 
            //fill the current dictionary
            while (index < LetterDict.Count)
                dict.Add(LetterDict[index].ToString(), index++);
            //intial current string
            string curr = input[0] + "";
            #endregion

            #region main algorithm
            while (curr.Length > 0)
            {
                //find longest sequance that didn't exist in dictionary
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
                //add the last block found to the result
                res.Add(last);
                //add them to dictionary if valied 
                if (index < mymax && !(dict.ContainsKey(curr))) //max for bit
                    dict.Add(curr, index++);
                //remove last block from the string
                curr = curr.Remove(0, slast);
            }
            return res;
            #endregion
        }

        //convert integer list to binary code
        public static IList<char> convertbinary(IList<int> input) 
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
            IList<char> res = new List<char>();
            int maxcurr = Convert.ToString(LetterDict.Count, 2).Length;
            int x = LetterDict.Count;
            #endregion

            #region main algorithm
            for (int i = 0; i < input.Count; i++)
            {
                //convert int to binary string 
                string binary = Convert.ToString(input[i], 2).PadLeft(maxcurr, '0');
                //add it to final result
                for (int j = 0; j < binary.Length; j++)
                    res.Add(binary[j]);
                //update size of int 
                maxcurr = Convert.ToString(++x, 2).Length < maxbit ? Convert.ToString(x, 2).Length : maxbit;
            }
            //the length must be divisable by 8 to store it as byte
            //if not add zeroes until it is
            while (res.Count % 8 != 0)
                res.Add('0');
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

        //given data ,output compressed code
        public static IList<char> codeData(IList<char> input)
        {

            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (huffmanlist == null || huffmanlist.Count == 0)
                return null;
            #endregion 

            #region intial variable
            IList<char> res = new List<char>(); //final result code 
            Dictionary<string, string> dict = new Dictionary<string, string>(); //add huffman list to dictionary for 
            for (int i = 0; i < huffmanlist.Count; i++)
                dict.Add(huffmanlist[i].data, huffmanlist[i].code);
            string curr = ""; //current string
            string newres="";
            #endregion 

            #region main algorithm
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (dict.TryGetValue(curr,out newres)) 
                {
                    //if found add them to result
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
        //static public int number; //number of all letter
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

        //given string , return compressed binary code
        public static IList<char> buildbinary(IList<char> input)
        {
            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (arthmitclist == null || arthmitclist.Count < 2)
                return null;
            #endregion

            #region intial variable
            IList<char> res = new List<char>();
            IList<Double> x = new List<Double>();
            #endregion

            #region main algorithm
            x = codeData(input);
            for (int i = 0; i < x.Count; i++)
            {
                string curr = arithmetic.doubletobinary(x[i]);
                for(int j=0;j<curr.Length;j++)
                    res.Add(curr[j]);
            }
            return res;
            #endregion

        }

        //given data ,output list of double
        public static IList<double> codeData(IList<char> input) 
        {

            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (arthmitclist == null || arthmitclist.Count < 2)
                return null;
            #endregion

            #region intial variable
            IList<double> res = new List<double>();
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
            #endregion 

            #region main algorithm 
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
                            res.Add((up + down) / 2.0);
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
            return res;
            #endregion

        }

        //convert double to binary 
        public static string doubletobinary(double x)
        {
            long m = BitConverter.DoubleToInt64Bits(x);
            string str = Convert.ToString(m, 2);
            return str;
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

        public static IList<char> build(IList<char> input)
        {
            #region validtion 
            if (maxbit < 1)
                return null;
            if (input == null || input.Count < maxbit)
                return null;
            #endregion

            #region intial 
            IList<char> res = new List<char>(); //final result
            bool test = false;                  //for switch between 1 and 0
            int curr = 0;                       // number for switch between 1 and 0
            int num = 0;                       //number of repeated 0 or 1 untill now
            #endregion

            #region main algorithm
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
            #endregion

        }

        #endregion 
    }

    class optmize
    {
        //for compress the binary seqance
        #region variable
        static private IList<string> mychar; //include all possible sequance 
        static public int length = 8; 
        #endregion

        #region function
        //compress  the binary seqance
        static public IList<char> build(IList<char> input)
        {

            #region validtion
            if (input == null || input.Count == 0)
                return null;
            if (mychar == null || mychar.Count < 1)
                return null;
            if (length < 1)
                return null;
            #endregion

            #region initial variable
            IList<char> res = new List<char>();
            mychar = new List<string>();
            optmize g = new optmize();
            g.generate("", length);

            Dictionary<string, int> currlist = new Dictionary<string, int>();
            for (int i = 0; i < mychar.Count; i++)
                currlist.Add(mychar[i], 0);
            string curr = "";
            //count number of rebeat for each sequance 
            for (int i = 0; i < input.Count; i++)
            {
                curr += input[i];
                if (curr.Length == length) 
                {
                    currlist[curr]++;
                    curr = "";
                }
            }
            IList<int> dict = new List<int>();
            Huffman.build(currlist);
            #endregion 

           
            //convert each number of rebeat to binary 
            for (int i = 0; i < currlist.Count; i++)
            {
                int x = currlist.ToList()[i].Value;
                dict.Add(x);
                string binary = Convert.ToString(x, 2).PadLeft(20, '0');
                for (int j = 0; j < binary.Length; j++)
                    res.Add(binary[j]);
            }

            /*arthmitc.Main(mychar, dict);
            IList<char> newcurr = arthmitc.buildbinary(input);
            for (int i = 0; i < newcurr.Count; i++)
                res.Add(newcurr[i]);*/

            IList<char> newres = Huffman.codeData(input);
            for (int i = 0; i < newres.Count; i++)
                res.Add(newres[i]);

            while (res.Count % 8 != 0)
                res.Add('0');
            return res;
        }

        //genrate all possible sequance 
        private void generate(string x, int y)
        {
            if (y == 0)
                mychar.Add(x);
            else {
                y--;
                generate(x + '0', y);
                generate(x + '1', y);
            }
           
        }
        #endregion
    }
}

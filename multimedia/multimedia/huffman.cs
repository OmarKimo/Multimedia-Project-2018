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
            this.data = leftChild.data + rightChild.data;
            this.number = leftChild.number + rightChild.number;
        }
    }

    class Huffman
    {
        static private IList<Node> huffmanlist;
        public IList<Node> Gethuffman() //get copy of list
        {
            IList<Node> newlist = new List<Node>();

            for (int i = 0; i < huffmanlist.Count; i++)
            {
                Node tempNode = huffmanlist[i];
                newlist.Add(tempNode);
            } 
            return newlist;
        }
        static void Main(string[] input, int[] array) //give them array of string & array of number of each string
        {
            IList<Node> list = new List<Node>();
            IList<Node> huffmanlist = new List<Node>();
            for (int i = 0; i < array.Length; i++)
            {
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

            GenerateCode(parentNode1, "");

        }

        public static Stack<Node> GetSortedStack(IList<Node> list) //sort thd probability
        {
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

        public static void GenerateCode(Node parentNode, string code) //after build tree , apply this function on the parent node to get the code for each string 
        {
            if (parentNode == null)
                return;
            else if (parentNode.leftChild == null)
            {
                parentNode.code = code;
                huffmanlist.Add(parentNode);
            }
            else
            {
                GenerateCode(parentNode.leftChild, code + "0");
                GenerateCode(parentNode.rightChild, code + "1");
            }
            
        }

        public static string codeData(string input) //given data ,output code
        {
            string res = "";
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < huffmanlist.Count; j++)
                {
                    if (input[i] == huffmanlist[j].data[0])
                    {
                        res += huffmanlist[j].code;
                        break;
                    }
                }
            }
            return res;
        }

        public static string DecodeData(string input) //given code ,output data
        {
            string res = "";
            string curr = "";
            for (int i = 0; i < input.Length; i++)
            {
                //add current code
                curr += input[i];
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
            IList<Node> newlist = new List<Node>();
            for (int i = 0; i < huffmanlist.Count; i++)
            {
                for (int j = i; j < huffmanlist.Count; j++)
                {
                    newlist.Add(new Node(huffmanlist[i].data + huffmanlist[j].data, huffmanlist[i].number * huffmanlist[j].number));
                }
            }
            huffmanlist = newlist;

        }
    }

    class letter
    {
        public Double upper;
        public Double lower;
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
        static private IList<letter> arthmitclist;
        static void Main(string[] input, int[] array) //give them array of string & array of number of each string
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
                arthmitclist.Add(new letter(input[i], curr / sum, (curr + array[i]) / sum));
                curr += array[i];

            }
        }
        public static IList<Double> codeData(string input) //given data ,output code
        {
            IList<Double> list = new List<Double>();
            IList<letter> artlist = new List<letter>();
            for (int i = 0; i < arthmitclist.Count; i++)
            { 
                artlist.Add(new letter(arthmitclist[i].data,arthmitclist[i].upper,arthmitclist[i].lower));
            }
            Double up=1, down=0;
            string end = input[input.Length - 1]+"";
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
                        Double ratio = up - down;
                        for (int k = 0; k < arthmitclist.Count; k++)
                        {
                            artlist[j].lower = arthmitclist[j].lower * ratio + down;
                            artlist[j].upper = arthmitclist[j].upper * ratio + down;
                        }
                        if (artlist[j]+""==end) //end of coding  need to change
                        {
                            list.Add((up + down) / 2.0);
                            up = 1;
                            down = 0;
                            artlist.Clear();
                            for (int k = 0; k < arthmitclist.Count; k++)
                            {
                                artlist.Add(new letter(arthmitclist[k].data, arthmitclist[k].upper, arthmitclist[k].lower));
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static string decodeData(Double input,string end) //given double & the last symple in the text ,output data  for each double
        {
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
                    if (input < arthmitclist[j].upper && input > arthmitclist[j].lower)
                    {
                        res += arthmitclist[j].data;
                        if (arthmitclist[j].data == end) //end of coding  need to change 
                        {
                            return res;
                        }
                        Double up = artlist[j].upper;
                        Double down = artlist[j].lower;
                        Double ratio = up - down;
                        for (int k = 0; k < arthmitclist.Count; k++)
                        {
                            artlist[j].lower = arthmitclist[j].lower * ratio + down;
                            artlist[j].upper = arthmitclist[j].upper * ratio + down;
                        }
                    }
                }
            }
        }



    }


    class lzw
    {
        static public IList<char> LetterDict;

        static void Main(string[] input, int[] array) //give them array of string & array of number of each string
        {
            LetterDict = new List<char>();
            //fill letterdict with all letter in the data set
        }



        static IList<int> Coding(string input) //given string , output code
        {
            IList<int> mylist=new List<int>();
            IList<string> Dict=new List<string>();
            for (int i = 0; i < LetterDict.Count; i++)
            {
                string x =""+ LetterDict[i];
                Dict.Add(x);
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
    }
}

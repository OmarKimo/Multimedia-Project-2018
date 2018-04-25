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
        static private IList<Node> mylist;
        public IList<Node> Getmylist() //get copy of list
        {
            IList<Node> newlist = new List<Node>();

            for (int i = 0; i < mylist.Count; i++)
            {
                Node tempNode = mylist[i];
                newlist.Add(tempNode);
            } 
            return newlist;
        }
        static void Main(string[] input, int[] array) //give them array of string & array of number of each string
        {
            IList<Node> list = new List<Node>();
            IList<Node> mylist = new List<Node>();
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
                mylist.Add(parentNode);
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
                for (int j = 0; j < mylist.Count; j++)
                {
                    if (input[i] == mylist[j].data[0])
                    {
                        res += mylist[j].code;
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
                for (int j = 0; j < mylist.Count; j++)
                {
                    if (curr == mylist[j].code) //if found then add data to result and search on the next symple 
                    {
                        res += mylist[j].data;
                        curr = "";
                        break;
                    }
                }
            }
            return res;
        }

        public static IList<Node> extendhuffman(IList<Node> list)//extend huffman code 
        {
            IList<Node> newlist = new List<Node>();

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i; j < list.Count; j++)
                {
                    newlist.Add(new Node(list[i].data+list[j].data,list[i].number*list[j].number));
                }
            }
            return newlist;
        }
    }
}

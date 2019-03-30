using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryTreeSearch
{
    class Program
    {
        private static List<String> instruction = new List<String>();
        private static List<BSTN> tree = new List<BSTN>();
        static void Main(string[] args)
        {
            //Import File
            String roster = null; //roster.txt
            Boolean filecheck = true;
            try
            {
                roster = args[0]; //roster.txt
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                filecheck = false;
            }
            if (filecheck == true)
            {
                Console.WriteLine("This is roster file you called: " + roster);
                try
                {
                    using (StreamReader sr = new StreamReader(roster))
                    {
                        int nol = int.Parse(sr.ReadLine());
                        for (int i = 0; i < nol; i++)
                        {
                            String line = sr.ReadLine();
                            instruction.Add(line);
                        }
                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
            }
            //Root initiation
            tree.Add(null);

            //Execute Instruction
            foreach(String s in instruction)
            {
                String[] papaya = s.Split(" ");
                String inst = papaya[0];
                int para = -1;
                if(papaya.Length == 2)
                {
                    para = int.Parse(papaya[1]);
                }
                switch(inst)
                {
                    case "ADD":
                        Add(para);
                        break;
                    case "FIND":
                        Console.Write("Looking for " + para + " ...");
                        Find(tree[0], para);
                        break;
                    case "CLEAR":
                        Clear();
                        break;
                    case "PRINT":
                        Print();
                        break;
                    case "PREORDER":
                        Console.Write("Preorder: ");
                        Preorder(tree[0]);
                        Console.WriteLine("");
                        break;
                    case "INORDER":
                        Console.Write("Inorder: ");
                        Inorder(tree[0]);
                        Console.WriteLine("");
                        break;
                    case "POSTORDER":
                        Console.Write("Postorder: ");
                        Postorder(tree[0]);
                        Console.WriteLine("");
                        break;
                    default:
                        break;
                }
            }
        }
        /*
         * Method group, root of the tree is tree[0]
         * Add:
         * Find:
         * Clear:
         * Print:
         * Preorder:
         * Inorder:
         * Postorder:       
         */

        public static void Add(int n)
        {
            Console.WriteLine("Adding " + n);
            BSTN y = null;
            BSTN x = tree[0];
            BSTN z = new BSTN(n);
            while (x != null)
            {
                y = x;
                if(z < x)
                {
                    x = x.get("left");
                }
                else
                {
                    x = x.get("right");
                }
            }
            z.setParent(y);
            if(y == null)
            {
                tree.Insert(0, z);
            } 
            else if (z < y)
            {
                y.setLeft(z);
                tree.Add(z);
            }
            else
            {
                y.setRight(z);
                tree.Add(z);
            }
        }

        public static BSTN Find(BSTN o1, int n)
        {
            if (o1 == null)
            {
                Console.Write(" : Not Found.\n");
                return null;
            }
            else
            {
                if (o1.getkey() == n)
                {
                    Console.Write(o1.getkey() + " : Found!\n");
                    return o1;
                }
                else if (n < o1.getkey())
                {
                    Console.Write(o1.getkey() + " ");
                    return Find(o1.get("left"), n);
                }
                else
                {
                    Console.Write(o1.getkey() + " ");
                    return Find(o1.get("right"), n);
                }
            }
        }

        public static void Clear()
        {
            tree.Clear();
            tree.Add(null);
        }

        public static void Print()
        {
            foreach(BSTN node in tree)
            {
                if(node != null)
                {
                    node.print();
                }
            }
        }

        public static void Preorder(BSTN node)
        {
            if(node != null)
            {
                Console.Write(" " + node.getkey());
                Preorder(node.get("left"));
                Preorder(node.get("right"));
            }
        }

        public static void Inorder(BSTN node)
        {
            if(node != null)
            {
                Inorder(node.get("left"));
                Console.Write(" " + node.getkey());
                Inorder(node.get("right"));
            }
        }

        public static void Postorder(BSTN node)
        {
            if(node != null)
            {
                Postorder(node.get("left"));
                Postorder(node.get("right"));
                Console.Write(" " + node.getkey());
            }
        }
    }
        /*
         * Binary Search Tree Node (BSTN)
         */
    class BSTN
    {
        int key;
        BSTN left;
        BSTN right;
        BSTN parent;

        public BSTN(int n)
        {
            this.key = n;
            this.left = null;
            this.right = null;
            this.parent = null;
        }
        public BSTN get(String str)
        {
            if (str.Equals("left"))
            {
                return this.left;
            }
            else if(str.Equals("right"))
            {
                return this.right;
            }
            else if(str.Equals("parent"))
            {
                return this.parent;
            }
            return null;
        }
        public int getkey()
        {
            return key;
        }

        public void setLeft(BSTN node)
        {
            this.left = node;
        }

        public void setRight(BSTN node)
        {
            this.right = node;
        }

        public void setParent(BSTN node)
        {
            this.parent = node;
        }

        public static bool operator <(BSTN o1, BSTN o2)
        {
            return o1.key < o2.key;
        }
        public static bool operator >(BSTN o1, BSTN o2)
        {
            return o1.key > o2.key;
        }
        public void print()
        {
            Console.Write("Node: " + this.key);
            if(this.left != null)
            {
                Console.Write(" ; L: " + this.left.getkey());
            }
            else
            {
                Console.Write(" ; L: null");
            }
            if (this.right != null)
            {
                Console.Write(" ; R: " + this.right.getkey());
            }
            else
            {
                Console.Write(" ; R: null");
            }
            Console.WriteLine("");
        }
    }
}

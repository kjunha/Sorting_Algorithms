using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Diagnostics;

namespace FinalProj301_2
{
    class Program
    {
        private static List<String> branches = new List<string>();
        private static List<byte> rawbyte = new List<byte>();
        private static String binstring;
        private static List<HuffmanNode> tree = new List<HuffmanNode>();
        static void Main(string[] args)
        {
            Stopwatch stw = new Stopwatch();
            stw.Start();
            String fname = null; //fname.txt
            Boolean filecheck = true;
            try
            {
                fname = args[0]; //fname.txt
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                filecheck = false;
            }
            if (filecheck == true)
            {
                Console.WriteLine("This is roster file you called: " + fname);
                try
                {
                    using (BinaryReader br = new BinaryReader(File.OpenRead(fname)))
                    {
                        StringBuilder sb = new StringBuilder();
                        bool part1 = true;
                        int binlength = 0;
                        while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            /*
                             * Part 1                          
                             * Read Each char until the br buffer reads '\n'.
                             * Once it read '\n', it translate char[] to String, save each line to branches.
                             * After it read "*****", it read one more line to save it as binsize
                             *                            
                             * part 2                           
                             * Start to read each byte, save it as rawbyte;                           
                             */
                            String line = "";
                            String compp = "*****\r";
                            while (part1)
                            {
                                char c = br.ReadChar();
                                if (c == '\n')
                                {
                                    line = sb.ToString();
                                    if (String.Equals(line, compp))
                                    {
                                        sb.Clear();
                                        bool bin = true;
                                        while (bin)
                                        {
                                            char cc = br.ReadChar();
                                            if (cc == '\n')
                                            {
                                                binlength = int.Parse(sb.ToString());
                                                bin = false;
                                                break;
                                            }
                                            else
                                            {
                                                sb.Append(cc);
                                            }
                                        }
                                        part1 = false;
                                    }
                                    else
                                    {
                                        sb.Clear();
                                        branches.Add(line);
                                        line = "";
                                    }
                                }
                                else
                                {
                                    sb.Append(c);
                                }
                            }
                            rawbyte.Add(br.ReadByte());
                        }
                        /*
                         * Build binary as string again.
                         * cut with substring to the length only that we need.                       
                         */
                        StringBuilder binstrbuffer = new StringBuilder();
                        foreach (byte b in rawbyte)
                        {
                            binstrbuffer.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                        }
                        binstring = binstrbuffer.ToString().Substring(0, binlength);
                    }
                    Console.WriteLine("roster call Successful!");
                }

                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }

                /*
                 * Main Program Sequence
                 */

                HuffmanNode key = buildHuffmanTree();
                StringBuilder output = new StringBuilder();
                HuffmanNode scope = key;
                Console.WriteLine("Huffman Tree Rebuilt Successful!");
                /*
                 * File Writing process
                 */
                String[] title = fname.Split('.');
                String fname_out = title[0] + "2.txt";
                using (StreamWriter sw = new StreamWriter(File.Open(fname_out, FileMode.Create)))
                {
                    foreach (char c in binstring)
                    {
                        if (c == '0')
                        {
                            scope = scope.getLeft();
                        }
                        else
                        {
                            //Move Right until the end, reset on end
                            scope = scope.getRight();
                        }
                        if (scope.getLeft() == null && scope.getRight() == null)
                        {
                            sw.Write(scope.getLatter());
                            scope = key;
                        }
                    }
                }
                Console.WriteLine("Runtime: " + stw.ElapsedMilliseconds.ToString());
                Console.WriteLine("End of Process");
                stw.Stop();
            }
        }
        /*
         * Step1. Create Endnode for the tree
         * Step2. Create a empty top node
         * Step3. build branches which gives endnode a correct position.
         */
        public static HuffmanNode buildHuffmanTree()
        {
            //Step1.
            foreach (String s in branches)
            {
                String[] sa = s.Split(' ');
                if (sa[1] == "NL\r")
                {
                    sa[1] = "\n";
                }
                else if (sa[1] == "TB\r")
                {
                    sa[1] = "\t";
                }
                else if (sa[1] == "SP\r")
                {
                    sa[1] = " ";
                }
                sa[1] = sa[1][0].ToString();
                tree.Add(new HuffmanNode(char.Parse(sa[1]), sa[0]));
            }
            //Step2.
            HuffmanNode huf_tree = new HuffmanNode('$', "");
            /*
             * Step3. for each node in tree, read the frequency.
             * Starting from the root, if the char is '0', go left
             * if nothings on left, add a new node and look at it (scoping)
             * If the char is '1', go right
             * if nothings on right, add a new node and look at it.(scoping)
             */
            foreach (HuffmanNode huf in tree)
            {
                String str = huf.getFrequency();
                HuffmanNode scope = huf_tree;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '0')
                    {
                        if (scope.getLeft() == null)
                        {
                            if (i == str.Length - 1)
                            {
                                scope.setLeft(huf);
                                scope = huf_tree;
                            }
                            else
                            {
                                String freq = scope.getFrequency() + "0";
                                scope.setLeft(new HuffmanNode('$', freq));
                                scope = scope.getLeft();
                            }
                        }
                        else
                        {
                            scope = scope.getLeft();
                        }
                    }
                    else
                    {
                        if (scope.getRight() == null)
                        {
                            if (i == str.Length - 1)
                            {
                                scope.setRight(huf);
                                scope = huf_tree;
                            }
                            else
                            {
                                String freq = scope.getFrequency() + "0";
                                scope.setRight(new HuffmanNode('$', freq));
                                scope = scope.getRight();
                            }
                        }
                        else
                        {
                            scope = scope.getRight();
                        }
                    }
                }

            }
            return huf_tree;
        }
    }
    /*
     * Node Objects Class
     * Field: 
     * Left, Right Nodes
     * latter, count, frequency(Huffman)
     * Method:
     * Getter Methods
     */
    class HuffmanNode : IComparable<HuffmanNode>
    {
        private HuffmanNode left;
        private HuffmanNode right;
        private char latter;
        private int chcode; //ASCII code for sorting
        private String frequency;

        /*
         * Regular Node Constructor
         */
        public HuffmanNode(char c, String s)
        {
            this.latter = c;
            this.chcode = (int)c;
            this.frequency = s;
            left = null;
            right = null;
        }

        public int getASCII()
        {
            return this.chcode;
        }

        public HuffmanNode getLeft()
        {
            if (left != null)
            {
                return this.left;
            }
            else
            {
                return null;
            }
        }

        public HuffmanNode getRight()
        {
            if (right != null)
            {
                return this.right;
            }
            else
            {
                return null;
            }
        }
        public void setRight(HuffmanNode huf)
        {
            this.right = huf;
        }
        public void setLeft(HuffmanNode huf)
        {
            this.left = huf;
        }

        public char getLatter()
        {
            return this.latter;
        }

        public String getFrequency()
        {
            return this.frequency;
        }

        public int CompareTo(HuffmanNode other)
        {
            return other.chcode.CompareTo(this.chcode);
        }
    }
}

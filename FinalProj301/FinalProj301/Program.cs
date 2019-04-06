using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace FinalProj301
{
    /*
     * Encoding Process: FinalProj301
     * Decoding Process: FinalProj301_2
     */
    class Program
    {
        private static List<char>[] hashmap = new List<char>[128];
        private static String inputsource = "";
        private static StringBuilder binarycode = new StringBuilder();
        private static List<HuffmanNode> roster = new List<HuffmanNode>();
        private static List<HuffmanNode> rep = new List<HuffmanNode>(); //repository, Save original roster
        static void Main(string[] args)
        {
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
                for(int i = 0; i < 128; i++)
                {
                    hashmap[i] = null;
                }
                try
                {
                    using (StreamReader sr = new StreamReader(fname))
                    {
                        StringBuilder sb = new StringBuilder();
                        while (!sr.EndOfStream)
                        {
                            String line = sr.ReadLine();
                            line = line + '\n';
                            sb.Append(line);
                            foreach (char c in line)
                            {
                                int index = ((int)c) % 128;
                                if(hashmap[index] == null)
                                {
                                    hashmap[index] = new List<char>();
                                }
                                hashmap[index].Add(c);
                            }
                        }
                        inputsource = sb.ToString();
                        for(int i = 0; i < hashmap.Length; i++)
                        {
                            if(hashmap[i] != null)
                            {
                                char c = Convert.ToChar(i);
                                int cnt = hashmap[i].Count;
                                roster.Add(new HuffmanNode(c, cnt));
                            }
                        }
                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                /*
                 * Huffman Tree Build
                 */
                HuffmanTree();
                Console.WriteLine("Huffman Tree is ready.");
                /*
                 * Translate Original text to Huffman Code(start.)
                 */

                foreach (char c in inputsource)
                {
                int search = (int)c;
                    binarycode.Append(roster[BinarySearch(search, 0, roster.Count - 1)].getFrequency());
                }
                Console.WriteLine("Binary Code Ready.");
                /*
                 * Naming file, ensure 8 bit
                 */
                String[] title = fname.Split('.');
                String fname_out = title[0] + ".zip301";
                int originallength = binarycode.Length;
                int numofzero = (((int)(binarycode.Length / 8) + 1) * 8) - binarycode.Length;
                for(int i = 0; i < numofzero; i++)
                {
                    binarycode.Append("0");
                }
                String[] bin_ready = new string[(int)(binarycode.Length / 8)];
                String trunk = "";
                for(int i = 0; i < binarycode.Length; i++)
                {
                    if (i % 8 == 7)
                    {
                        trunk = trunk + binarycode[i];
                        bin_ready[(int)i / 8] = trunk;
                        trunk = "";
                    }
                    else
                    {
                        trunk = trunk + binarycode[i];
                    }
                }
                /*
                 * File Writing process (start.)
                 */
                using (StreamWriter sw = new StreamWriter(File.Open(fname_out, FileMode.Create)))
                {
                    foreach(HuffmanNode huf in roster)
                    {
                        sw.WriteLine(huf.printInfo());
                    }
                    sw.WriteLine("*****");
                    sw.WriteLine(originallength);
                }
                using (BinaryWriter bw = new BinaryWriter(File.Open(fname_out, FileMode.Append)))
                {
                    foreach(String s in bin_ready)
                    {
                        Console.Write(s);
                        byte bin = Convert.ToByte(s, 2);
                        bw.Write(bin);
                    }
                }
                Console.WriteLine("End of Process");
            }
        }
        /*
         * Build tree:
         * Part1. Sort - Concatenate 2 nodes - Delete first 2 - Sort again - ...
         * Part2. After initiate tree, set Frequency
         * Part3. After set Frequency, extract only nodes on the tip of the tree (root <-> tip)
         * Part4. Remove the root node (node contains the entire tree.)
         */
        public static void HuffmanTree()
        {
            int n = roster.Count;
            foreach(HuffmanNode hfn in roster)
            {
                rep.Add(hfn);
            }
            for (int i = 0; i < n - 1; i++)
            {
                MergeSort(0, roster.Count - 1);
                roster.Add(HuffmanNode.concatenateNodes(roster[0], roster[1]));
                roster.RemoveAt(0);
                roster.RemoveAt(0);
            }
            setHuffmanFrequency(roster[0]);
            extractHuffmanTree(roster[0]);
            roster.RemoveAt(0);
            roster.Sort();
        }

        /*
         * Recursive call to set the frequency char by char
         * Call left node, add '0' to frequency
         * Call right node, add '1' to frequency
         */
        public static void setHuffmanFrequency(HuffmanNode huf)
        {
            if(huf.getLeft() != null)
            {
                huf.getLeft().setFrequency(huf.getFrequency());
                huf.getLeft().setFrequency("0");
                setHuffmanFrequency(huf.getLeft());
            }
            if(huf.getRight() != null)
            {
                huf.getRight().setFrequency(huf.getFrequency());
                huf.getRight().setFrequency("1");
                setHuffmanFrequency(huf.getRight());
            }
        }

        public static void extractHuffmanTree(HuffmanNode huf)
        {
            if (huf.getLeft() != null)
            {
                extractHuffmanTree(huf.getLeft());
            }
            if (huf.getRight() != null)
            {
                extractHuffmanTree(huf.getRight());
            }
            if (huf.getLeft() == null && huf.getRight() == null)
            {
                roster.Add(huf);
            }
        }
        /*
         * Merge sort for Nodes
         */
        public static void MergeSort(int p, int r)//p is beginning , r is end index
        {
            if (p < r)
            {
                int q = (int)((p + r) / 2);
                MergeSort(p, q);
                MergeSort(q + 1, r);
                Merge(p, q, r);
            }
        }

        public static void Merge(int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            List<HuffmanNode> arr_l = new List<HuffmanNode>();
            List<HuffmanNode> arr_r = new List<HuffmanNode>();
            for (int i = 1; i <= n1; i++)
            {
                arr_l.Add(roster[p + i - 1]);
            }
            for (int j = 1; j <= n2; j++)
            {
                arr_r.Add(roster[q + j]);
            }
            arr_l.Add(new HuffmanNode('$',int.MaxValue));
            arr_r.Add(new HuffmanNode('$',int.MaxValue));
            int li = 0;
            int ri = 0;
            for (int k = p; k <= r; k++)
            {
                if (arr_l[li].getCount() <= arr_r[ri].getCount())
                {
                    roster[k] = arr_l[li];
                    li++;
                }
                else
                {
                    roster[k] = arr_r[ri];
                    ri++;
                }
            }
        }
        /* 
         * Search: what char you find (in ASCII)
         * size: total search size (Array Size)
         * low: start from 0       
         */
        public static int BinarySearch(int search, int btm, int size)
        {
            int lo = btm;
            int hi = Math.Max(lo, size + 1);
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                if (search > roster[mid].getASCII())
                {
                    hi = mid;
                }
                else
                {
                    lo = mid + 1;
                }
            }
            return (hi - 1);
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
        private List<int> count = new List<int>();
        private String frequency;

        /*
         * Regular Node Constructor
         */
        public HuffmanNode(char c, int i)
        {
            this.latter = c;
            this.chcode = (int)c;
            this.count.Add(i);
            frequency = "";
        }
        /*
         * Concatenate two nodes
         * private constructor       
         */
        private HuffmanNode(HuffmanNode ln, HuffmanNode rn)
        {
            this.left = ln;
            this.right = rn;
            this.latter = '$';
            this.count.Add(ln.getCount());
            this.count.Add(rn.getCount());
            this.frequency = "";

        }
        /*
         * Each left and right node saved saperately inside of the count list
         */
        public int getCount()
        {
            int total = 0;
            foreach (int i in count)
            {
                total = total + i;
            }
            return total;
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

        public char getLatter()
        {
            return this.latter;
        }

        /*
         * Constructor Method to concatenate Nodes
         */
        public static HuffmanNode concatenateNodes(HuffmanNode ln, HuffmanNode rn)
        {
            return new HuffmanNode(ln, rn);
        }

        /*
         * Frequency can be added char by char
         */
        public void setFrequency(String s)
        {
            this.frequency = frequency + s;
        }

        public String getFrequency()
        {
            return this.frequency;
        }

        public String printInfo()
        {
            int i = getCount();
            StringBuilder output = new StringBuilder();
            output.Append(this.frequency).Append(" ");
            if (latter == '\n')
            {
                output.Append("NL");
            }
            else if (latter == '\t')
            {
                output.Append("TB");
            }
            else if (latter == ' ')
            {
                output.Append("SP");
            }
            else
            {
                output.Append(latter);
            }

            return output.ToString();
        }

        public int CompareTo(HuffmanNode other)
        {
            return other.chcode.CompareTo(this.chcode);
        }
    }
}

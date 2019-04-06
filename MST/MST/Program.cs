using System;
using System.Collections.Generic;
using System.IO;

namespace MST
{
    class Program
    {
        private static List<Node> nodes = new List<Node>();
        private static List<Edge> bucket = new List<Edge>();
        private static List<Edge> buffer = new List<Edge>();
        static void Main(string[] args)
        {
            String roster = null; //roster.txt
            Boolean filecheck = true;
            /*
             * File Reading
             */
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
                        String nodeinit = sr.ReadLine();
                        foreach(char c in nodeinit)
                        {
                            nodes.Add(new Node(c));
                        }
                        while(!sr.EndOfStream)
                        {
                            String[] line = sr.ReadLine().Split(" ");
                            Node left = null;
                            Node right = null;
                            foreach(Node n in nodes)
                            {
                                if(n.searchNode(line[0][0]) != null)
                                {
                                    left = n;
                                }
                                if(n.searchNode(line[1][0]) != null)
                                {
                                    right = n;
                                }
                            }
                            bucket.Add(new Edge(left, right, int.Parse(line[2])));
                        }
                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                /*
                 * Main Sequence
                 */
                bucket.Sort();
                foreach (Edge eg in bucket)
                {
                    Console.WriteLine("Edge Weight: " + eg.getWeight());
                }
                MSTPrim(bucket[bucket.Count - 1].getLeft());
            }
        }
        public static void MSTPrim(Node start)
        {
            Console.WriteLine("Start Node: " + start.getID());
            List<Edge> mstedge = new List<Edge>();
            int totalweight = 0;
            foreach(Edge eg in bucket)
            {
                if(eg.getLeft().getID() == start.getID() || eg.getRight().getID() == start.getID())
                {
                    buffer.Add(eg);
                }
            }
            Console.WriteLine("Number of Candy: " + buffer.Count);
            Edge e = getBestEdge();
            while (e != null)
            {
                /*
                 * Each Edge has a nodes
                 */
                Node lft = e.getLeft();
                Node rgt = e.getRight();
                lft.Visit();
                rgt.Visit();
                totalweight = totalweight + e.getWeight();
                Console.WriteLine("Checkpoint: " + totalweight + " / " + mstedge.Count + " Node saved.");
                mstedge.Add(e);
                bucket.Remove(e);
                buffer.Clear();
                foreach(Edge eg in bucket)
                {
                    if(eg.getLeft().getID() == lft.getID() || eg.getRight().getID() == lft.getID())
                    {
                        if(buffer.IndexOf(eg) == -1)
                        {
                            buffer.Add(eg);
                        }
                    }
                    if (eg.getLeft().getID() == rgt.getID() || eg.getRight().getID() == rgt.getID())
                    {
                        if (buffer.IndexOf(eg) == -1)
                        {
                            buffer.Add(eg);
                        }
                    }
                }
                e = getBestEdge();
            }
            Console.WriteLine("Total Weight: " + totalweight);
            foreach(Edge eg in mstedge)
            {
                Console.WriteLine(eg.printNodes());
            }

        }
        /*
         * Get Best Edge Method
         */
        public static Edge getBestEdge()
        {
            Edge best = null;
            List<Edge> temp = new List<Edge>();
            while (best == null && bucket.Count > 0)
            {
                if(buffer.Count > 0)
                {
                    best = buffer[buffer.Count - 1];
                }
                else
                {
                    bucket.Sort();
                    best = bucket[bucket.Count - 1];
                }

                Console.WriteLine("Best Attempt: " + best.getLeft().getID() + " - " + best.getRight().getID() + ", " + best.getWeight());
                if (best.getLeft().isVisited() && best.getRight().isVisited())
                {
                    buffer.Remove(best);
                    bucket.Remove(best);
                    best = null;
                    buffer.Sort();
                }
            }
            return best;
        }
    }
    /*
     * Edge Class (Weight Sortable)
     */
    class Edge:IComparable<Edge>
    {
        private Node left;
        private Node right;
        private int weight;

        public Edge(Node l, Node r, int i)
        {
            this.left = l;
            this.right = r;
            this.weight = i;
        }

        public bool ifContains(char c)
        {
            if(this.left.getID() == c || this.right.getID() == c)
            {
                return true;
            }
            return false;
        }

        public int getWeight()
        {
            return this.weight;
        }

        public Node getLeft()
        {
            return this.left;
        }

        public Node getRight()
        {
            return this.right;
        }

        public int CompareTo(Edge other)
        {
            return other.weight.CompareTo(this.weight);
        }

        public String printNodes()
        {
            return this.left.getID() + " - " + this.right.getID();
        }
    }
    /*
     * Node Class
     */
    class Node
    {
        char id;
        bool visited;

        public Node(char c)
        {
            this.id = c;
            this.visited = false;
        }

        public void Visit()
        {
            this.visited = true;
        }

        public bool isVisited()
        {
            return this.visited;
        }

        public Node searchNode(char c)
        {
            if(c == this.id)
            {
                return this;
            }
            return null;
        }

        public char getID()
        {
            return this.id;
        }
    }
}

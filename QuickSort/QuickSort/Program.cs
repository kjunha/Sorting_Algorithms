using System;
using System.Collections.Generic;
using System.IO;

namespace QuickSort
{
    class Program
    {
        private static List<String> query_dat = new List<String>();
        private static List<Employee> roster_dat = new List<Employee>();
        private static List<Employee> roster_rep = new List<Employee>();
        private static int cp = 0; //Compare
        private static int ex = 0; //Exchange
        private static int rec = -1; //Recursive
        private static int fileno = 1;//File Number
        static void Main(string[] args)
        {
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
                            String[] papaya = line.Split('|');
                            Employee obj = new Employee(papaya[0], papaya[1], papaya[2], papaya[3], papaya[4]);
                            roster_dat.Add(obj);
                        }
                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }

                //Basic Quick Sort
                Console.WriteLine("Basic Quick Sort");
                makeCopyRaw();
                printSorted();
                getQuickSort(0, roster_rep.Count - 1);
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
                Console.WriteLine("Recursive: " + rec);
                cp = 0;
                ex = 0;
                rec = 0;

                //Random Quick Sort
                Console.WriteLine("Random Quick Sort");
                makeCopyRaw();
                //printSorted();
                RandomizedQuickSort(0, roster_rep.Count - 1);
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
                Console.WriteLine("Recursive: " + rec);
                cp = 0;
                ex = 0;
                rec = 0;

                //Tail Recursive QuickSort
                Console.WriteLine("Tail Recursive Quick Sort");
                makeCopyRaw();
                //printSorted();
                TailRecursiveQuicksort(0, roster_rep.Count - 1);
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
                Console.WriteLine("Recursive: " + rec);
            }
        }

        //----------Algorithm Part Start!!!!!-------------

        //Regular QuickSort
        public static void getQuickSort(int p, int r) //p: start index, r: end index
        {
            rec++;
            if (p < r)
            {
                int q = Partition(p, r);
                getQuickSort(p, q - 1);
                getQuickSort(q + 1, r);
            }
        }

        public static int Partition(int p, int r)
        {
            Employee emp = roster_rep[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                cp++;
                if(roster_rep[j].getID() <= emp.getID())
                {
                    i++;
                    Employee temp1 = roster_rep[i];
                    roster_rep[i] = roster_rep[j];
                    roster_rep[j] = temp1;
                    ex++;
                    //Print
                    printSorted();
                }
            }
            Employee temp2 = roster_rep[i + 1];
            roster_rep[i + 1] = roster_rep[r];
            roster_rep[r] = temp2;
            ex++;
            //Print
            printSorted();
            return (i + 1);
        }

        //Randomized QuickSort
        public static void RandomizedQuickSort(int p, int r)
        {
            rec++;
            if (p < r)
            {
                int q = RandomizedPartition(p, r);
                RandomizedQuickSort(p, q - 1);
                RandomizedQuickSort(q + 1, r);
            }
        }

        public static int RandomizedPartition(int p, int r)
        {
            Random rand = new Random();
            Employee emp = roster_rep[r];
            int i = rand.Next(p,r);
            Employee temp2 = roster_rep[i];
            roster_rep[i] = roster_rep[r];
            roster_rep[r] = temp2;
            ex++;
            //Print
            //printSorted();
            return Partition(p,r);
        }
        //Tail Recursive Quicksort
        public static void TailRecursiveQuicksort(int p, int r)
        {
            while(p < r)
            {
                int q = Partition(p, r);
                TailRecursiveQuicksort(p, q - 1);
                p = q + 1;
                rec++;
            }
        }

        //-----Algorithm Part Over!!!!!------

        public static void writeSorted()
        {
            String filename = "sorted" + fileno + ".txt";
            using (StreamWriter file = File.CreateText(filename))
            {
                file.WriteLine(roster_rep.Count);
                foreach (Employee em in roster_rep)
                {
                    file.WriteLine(em.getFormat());
                }
            }
            fileno++;
        }

        public static void printSorted()
        {
            foreach (Employee emp in roster_rep)
            {
                Console.Write(emp.getID() + " ");
            }
            Console.WriteLine();
        }

        //Preserve Original Data
        public static void makeCopyRaw()
        {
            roster_rep.Clear();
            foreach (Employee emp in roster_dat)
            {
                roster_rep.Add(emp);
            }
            Console.WriteLine("Starting a new sequence.");
        }
    }

    class Employee
    {
        String name;
        String ID;
        String age;
        String job;
        String hireyear;

        public Employee(String n, String i, String a, String j, String h)
        {
            this.name = n;
            this.ID = i;
            this.age = a;
            this.job = j;
            this.hireyear = h;
        }
        public Employee(int i)
        {
            this.ID = i.ToString();
            this.name = null;
            this.age = null;
            this.job = null;
            this.hireyear = null;
        }

        public Boolean compareID(String id)
        {
            if (this.ID == id)
            {
                return true;
            }
            return false;
        }

        public String getName()
        {
            return this.name;
        }
        public int getID()
        {
            return int.Parse(this.ID);
        }

        public String getFormat()
        {
            return (this.name + "|" + this.ID + "|" + this.age + "|" + this.job + "|" + this.hireyear);
        }
    }
}

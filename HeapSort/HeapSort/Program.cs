using System;
using System.Collections.Generic;
using System.IO;

namespace HeapSort
{
    class Program
    {
        private static List<String> query_dat = new List<String>();
        private static List<Employee> roster_dat = new List<Employee>();
        private static int cp = 0;
        private static int ex = 0;
        private static int hsize = 0;
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
                        roster_dat.Add(null);
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

                //sorting seq
                getHeapsort();
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
            }
        }

        public static void maxHeapify(int i)
        {
            int l = 2 * i;
            int r = l + 1;
            int lrgst = -1;
            if (l <= hsize && roster_dat[l].getID() > roster_dat[i].getID())
            {
                lrgst = l;
            }
            else
            {
                lrgst = i;
            }
            if (r <= hsize && roster_dat[r].getID() > roster_dat[lrgst].getID())
            {
                lrgst = r;
            }
            cp = cp + 5;
            if (lrgst != i)
            {
                ex++;
                Employee temp = roster_dat[i];
                roster_dat[i] = roster_dat[lrgst];
                roster_dat[lrgst] = temp;
                maxHeapify(lrgst);

            }
        }

        public static void buildMaxHeap()
        {
            hsize = roster_dat.Count - 1;
            for(int i = (roster_dat.Count - 1) / 2; i >= 1; i--)
            {
                maxHeapify(i);
            }
        }

        public static void getHeapsort()
        {
            //print1
            print();
            Console.WriteLine("");
            buildMaxHeap();
            //print2
            print();
            Console.WriteLine("");
            for (int i = (roster_dat.Count - 1); i >= 2; i--)
            {
                ex++;
                Employee temp = roster_dat[1];
                roster_dat[1] = roster_dat[i];
                roster_dat[i] = temp;
                hsize--;
                maxHeapify(1);
                //print3
                print();
            }
        }


        public static void writeSorted()
        {
            using (StreamWriter file = File.CreateText("sorted.txt"))
            {
                file.WriteLine(roster_dat.Count);
                for(int i = 1; i < roster_dat.Count; i++)
                {
                    file.WriteLine(roster_dat[i].getFormat());
                }
            }
        }

        public static void print()
        {
            for(int i = 1; i < roster_dat.Count; i++)
            {
                Console.Write(roster_dat[i].getID() + " ");
            }
            Console.WriteLine("");
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

using System;
using System.Collections.Generic;
using System.IO;

namespace BinarySearch
{
    class Program
    {
        static List<String> query_dat = new List<String>();
        static List<Employee> roster_dat = new List<Employee>();
        static  double loop = 0;
        static double store = 0;
        static double dino = 0;

        static void Main(string[] args)
        {
            String roster = null; //roster.txt
            String query = null;
            Boolean filecheck = true;
            try
            {
                roster = args[0]; //roster.txt
                query = args[1]; //query.txt
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                filecheck = false;
            }
            if (filecheck == true)
            {
                Console.WriteLine("This is roster file you called: " + roster);
                Console.WriteLine("This is query file you called: " + query);
                try
                {
                    using (StreamReader sr = new StreamReader(query))
                    {
                        int nol = int.Parse(sr.ReadLine());
                        for (int i = 0; i < nol; i++)
                        {
                            query_dat.Add(sr.ReadLine());
                        }
                        Console.WriteLine("query call successful!");
                    }
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

                //searching seq
                foreach (String st in query_dat)
                {
                    int index = BinarySearch(int.Parse(st), 1, (roster_dat.Count));
                    Console.WriteLine("Looking for " + st + "... Found " + roster_dat[index].getName()  + " at position " + index + " after " + (int)(loop) + " comparisons.");
                    loop = 0;
                    dino++;

                }
                Console.WriteLine("Average number of comparisons overall: " + store / dino);
            }
        }
        //Search: what ID to find
        //low: start from 0
        //size: total search size
        public static int BinarySearch(int search, int btm, int size)
        {
            int lo = btm;
            int hi = Math.Max(lo, size + 1);
            while(lo < hi)
            {
                int mid = (lo + hi) / 2;
                if(search < int.Parse(roster_dat[mid].getID()))
                {
                    hi = mid;
                }
                else
                {
                    lo = mid + 1;
                }
                loop++;
            }
            store = store + loop;
            return (hi - 1);
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

        public String getID()
        {
            return this.ID;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LinearSearch
{
    class Program
    {
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
                List<String> query_dat = new List<String>();
                List<Employee> roster_dat = new List<Employee>();
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
                double loop = 0;
                double store = 0;
                double dino = 0;
                foreach (String st in query_dat)
                {
                    foreach (Employee emp in roster_dat)
                    {
                        if (emp.compareID(st) == true)
                        {
                            //Console.WriteLine("Looking for " + st + "... Found " + emp.getName() + " at position " + (int)(loop) + " after " + (int)(loop + 1) + " comparisons.");
                            break;
                        }
                        loop++;
                    }
                    store = store + loop + 1;
                    loop = 0;
                    dino++;
                }
                Console.WriteLine("Average number of comparisons overall: " + store / dino);
            }
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
            if(this.ID == id)
            {
                return true;
            }
            return false;
        }

        public String getName()
        {
            return this.name;
        }
    }
}

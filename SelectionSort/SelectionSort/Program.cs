using System;
using System.Collections.Generic;
using System.IO;

namespace SelectionSort
{
    class Program
    {
        private static List<String> query_dat = new List<String>();
        private static List<Employee> roster_dat = new List<Employee>();
        private static int cp = 0;
        private static int ex = 0;
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

                //sorting seq
                SelectionSort();
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
            }
        }

        public static void SelectionSort()
        {
            for(int i = 0; i < roster_dat.Count - 1; i++)
            {
                /*foreach(Employee emp in roster_dat)
                {
                    Console.Write(emp.getID() + " ");
                }
                Console.WriteLine();*/
                Employee min = roster_dat[i];
                int minind = i;
                for(int j = i; j < roster_dat.Count; j++)
                {
                    cp++;
                    if(roster_dat[j].getID() < min.getID())
                    {
                        min = roster_dat[j];
                        minind = j;
                    }
                }
                //Exchange
                ex++;
                Employee exc = roster_dat[minind];
                roster_dat[minind] = roster_dat[i];
                roster_dat[i] = exc;
            }
            foreach (Employee emp in roster_dat)
            {
                Console.Write(emp.getID() + " ");
            }
            Console.WriteLine();
        }

        public static void writeSorted()
        {
            using (StreamWriter file = File.CreateText("sorted.txt"))
            {
                file.WriteLine(roster_dat.Count);
                foreach (Employee em in roster_dat)
                {
                    file.WriteLine(em.getFormat());
                }
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

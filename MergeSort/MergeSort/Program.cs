using System;
using System.Collections.Generic;
using System.IO;

namespace MergeSort
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
                MergeSort(0, roster_dat.Count - 1);
                writeSorted();
                Console.WriteLine("Compare: " + cp);
                Console.WriteLine("Exchange: " + ex);
            }
        }

        public static void MergeSort(int p, int r)//p is beginning , r is end index
        {
            if(p < r)
            {
                int q = (int)((p + r) / 2);
                MergeSort(p, q);
                MergeSort(q + 1, r);
                Merge(p, q, r);

                //printing
                foreach (Employee emp in roster_dat)
                {
                    Console.Write(emp.getID() + " ");
                }
                Console.WriteLine();
            }
        }

        public static void Merge(int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            List<Employee> arr_l = new List<Employee>();
            List<Employee> arr_r = new List<Employee>();
            for(int i = 1; i <= n1; i++)
            {
                arr_l.Add(roster_dat[p + i - 1]);
            }
            for(int j = 1; j <= n2; j++)
            {
                arr_r.Add(roster_dat[q + j]);
            }
            arr_l.Add(new Employee(int.MaxValue));
            arr_r.Add(new Employee(int.MaxValue));
            int li = 0;
            int ri = 0;
            for(int k = p; k <= r; k++)
            {
                if(arr_l[li].getID() <= arr_r[ri].getID())
                {
                    roster_dat[k] = arr_l[li];
                    li++;
                }
                else
                {
                    roster_dat[k] = arr_r[ri];
                    ri++;
                }
                cp++;
            }
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

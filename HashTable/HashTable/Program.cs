using System;
using System.Collections.Generic;
using System.IO;

namespace HashTable
{
    class Program
    {
        private static List<String> query_dat = new List<String>();
        private static List<Employee> roster_dat = new List<Employee>();
        private static List<Employee>[] chainhash;
        private static int m; //Array size m
        private static int col = 0; //collision
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
                        m = int.Parse(sr.ReadLine());
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
                Employee[] hashtable = new Employee[m];

                //sorting seq
                //divisionHashTable(hashtable, m);
                //multipleHashTable(hashtable, m);
                gracefulHashTable(m);
                Console.WriteLine("Total collisions: " + col);
                //writeSorted();
            }
        }

        /*
         * Receives hash table and the size and do the division method
         * h(k) = key%m
         * value of m is critical, cannot be Power of 2, far from power of 2.        
         */        
        public static void divisionHashTable(Employee[] hashtable, int m)
        {
            foreach(Employee em in roster_dat)
            {
                int hk = em.getID() % m;
                Console.Write("Attempting to hash " + em.getName() + " at index " + hk + "...");
                if (hashtable[hk] == null)
                {
                    hashtable[hk] = em;
                    Console.Write("Success!\n");
                }
                else
                {
                    Console.Write("OUCH! Collision with " + hashtable[hk].getName() +"!\n");
                    col++;
                }
            }
        }

        /*
         * Receives hash table and the size and do the multiplication method
         * h(k) = floor(m*(kA%1))
         * value m is not so much critical (Power of 2 is allowed.)        
         */
        public static void multipleHashTable(Employee[] hashtable, int m)
        {
            double a = 0.6180339887;
            foreach(Employee em in roster_dat)
            {
                double fka = ((double)em.getID() * a)%1; //functional kA
                int hk = (int)Math.Floor(m * fka);
                Console.Write("Attempting to hash " + em.getName() + " at index " + hk + "...");
                if (hashtable[hk] == null)
                {
                    hashtable[hk] = em;
                    Console.Write("Success!\n");
                }
                else
                {
                    Console.Write("OUCH! Collision with " + hashtable[hk].getName() + "!\n");
                    col++;
                }
            }
        }
        /*
         * Receives hash table and the size and do the division method with chaning collision protector.
         * 
         */
        public static void gracefulHashTable(int m)
        {
            //chainhash initialization
            chainhash = new List<Employee>[m];

            //division method, save it into chainhash.
            foreach (Employee em in roster_dat)
            {
                int hk = em.getID() % m;
                Console.Write("Adding " + em.getName() + " to table at index " + hk);
                if (chainhash[hk] == null)
                {
                    chainhash[hk] = new List<Employee>();
                    chainhash[hk].Add(em);
                    Console.Write(" (0 collisions)\n");
                }
                else
                {
                    int loc_col = chainhash[hk].Count; //local collision for each elements
                    Console.Write(" (" + loc_col + "collisions)\n");
                    chainhash[hk].Add(em);
                    col++;
                }
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

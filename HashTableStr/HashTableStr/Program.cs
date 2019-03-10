using System;
using System.Collections.Generic;
using System.IO;

namespace HashTableStr
{
    class Program
    {
        private static List<String> query_dat = new List<String>();
        private static List<Employee> roster_dat = new List<Employee>();
        private static List<String> lookup = new List<String>();
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

                        sr.ReadLine();
                        int ool = int.Parse(sr.ReadLine());
                        for(int i = 0; i < ool; i++)
                        {
                            lookup.Add(sr.ReadLine());
                        }
                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }

                gracefulHashTable(m);
                Console.WriteLine("Total collisions: " + col);
                //writeSorted();
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
                int key = perseStringtoInt(em.getName());
                int hk = key % m;
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
            foreach(String str in lookup)
            {
                lookupHashTable(str);
            }
        }

        public static void lookupHashTable(string name)
        {
            int key = perseStringtoInt(name);
            int lookupkey = key % m;
            int loc_col = 0;
            if(chainhash[lookupkey] == null)
            {
                Console.WriteLine(name + "is not found.");
            }
            else
            {
                foreach(Employee em in chainhash[lookupkey])
                {
                    if(em.getName() == name)
                    {
                        Console.WriteLine("Found " + name + " after " + loc_col + " collisions at index " + lookupkey + " in the hashtable.");
                        Console.WriteLine("COMPLETE RECORD: " + em.getFormat());
                    }
                    loc_col++;
                }
            }
        }

        private static int perseStringtoInt(string str)
        {
            int result = 0;
            foreach(char c in str)
            {
                result = result + (int)c;
            }
            return result;
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

using System;
using System.IO;
using System.Collections;

namespace FileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            //User input read
            Console.WriteLine("This is file you called: ");
            String filename = args[0] + ".txt";
            Console.WriteLine(filename);
            ArrayList data = new ArrayList();
            //Error Catching caused by file reading
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    int ln = int.Parse(sr.ReadLine());
                    for (int i = 0; i < ln; i++)
                    {
                        String line = sr.ReadLine();
                        String[] papaya = line.Split('|');
                        Employee obj = new Employee(papaya[0], papaya[1], papaya[2], papaya[3], papaya[4]);
                        data.Add(obj);
                    }
                }
                
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            //Printing part
            foreach(Employee e in data)
            {
                e.print();
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

            //Well-Formatted printing
            public void print()
            {
                Console.WriteLine("Name : " + "\t" + "\t" + name);
                Console.WriteLine("ID: " + "\t" + "\t" + ID);
                Console.WriteLine("Age: " + "\t" + "\t" + age);
                Console.WriteLine("Job: " + "\t" + "\t" + job);
                Console.WriteLine("Hire Year: " + "\t" + hireyear);
                Console.WriteLine("**********************************");
            }
        }
    }
}

using System;
using System.IO;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StreamReader sr = new StreamReader("input.txt");
            String firstLine = sr.ReadLine();
            int n = int.Parse(firstLine);
            String[] res = new string[n];
            for(int i = 0; i < n; i++)
            {
                String line = sr.ReadLine();
                //Console.WriteLine(line);
                res[i] = line;
            }
            foreach(String s in res)
            {
                Console.WriteLine(s);
                if(s == "This")
                {
                    Console.WriteLine(s);
                    Console.WriteLine(s);
                }
            }
        }
    }
}

using System;
using System.IO;

namespace LCS
{
    class Program
    {
        static String ln1;
        static String ln2;
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
                        ln1 = sr.ReadLine();
                        ln2 = sr.ReadLine();

                        Console.WriteLine("roster call successful!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine("String X:" + ln1);
            Console.WriteLine("String Y:" + ln2);
            Console.Write("LSC: ");
            LCSlength(ln1, ln2);
        }

        public static void LCSlength(String ln1, String ln2)
        {
            int m = ln1.Length;
            int n = ln2.Length;
            int[,] c = new int[m + 1, n + 1];//0->m, 0->n
            char[,] b = new char[m + 1, n + 1]; //1->m, 1->n
            for (int j = 0; j <= n; j++)
            {
                c[0, j] = 0;
                b[0, j] = '0';
            }
            for (int i = 1; i <= m; i++)
            {
                c[i, 0] = 0;
                b[i, 0] = '0';
            }
            for(int i = 1; i <= m; i++)
            {
                for(int j = 1; j <= n; j++)//'d' for diagonal arrow, 'u' for up arrow, 'l' for left arrow
                {
                    if (ln1[i - 1] == ln2[j - 1])
                    {
                        c[i, j] = c[i - 1, j - 1] + 1;
                        b[i, j] = 'd';
                    }
                    else if (c[i - 1, j] >= c[i, j - 1])
                    {
                        c[i, j] = c[i - 1, j];
                        b[i, j] = 'u';
                    }
                    else
                    {
                        c[i, j] = c[i, j - 1];
                        b[i, j] = 'l';
                    }
                }
            }
            printLCS(b, ln1, m, n);
        }

        public static void printLCS(char[,] b, String ln1, int m, int n)
        {
            if(m == 0 || n == 0)
            {
                Console.Write("\n");
                return;
            }
            if(b[m,n] == 'd')
            {
                printLCS(b, ln1, m - 1, n - 1);
                Console.Write(ln1[m - 1]);
            }
            else if(b[m,n] == 'u')
            {
                printLCS(b, ln1, m - 1, n);
            }
            else
            {
                printLCS(b, ln1, m, n - 1);
            }
        }
    }
}

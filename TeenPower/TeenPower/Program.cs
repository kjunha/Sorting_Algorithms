using System;

namespace TeenPower
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Dexter's Console Game: Teen Power!");
                Console.WriteLine("How old are you?");
                int input = int.Parse(Console.ReadLine());
                if (input >= 0)
                {
                    //Upper 19
                    if (input >= 20)
                    {
                        int cal = input - 19;
                        Console.WriteLine("You lost your teen power " + cal + " years ago.");
                    }
                    //Between 13 to 19
                    else if (input <= 20 && input >= 13)
                    {
                        for (int i = 0; i < input; i++)
                        {
                            Console.WriteLine("Go Teen Power!!");
                        }
                    }
                    //Lower than 13
                    else if (input < 13)
                    {
                        int cal = 13 - input;
                        Console.WriteLine("You don't have Teen Power yet.");
                        Console.WriteLine("Try again after " + cal +" years later.");
                    }
                }
                else
                {
                    Console.WriteLine("Your age has to be greater than 0!");
                }
                Console.WriteLine("Do you want to try it again? (y/n)");
                String inp = Console.ReadLine();
                inp.ToLower();
                if (inp == "y")
                {
                    continue;
                }
                else if (inp == "n")
                {
                    Console.WriteLine("Thank you for playing!");
                    break;
                }
            }

        }
    }
}

using System;

namespace Testcase
{
    public static class Program
    {
        static void Main()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Input your expression below and press enter to calculate:");
                var expressionToCalculate = Console.ReadLine();

                Console.WriteLine("\nResult:\n" + Calculator.Calculate(expressionToCalculate) + 
                    "\nPress enter to calculate again or escape to exit");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}

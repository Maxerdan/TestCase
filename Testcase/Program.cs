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

                try
                {
                    Console.WriteLine("\nResult:\n" + Calculator.Calculate(expressionToCalculate));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Press enter to calculate again or escape to exit");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}

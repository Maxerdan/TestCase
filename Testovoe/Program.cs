using System;
using System.Collections.Generic;

namespace Testovoe
{
    public static class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Input your expression below and press enter to calculate:");
                var expressionToCalculate = Console.ReadLine();

                Console.WriteLine("\nResult:\n" + Calculate(expressionToCalculate) + "\nPress enter to calculate again or escape to exit");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        public static string Calculate(string expression)
        {
            Stack<Entity> nums = new Stack<Entity>();
            Stack<Entity> operations = new Stack<Entity>();

            var parseException = ParseExpression(expression, nums, operations);
            if (parseException != "")
                return parseException;
            while (operations.Count != 0)
            {
                var operationException = DoOperation(nums, operations);
                if (operationException != "")
                    return operationException;
            }

            return Math.Round(nums.Pop().Value, 4).ToString();
        }

        private static string ParseExpression(string expression, Stack<Entity> nums, Stack<Entity> operations)
        {
            Entity entity;
            bool unaryOpFlag = false;
            expression = expression.Replace(" ", "");

            var exceptions = Exceptions.CheckForExceptions(expression);
            if (exceptions != "")
                return exceptions;

            for (var i = 0; i < expression.Length; i++)
            {
                entity = new Entity();
                if (expression[i].IsNum() || expression[i] == '-' && !unaryOpFlag) // num
                {
                    string num = expression[i].ToString();
                    unaryOpFlag = true;
                    while (expression.Length != i + 1 && !expression[i + 1].IsOperation(unaryOpFlag))
                    {
                        if (expression[i + 1] == ')')
                            break;
                        if (expression[i + 1].IsNum() || expression[i + 1] == '.')
                        {
                            num += expression[i + 1];
                        }

                        i++;
                    }
                    entity.Type = '0';
                    entity.Value = float.Parse(num.Replace('.', ','));
                    nums.Push(entity);
                }
                else if (expression[i].IsOperation(unaryOpFlag)) // operation
                {
                    if (operations.Count == 0)
                    {
                        entity.Type = expression[i];
                        entity.Value = 0;
                        operations.Push(entity);
                    }
                    else if (operations.Count != 0 && GetRang(expression[i]) > GetRang(operations.Peek().Type))
                    {
                        entity.Type = expression[i];
                        entity.Value = 0;
                        operations.Push(entity);
                    }
                    else if (operations.Count != 0 && GetRang(expression[i]) <= GetRang(operations.Peek().Type))
                    {
                        var operationException = DoOperation(nums, operations);
                        if (operationException != "")
                            return operationException;
                        i--;
                    }
                }
                else if (expression[i] == '(')
                {
                    entity.Type = expression[i];
                    entity.Value = 0;
                    operations.Push(entity);
                }
                else if (expression[i] == ')')
                {
                    while (operations.Peek().Type != '(')
                    {
                        var operationException = DoOperation(nums, operations);
                        if (operationException != "")
                            return operationException;
                    }
                    operations.Pop();
                }
                else // exception
                {
                    return $"Exception! - Wrong symbol (not number or operating): {expression[i]}";
                }
            }

            return ""; // no exceptions
        }

        private static string DoOperation(Stack<Entity> nums, Stack<Entity> operations)
        {
            Entity entity = new Entity(); ;
            float a, b, c;
            a = nums.Pop().Value;
            b = nums.Pop().Value;

            switch (operations.Pop().Type)
            {
                case '+':
                    {
                        c = a + b;
                        break;
                    }
                case '-':
                    {
                        c = b - a;
                        break;
                    }
                case '*':
                    {
                        c = a * b;
                        break;
                    }
                case '/':
                    {
                        if (a == 0)
                            return "Exception! - Division by zero";
                        c = b / a;
                        break;
                    }
                default:
                    return "UnexpectableException!";
            }
            entity.Type = '0';
            entity.Value = c;
            nums.Push(entity);

            return ""; // no exceptions
        }

        private static int GetRang(char sym)
        {
            if (sym == '+' || sym == '-') return 1;
            if (sym == '*' || sym == '/') return 2;
            return 0;
        }

        private static bool IsOperation(this char sym, bool unaryOpFlag)
        {
            return sym == '+' || sym == '-' && unaryOpFlag || sym == '*' || sym == '/';
        }

        private static bool IsNum(this char sym)
        {
            return char.IsDigit(sym);
        }
    }
}

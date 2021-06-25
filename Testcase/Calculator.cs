using System;
using System.Collections.Generic;

namespace Testcase
{
    public static class Calculator
    {
        public static string Calculate(string expression)
        {
            var nums = new Stack<float>();
            var operations = new Stack<char>();

            var parseException = ParseExpression(expression, nums, operations);
            if (parseException != "")
                return parseException;
            while (operations.Count != 0)
            {
                var operationException = DoOperation(nums, operations);
                if (operationException != "")
                    return operationException;
            }

            if (nums.Count == 1)
                return Math.Round(nums.Pop(), 4).ToString();
            else
                return "Exception! - No numbers in expression";
        }

        private static string ParseExpression(string expression, Stack<float> nums, Stack<char> operations)
        {
            bool unaryOpFlag = false;
            expression = expression.Replace(" ", "");

            var exceptions = Exceptions.CheckForExceptions(expression);
            if (exceptions != "")
                return exceptions;

            for (var i = 0; i < expression.Length; i++)
            {
                if (operations.Count > 0 && expression[i - 1] == '(' && expression[i] == '-')
                    unaryOpFlag = false;
                if (expression[i].IsNum() || expression[i] == '-' && !unaryOpFlag) // num
                {
                    unaryOpFlag = true;
                    string num = expression[i].ToString();

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
                    var number = float.Parse(num.Replace('.', ','));
                    nums.Push(number);
                }
                else if (expression[i].IsOperation(unaryOpFlag)) // operation
                {
                    if (operations.Count == 0)
                    {
                        operations.Push(expression[i]);
                    }
                    else if (operations.Count != 0 && GetRang(expression[i]) > GetRang(operations.Peek()))
                    {
                        operations.Push(expression[i]);
                    }
                    else if (operations.Count != 0 && GetRang(expression[i]) <= GetRang(operations.Peek()))
                    {
                        var operationException = DoOperation(nums, operations);
                        if (operationException != "")
                            return operationException;
                        i--;
                    }
                }
                else if (expression[i] == '(')
                {
                    operations.Push(expression[i]);
                }
                else if (expression[i] == ')')
                {
                    while (operations.Peek() != '(')
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

        private static string DoOperation(Stack<float> nums, Stack<char> operations)
        {
            float a, b, c;
            a = nums.Pop();
            b = nums.Pop();

            switch (operations.Pop())
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
            nums.Push(c);

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

using System;
using System.Collections.Generic;

namespace Testcase
{
    public static class Calculator
    {
        public static float Calculate(string expression)
        {
            var nums = new Stack<float>();
            var operations = new Stack<char>();

            ParseExpression(expression, nums, operations);
            while (operations.Count != 0)
            {
                DoOperation(nums, operations);
            }

            if (nums.Count == 1)
                return float.Parse(Math.Round(nums.Pop(), 4).ToString());
            else
                throw new Exception("Exception! - No numbers in expression");
        }

        private static void ParseExpression(string expression, Stack<float> nums, Stack<char> operations)
        {
            bool unaryOpFlag = true;
            expression = expression.Replace(" ", "");

            Exceptions.CheckForExceptions(expression);

            for (var i = 0; i < expression.Length; i++)
            {
                if (IsUnaryBracket(expression, operations, i))
                    unaryOpFlag = true;

                if (char.IsDigit(expression[i]) || expression[i] == '-' && unaryOpFlag)
                {
                    unaryOpFlag = false;
                    ReadNum(expression, nums, unaryOpFlag, ref i);
                    continue;
                }

                if (expression[i].IsOperation(unaryOpFlag))
                {
                    ReadOperation(expression, nums, operations, ref i);
                    continue;
                }

                if (expression[i] == '(')
                {
                    operations.Push(expression[i]);
                    continue;
                }

                if (expression[i] == ')')
                {
                    while (operations.Peek() != '(')
                    {
                        DoOperation(nums, operations);
                    }
                    operations.Pop();
                    continue;
                }

                throw new Exception($"Exception! - Wrong symbol (not number or operating): {expression[i]}");
            }
        }

        private static bool IsUnaryBracket(string expression, Stack<char> operations, int i)
        {
            return operations.Count > 0 && expression[i - 1] == '(' && expression[i] == '-';
        }

        private static void ReadNum(string expression, Stack<float> nums, bool unaryOpFlag, ref int i)
        {
            string num = expression[i].ToString();

            while (expression.Length != i + 1 && !expression[i + 1].IsOperation(unaryOpFlag))
            {
                if (expression[i + 1] == ')')
                    break;
                if (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.')
                {
                    num += expression[i + 1];
                }

                i++;
            }
            var number = float.Parse(num.Replace('.', ','));
            nums.Push(number);
        }

        private static void ReadOperation(string expression, Stack<float> nums, Stack<char> operations, ref int i)
        {
            if (operations.Count == 0)
            {
                operations.Push(expression[i]);
            }
            else if (GetRang(expression[i]) > GetRang(operations.Peek()))
            {
                operations.Push(expression[i]);
            }
            else if (GetRang(expression[i]) <= GetRang(operations.Peek()))
            {
                DoOperation(nums, operations);
                i--;
            }
        }

        private static void DoOperation(Stack<float> nums, Stack<char> operations)
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
                            throw new DivideByZeroException("Exception! - Division by zero");
                        c = b / a;
                        break;
                    }
                default:
                    throw new Exception("UnexpectableException!");
            }
            nums.Push(c);
        }

        private static int GetRang(char sym)
        {
            if (sym == '+' || sym == '-') return 1;
            if (sym == '*' || sym == '/') return 2;
            return 0;
        }

        private static bool IsOperation(this char sym, bool unaryOpFlag)
        {
            return sym == '+' || sym == '-' && !unaryOpFlag || sym == '*' || sym == '/';
        }
    }
}

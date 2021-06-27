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
            bool unaryOpFlag = false;
            expression = expression.Replace(" ", "");

            Exceptions.CheckForExceptions(expression);

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

                        DoOperation(nums, operations);
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

                        DoOperation(nums, operations);

                    }
                    operations.Pop();
                }
                else
                {
                    throw new Exception($"Exception! - Wrong symbol (not number or operating): {expression[i]}");
                }
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
            return sym == '+' || sym == '-' && unaryOpFlag || sym == '*' || sym == '/';
        }

        private static bool IsNum(this char sym)
        {
            return char.IsDigit(sym);
        }
    }
}

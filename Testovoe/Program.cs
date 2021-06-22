using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovoe
{
    public static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var expressionToCount = Console.ReadLine();
                Console.WriteLine(Calculate(expressionToCount));
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static float Calculate(string expression)
        {
            Stack<Entity> nums = new Stack<Entity>();
            Stack<Entity> operations = new Stack<Entity>();

            ParseExpression(expression, nums, operations);
            while (operations.Count != 0)
                DoOperation(nums, operations);

            return (float)Math.Round(nums.Pop().Value, 4);
        }

        private static void ParseExpression(string expression, Stack<Entity> nums, Stack<Entity> operations)
        {
            Entity entity;
            bool unaryOpFlag = false;

            // need to check expression

            for (var i = 0; i < expression.Length; i++)
            {
                entity = new Entity();
                if (expression[i].IsNum() || expression[i] == '-' && !unaryOpFlag) // num
                {
                    string num = expression[i].ToString();
                    unaryOpFlag = true;
                    while (expression.Length != i + 1 && !expression[i + 1].IsOperation(unaryOpFlag))
                    {
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
                        DoOperation(nums, operations);
                        i--;
                    }
                }
                else // exception
                {
                    throw new Exception("Exception");
                }
            }
        }

        private static void DoOperation(Stack<Entity> nums, Stack<Entity> operations)
        {
            Entity entity;
            float a, b, c;
            a = nums.Pop().Value;
            entity = new Entity();
            switch (operations.Pop().Type)
            {
                case '+':
                    {
                        b = nums.Pop().Value;
                        c = a + b;
                        entity.Type = '0';
                        entity.Value = c;
                        nums.Push(entity);
                        break;
                    }
                case '-':
                    {
                        b = nums.Pop().Value;
                        c = b - a;
                        entity.Type = '0';
                        entity.Value = c;
                        nums.Push(entity);
                        break;
                    }
                case '*':
                    {
                        b = nums.Pop().Value;
                        c = a * b;
                        entity.Type = '0';
                        entity.Value = c;
                        nums.Push(entity);
                        break;
                    }
                case '/':
                    {
                        b = nums.Pop().Value;
                        //check for /0
                        c = b / a;
                        entity.Type = '0';
                        entity.Value = c;
                        nums.Push(entity);
                        break;
                    }
            }
        }

        static int GetRang(char sym)
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

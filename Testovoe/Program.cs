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
            var expressionToCount = Console.ReadLine();
            Calculate(expressionToCount);
        }

        public static float Calculate(string expression)
        {
            Stack<Entity> nums = new Stack<Entity>();
            Stack<Entity> operations = new Stack<Entity>();

            ParseExpression(expression, nums, operations);
            DoOperations(nums, operations);

            return (float)Math.Round(nums.Pop().Value, 4); // test round
        }

        private static void ParseExpression(string expression, Stack<Entity> nums, Stack<Entity> operations)
        {
            Entity entity;

            // need to check expression

            for (var i = 0; i < expression.Length; i++)
            {
                entity = new Entity();
                if (expression[i].IsNum()) // num
                {
                    string num = expression[i].ToString();
                    while (expression.Length != i + 1 && !expression[i + 1].IsOperation())
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
                else if (expression[i].IsOperation()) // operation
                {
                    entity.Type = expression[i];
                    entity.Value = 0;
                    operations.Push(entity);
                }
                else // exception
                {
                    throw new Exception("Exception");
                }
            }
        }

        private static void DoOperations(Stack<Entity> nums, Stack<Entity> operations)
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

        private static bool IsOperation(this char sym)
        {
            return sym == '+' || sym == '-' || sym == '*' || sym == '/';
        }
        private static bool IsNum(this char sym)
        {
            return char.IsDigit(sym);
        }
    }
}

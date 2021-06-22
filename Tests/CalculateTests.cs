using NUnit.Framework;
using Testovoe;

namespace Tests
{
    public class CalculateTest
    {
        [TestCase("1+2", 3f)]
        [TestCase("3-4", -1f)]
        [TestCase("5*6", 30f)]
        [TestCase("7/7", 1f)]
        [TestCase("1.1+2.0", 3.1f)]
        [TestCase("3.3-4.4", -1.1f)]
        [TestCase("5.5*6.6", 36.3f)]
        [TestCase("7.7/8.8", 0.875f)]
        [TestCase("-1-2", -3f)]
        public void Simple(string expression, float result)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        [TestCase("1+2-7", -4f)]
        [TestCase("1-2+7", 6f)]
        [TestCase("1+2+7", 10f)]
        [TestCase("1-2-7", -8f)]
        [TestCase("1*2-7", -5f)]
        [TestCase("1-2*7", -13f)]
        [TestCase("1*2*7", 14f)]
        [TestCase("1*2/4", 0.5f)]
        [TestCase("1/2*7", 3.5f)]
        [TestCase("1/2/4", 0.125f)]
        [TestCase("1-2/4", 0.5f)]
        [TestCase("1/2-7", -6.5f)]
        public void TwoOperations(string expression, float result)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        [TestCase("1+2+4+7+8", 22f)]
        [TestCase("1+2-4+7-8", -2f)]
        [TestCase("1-2+4+7-8", 2f)]
        [TestCase("1-2+4+7+8", 18f)]
        [TestCase("1+2+4+7-8", 6f)]
        [TestCase("1+2-4*7+8", -17f)]
        [TestCase("1+2*4-7+8", 10f)]
        [TestCase("1+2*4*7+8", 65f)]
        [TestCase("1*2+4*7+8", 38f)]
        [TestCase("1*2*4+7*8", 64f)]
        [TestCase("1-2+4/8+8", 7.5f)]
        [TestCase("1-2/4/8+8", 8.9375f)]
        [TestCase("1-2+4/8/8", -0.9375f)]
        [TestCase("1/2/4/8-8", -7.984375f)]
        [TestCase("1/2*4/8-8", -7.75f)]
        [TestCase("1/2*4*7/8", 1.75f)]
        public void FourOperations(string expression, float result)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        [TestCase("2/2*4*7/8-6+5*60/10-20+40*5*2*10-80/20/10", 4007.1f)]
        [TestCase("1-1*20*5-80-2+20+10-80/100/2*100*20", -951f)]
        public void ComplexOperations(string expression, float result)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        [TestCase("(1-1*20*5-80)-2+20+10-80/100/2*100*20", -951f)]
        [TestCase("((1-1*20)*5-80)-2+20+10-80/100/2*100*20", -947f)]
        [TestCase("(1-1*(20*5-80))-2+20+10-80/100/2*100*20", -791f)]
        [TestCase("(1-1*20*5-80)-(2+20+10-80/100)/2*100*20", -31379f)]
        [TestCase("((1-1*20*5-80)-2+20+10-80/100)/2*100*20", -151800f)]
        [TestCase("(1-1*20*5-80)-2+20+(10-(80/100)/2*100*20)", -951f)]
        public void BracketsOperations(string expression, float result)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        //exception test
        [TestCase("(1-1*20*5-80-2+20+(10-(80/100)/2*100*20)", "BracketException")]
        [TestCase("(1-1*20*5-80)-2+20+(10-80/100)/2*100*20)", "BracketException")]
        [TestCase("abc", "SymbolException")]
        [TestCase("1a*2*4+7*8", "SymbolException")]
        [TestCase("1.*2*4+7*8", "Exception")]
        [TestCase("1*2*4++7*8", "Exception")]
        public void WrongExpression(string expression, string exception)
        {
            var countResult = Program.Calculate(expression);
            Assert.That(countResult, Is.EqualTo(exception));
        }
    }
}
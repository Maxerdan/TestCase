using NUnit.Framework;
using Testovoe;

namespace Tests
{
    public class CountResultTest
    {
        [TestCase("1+2", 3f)]
        [TestCase("3-4", -1f)]
        [TestCase("5*6", 30f)]
        [TestCase("7/7", 1f)]
        [TestCase("1.1+2.0", 3.1f)]
        [TestCase("3.3-4.4", -1.1f)]
        [TestCase("5.5*6.6", 36.3f)]
        [TestCase("7.7/8.8", 0.875f)]
        public void Simple(string expression, float result)
        {
            float countResult = Program.CountResult(expression);
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
            float countResult = Program.CountResult(expression);
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
            float countResult = Program.CountResult(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }

        [TestCase("1/2*4*7/8", 1.75f)]
        public void ComplexOperations(string expression, float result)
        {
            float countResult = Program.CountResult(expression);
            Assert.That(countResult, Is.EqualTo(result));
        }
    }
}
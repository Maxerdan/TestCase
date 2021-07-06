using NUnit.Framework;
using System;
using System.Diagnostics;
using Testcase;
using System.Collections;

namespace Tests
{
    class MyListTests
    {
        [Test]
        public void Simple()
        {
            MyList<int> myList = new MyList<int>();

            Assert.That(myList.Count, Is.EqualTo(0));
            Assert.That(myList.IsReadOnly, Is.EqualTo(false));

            for (int i = 0; i < 10; i++)
            {
                myList.Add(i);
            }
            Assert.That(myList.Count, Is.EqualTo(10));

            for (int i = 0; i < 10; i++)
            {
                Assert.That(myList.IndexOf(i), Is.EqualTo(i));
            }

            Assert.That(myList.Remove(0), Is.EqualTo(true));
            Assert.That(myList.Remove(9), Is.EqualTo(true));
            Assert.That(myList.Remove(0), Is.EqualTo(false));
            Assert.That(myList.IndexOf(0), Is.EqualTo(-1));

            myList.RemoveAt(myList.IndexOf(2));
            Assert.That(myList.IndexOf(2), Is.EqualTo(-1));
            Assert.That(string.Join("", myList), Is.EqualTo("1345678"));

            myList.RemoveAt(myList.IndexOf(1));
            Assert.That(string.Join("", myList), Is.EqualTo("345678"));

            myList.RemoveAt(myList.IndexOf(8));
            Assert.That(string.Join("", myList), Is.EqualTo("34567"));
            Assert.That(myList.Count, Is.EqualTo(5));

            myList.Add(4000);
            myList.Add(4000);
            Assert.That(string.Join("", myList), Is.EqualTo("3456740004000"));

            var count = myList.Count;
            for (int i = 0; i < count; i++)
            {
                myList.RemoveAt(0);
            }
            Assert.That(myList.Count, Is.EqualTo(0));
            Assert.That(string.Join("", myList), Is.EqualTo(""));
        }

        [Test]
        public void CopyToIndexOutOfRange()
        {
            MyList<int> myList = new MyList<int>();
            for (int i = 0; i < 5; i++)
            {
                myList.Add(i);
            }
            var array = new int[8] { 7, 7, 7, 7, 7, 7, 7, 7 };

            myList.CopyTo(array, 1);
            Assert.That(string.Join("", array), Is.EqualTo("70123477"));

            try
            {
                myList.CopyTo(array, 5);
                Assert.Fail();
            }
            catch(IndexOutOfRangeException e)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void MemoryUsage()
        {
            MyList<int> myList = new MyList<int>();
            for (int i = 0; i < 365; i++)
            {
                myList.Add(i);
            }

            myList.Clear();

            Process proc = Process.GetCurrentProcess();
            Console.WriteLine(proc.PrivateMemorySize64);
        }

        [Test]
        public void NullElements()
        {
            MyList<int?> myList = new MyList<int?>();

            Assert.That(myList.Count, Is.EqualTo(0));
            Assert.That(myList.IsReadOnly, Is.EqualTo(false));

            for (int i = 0; i < 10; i++)
            {
                myList.Add(i);
            }
            myList[1] = null;
            Assert.That(myList.Count, Is.EqualTo(10));

            for (int i = 0; i < 10; i++)
            {
                Assert.That(myList.IndexOf(i), Is.EqualTo(i));
            }

            Assert.That(myList.Remove(0), Is.EqualTo(true));
            Assert.That(myList.Remove(9), Is.EqualTo(true));
            Assert.That(myList.Remove(0), Is.EqualTo(false));
            Assert.That(myList.IndexOf(0), Is.EqualTo(-1));

            myList.RemoveAt(myList.IndexOf(2));
            Assert.That(myList.IndexOf(2), Is.EqualTo(-1));
            Assert.That(string.Join("", myList), Is.EqualTo("1345678"));

            myList.RemoveAt(myList.IndexOf(1));
            Assert.That(string.Join("", myList), Is.EqualTo("345678"));

            myList.RemoveAt(myList.IndexOf(8));
            Assert.That(string.Join("", myList), Is.EqualTo("34567"));
            Assert.That(myList.Count, Is.EqualTo(5));

            myList.Add(4000);
            myList.Add(4000);
            Assert.That(string.Join("", myList), Is.EqualTo("3456740004000"));

            var count = myList.Count;
            for (int i = 0; i < count; i++)
            {
                myList.RemoveAt(0);
            }
            Assert.That(myList.Count, Is.EqualTo(0));
            Assert.That(string.Join("", myList), Is.EqualTo(""));
        }
    }
}

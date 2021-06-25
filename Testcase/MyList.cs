using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase
{
    public class MyList<T> : IList<T>
    {
        private T[] _items;
        private int _count;
        private readonly int _size = 8;

        public MyList()
        {
            _items = new T[_size];
            _count = 0;
        }

        public T this[int index] { get => _items[index]; set => _items[index] = value; }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_count == _items.Length)
                DoubleUpSize();

            _items[_count] = item;
            _count++;
        }

        public void Clear()
        {
            _items = new T[_size];
            _count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(_items[i], arrayIndex++);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEntity<T>(_items, _count);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 && index > Count)
                throw new ArgumentOutOfRangeException();

            if (Count == _items.Length)
                DoubleUpSize();

            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
            _count++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 && index >= Count)
                throw new ArgumentOutOfRangeException();

            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
            if (Count == _items.Length)
                HalveSize();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void DoubleUpSize()
        {
            var contentsTemp = _items;
            var newSize = _items.Length * 2;
            _items = new T[newSize];
            for (int i = 0; i < Count; i++)
            {
                _items[i] = contentsTemp[i];
            }
        }

        private void HalveSize()
        {
            var contentsTemp = _items;
            var newSize = _items.Length / 2;
            _items = new T[newSize];
            for (int i = 0; i < Count; i++)
            {
                _items[i] = contentsTemp[i];
            }
        }
    }
}

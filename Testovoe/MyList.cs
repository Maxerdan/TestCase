﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testovoe
{
    class MyList<T> : IList<T>
    {
        private T[] _contents;
        private int _count;

        public MyList()
        {
            _contents = new T[8];
            _count = 0;
        }

        public T this[int index] { get => _contents[index]; set => _contents[index] = value; }

        public int Count => _count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_count == _contents.Length)
                IncreaseContentSize();

            _contents[_count] = item;
            _count++;
        }

        public void Clear()
        {
            _contents = new T[8];
            _count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_contents[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(_contents[i], arrayIndex++);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_contents[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index >= 0 && index < Count)
            {
                if (Count == _contents.Length)
                    IncreaseContentSize();
                for (int i = Count; i > index; i--)
                {
                    _contents[i] = _contents[i - 1];
                }
                _contents[index] = item;
                _count++;
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index != -1) // found index
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _contents[i] = _contents[i + 1];
                }
                _count--;
                if (Count == _contents.Length)
                    DecreaseContentSize();
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void IncreaseContentSize()
        {
            var contentsTemp = _contents;
            var newSize = _contents.Length * 2;
            _contents = new T[newSize];
            for (int i = 0; i < Count; i++)
            {
                _contents[i] = contentsTemp[i];
            }
        }

        private void DecreaseContentSize()
        {
            var contentsTemp = _contents;
            var newSize = _contents.Length / 2;
            _contents = new T[newSize];
            for (int i = 0; i < Count; i++)
            {
                _contents[i] = contentsTemp[i];
            }
        }
    }
}

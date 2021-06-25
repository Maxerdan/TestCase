using System;
using System.Collections;
using System.Collections.Generic;

namespace Testcase
{
    public class MyListEntity<T> : IEnumerator<T>
    {
        private T[] _items;
        private int _position;
        private readonly int _count;
        private bool IsDisposed = false;

        public MyListEntity(T[] contents, int count)
        {
            _items = contents;
            _position = -1;
            _count = count;
        }

        public T Current
        {
            get
            {
                try
                {
                    return _items[_position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _count);
        }

        public void Reset()
        {
            _position = -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) 
                return;

            if (disposing)
            {
                _items = null;
            }
            IsDisposed = true;
        }
    }
}

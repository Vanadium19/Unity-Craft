using System;

namespace Leopotam.EcsLite
{
    public class EcsStack<T> : IEcsQueue
    {
        public int Count => _count;

        private int _count;
        private T[] _items;

        public EcsStack()
        {
            _items = new T[1];
            _count = 0;
        }

        public void Push(in T data)
        {
            int capacity = _items.Length;
            if (_count == capacity)
                Array.Resize(ref _items, capacity * 2);

            _items[_count++] = data;
        }

        public bool Pop(out T data)
        {
            if (_count == 0)
            {
                data = default;
                return false;
            }

            data = _items[--_count];
            return true;
        }

        public void Clear()
        {
            _count = 0;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IDisposable
        {
            public T Current => _current;

            private EcsStack<T> _stack;
            private int _index;
            private T _current;

            public Enumerator(EcsStack<T> stack)
            {
                _stack = stack;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _stack._count)
                    return false;

                _current = _stack._items[++_index];
                return true;
            }

            public void Dispose()
            {
                _stack = null;
            }
        }
    }
}
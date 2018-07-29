using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    /// <summary>
    /// Defines generic queue data structure
    /// </summary>
    /// <typeparam name="T"> Type of nodes int a queue </typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        #region Fields
        private T[] _array;
        private int _head;
        private int _tail;
        private int _version;
        #endregion

        #region Public API

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DataStructure.Queue<T>"/> class
        /// </summary>
        public Queue()
        {
            _array = new T[10];
            _head = 0;
            _tail = 0;
            Size = 0;
            _version = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DataStructure.Queue<T>"/> class
        /// using the specified collection
        /// </summary>
        /// <param name="enumerable"> The collection used to initialize the instance
        /// of the <see cref="T:DataStructure.Queue<T>"/> class
        /// </param>
        /// <exception cref="ArgumentNullException"> Thrown when the input argument is null </exception>
        public Queue(IEnumerable<T> enumerable)
        {
            ValidateEnumerable(enumerable);

            var list = new List<T>();

            foreach (var item in enumerable)
            {
                list.Add(item);
            }

            _array = new T[list.Count + 10];
            T[] temp = list.ToArray();
            Array.Copy(temp, _array, temp.Length);

            _head = 0;
            _tail = list.Count - 1;
            Size = list.Count;
            _version = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current size of the queue
        /// </summary>
        public int Size { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Puts a new item into the queue
        /// </summary>
        /// <param name="item"> The item to put into the queue </param>
        public void Enqueue(T item)
        {
            if (Size == _array.Length)
            {
                EnlargeArray();
            }

            if (Size > 0)
            {
                _tail++;
            }

            _array[_tail] = item;
            Size++;
            _version++;
        }

        /// <summary>
        /// Pops item from the queue and returns it
        /// </summary>
        /// <returns> The element extracted from the queue </returns>
        /// <exception cref="InvalidOperationException"> Thrown if the queue is empty </exception>
        public T Dequeue()
        {
            CheckQueue();

            T item = _array[_head];
            _array[_head] = default(T);

            if (Size == 1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }

            Size--;
            _version++;

            return item;
        }

        /// <summary>
        /// Returns the first element in the queue without extracting it
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            CheckQueue();

            return _array[_head];
        }

        /// <summary>
        /// Returns the user enumerator for iteration by queue
        /// </summary>
        /// <returns> The custom iterator </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueIterator(this);
        }
        #endregion

        #endregion

        #region Private Members

        #region Methods
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void CheckQueue()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException(
                    "The queue is empty");
            }
        }

        private void EnlargeArray()
        {
            int size = Size + ((Size * 50) / 100);
            T[] array = new T[size];

            Array.Copy(_array, _head, array, 0, Size);
            _array = array;

            _head = 0;
            _version++;
        }

        private void ValidateEnumerable(
            IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(
                    "The parameneter can't be null",
                    nameof(enumerable));
            }
        }
        #endregion

        #region Nested Types
        private class QueueIterator : IEnumerator<T>
        {
            private readonly Queue<T> _queue;
            private readonly int _initialVersion;
            private readonly int _defaultPosition;
            private int position;

            public QueueIterator(Queue<T> queue)
            {
                _queue = queue;
                _initialVersion = queue._version;
                _defaultPosition = queue._head;
                position = queue._head - 1;
            }

            public T Current
            {
                get
                {
                    CheckVersion();

                    return _queue._array[position];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                CheckVersion();

                if (position < _queue._tail)
                {
                    position++;

                    return true;
                }

                return false;
            }

            public void Reset()
            {
                position = _defaultPosition;
            }

            private void CheckVersion()
            {
                if (_initialVersion != _queue._version)
                {
                    throw new InvalidOperationException(
                        "The queue has changed");
                }
            }
        }
        #endregion

        #endregion
    }
}
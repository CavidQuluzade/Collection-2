using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class CustomList<T> : IEnumerable<T>
    {
        private const int defaultcapacity = 4;
        private T[] array;
        private int count;
        private int capacity;
        public int Capacity { get; }
        public int Count { get => count; }

        public CustomList()
        {
            array = new T[defaultcapacity];
            Capacity = array.Length;
        }
        public void Add(T item)
        {
            if (count == capacity)
            {
                Array.Resize(ref array, capacity == 0 ? defaultcapacity : array.Length * 2);
                capacity = array.Length;
            }
            array[count++] = item;
        }
        public void GetAll()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
        public void Remove(T item)
        {
            var index = Array.IndexOf(array, item);
            if (index >= 0)
            {
                for (int i = index; i < count; i++)
                {
                    array[i] = array[i + 1];
                }
                count--;
            }
        }
        public bool Contain(T item)
        {
            var Index = Array.IndexOf(array, item);
            if (Index >= 0)
            {
                return true;
            }
            return false;
        }
        public bool Any(Predicate<T> predicate = null)
        {
            if (count>0 && predicate is null)
            {
                return true;
            }
            for (int i = 0; i < count; i++)
            {
                if (predicate(array[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                array[i] = default;
                //Console.WriteLine(array[i]);
            }
            count = 0;
        }
        public T FirstorDefault(Predicate<T> predicate = null)
        {
            if (count > 0 && predicate is null)
            {
                return array[0];
            }
            for(int i = 0; i < count; i++)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }
            return default;
        }
        public T ElementorDefault(int index)
        {
            if ((index >= 0) && (index < count))
            {
                return array[index];
            }
            return default;
        }
        public T LastOrDefault(Predicate<T> predicate = null)
        {
            if (Any() && predicate is null)
            {
                return array[count - 1];
            }
            for(int i =  count - 1; i >= 0; i--)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0;i < count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


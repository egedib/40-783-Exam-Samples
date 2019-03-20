using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    class Program
    {
        /// <summary>
        /// Thread-safe data adding and removal
        /// </summary>
        /// <param name="args"></param>
        static void ThreadSafeThenGet(string[] args)
        {
            BlockingCollection<string> col = new BlockingCollection<string>();
            Task read = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(col.Take());
                }
            });
            Task write = Task.Run(() =>
            {
                while (true)
                {
                    string s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s)) break;
                    col.Add(s);
                }
            });
            write.Wait();
            foreach (string s in col.GetConsumingEnumerable())
            {
                Console.WriteLine(s);
            }
        }

        static void ConcurrentBagExample()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            bag.Add(42);
            bag.Add(21);
            if (bag.TryTake(out int result)) {
                Console.WriteLine(result);
            }
            if (bag.TryPeek(out result)) {
                Console.WriteLine("There is a next item: {0}", result);
            }

            Task.Run(() =>
            {
                foreach (int i in bag)
                    Console.WriteLine(i);
            }).Wait();
        }

        static void LIFO()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            stack.Push(42);
            int result;
            if (stack.TryPop(out result))
                Console.WriteLine("Popped: { 0}", result);
            stack.PushRange(new int[] { 1, 2, 3 });
            int[] values = new int[2];
            stack.TryPopRange(values);
            foreach (int i in values)
                Console.WriteLine(i);
        }

        static void FIFO()
        {
            ConcurrentQueue<int> stack = new ConcurrentQueue<int>();
            stack.Enqueue(42);
            stack.Enqueue(42);
            int result;
            if (stack.TryDequeue(out result))
            {
                Console.WriteLine("Popped: { 0}", result);
            }  
            stack.TryPeek(out int value);
            Console.WriteLine(value);
        }

        static void ConcurrentDict()
        {
            var dict = new ConcurrentDictionary<string, int>();
            if (dict.TryAdd(“k1”, 42))
            {
                Console.WriteLine(“Added”);
            }
            if (dict.TryUpdate(“k1”, 21, 42))
            {
                Console.WriteLine(“42 updated to 21”);
            }
            dict[“k1”] = 42; // Overwrite unconditionally
            int r1 = dict.AddOrUpdate(“k1”, 3, (s, i) => i * 2);
            int r2 = dict.GetOrAdd(“k2”, 3);
        }
    }
}

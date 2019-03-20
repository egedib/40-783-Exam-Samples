using System;
using System.Threading;

namespace Chapter1
{
    /// <summary>
    /// _field is ThreadStatic which means this variable is unique for each Thread
    /// This case both thread counts 0-10 separately, without annotation it would be 1-10 then 11-20.
    /// </summary>
    public static class Program
    {
        [ThreadStatic]// == unique to each thread
        public static int _field;
        public static void Main()
        {
            new Thread(() =>{
                for (int x = 0; x < 10; x++)
                {
                    _field++;
                    Console.WriteLine("Thread A: {0}", _field);
                }
            }).Start();
            new Thread(() =>{
            for (int x = 0; x < 10; x++)
            {
                _field++;
                Console.WriteLine("Thread B: {0}", _field);
            }
        }).Start();
            Console.ReadKey();
        }
    }
}
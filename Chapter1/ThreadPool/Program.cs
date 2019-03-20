using System;
using System.Threading;
namespace Chapter1
{
    /// <summary>
    /// ?
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Working on a thread from threadpool");
            });
            Console.ReadLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Task SleepAsyncA(int millisecondsTimeout)
            {
                return Task.Run(() => Thread.Sleep(millisecondsTimeout));
            }
            Task SleepAsyncB(int millisecondsTimeout)
            {
                TaskCompletionSource<bool> tcs = null;
                var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
                tcs = new TaskCompletionSource<bool>(t);
                t.Change(millisecondsTimeout, -1);
                return tcs.Task;
            }
        }
    }
}

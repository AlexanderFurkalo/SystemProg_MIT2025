using System;
using System.Threading;

namespace ThreadPriorityDistribution
{
    class MyThread
    {
        public int Count;
        public Thread Thrd;
        private static bool stop = false;
        private static object lockObj = new object();
        private static int totalIterations = 0;

        public MyThread(string name, ThreadPriority priority)
        {
            Count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
            Thrd.Priority = priority;
        }

        void Run()
        {
            Console.WriteLine("Thread " + Thrd.Name + " is starting with priority " + Thrd.Priority + ".");
            while (!stop)
            {
                Count++;
                lock (lockObj)
                {
                    totalIterations++;
                    if (totalIterations >= 1_000_000)
                    {
                        stop = true;
                    }
                }
            }
            Console.WriteLine("Thread " + Thrd.Name + " is completed.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Main thread is starting.");

            MyThread mt1 = new MyThread("Lowest Priority", ThreadPriority.Lowest);
            MyThread mt2 = new MyThread("Above Normal Priority", ThreadPriority.AboveNormal);
            MyThread mt3 = new MyThread("Below Normal Priority", ThreadPriority.BelowNormal);
            MyThread mt4 = new MyThread("Highest Priority", ThreadPriority.Highest);

            mt1.Thrd.Start();
            mt2.Thrd.Start();
            mt3.Thrd.Start();
            mt4.Thrd.Start();

            mt1.Thrd.Join();
            mt2.Thrd.Join();
            mt3.Thrd.Join();
            mt4.Thrd.Join();

            Console.WriteLine("\nAll threads completed. Calculating distribution...");

            int total = mt1.Count + mt2.Count + mt3.Count + mt4.Count;

            Console.WriteLine($"{mt1.Thrd.Name} counted to {mt1.Count} ({(mt1.Count * 100.0 / total):F2}%)");
            Console.WriteLine($"{mt2.Thrd.Name} counted to {mt2.Count} ({(mt2.Count * 100.0 / total):F2}%)");
            Console.WriteLine($"{mt3.Thrd.Name} counted to {mt3.Count} ({(mt3.Count * 100.0 / total):F2}%)");
            Console.WriteLine($"{mt4.Thrd.Name} counted to {mt4.Count} ({(mt4.Count * 100.0 / total):F2}%)");

            Console.WriteLine("Main thread is completed.");
            Console.ReadLine();
        }
    }
}

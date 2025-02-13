using System;
using System.Threading;
using System.Collections.Generic;

namespace DynamicThreadPriority
{
    class MyThread
    {
        public int Count;
        public Thread Thrd;
        private static bool stop = false;
        private static object lockObj = new object();
        private static int totalIterations = 0;
        private static int totalTarget;

        public MyThread(string name, ThreadPriority priority, int target)
        {
            Count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
            Thrd.Priority = priority;
            totalTarget = target;
        }

        void Run()
        {
            Console.WriteLine($"{Thrd.Name} with priority {Thrd.Priority} starting.");

            while (!stop)
            {
                Count++;

                lock (lockObj)
                {
                    totalIterations++;

                    if (totalIterations >= totalTarget)
                    {
                        stop = true;
                    }
                }
            }
            Console.WriteLine($"{Thrd.Name} completed with count {Count}.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Main thread is starting.");

            Console.Write("Enter the number of threads: ");
            int numThreads = int.Parse(Console.ReadLine());

            Console.Write("Enter the total number of iterations: ");
            int totalIterations = int.Parse(Console.ReadLine());

            List<MyThread> threadList = new List<MyThread>();

            for (int i = 0; i < numThreads; i++)
            {
                Console.Write($"Enter name for Thread #{i + 1}: ");
                string name = Console.ReadLine();

                Console.WriteLine("Select priority:");
                Console.WriteLine("1. Lowest\n2. BelowNormal\n3. Normal\n4. AboveNormal\n5. Highest");
                Console.Write("Choice: ");
                int priorityChoice = int.Parse(Console.ReadLine());

                ThreadPriority priority = ThreadPriority.Normal;
                switch (priorityChoice)
                {
                    case 1: priority = ThreadPriority.Lowest; break;
                    case 2: priority = ThreadPriority.BelowNormal; break;
                    case 3: priority = ThreadPriority.Normal; break;
                    case 4: priority = ThreadPriority.AboveNormal; break;
                    case 5: priority = ThreadPriority.Highest; break;
                }

                MyThread myThread = new MyThread(name, priority, totalIterations);
                threadList.Add(myThread);
            }

            foreach (var thread in threadList)
            {
                thread.Thrd.Start();
            }

            while (!threadList.TrueForAll(t => !t.Thrd.IsAlive))
            {
                Console.Clear();
                Console.WriteLine("Progress:");
                foreach (var t in threadList)
                {
                    Console.WriteLine($"{t.Thrd.Name}: {t.Count} iterations");
                }
                Thread.Sleep(500);
            }

            Console.WriteLine("\nAll threads completed.\n");

            int total = 0;
            foreach (var t in threadList)
            {
                total += t.Count;
            }

            foreach (var t in threadList)
            {
                double percentage = (t.Count * 100.0) / total;
                Console.WriteLine($"{t.Thrd.Name} counted to {t.Count} ({percentage:F2}%)");
            }

            Console.WriteLine("Main thread is completed.");
            Console.ReadLine();
        }
    }
}
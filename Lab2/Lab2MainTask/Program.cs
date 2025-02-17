using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2MainTask
{
    class Program
    {
        static void MyTask(int taskId)
        {
            Console.WriteLine($"Task {taskId} is started.");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(200 * taskId); 
                Console.WriteLine($"Task {taskId}, counter = {count}");
            }

            Console.WriteLine($"Task {taskId} is done.");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");

            /*Task tsk1 = Task.Factory.StartNew(() =>
            {
                MyTask(1); 
            });

            Task tsk2 = Task.Factory.StartNew(() =>
            {
                MyTask(2);
            });

            Task.WaitAll(tsk1, tsk2);*/
            Parallel.Invoke(
              () => MyTask(1),  
              () => MyTask(2)   
            );

            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4Example1
{
    class Program
    {
        public static void MyTask()
        {
            Console.WriteLine("MyTask() is started.");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("In the method MyTask() counter = " + count);
            }
            Console.WriteLine("MyTask() is done.");
        }

        static void Main(string[] args)
        {
            //Сконструюввати об'єкт задачі
            Task tsk = new Task(Program.MyTask);
            //Запустити задачу на виконання
            tsk.Start();
            tsk.Wait();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    /*class Program
    {
        static void MyTask()
        {
            Console.WriteLine("MyTask №" + Task.CurrentId + " is started.");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("In the method MyTask() №" + Task.CurrentId + " counter = " + count);
            }
            Console.WriteLine("MyTask() №" + Task.CurrentId + " is done.");
        }
        static void ContTask(Task prevTask)
        {
            Console.WriteLine("ContTask is started after MyTask.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("In the method ContTask counter = " + count);
            }
            Console.WriteLine("ContTask is done.");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            Task tsk1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Lambda_expr is started.");
                for (int count = 0; count < 10; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("In the Lambda_expr counter = " + count);
                }
                Console.WriteLine("Lambda_expr is done.");
            });
            Task tsk2 = Task.Factory.StartNew(MyTask);
            Console.WriteLine("Id of task tsk1 = " + tsk1.Id);
            Console.WriteLine("Id of task tsk2 = " + tsk2.Id);
            Task tsk = new Task(MyTask);

            Task TaskCont = tsk.ContinueWith(ContTask);

            Task LambdaTaskCont = TaskCont.ContinueWith((first) =>
            {
                Console.WriteLine("LambdaTaskCont is started.");
                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("In the method LambdaTaskCont counter = " + count);
                }
                Console.WriteLine("LambdaTaskCont is done.");
            });

            tsk.Start();

            for (int i = 0; i < 60; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Task.WaitAll(tsk1, tsk2, LambdaTaskCont);
            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }*/

    /* Інша частина коду: class Program
    {
        //Метод без аргументів, що повертає результат
        static bool MyTask()
        {
            return true;
        }
        //Метод, що повертає суму чисел, що менше заданого параметру
        static int SumIt(object v)
        {
            int x = (int)v;
            int sum = 0;
            for (; x > 0; x--)
                sum += x;
            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            //Сконструюввати та запустити об'єкт першої задачі
            Task<bool> tsk1 = Task<bool>.Factory.StartNew(MyTask);
            Console.WriteLine("The Result after running of MyTask = " + tsk1.Result);
            //Сконструюввати та запустити об'єкт другої задачі
            Task<int> tsk2 = Task<int>.Factory.StartNew(SumIt, 5);
            Console.WriteLine("The Result after running of SumIt = " +
            tsk2.Result);
            tsk1.Dispose();
            tsk2.Dispose();
            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    } */

    class Program
    {
        //Метод1, що виконується як задача
        static void MyMeth1()
        {
            Console.WriteLine("MyMeth1() is started.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("In the method MyMeth1() counter = " + count);
            }
            Console.WriteLine("MyMeth1() is done.");
        }
        //Метод2, що виконується як задача
        static void MyMeth2()
        {
            Console.WriteLine("MyMeth2() is started.");
            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("In the method MyMeth2() counter = " + count);
            }
            Console.WriteLine("MyMeth2() is done.");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            //Виконати паралельно два іменованих метода, метод Main призупиняється, поки виконуються два методи
        Parallel.Invoke(MyMeth1, MyMeth2);
            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }

}

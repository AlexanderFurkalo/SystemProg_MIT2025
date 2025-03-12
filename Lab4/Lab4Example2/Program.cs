using System;
using System.Threading.Tasks;

namespace Lab4Example2
{
    class Program
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
            Console.WriteLine("The Result after running of MyTask =  " + tsk1.Result);
            //Сконструюввати та запустити об'єкт другої задачі
            Task<int> tsk2 = Task<int>.Factory.StartNew(SumIt, 5);
            Console.WriteLine("The Result after running of SumIt = " + tsk2.Result);
            tsk1.Dispose();
            tsk2.Dispose();
            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}

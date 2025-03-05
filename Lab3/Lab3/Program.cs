using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab3
{
    class Program
    {
        static double[] data;
        //Метод, що служить в якості тіла паралельного циклу.
        static void MyTransform(int i)
        {
            data[i] /= 10;
            if (data[i] < 10000) data[i] = 0;
            if ((data[i] >= 10000) & (data[i] < 20000)) data[i] = 100;
            if ((data[i] >= 20000) & (data[i] < 30000)) data[i] = 200;
            if (data[i] > 30000) data[i] = 300;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread is starting.");
            Stopwatch sw = new Stopwatch();
            data = new double[100000000];
            sw.Start();
            //Ініціювати дані в звичайному циклі for
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            sw.Stop();
            Console.WriteLine("Serial initialization of cycle= "+sw.Elapsed.TotalSeconds+" seconds.");
            sw.Reset();
            sw.Start();
            //Розпаралелити цикл методом Parallel.For
            Parallel.For(0, data.Length, MyTransform);
            sw.Stop();
            Console.WriteLine("Parallel transformation = " +
            sw.Elapsed.TotalSeconds + " seconds.");
            sw.Reset();
            sw.Start();
            for (int i = 0; i < data.Length; i++)
            {
                MyTransform(i);
            }
            sw.Stop();
            Console.WriteLine("Serial Transformation = " +
            sw.Elapsed.TotalSeconds + " seconds.");
            Console.WriteLine("Main() is done.");
            Console.ReadLine();
        }
    }
}
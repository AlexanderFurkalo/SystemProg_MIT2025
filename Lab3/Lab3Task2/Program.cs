using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab3Task2
{
    class Program
    {
        static int[] intData;
        static double[] doubleData;
        const double target = 50000;
        const double deviation = 100;

        static void TransformInt(int i, int complexity, ParallelLoopState loopState = null)
        {
            switch (complexity)
            {
                case 1: intData[i] = intData[i] / 10; break;
                case 2: intData[i] = (int)(intData[i] / Math.PI); break;
                case 3: intData[i] = (int)(Math.Exp(intData[i]) / Math.Pow(intData[i], Math.PI)); break;
                case 4: intData[i] = (int)(Math.Exp(Math.PI * intData[i]) / Math.Pow(intData[i], Math.PI)); break;
            }

            if (loopState != null && Math.Abs(intData[i] - target) <= deviation)
            {
                Console.WriteLine($"Breaking int loop at index {i}, value = {intData[i]}");
                loopState.Break();
            }
        }

        static void TransformDouble(int i, int complexity, ParallelLoopState loopState = null)
        {
            switch (complexity)
            {
                case 1: doubleData[i] = doubleData[i] / 10; break;
                case 2: doubleData[i] = doubleData[i] / Math.PI; break;
                case 3: doubleData[i] = Math.Exp(doubleData[i]) / Math.Pow(doubleData[i], Math.PI); break;
                case 4: doubleData[i] = Math.Exp(Math.PI * doubleData[i]) / Math.Pow(doubleData[i], Math.PI); break;
            }

            if (loopState != null && Math.Abs(doubleData[i] - target) <= deviation)
            {
                Console.WriteLine($"Breaking double loop at index {i}, value = {doubleData[i]:F2}");
                loopState.Break();
            }
        }

        static void SerialExecutionInt(int complexity)
        {
            for (int i = 0; i < intData.Length; i++)
            {
                TransformInt(i, complexity);
            }
        }

        static void SerialExecutionDouble(int complexity)
        {
            for (int i = 0; i < doubleData.Length; i++)
            {
                TransformDouble(i, complexity);
            }
        }

        static void ParallelExecutionInt(int complexity)
        {
            ParallelLoopResult result = Parallel.For(0, intData.Length, (i, loopState) =>
            {
                TransformInt(i, complexity, loopState);
            });

            if (!result.IsCompleted)
                Console.WriteLine($"Parallel int loop exited at iteration {result.LowestBreakIteration}");
        }

        static void ParallelExecutionDouble(int complexity)
        {
            ParallelLoopResult result = Parallel.For(0, doubleData.Length, (i, loopState) =>
            {
                TransformDouble(i, complexity, loopState);
            });

            if (!result.IsCompleted)
                Console.WriteLine($"Parallel double loop exited at iteration {result.LowestBreakIteration}");
        }

        static void Testing(int arraySize, int complexity)
        {
            Console.WriteLine($"Size: {arraySize}, complexity: {complexity}");
            intData = new int[arraySize];
            doubleData = new double[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                intData[i] = i + 1;
                doubleData[i] = i + 1;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SerialExecutionInt(complexity);
            sw.Stop();
            Console.WriteLine($"Serial time for int: {sw.Elapsed.TotalSeconds} sec");
            sw.Reset();
            sw.Start();
            SerialExecutionDouble(complexity);
            sw.Stop();
            Console.WriteLine($"Serial time for double: {sw.Elapsed.TotalSeconds} sec");
            for (int i = 0; i < arraySize; i++)
            {
                intData[i] = i + 1;
                doubleData[i] = i + 1;
            }
            sw.Reset();
            sw.Start();
            ParallelExecutionInt(complexity);
            sw.Stop();
            Console.WriteLine($"Parallel time for int: {sw.Elapsed.TotalSeconds} sec");
            sw.Reset();
            sw.Start();
            ParallelExecutionDouble(complexity);
            sw.Stop();
            Console.WriteLine($"Parallel time for double: {sw.Elapsed.TotalSeconds} sec");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Starting task1, tests");
            int[] sizes = { 1000000, 10000000 };
            int[] complexities = { 1, 2, 3, 4 };
            foreach (int size in sizes)
            {
                foreach (int complexity in complexities)
                {
                    Testing(size, complexity);
                }
            }
            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}

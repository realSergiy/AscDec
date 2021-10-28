using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AscDec
{
    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Started");
            Console.WriteLine();
            
            var arr1T = Task.Run(() => Generator.Produce());
            var arr2T = Task.Run(() => Generator.Produce());
            var arr3T = Task.Run(() => Generator.Produce());
            var arr4T = Task.Run(() => Generator.Produce());
            var arr5T = Task.Run(() => Generator.Produce());

            Task.WaitAll(arr1T, arr2T, arr3T, arr4T, arr5T);

            var arr1 = arr1T.Result;
            var arr2 = arr2T.Result;
            var arr3 = arr3T.Result;
            var arr4 = arr4T.Result;
            var arr5 = arr5T.Result;
            
            Console.WriteLine();

            var seed = DateTime.Now.Millisecond;
            var rnd = new Random(seed);

            var questionIndexes1 = Enumerable.Range(0, 5).Select(_ => rnd.Next(arr1.Length - 1)).ToArray();
            var questionIndexes2 = Enumerable.Range(0, 5).Select(_ => rnd.Next(arr2.Length - 1)).ToArray();
            var questionIndexes3 = Enumerable.Range(0, 5).Select(_ => rnd.Next(arr3.Length - 1)).ToArray();
            var questionIndexes4 = Enumerable.Range(0, 5).Select(_ => rnd.Next(arr4.Length - 1)).ToArray();
            var questionIndexes5 = Enumerable.Range(0, 5).Select(_ => rnd.Next(arr5.Length - 1)).ToArray();

            var watch = new Stopwatch();
            watch.Start();

            var t1 = Runner.RunAsync(arr1, questionIndexes1, Find);
            var t2 = Runner.RunAsync(arr2, questionIndexes2, Find);
            var t3 = Runner.RunAsync(arr3, questionIndexes3, Find);
            var t4 = Runner.RunAsync(arr4, questionIndexes4, Find);
            var t5 = Runner.RunAsync(arr5, questionIndexes5, Find);
            var ti1 = Runner.RunAsync(arr1, questionIndexes1, Array.IndexOf);
            var ti2 = Runner.RunAsync(arr2, questionIndexes2, Array.IndexOf);
            var ti3 = Runner.RunAsync(arr3, questionIndexes3, Array.IndexOf);
            var ti4 = Runner.RunAsync(arr4, questionIndexes4, Array.IndexOf);
            var ti5 = Runner.RunAsync(arr5, questionIndexes5, Array.IndexOf);

            Task.WaitAll(ti1, ti2, ti3, ti4, ti5, t1, t2, t3, t4, t5);
            Console.WriteLine($"Overall Run Time: {watch.ElapsedMilliseconds}");
            
            Console.WriteLine();
            var indexOfElapsed = ti1.Result + ti2.Result + ti3.Result + ti4.Result + ti5.Result;
            var findElapsed = t1.Result + t2.Result + t3.Result + t4.Result + t5.Result;
            
            Console.WriteLine($"{nameof(Array.IndexOf)}: {indexOfElapsed}");
            Console.WriteLine($"{nameof(Find)}: {findElapsed}");
            Console.WriteLine($"Compared to IndexOf: {(double)findElapsed/indexOfElapsed:P}");
        }

        static int Find(int[] array, int numberToFind)
        {
            // replace this implementation and see if you can beat the IndexOf method
            for (var index = 0; index < array.Length; index++)
            {
                if (array[index] == numberToFind)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}

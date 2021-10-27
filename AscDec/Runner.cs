using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AscDec
{
    internal static class Runner
    {
        public static Task<long> RunAsync(int[] arr, int[] questionIndexes, Func<int[], int, int> searcher)
        {
            return Task.Run(() => Run(arr, questionIndexes, searcher));
        }

        static long Run(int[] array, IEnumerable<int> questionIndexes, Func<int[], int, int> searcher)
        {
            return questionIndexes.AsParallel().Select(questionIdx => Run(array, searcher, questionIdx)).Sum();
        }

        static long Run(int[] array, Func<int[], int, int> searcher, int questionIdx)
        {
            var watch = new Stopwatch();
            watch.Start();
            var number = array[questionIdx];

            var answer = searcher(array, number);
            var elapsed = watch.ElapsedMilliseconds;
            var time = elapsed.ToString().PadRight(5);
            var expected = questionIdx.ToString().PadRight(12);
            var found = answer.ToString().PadRight(12);

            var oComplexity = (double) answer / array.Length;
            var oComplexityStr = oComplexity.ToString("0.##").PadRight(10);
            var oTimeComplexityRatio = (elapsed / oComplexity).ToString("0.##").PadRight(10);

            Console.WriteLine(
                $"{time}: Expected {expected} Found {found} searching {number.ToString().PadRight(12)} with {searcher.Method.Name.PadRight(10)} complexity {oComplexityStr} ratio {oTimeComplexityRatio} in {elapsed.ToString().PadRight(7)} ms (on {Thread.CurrentThread.ManagedThreadId})");
            return elapsed;
        }
    }
}
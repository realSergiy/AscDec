using System;
using System.Diagnostics;

namespace AscDec
{
    internal static class Generator
    {
        const int ArrayMaxSize = 2146435071;
        public static int[] Produce(bool debug = false)
        {
            var arraySize = debug ? 30 : ArrayMaxSize;
            
            var watch = new Stopwatch();
            watch.Start();
            
            var rnd = new RandomSecure();
            var stepRandomnessLevel = Math.Min(512, arraySize);
            var proportion = rnd.GetDouble();
            
            var leftIdx = 0;
            var rightIdx = 0;

            var result = new int[arraySize];
            var randoms = new bool[stepRandomnessLevel];
            
            for (var i = 0; i < stepRandomnessLevel; i++)
            {
                var d = rnd.GetDouble();
                randoms[i] = d < proportion;
            }

            for (var i = 0; i < arraySize; i++)
            {
                var addLeft = randoms[i % stepRandomnessLevel];
                if (addLeft)
                {
                    result[leftIdx] = i;
                    leftIdx++;
                }
                else
                {
                    result[arraySize - rightIdx - 1] = i;
                    rightIdx++;
                }
            }
            
            watch.Stop();
            
            var arrStr = debug
                ? string.Join(",", result)
                : arraySize.ToString();

            var proportionStr = proportion.ToString("0.###").PadRight(5);
            
            Console.WriteLine($"Proportion: ${proportionStr}, array: {arrStr}, generated in {watch.ElapsedMilliseconds} ms");
            
            return result;
        }
    }
}
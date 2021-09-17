using BenchmarkDotNet.Running;
using System;

namespace StringBenchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringMasking>(null,args);
        }
    }
}

using BenchmarkDotNet.Running;

namespace FastHlsBenchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<ManifestGeneratorBenchmarks>();
        }
    }
}
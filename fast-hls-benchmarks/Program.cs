using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace FastHlsBenchmarks
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            BenchmarkRunner.Run<ManifestGeneratorBenchmarks>();

            //for(var i = 0; i < 50; i++)
            //{
            //    await new ManifestGeneratorBenchmarks().MediaManifest();
            //}

        }
    }
}
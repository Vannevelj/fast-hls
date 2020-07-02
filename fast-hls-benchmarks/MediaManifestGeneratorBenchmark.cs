using System;
using FastHls;
using BenchmarkDotNet.Attributes;
using System.IO;
using System.Threading.Tasks;

namespace FastHlsBenchmarks
{
    [MemoryDiagnoser]
    public class MediaManifestGeneratorBenchmark
    {
        [Benchmark]
        public async Task WritingDataToMemoryOutputStreamContinuously() {
            var generator = new MediaManifestGenerator(new MemoryStream(), continuousPersistence: true);
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++) {
                await generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
        }

        [Benchmark]
        public async Task WritingDataToMemoryOutputStream() {
            var generator = new MediaManifestGenerator();
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++) {
                await generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
            await generator.WriteToStream(new MemoryStream());
        }
        
        [Benchmark]
        public async Task WritingDataInternally() {
            var generator = new MediaManifestGenerator();
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++) {
                await generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
        }

        
    }
}

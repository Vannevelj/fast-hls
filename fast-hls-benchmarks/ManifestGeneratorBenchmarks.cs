using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FastHls.Models;
using FastHls.Models.Manifests;

namespace FastHlsBenchmarks
{
    [MemoryDiagnoser]
    public class ManifestGeneratorBenchmarks
    {
        [Benchmark]
        public async Task MediaManifest()
        {
            var manifest = new MediaManifest(version: 8, playlistType: PlaylistType.VOD, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++)
            {
                manifest.Add(new MediaFile
                {
                    Path = $"{i}.ts",
                    Duration = 2.0
                });
            }
            var stream = new MemoryStream();
            var writer = new MediaManifestWriter(manifest, stream);
            await writer.Render();
        }
    }
}

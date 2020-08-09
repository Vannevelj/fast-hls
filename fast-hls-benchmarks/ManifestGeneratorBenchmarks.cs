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
        //[Benchmark]
        //public async Task MediaManifest_WritingDataInternally()
        //{
        //    var manifest = new MediaManifest(version: 8, playlistType: PlaylistType.VOD, targetDuration: 2.0);
        //    for (var i = 0; i < 1000; i++)
        //    {
        //        manifest.Add(new MediaFile
        //        {
        //            Path = $"{i}.ts",
        //            Duration = 2.0
        //        });
        //    }
        //    await manifest.Finish();
        //}

        //[Benchmark]
        //public async Task MediaManifest_WritingDataToMemoryOutputStream()
        //{
        //    var manifest = new MediaManifest(version: 8, playlistType: PlaylistType.VOD, targetDuration: 2.0);
        //    for (var i = 0; i < 1000; i++)
        //    {
        //        manifest.Add(new MediaFile
        //        {
        //            Path = $"{i}.ts",
        //            Duration = 2.0
        //        });
        //    }
        //    await manifest.Finish();
        //    await manifest.WriteToStream(new MemoryStream());
        //}

        //[Benchmark]
        //public async Task MediaManifest_WritingDataToMemoryOutputStreamContinuously()
        //{
        //    var manifest = new MediaManifestGenerator(new MemoryStream(), continuousPersistence: true);
        //    manifest.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
        //    for (var i = 0; i < 1000; i++)
        //    {
        //        manifest.AddMediaFile($"{i}.ts", duration: 2);
        //    }
        //    await manifest.Finish();
        //}

        //[Benchmark]
        //public async Task MasterManifest_WritingDataInternally()
        //{
        //    var manifest = new MasterManifest(version: 3);
        //    for (var i = 0; i < 1000; i++)
        //    {
        //        manifest.AddMedia(
        //            mediaType: MediaType.AUDIO,
        //            groupId: "audio-hi",
        //            name: "Dutch",
        //            uri: "dutch.m3u8",
        //            language: "nl-BE",
        //            assocLanguage: "nl-NL",
        //            isDefault: true,
        //            autoselect: true,
        //            forced: false,
        //            instreamId: null,
        //            characteristics: new string[] { "public.accessibility.describes-video" }
        //        );

        //        manifest.AddVariantStream(
        //            uri: "high.m3u8",
        //            bandwidth: 9_000_000,
        //            averageBandwidth: 8_500_000,
        //            codecs: new[] { "avc1.640029", "mp4a.40.2" },
        //            resolution: new Resolution(720, 1024),
        //            audio: "audio-hi",
        //            video: "video-hi",
        //            subtitles: "subtitles-en",
        //            closedCaptions: "cc-en"
        //        );
        //    }
        //    await manifest.Finish();
        //}
    }
}

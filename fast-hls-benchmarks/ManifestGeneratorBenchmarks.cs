namespace FastHlsBenchmarks
{
    [MemoryDiagnoser]
    public class ManifestGeneratorBenchmarks
    {
        [Benchmark]
        public async Task MediaManifest_WritingDataInternally()
        {
            var generator = new MediaManifestGenerator();
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++)
            {
                generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
        }

        [Benchmark]
        public async Task MediaManifest_WritingDataToMemoryOutputStream()
        {
            var generator = new MediaManifestGenerator();
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++)
            {
                generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
            await generator.WriteToStream(new MemoryStream());
        }

        [Benchmark]
        public async Task MediaManifest_WritingDataToMemoryOutputStreamContinuously()
        {
            var generator = new MediaManifestGenerator(new MemoryStream(), continuousPersistence: true);
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 2.0);
            for (var i = 0; i < 1000; i++)
            {
                generator.AddMediaFile($"{i}.ts", duration: 2);
            }
            await generator.Finish();
        }

        [Benchmark]
        public async Task MasterManifest_WritingDataInternally()
        {
            var generator = new MasterManifestGenerator();
            generator.Start(version: 3);
            for (var i = 0; i < 1000; i++)
            {
                generator.AddMedia(
                    mediaType: MediaType.AUDIO,
                    groupId: "audio-hi",
                    name: "Dutch",
                    uri: "dutch.m3u8",
                    language: "nl-BE",
                    assocLanguage: "nl-NL",
                    isDefault: true,
                    autoselect: true,
                    forced: false,
                    instreamId: null,
                    characteristics: new string[] { "public.accessibility.describes-video" }
                );

                generator.AddVariantStream(
                    uri: "high.m3u8",
                    bandwidth: 9_000_000,
                    averageBandwidth: 8_500_000,
                    codecs: new[] { "avc1.640029", "mp4a.40.2" },
                    resolution: new Resolution(720, 1024),
                    audio: "audio-hi",
                    video: "video-hi",
                    subtitles: "subtitles-en",
                    closedCaptions: "cc-en"
                );
            }
            await generator.Finish();
        }
    }
}

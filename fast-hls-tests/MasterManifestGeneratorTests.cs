using System.Threading.Tasks;
using FastHls;
using FastHls.Models;
using Xunit;

namespace FastHlsTests
{
    public class MasterManifestGeneratorTests
    {
        private MasterManifestGenerator generator = new MasterManifestGenerator();

        [Fact]
        public async Task WritesHeader()
        {
            generator.Start(version: 3);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
");
        }

        [Fact]
        public async Task CanOmitIndependentSegments()
        {
            generator.Start(version: 3, independentSegments: false);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
");
        }

        [Fact]
        public async Task WritesMedia()
        {
            generator.Start(version: 3);
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
                characteristics: new string[] {"public.accessibility.describes-video"}
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID=""audio-hi"",NAME=""Dutch"",LANGUAGE=""nl-BE"",ASSOC-LANGUAGE=""nl-NL"",DEFAULT=YES,AUTOSELECT=YES,CHARACTERISTICS=""public.accessibility.describes-video"",URI=""dutch.m3u8""
");
        }

        [Fact]
        public async Task WritesClosedCaptionMedia()
        {
            generator.Start(version: 3);
            generator.AddMedia(
                mediaType: MediaType.CLOSEDCAPTIONS,
                groupId: "audio-hi",
                name: "Dutch",
                uri: "dutch.m3u8"
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-MEDIA:TYPE=CLOSED-CAPTIONS,GROUP-ID=""audio-hi"",NAME=""Dutch""
");
        }

        [Fact]
        public async Task WritesVariantStream()
        {
            generator.Start(version: 3);
            generator.AddVariantStream(
                uri: "high.m3u8",
                bandwidth: 9_000_000,
                averageBandwidth: 8_500_000,
                codecs: new [] { "avc1.640029", "mp4a.40.2" },
                resolution: new Resolution(720, 1024),
                audio: "audio-hi",
                video: "video-hi",
                subtitles: "subtitles-en",
                closedCaptions: "cc-en"
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-STREAM-INF:BANDWIDTH=9000000,AVERAGE-BANDWIDTH=8500000,CODECS=""avc1.640029,mp4a.40.2"",RESOLUTION=1024x720,AUDIO=""audio-hi"",VIDEO=""video-hi"",SUBTITLES=""subtitles-en"",CLOSED-CAPTIONS=""cc-en""
high.m3u8
");
        }

        [Fact]
        public async Task WritesIFrameVariantStream()
        {
            generator.Start(version: 3);
            generator.AddIFrameVariantStream(
                uri: "high.m3u8",
                bandwidth: 9_000_000,
                averageBandwidth: 8_500_000,
                codecs: new [] { "avc1.640029", "mp4a.40.2" },
                resolution: new Resolution(720, 1024),
                video: "video-hi"
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=9000000,URI=""high.m3u8"",AVERAGE-BANDWIDTH=8500000,CODECS=""avc1.640029,mp4a.40.2"",RESOLUTION=1024x720,VIDEO=""video-hi""
");
        }

        [Fact]
        public async Task WritesSessionData()
        {
            generator.Start(version: 3);
            generator.AddSessionData(
                dataId: "com.fasthls.custom.field",
                value: "fast hls is fast",
                language: "en-UK"
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-SESSION-DATA:DATA-ID=""com.fasthls.custom.field"",VALUE=""fast hls is fast"",LANGUAGE=""en-UK""
");
        }
    }
}
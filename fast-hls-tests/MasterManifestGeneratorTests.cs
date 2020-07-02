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
            await generator.Start(version: 3);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS\r\n");
        }

        [Fact]
        public async Task CanOmitIndependentSegments()
        {
            await generator.Start(version: 3, independentSegments: false);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3\r\n");
        }

        [Fact]
        public async Task WritesMedia()
        {
            await generator.Start(version: 3);
            await generator.AddMedia(
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
#EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID=""audio-hi"",NAME=""Dutch"",LANGUAGE=""nl-BE"",ASSOC-LANGUAGE=""nl-NL"",DEFAULT=YES,AUTOSELECT=YES,FORCED=NO,CHARACTERISTICS=""public.accessibility.describes-video"",URI=""dutch.m3u8""");
        }

        [Fact]
        public async Task WritesClosedCaptionMedia()
        {
            await generator.Start(version: 3);
            await generator.AddMedia(
                mediaType: MediaType.CLOSEDCAPTIONS,
                groupId: "audio-hi",
                name: "Dutch",
                uri: "dutch.m3u8"
            );

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS
#EXT-X-MEDIA:TYPE=CLOSED-CAPTIONS,GROUP-ID=""audio-hi"",NAME=""Dutch""");
        }

        [Fact]
        public async Task WritesVariantStream()
        {
            await generator.Start(version: 3);
            await generator.AddVariantStream(
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
#EXT-X-STREAM-INF:BANDWIDTH=9000000,AVERAGE-BANDWIDTH=8500000,CODECS=""avc1.640029,mp4a.40.2"",RESOLUTION=1024x720,AUDIO=""audio-hi"",VIDEO=""video-hi"",SUBTITLES=""subtitles-en"",CLOSED-CAPTIONS=""cc-en""");
        }
    }
}
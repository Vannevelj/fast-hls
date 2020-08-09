using System.Collections.Generic;
using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class VariantStreamTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { "high.m3u8", 9_000_000, 8_500_000, new[] { "avc1.640029", "mp4a.40.2" }, new Resolution(720, 1024), "audio-hi", "video-hi", "subtitles-en", "cc-en", @"#EXT-X-STREAM-INF:BANDWIDTH=9000000,AVERAGE-BANDWIDTH=8500000,CODECS=""avc1.640029,mp4a.40.2"",RESOLUTION=1024x720,AUDIO=""audio-hi"",VIDEO=""video-hi"",SUBTITLES=""subtitles-en"",CLOSED-CAPTIONS=""cc-en""
high.m3u8" },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void WritesVariantStream(string uri, int bandwidth, int? averageBandwidth, string?[] codecs, Resolution? resolution, string? audio, string? video, string? subtitles, string? closedCaptions, string expected)
        {
            var variantStream = new VariantStream
            {
                Uri = uri,
                Bandwidth = bandwidth,
                AverageBandwidth = averageBandwidth,
                Codecs = codecs,
                Resolution = resolution,
                Video = video,
                Audio = audio,
                Subtitles = subtitles,
                ClosedCaptions = closedCaptions
            };
            Assert.Equal(expected, variantStream.Render());
        }
    }
}

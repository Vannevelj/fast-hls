using System.Collections.Generic;
using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class IFrameVariantStreamTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { "high.m3u8", 9_000_000, 8_500_000, null, null, "video-hi", @"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=9000000,URI=""high.m3u8"",AVERAGE-BANDWIDTH=8500000,VIDEO=""video-hi""" },
                new object[] { "high.m3u8", 9_000_000, 8_500_000, new[] { "avc1.640029", "mp4a.40.2" }, new Resolution(720, 1024), "video-hi", @"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=9000000,URI=""high.m3u8"",AVERAGE-BANDWIDTH=8500000,CODECS=""avc1.640029,mp4a.40.2"",RESOLUTION=1024x720,VIDEO=""video-hi""" },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void WritesIFrameVariantStream(string uri, int bandwidth, int? averageBandwidth, string?[] codecs, Resolution? resolution, string? video, string expected)
        {
            var iframeVariantStream = new IFrameVariantStream
            {
                Uri = uri,
                Bandwidth = bandwidth,
                AverageBandwidth = averageBandwidth,
                Codecs = codecs,
                Resolution = resolution,
                Video = video
            };
            AssertEqualWithNewline(expected, iframeVariantStream.Render());
        }
    }
}

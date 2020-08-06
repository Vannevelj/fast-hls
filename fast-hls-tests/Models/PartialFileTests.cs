using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class PartialFileTests
    {
        [Theory]
        [InlineData("http://example.com/movie1/fileSequenceA.ts", 2.0, false, null, null, null, @"#EXT-X-PART:DURATION=2,URI=""http://example.com/movie1/fileSequenceA.ts""")]
        [InlineData("http://example.com/movie1/fileSequenceA.ts", 2.0, true, null, null, null, @"#EXT-X-PART:DURATION=2,URI=""http://example.com/movie1/fileSequenceA.ts"",INDEPENDENT=YES")]
        public void WritesPartialMediaFiles(string path, double duration, bool? isIndependent, bool? hasGap, int? length, int? offset, string expected)
        {
            var mediaFile = new PartialFile
            {
                Path = path,
                Duration = duration,
                IsIndependent = isIndependent,
                HasGap = hasGap,
                ByteRange = length.HasValue ? new ByteRange(length.Value, offset) : default
            };
            Assert.Equal(expected, mediaFile.Render());
        }
    }
}

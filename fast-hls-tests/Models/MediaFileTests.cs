using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class MediaFileTests
    {
        [Theory]
        [InlineData("http://example.com/movie1/fileSequenceA.ts", 10, null,
@"#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts")]
        public void WritesMediaFiles(string path, double duration, ByteRange? byteRange, string expected)
        {
            var mediaFile = new MediaFile
            {
                Path = path,
                Duration = duration,
                ByteRange = byteRange
            };
            AssertEqualWithNewline(expected, mediaFile.Render());
        }
    }
}

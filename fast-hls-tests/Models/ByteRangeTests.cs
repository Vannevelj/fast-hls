using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class ByteRangeTests
    {
        [Theory]
        [InlineData(9_876_543, 321, "#EXT-X-BYTERANGE:9876543@321")]
        [InlineData(9_876_543, null, "#EXT-X-BYTERANGE:9876543")]
        public void WritesByteRange(int length, int? offset, string expected)
        {
            var byteRange = new ByteRange(length, offset);
            AssertExtensions.AssertStreamContentEqual(expected, byteRange, hasNewline: false);
        }
    }
}

using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class MapTests
    {
        [Theory]
        [InlineData("main.mp4", 9_876_543, 123, @"#EXT-X-MAP:URI=""main.mp4"",BYTERANGE=""9876543@123""")]
        [InlineData("main.mp4", 9_876_543, null, @"#EXT-X-MAP:URI=""main.mp4"",BYTERANGE=""9876543""")]
        public void WritesMap(string uri, int length, int? offset, string expected)
        {
            var map = new Map
            {
                Uri = uri,
                ByteRange = new ByteRange(length, offset)
            };
            Assert.Equal(expected, map.Render());
        }
    }
}

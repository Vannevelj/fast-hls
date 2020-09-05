using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class DiscontinuityTests
    {
        [Theory]
        [InlineData(@"#EXT-X-DISCONTINUITY")]
        public void WritesDiscontinuity(string expected)
        {
            var discontinuity = new Discontinuity();
            AssertStreamContentEqual(expected, discontinuity, hasNewline: false);
        }
    }
}

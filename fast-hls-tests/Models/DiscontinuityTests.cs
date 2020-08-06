using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class DiscontinuityTests
    {
        [Theory]
        [InlineData(@"#EXT-X-DISCONTINUITY")]
        public void WritesDiscontinuity(string expected)
        {
            var discontinuity = new Discontinuity();
            Assert.Equal(expected, discontinuity.Render());
        }
    }
}

using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class EndListTests
    {
        [Theory]
        [InlineData(@"#EXT-X-ENDLIST")]
        public void WritesEndList(string expected)
        {
            var endList = new EndList();
            Assert.Equal(expected, endList.Render());
        }
    }
}

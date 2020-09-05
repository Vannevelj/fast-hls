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
            AssertExtensions.AssertStreamContentEqual(expected, endList, hasNewline: false);
        }
    }
}

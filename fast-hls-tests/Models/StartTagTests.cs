using System;
using FastHls.Models.Manifests;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class StartTagTests
    {
        [Theory]
        [InlineData(-10.5, true, "#EXT-X-START:TIME-OFFSET=-10.5,PRECISE=YES")]
        public void WritesStartTag(double offset, bool isPrecise, string expected)
        {
            var startTag = new Start
            {
                Offset = TimeSpan.FromSeconds(offset),
                IsPrecise = isPrecise
            };
            AssertEqualWithNewline(expected, startTag.Render());
        }
    }
}

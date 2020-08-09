using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class RenditionReportTests
    {
        [Theory]
        [InlineData("../1M/waitForMSN.php", 273, null, @"#EXT-X-RENDITION-REPORT:URI=""../1M/waitForMSN.php"",LAST-MSN=273")]
        [InlineData("../1M/waitForMSN.php", 273, 1, @"#EXT-X-RENDITION-REPORT:URI=""../1M/waitForMSN.php"",LAST-MSN=273,LAST-PART=1")]
        public void WritesMap(string path, int lastMsn, int? lastPart, string expected)
        {
            var renditionReport = new RenditionReport
            {
                Path = path,
                LastMsn = lastMsn,
                LastPart = lastPart
            };
            AssertEqualWithNewline(expected, renditionReport.Render());
        }
    }
}

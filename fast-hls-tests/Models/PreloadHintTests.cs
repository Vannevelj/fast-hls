using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class PreloadHintTests
    {
        [Theory]
        [InlineData(HintType.PART, "http://example.com/movie1/fileSequenceB.ts", null, null, @"#EXT-X-PRELOAD-HINT:TYPE=PART,URI=""http://example.com/movie1/fileSequenceB.ts""")]
        public void WritesMap(HintType hint, string path, int? start, int? length, string expected)
        {
            var preloadHint = new PreloadHint
            {
                Type = hint,
                Path = path,
                Start = start,
                Length = length
            };
            Assert.Equal(expected, preloadHint.Render());
        }
    }
}

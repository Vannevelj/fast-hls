using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class SessionDataTests
    {
        [Theory]
        [InlineData("com.fasthls.custom.field", "fast hls is fast", null, "en-UK", @"#EXT-X-SESSION-DATA:DATA-ID=""com.fasthls.custom.field"",VALUE=""fast hls is fast"",LANGUAGE=""en-UK""")]
        public void WritesSessionData(string dataId, string? value, string? uri, string? language, string expected)
        {
            var sessionData = new SessionData
            {
                DataId = dataId,
                Value = value,
                Uri = uri,
                Language = language
            };
            AssertStreamContentEqual(expected, sessionData);
        }
    }
}

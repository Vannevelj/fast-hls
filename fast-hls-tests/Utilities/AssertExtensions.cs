using Xunit;

namespace FastHlsTests
{
    public static class AssertExtensions
    {
        public static void AssertEqualWithNewline(string expected, string content) => Assert.Equal(expected + "\r\n", content);
    }
}
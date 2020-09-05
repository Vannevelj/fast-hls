using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using FastHls.Models.Interfaces;
using Xunit;

namespace FastHlsTests
{
    public static class AssertExtensions
    {
        public static void AssertEqualWithNewline(string expected, string content) => Assert.Equal(expected + "\r\n", content);
        public static void AssertStreamContentEqual(string expected, IManifestItem item, bool hasNewline = true)
        {
            var outputStream = new MemoryStream();
            var outputWriter = new StreamWriter(outputStream);
            item.Render(outputWriter);
            outputWriter.Flush();
            outputStream.Position = 0;
            var output = Encoding.ASCII.GetString(outputStream.ToArray());
            if (hasNewline)
            {
                AssertEqualWithNewline(expected, output);
            }
            else
            {
                Assert.Equal(expected, output);
            }             
        }
    }
}
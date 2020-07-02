using System.IO;
using System.Threading.Tasks;
using FastHls.Abstractions;
using Xunit;

namespace FastHlsTests
{
    internal static class ManifestGeneratorExtensions
    {
        public static async Task AssertGeneratedContent(this AbstractManifestGenerator generator, string content)
        {
            var outputStream = new MemoryStream();
            await generator.WriteToStream(outputStream);
            outputStream.Position = 0;
            var output = await new StreamReader(outputStream).ReadToEndAsync();

            Assert.Equal(content, output);
        }
    }
}
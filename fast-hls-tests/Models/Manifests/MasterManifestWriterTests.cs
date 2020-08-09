using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Models.Manifests;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models.Manifests
{
    public class MasterManifestWriterTests
    {
        [Theory]
        [InlineData(3, false, @"#EXTM3U
#EXT-X-VERSION:3")]
        [InlineData(3, true, @"#EXTM3U
#EXT-X-VERSION:3
#EXT-X-INDEPENDENT-SEGMENTS")]
        public async Task WritesManifest(int version, bool hasIndependentSegments, string expected)
        {
            var manifest = new MasterManifest(
                version,
                hasIndependentSegments
            );
            var outputStream = new MemoryStream();
            await new MasterManifestWriter(manifest, outputStream).Render();
            outputStream.Position = 0;
            var output = Encoding.ASCII.GetString(outputStream.ToArray());
            AssertEqualWithNewline(expected, output);
        }
    }
}

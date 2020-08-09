using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Models;
using FastHls.Models.Manifests;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models.Manifests
{
    public class MediaManifestWriterTests
    {
        [Theory]
        [InlineData(8, PlaylistType.VOD, 10, null, null, null, null, null, @"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-ENDLIST")]
        [InlineData(8, PlaylistType.EVENT, 10, null, null, null, null, null, @"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0")]
        public async Task WritesManifestHeader(int version, PlaylistType playlistType, double targetDuration, ServerControl? serverControl, double? partDuration, Start? start, Map? map, Encryption? encryption, string expected)
        {
            var manifest = new MediaManifest(
                version,
                playlistType,
                targetDuration,
                serverControl,
                partDuration,
                start,
                map,
                encryption
            );
            var outputStream = new MemoryStream();
            await new MediaManifestWriter(manifest, outputStream).Render();
            outputStream.Position = 0;
            var output = Encoding.ASCII.GetString(outputStream.ToArray());
            AssertEqualWithNewline(expected, output);
        }

        [Fact]
        public async Task WritesDiscontinuitySequence()
        {
            var manifest = new MediaManifest(
                version: 8,
                playlistType: PlaylistType.EVENT,
                targetDuration: 10
            );

            manifest.AddAndIncrementSequence(new Discontinuity());
            manifest.AddAndIncrementSequence(new Discontinuity());

            var outputStream = new MemoryStream();
            await new MediaManifestWriter(manifest, outputStream).Render();
            outputStream.Position = 0;
            var output = Encoding.ASCII.GetString(outputStream.ToArray());
            AssertEqualWithNewline(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-DISCONTINUITY-SEQUENCE:2", output);
        }

        [Fact]
        public async Task WritesMediaSequence()
        {
            var manifest = new MediaManifest(
                version: 8,
                playlistType: PlaylistType.EVENT,
                targetDuration: 10
            );

            manifest.Add(new MediaFile { Path = "test.mp4", Duration = 10 });
            manifest.AddAndIncrementSequence(new MediaFile { Path = "test2.mp4", Duration = 10 });

            var outputStream = new MemoryStream();
            await new MediaManifestWriter(manifest, outputStream).Render();
            outputStream.Position = 0;
            var output = Encoding.ASCII.GetString(outputStream.ToArray());
            AssertEqualWithNewline(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:1
#EXTINF:10.0,
test2.mp4", output);
        }
    }
}

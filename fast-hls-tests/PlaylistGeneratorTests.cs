using System;
using System.IO;
using System.Threading.Tasks;
using FastHls;
using Xunit;

namespace FastHlsTests
{
    internal static class PlaylistGeneratorExtensions
    {
        public static async Task AssertGeneratedContent(this PlaylistGenerator generator, string content)
        {
            var outputStream = new MemoryStream();
            await generator.WriteToStream(outputStream);
            outputStream.Position = 0;
            var output = await new StreamReader(outputStream).ReadToEndAsync();

            Assert.Equal(content, output);
        }
    }

    public class PlaylistGeneratorTests
    {
        private PlaylistGenerator generator = new PlaylistGenerator();

        [Fact]
        public async Task WritesHeader()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
");
        }

        [Fact]
        public async Task WritesEvent()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
");
        }

        [Fact]
        public async Task WritesTargetDuration()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 50);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:50
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
");
        }

        [Fact]
        public async Task WritesVersion()
        {
            generator.Start(PlaylistType.VOD, version: 4, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:4
#EXT-X-MEDIA-SEQUENCE:0
");
        }

        [Fact]
        public async Task WritesMediaFiles()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            generator.AddMediaFile("http://example.com/movie1/fileSequenceA.ts", duration: 10);
            generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts
");
        }

        [Fact]
        public async Task WritesEndTag()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            generator.AddMediaFile("http://example.com/movie1/fileSequenceA.ts", duration: 10);
            generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);
            generator.Finish();

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts
#EXT-X-ENDLIST");
        }
    }
}

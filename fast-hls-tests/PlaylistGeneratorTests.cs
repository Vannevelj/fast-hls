using System;
using System.IO;
using System.Threading.Tasks;
using FastHls;
using Xunit;

namespace FastHlsTests
{
    public class PlaylistGeneratorTests
    {
        private PlaylistGenerator generator = new PlaylistGenerator();

        [Fact]
        public async Task WritesHeader()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            var outputStream = new MemoryStream();
            await generator.WriteToStream(outputStream);
            outputStream.Position = 0;
            var output = await new StreamReader(outputStream).ReadToEndAsync();

            Assert.Equal(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
", output);
        }
    }
}

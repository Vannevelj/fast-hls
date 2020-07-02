using System;
using System.IO;
using System.Threading.Tasks;
using FastHls;
using FastHls.Abstractions;
using FastHls.Models;
using Xunit;

namespace FastHlsTests
{
    public class MediaManifestGeneratorTests
    {
        private MediaManifestGenerator generator = new MediaManifestGenerator();

        [Fact]
        public async Task WritesHeader()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0\r\n");
        }

        [Fact]
        public async Task WritesEvent()
        {
            await generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0\r\n");
        }

        [Fact]
        public async Task WritesTargetDuration()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 50);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:50
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0\r\n");
        }

        [Fact]
        public async Task WritesVersion()
        {
            await generator.Start(PlaylistType.VOD, version: 4, targetDuration: 10);
            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:4
#EXT-X-MEDIA-SEQUENCE:0\r\n");
        }

        [Fact]
        public async Task WritesMediaFiles()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            await generator.AddMediaFile("http://example.com/movie1/fileSequenceA.ts", duration: 10);
            await generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts\r\n");
        }

        [Fact]
        public async Task WritesEndTag()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);
            await generator.AddMediaFile("http://example.com/movie1/fileSequenceA.ts", duration: 10);
            await generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);
            await generator.Finish();

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts
#EXT-X-ENDLIST\r\n");
        }

        [Fact]
        public async Task WritesDiscontinuity()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);

            await generator.AddMediaFile("http://example.com/ads/ad1.ts", duration: 4);
            await generator.InsertDiscontinuity();
            await generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);
            await generator.Finish();

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:4.0,
http://example.com/ads/ad1.ts
#EXT-X-DISCONTINUITY
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts
#EXT-X-ENDLIST\r\n");
        }

        [Fact]
        public async Task WritesDiscontinuitySequence()
        {
            await generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10, discontinuitySequence: 4);
            await generator.AddMediaFile("http://example.com/ads/ad1.ts", duration: 4);
            await generator.Finish();

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-DISCONTINUITY-SEQUENCE:4
#EXTINF:4.0,
http://example.com/ads/ad1.ts
#EXT-X-DISCONTINUITY
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts
#EXT-X-ENDLIST\r\n");
        }

        [Fact]
        public async Task WritesEncryption()
        {
            await generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            await generator.AddEncryption(Encryption.AES128, "https://example.org/enc", iv: "123ABC", keyformat: "abc", keyformatVersions: new int[] { 1, 2 });

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-KEY:METHOD=AES-128,URI=""https://example.org/enc"",IV=""123ABC"",KEYFORMAT=""abc"",KEYFORMATVERSIONS=""1/2""");
        }

        [Fact]
        public async Task WritesStartTag()
        {
            await generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            await generator.AddStartTag(-10.0, isPrecise: true);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-START:TIME-OFFSET-10.0,PRECISE=YES");
        }
    }
}

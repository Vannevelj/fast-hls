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
#EXT-X-ENDLIST
");
        }

        [Fact]
        public async Task WritesDiscontinuity()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10);

            generator.AddMediaFile("http://example.com/ads/ad1.ts", duration: 4);
            generator.InsertDiscontinuity();
            generator.AddMediaFile("http://example.com/movie1/fileSequenceB.ts", duration: 10);
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
#EXT-X-ENDLIST
");
        }

        [Fact]
        public async Task WritesDiscontinuitySequence()
        {
            generator.Start(PlaylistType.VOD, version: 8, targetDuration: 10, discontinuitySequence: 4);
            generator.AddMediaFile("http://example.com/ads/ad1.ts", duration: 4);
            await generator.Finish();

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-DISCONTINUITY-SEQUENCE:4
#EXTINF:4.0,
http://example.com/ads/ad1.ts
#EXT-X-ENDLIST
");
        }

        [Fact]
        public async Task WritesEncryption()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddEncryption(Encryption.AES128, "https://example.org/enc", iv: "123ABC", keyformat: "abc", keyformatVersions: new int[] { 1, 2 });

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-KEY:METHOD=AES-128,URI=""https://example.org/enc"",IV=""123ABC"",KEYFORMAT=""abc"",KEYFORMATVERSIONS=""1/2""
");
        }

        [Fact]
        public async Task WritesStartTag()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddStartTag(-10.5, isPrecise: true);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-START:TIME-OFFSET=-10.5,PRECISE=YES
");
        }

        [Fact]
        public async Task WritesByteRange()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddByteRange(length: 9_876_543, offset: 321);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-BYTERANGE:9876543@321
");
        }

        [Fact]
        public async Task WritesByteRange_NoOffset()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddByteRange(length: 9_876_543);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-BYTERANGE:9876543
");
        }

        [Fact]
        public async Task WritesMap()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddMap(uri: "main.mp4", length: 9_876_543, offset: 123);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-MAP:URI=""main.mp4"",BYTERANGE=""9876543@123""
");
        }

        [Fact]
        public async Task WritesMap_NoOffset()
        {
            generator.Start(PlaylistType.EVENT, version: 8, targetDuration: 10);
            generator.AddMap(uri: "main.mp4", length: 9_876_543);

            await generator.AssertGeneratedContent(@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:EVENT
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:8
#EXT-X-MEDIA-SEQUENCE:0
#EXT-X-MAP:URI=""main.mp4"",BYTERANGE=""9876543""
");
        }
    }
}

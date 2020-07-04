using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Abstractions;
using FastHls.Models;

namespace FastHls
{
    public class MediaManifestGenerator : AbstractManifestGenerator
    {
        public MediaManifestGenerator() : base() {}

        public MediaManifestGenerator(Stream output, bool continuousPersistence) : base(output, continuousPersistence) {}

        public async Task Start(PlaylistType playlistType, int version, double targetDuration, int? discontinuitySequence = null)
        {
            var text = $@"#EXTM3U
#EXT-X-PLAYLIST-TYPE:{playlistType}
#EXT-X-TARGETDURATION:{targetDuration}
#EXT-X-VERSION:{version}
#EXT-X-MEDIA-SEQUENCE:0\r\n";

            if (discontinuitySequence.HasValue) {
                text += $"#EXT-X-DISCONTINUITY-SEQUENCE:{discontinuitySequence.Value}\r\n";
            }

            await Append(text);
        }

        public async Task AddStartTag(double offsetInSeconds, bool isPrecise = false) {
            var text = $"#EXT-X-START:TIME-OFFSET={offsetInSeconds}";

            if (isPrecise) {
                text += ",PRECISE=YES";
            }

            await Append(text);
        }

        public async Task AddEncryption(Encryption encryption, string uri = null, string iv = null, string keyformat = null, int[] keyformatVersions = null) {
            var text = $"#EXT-X-KEY:METHOD={encryption}";

            if (uri != null) {
                text += $",URI=\"{uri}\"";
            }

            if (iv != null) {
                text += $",IV=\"{iv}\"";
            }

            if (keyformat != null) {
                text += $",KEYFORMAT=\"{keyformat}\"";
            }

            if (keyformatVersions != null) {
                text += $",KEYFORMATVERSIONS=\"{string.Join("/", keyformatVersions)}\"";
            }

            await Append(text);
        }

        public async Task AddMediaFile(string path, double duration)
        {
            var text = $@"#EXTINF:{duration:F1},\r\n{path}\r\n";

            await Append(text);
        }

        public async Task InsertDiscontinuity() {
            var text = "#EXT-X-DISCONTINUITY\r\n";
            await Append(text);
        }

        public async Task AddByteRange(int length, int? offset = null) {
            var text = $"#EXT-X-BYTERANGE:{length}";

            if(offset.HasValue) {
                text += $"@{offset}";
            }

            await Append(text);
        }

        public override async Task Finish() 
        {
            var text = "#EXT-X-ENDLIST\r\n";
            await Append(text);

            await base.Finish();
        }
    }
}

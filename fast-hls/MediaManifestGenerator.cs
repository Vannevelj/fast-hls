using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Abstractions;

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
                text += $"EXT-X-DISCONTINUITY-SEQUENCE:{discontinuitySequence.Value}\r\n";
            }

            Append(text);
            await WriteContinuously(text);
        }

        public async Task AddMediaFile(string path, double duration)
        {
            var text = $@"#EXTINF:{duration:F1},\r\n{path}\r\n";

            Append(text);
            await WriteContinuously(text);
        }

        public async Task InsertDiscontinuity() {
            var text = "#EXT-X-DISCONTINUITY\r\n";
            Append(text);
            await WriteContinuously(text);
        }

        public override async Task Finish() 
        {
            var text = "#EXT-X-ENDLIST\r\n";
            Append(text);
            await WriteContinuously(text);

            await base.Finish();
        }
    }
}

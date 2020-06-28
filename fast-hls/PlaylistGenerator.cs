using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FastHls
{
    public class PlaylistGenerator
    {
        private string _playlist = "";

        public void Start(PlaylistType playlistType, int version, double targetDuration) {
            _playlist += "#EXTM3U\r\n";
            _playlist += $"#EXT-X-PLAYLIST-TYPE:{playlistType}\r\n";
            _playlist += $"#EXT-X-TARGETDURATION:{targetDuration}\r\n";
            _playlist += $"#EXT-X-VERSION:{version}\r\n";
            _playlist += "#EXT-X-MEDIA-SEQUENCE:0\r\n";
        }

        public void AddMediaFile(string path, double duration) {
            _playlist += $"#EXTINF:{duration:F1},\r\n";
            _playlist += $"{path}\r\n";
        }

        public void Finish() {
            _playlist += "#EXT-X-ENDLIST";
        }

        public async Task WriteToStream(Stream output) {
            await output.WriteAsync(Encoding.UTF8.GetBytes(_playlist));
        }
    }
}

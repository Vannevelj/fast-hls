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
            _playlist += "#EXT-X-PLAYLIST-TYPE:VOD\r\n";
            _playlist += "#EXT-X-TARGETDURATION:10\r\n";
            _playlist += "#EXT-X-VERSION:8\r\n";
            _playlist += "#EXT-X-MEDIA-SEQUENCE:0\r\n";
        }

        public async Task WriteToStream(Stream output) {
            await output.WriteAsync(Encoding.UTF8.GetBytes(_playlist));
        }
    }
}

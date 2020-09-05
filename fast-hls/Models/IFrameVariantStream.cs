using System.IO;
using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public class IFrameVariantStream : IManifestItem
    {
        public string Uri { get; set; }
        public int Bandwidth { get; set; }
        public int? AverageBandwidth { get; set; }
        public string[]? Codecs { get; set; }
        public Resolution? Resolution { get; set; }
        public string? Video { get; set; }

        public void Render(StreamWriter writer)
        {
            var builder = new StringBuilder();

            writer.Write($"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH={Bandwidth},URI=\"{Uri}\"");

            if (AverageBandwidth.HasValue)
            {
                writer.Write($",AVERAGE-BANDWIDTH={AverageBandwidth.Value}");
            }

            if (Codecs != null)
            {
                writer.Write($",CODECS=\"{string.Join(",", Codecs)}\"");
            }

            if (Resolution.HasValue)
            {
                writer.Write($",RESOLUTION={Resolution.Value.Width}x{Resolution.Value.Height}");
            }

            if (Video != null)
            {
                writer.Write($",VIDEO=\"{Video}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}

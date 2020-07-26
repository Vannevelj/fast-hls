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

        public string Render()
        {
            var builder = new StringBuilder();

            builder.Append($"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH={Bandwidth},URI=\"{Uri}\"");

            if (AverageBandwidth.HasValue)
            {
                builder.Append($",AVERAGE-BANDWIDTH={AverageBandwidth.Value}");
            }

            if (Codecs != null)
            {
                builder.Append($",CODECS=\"{string.Join(",", Codecs)}\"");
            }

            if (Resolution.HasValue)
            {
                builder.Append($",RESOLUTION={Resolution.Value.Width}x{Resolution.Value.Height}");
            }

            if (Video != null)
            {
                builder.Append($",VIDEO=\"{Video}\"");
            }

            builder.AppendNormalizedNewline();

            return builder.ToString();
        }
    }
}

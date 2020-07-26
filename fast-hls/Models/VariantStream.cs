

using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct VariantStream: IManifestItem
    {
        public string Uri { get; set; }
        public int Bandwidth { get; set; }
        public int? AverageBandwidth { get; set; }
        public string[]? Codecs { get; set; }
        public Resolution? Resolution { get; set; }
        public string? Audio { get; set; }
        public string? Video { get; set; }
        public string? Subtitles { get; set; }
        public string? ClosedCaptions { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();

            builder.Append($"#EXT-X-STREAM-INF:BANDWIDTH={Bandwidth}");

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

            if (Audio != null)
            {
                builder.Append($",AUDIO=\"{Audio}\"");
            }

            if (Video != null)
            {
                builder.Append($",VIDEO=\"{Video}\"");
            }

            if (Subtitles != null)
            {
                builder.Append($",SUBTITLES=\"{Subtitles}\"");
            }

            if (ClosedCaptions != null)
            {
                builder.Append($",CLOSED-CAPTIONS=\"{ClosedCaptions}\"");
            }

            builder.AppendNormalizedNewline();
            builder.AppendNormalizedline(Uri);
            return builder.ToString();
        }
    }
}

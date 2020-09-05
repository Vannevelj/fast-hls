

using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct VariantStream : IManifestItem
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

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-STREAM-INF:BANDWIDTH={Bandwidth}");

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

            if (Audio != null)
            {
                writer.Write($",AUDIO=\"{Audio}\"");
            }

            if (Video != null)
            {
                writer.Write($",VIDEO=\"{Video}\"");
            }

            if (Subtitles != null)
            {
                writer.Write($",SUBTITLES=\"{Subtitles}\"");
            }

            if (ClosedCaptions != null)
            {
                writer.Write($",CLOSED-CAPTIONS=\"{ClosedCaptions}\"");
            }

            writer.AppendNormalizedNewline();
            writer.AppendNormalizedline(Uri);
        }
    }
}

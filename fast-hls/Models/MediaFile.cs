using System.Text;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct MediaFile : ITimelineItem, IManifestItem
    {
        public string Path { get; set; }
        public double Duration { get; set; }
        public ByteRange? ByteRange { get; set; }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"#EXTINF:{Duration:F1},");
            if (ByteRange.HasValue)
            {
                sb.AppendLine($"#EXT-X-BYTERANGE:{ByteRange}");
            }
            sb.AppendLine(Path);

            return sb.ToString();
        }
    }
}
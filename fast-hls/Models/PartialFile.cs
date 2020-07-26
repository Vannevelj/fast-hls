using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct PartialFile : ITimelineItem, IManifestItem
    {
        public string Path { get; set; }
        public double Duration { get; set; }
        public bool? IsIndependent { get; set; }
        public ByteRange? ByteRange { get; set; }
        public bool? HasGap { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-PART:DURATION={Duration},URI=\"{Path}\"");

            if (IsIndependent.HasValue)
            {
                builder.Append($",INDEPENDENT={(IsIndependent.Value ? "YES" : "NO")}");
            }

            if (ByteRange.HasValue)
            {
                builder.Append($",BYTERANGE={ByteRange.Value}");
            }

            if (HasGap.HasValue)
            {
                builder.Append($",GAP={(HasGap.Value ? "YES" : "NO")}");
            }

            builder.AppendNormalizedNewline();
            return builder.ToString();
        }
    }
}
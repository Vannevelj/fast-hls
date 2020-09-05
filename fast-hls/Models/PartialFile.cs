using System.IO;
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

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-PART:DURATION={Duration},URI=\"{Path}\"");

            if (IsIndependent.HasValue)
            {
                writer.Write($",INDEPENDENT={( IsIndependent.Value ? "YES" : "NO" )}");
            }

            if (ByteRange.HasValue)
            {
                writer.Write($",BYTERANGE={ByteRange.Value}");
            }

            if (HasGap.HasValue)
            {
                writer.Write($",GAP={( HasGap.Value ? "YES" : "NO" )}");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
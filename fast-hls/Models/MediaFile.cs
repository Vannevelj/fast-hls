using System;
using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct MediaFile : ITimelineItem, IManifestItem
    {
        public string Path { get; set; }
        public double Duration { get; set; }
        public ByteRange? ByteRange { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.AppendNormalizedline(FormattableString.Invariant($"#EXTINF:{Duration:F1},"));
            if (ByteRange.HasValue)
            {
                writer.AppendNormalizedline($"#EXT-X-BYTERANGE:{ByteRange}");
            }
            writer.AppendNormalizedline(Path);
        }
    }
}
using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct RenditionReport : IManifestItem
    {
        public string Path { get; set; }
        public int LastMsn { get; set; }
        public int? LastPart { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-RENDITION-REPORT:URI=\"{Path}\",LAST-MSN={LastMsn}");

            if (LastPart.HasValue)
            {
                writer.Write($",LAST-PART={LastPart.Value}");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
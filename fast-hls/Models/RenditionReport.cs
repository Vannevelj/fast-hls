using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct RenditionReport: IManifestItem
    {
        public string Path { get; set; }
        public int LastMsn { get; set; }
        public int? LastPart { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-RENDITION-REPORT:URI=\"{Path}\",LAST-MSN={LastMsn}");

            if (LastPart.HasValue)
            {
                builder.Append($",LAST-PART={LastPart.Value}");
            }

            builder.AppendNormalizedNewline();
            return builder.ToString();
        }
    }
}
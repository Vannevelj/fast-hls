using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Map : IManifestItem
    {
        public string Uri { get; set; }
        public ByteRange? ByteRange { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-MAP:URI=\"{Uri}\"");

            if (ByteRange.HasValue)
            {
                builder.Append($",BYTERANGE=\"{ByteRange.Value}\"");
            }

            builder.AppendNormalizedNewline();
            return builder.ToString();
        }
    }
}
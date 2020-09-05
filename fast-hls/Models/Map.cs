using System.IO;
using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Map : IManifestItem
    {
        public string Uri { get; set; }
        public ByteRange? ByteRange { get; set; }

        public void Render(StreamWriter writer)
        {
            var builder = new StringBuilder();
            writer.Write($"#EXT-X-MAP:URI=\"{Uri}\"");

            if (ByteRange.HasValue)
            {
                writer.Write($",BYTERANGE=\"{ByteRange.Value}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
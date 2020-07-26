using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct SessionData : IManifestItem
    {
        public string DataId { get; set; }
        public string? Value { get; set; }
        public string? Uri { get; set; }
        public string? Language { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-SESSION-DATA:DATA-ID=\"{DataId}\"");

            if (Value != null)
            {
                builder.Append($",VALUE=\"{Value}\"");
            }

            if (Uri != null)
            {
                builder.Append($",URI=\"{Uri}\"");
            }

            if (Language != null)
            {
                builder.Append($",LANGUAGE=\"{Language}\"");
            }

            builder.AppendNormalizedNewline();
            return builder.ToString();
        }
    }
}

using System.IO;
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

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-SESSION-DATA:DATA-ID=\"{DataId}\"");

            if (Value != null)
            {
                writer.Write($",VALUE=\"{Value}\"");
            }

            if (Uri != null)
            {
                writer.Write($",URI=\"{Uri}\"");
            }

            if (Language != null)
            {
                writer.Write($",LANGUAGE=\"{Language}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}

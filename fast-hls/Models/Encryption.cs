using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Encryption : IManifestItem
    {
        public EncryptionKind EncryptionKind { get; set; }
        public string? Uri { get; set; }
        public string? IV { get; set; }
        public string? KeyFormat { get; set; }
        public int[]? KeyFormatVersions { get; set; }

        public void Render(StreamWriter writer)
        {
            
            writer.Write($"#EXT-X-KEY:METHOD={EncryptionKind}");

            if (Uri != null)
            {
                writer.Write($",URI=\"{Uri}\"");
            }

            if (IV != null)
            {
                writer.Write($",IV=\"{IV}\"");
            }

            if (KeyFormat != null)
            {
                writer.Write($",KEYFORMAT=\"{KeyFormat}\"");
            }

            if (KeyFormatVersions != null)
            {
                writer.Write($",KEYFORMATVERSIONS=\"{string.Join("/", KeyFormatVersions)}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Encryption : IManifestItem
    {
        public EncryptionKind EncryptionKind {get;set;}
        public string? Uri {get;set;}
        public string? IV {get;set;}
        public string? KeyFormat {get;set;}
        public int[]? KeyFormatVersions {get;set;}

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-KEY:METHOD={EncryptionKind}");

            if (Uri != null)
            {
                builder.Append($",URI=\"{Uri}\"");
            }

            if (IV != null)
            {
                builder.Append($",IV=\"{IV}\"");
            }

            if (KeyFormat != null)
            {
                builder.Append($",KEYFORMAT=\"{KeyFormat}\"");
            }

            if (KeyFormatVersions != null)
            {
                builder.Append($",KEYFORMATVERSIONS=\"{string.Join("/", KeyFormatVersions)}\"");
            }

            builder.AppendNormalizedNewline();

            return builder.ToString();
        }
    }
}
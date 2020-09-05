using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Media : IManifestItem
    {
        public MediaType MediaType { get; set; }
        public string GroupId { get; set; }
        public string Name { get; set; }
        public string? Uri { get; set; }
        public string? Language { get; set; }
        public string? AssocLanguage { get; set; }
        public bool IsDefault { get; set; }
        public bool AutoSelect { get; set; }
        public bool Forced { get; set; }
        public string? InstreamId { get; set; }
        public string[]? Characteristics { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-MEDIA:TYPE={MediaType},GROUP-ID=\"{GroupId}\",NAME=\"{Name}\"");

            if (Language != null)
            {
                writer.Write($",LANGUAGE=\"{Language}\"");
            }

            if (AssocLanguage != null)
            {
                writer.Write($",ASSOC-LANGUAGE=\"{AssocLanguage}\"");
            }

            if (IsDefault)
            {
                writer.Write(",DEFAULT=YES");
            }

            if (AutoSelect)
            {
                writer.Write(",AUTOSELECT=YES");
            }

            if (Forced)
            {
                writer.Write(",FORCED=YES");
            }

            if (InstreamId != null)
            {
                writer.Write($",INSTREAM-ID=\"{InstreamId}\"");
            }

            if (Characteristics != null)
            {
                writer.Write($",CHARACTERISTICS=\"{string.Join(",", Characteristics)}\"");
            }

            if (Uri != null)
            {
                writer.Write($",URI=\"{Uri}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}


using System.Text;
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
        public string? Language { get; set;}
        public string AssocLanguage {get;set;}
        public bool IsDefault {get;set;}
        public bool AutoSelect {get;set;}
        public bool Forced {get;set;}
        public string? InstreamId {get;set;}
        public string[]? Characteristics {get;set;}

        public string Render()
        {
            var builder = new StringBuilder();

            builder.Append($"#EXT-X-MEDIA:TYPE={MediaType},GROUP-ID=\"{GroupId}\",NAME=\"{Name}\"");

            if (Language != null)
            {
                builder.Append($",LANGUAGE=\"{Language}\"");
            }

            if (AssocLanguage != null)
            {
                builder.Append($",ASSOC-LANGUAGE=\"{AssocLanguage}\"");
            }

            if (IsDefault)
            {
                builder.Append(",DEFAULT=YES");
            }

            if (AutoSelect)
            {
                builder.Append(",AUTOSELECT=YES");
            }

            if (Forced)
            {
                builder.Append(",FORCED=YES");
            }

            if (InstreamId != null)
            {
                builder.Append($",INSTREAM-ID=\"{InstreamId}\"");
            }

            if (Characteristics != null)
            {
                builder.Append($",CHARACTERISTICS=\"{string.Join(",", Characteristics)}\"");
            }

            if (Uri != null)
            {
                builder.Append($",URI=\"{Uri}\"");
            }

            builder.AppendNormalizedNewline();

            return builder.ToString();
        }
    }
}


using System.Text;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct PreloadHint : IManifestItem
    {
        public HintType Type { get; set; }
        public string Path { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-PRELOAD-HINT:TYPE={Type},URI=\"{Path}\"");

            if (Start.HasValue)
            {
                builder.Append($",BYTERANGE-START=\"{Start.Value}\"");
            }

            if (Length.HasValue)
            {
                builder.Append($",BYTERANGE-LENGTH=\"{Length.Value}\"");
            }

            builder.AppendNormalizedNewline();
            return builder.ToString();
        }
    }
}
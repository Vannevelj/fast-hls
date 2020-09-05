using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct PreloadHint : IManifestItem
    {
        public HintType Type { get; set; }
        public string Path { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-PRELOAD-HINT:TYPE={Type},URI=\"{Path}\"");

            if (Start.HasValue)
            {
                writer.Write($",BYTERANGE-START=\"{Start.Value}\"");
            }

            if (Length.HasValue)
            {
                writer.Write($",BYTERANGE-LENGTH=\"{Length.Value}\"");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
using System;
using System.Text;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models.Manifests
{
    public struct Start : IManifestItem
    {
        public TimeSpan Offset { get; set; }
        public bool? IsPrecise { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();
            builder.Append($"#EXT-X-START:TIME-OFFSET={Offset.TotalSeconds}");

            if (IsPrecise.HasValue && IsPrecise.Value)
            {
                builder.Append(",PRECISE=YES");
            }

            builder.AppendNormalizedNewline();

            return builder.ToString();
        }
    }
}
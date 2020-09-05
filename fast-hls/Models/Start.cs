using System;
using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models.Manifests
{
    public struct Start : IManifestItem
    {
        public TimeSpan Offset { get; set; }
        public bool? IsPrecise { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.Write(FormattableString.Invariant($"#EXT-X-START:TIME-OFFSET={Offset.TotalSeconds}"));

            if (IsPrecise.HasValue && IsPrecise.Value)
            {
                writer.Write(",PRECISE=YES");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
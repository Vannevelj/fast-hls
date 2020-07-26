using System.Collections.Generic;
using FastHls.Abstractions;

namespace FastHls.Models.Manifests
{
    public class MasterManifest : AbstractManifestGenerator
    {
        private int Version { get; set; }
        private bool? HasIndependentSegments { get; set; }
        public List<Media> Media { get; set; } = new List<Media>();
        public List<VariantStream> VariantStreams { get; set; } = new List<VariantStream>();
        public List<IFrameVariantStream> IFrameVariantStreams { get; set; } = new List<IFrameVariantStream>();
        public List<SessionData> SessionData { get; set; } = new List<SessionData>();

        public MasterManifest(int version)
        {
            Version = version;
        }

        public void Render()
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-VERSION:{Version}");

            if (HasIndependentSegments.HasValue && HasIndependentSegments.Value)
            {
                AppendLine($"#EXT-X-INDEPENDENT-SEGMENTS");
            }

            foreach (var media in Media)
            {
                Append(media.Render());
            }

            foreach (var variantStream in VariantStreams)
            {
                Append(variantStream.Render());
            }

            foreach (var iframeVariantStream in IFrameVariantStreams)
            {
                Append(iframeVariantStream.Render());
            }

            foreach (var sessionData in SessionData)
            {
                Append(sessionData.Render());
            }
        }
    }
}

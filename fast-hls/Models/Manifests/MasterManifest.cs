using FastHls.Abstractions;

namespace FastHls.Models.Manifests
{
    public class MasterManifest : AbstractManifestGenerator
    {
        public int Version { get; set; }
        public bool HasIndependentSegments { get; set; }
        public List<Media> Media { get; set; }
        public List<VariantStream> VariantStreams { get; set; }
        public List<IFrameVariantStream> IFrameVariantStreams { get; set; }
        public List<SessionData> SessionData { get; set; }

        private void Render()
        {
            AppendLine("#EXTM3U");
            AppendLine($"#EXT-X-VERSION:{Version}");

            if (HasIndependentSegments)
            {
                AppendLine($"#EXT-X-INDEPENDENT-SEGMENTS");
            }
        }
    }
}

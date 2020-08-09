using System.Collections.Generic;

namespace FastHls.Models.Manifests
{
    public class MasterManifest
    {
        public int Version { get; }
        public bool? HasIndependentSegments { get; }
        public List<Media> Media { get; set; } = new List<Media>();
        public List<VariantStream> VariantStreams { get; set; } = new List<VariantStream>();
        public List<IFrameVariantStream> IFrameVariantStreams { get; set; } = new List<IFrameVariantStream>();
        public List<SessionData> SessionData { get; set; } = new List<SessionData>();

        public MasterManifest(int version, bool hasIndependentSegments = false)
        {
            Version = version;
            HasIndependentSegments = hasIndependentSegments;
        }
    }
}

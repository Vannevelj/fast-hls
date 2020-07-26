using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Discontinuity : ITimelineItem, IManifestItem {
        public string Render() => "#EXT-X-DISCONTINUITY";
    }
}
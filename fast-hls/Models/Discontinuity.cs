using System.IO;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct Discontinuity : ITimelineItem, IManifestItem
    {
        public void Render(StreamWriter writer) => writer.Write("#EXT-X-DISCONTINUITY");
    }
}
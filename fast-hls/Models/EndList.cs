using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct EndList : IManifestItem
    {
        public string Render() => "#EXT-X-ENDLIST";
    }
}
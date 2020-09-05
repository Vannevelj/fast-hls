using System.IO;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct EndList : IManifestItem
    {
        public void Render(StreamWriter writer) => writer.Write("#EXT-X-ENDLIST");
    }
}
using System.IO;

namespace FastHls.Models.Interfaces
{
    public interface IManifestItem
    {
        void Render(StreamWriter writer);
    }
}
using System.IO;

namespace FastHls.Models.Interfaces
{
    public interface ITimelineItem
    {
        void Render(StreamWriter writer);
    }
}
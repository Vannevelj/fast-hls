namespace FastHls.Models
{
    public struct MediaFile : ITimelineItem
    {
        public string Path {get;set;}
        public double Duration {get;set;}
        public ByteRange? ByteRange {get;set;}
    }
}
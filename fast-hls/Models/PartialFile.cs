namespace FastHls.Models
{
    public struct PartialFile : ITimelineItem
    {
        public string Path {get;set;}
        public double Duration {get;set;}
        public bool? IsDependent {get;set;}
        public ByteRange? ByteRange {get;set;}
        public bool? HasGap {get;set;}
    }
}
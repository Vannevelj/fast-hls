namespace FastHls.Models
{
    public struct RenditionReport
    {
        public string Path {get;set;}
        public int LastMsn {get;set;}
        public int? LastPart {get;set;}
    }
}
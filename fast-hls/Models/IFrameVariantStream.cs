namespace FastHls.Models 
{
    public class IFrameVariantStream 
    { 
        public string Uri {get;set;}
        public int Bandwidth {get;set;}
        public int? AverageBandwidth {get;set;}
        public string[]? codecs {get;set;}
        public Resolution? Resolution {get;set;}
        public string? Video {get;set;}
    }
}

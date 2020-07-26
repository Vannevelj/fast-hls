

namespace FastHls.Models
{
    public class VariantStream
    {
        public string Uri { get; set; }
        public int Bandwidth { get; set; }
        public int? AverageBandwidth { get; set; }
        public string[]? Codecs { get; set; }
        public Resolution? Resolution { get; set; }
        public string? Audio { get; set; }
        public string? Video { get; set; }
        public string? Subtitles { get; set; }
        public string? ClosedCaptions { get; set; }
    }
}

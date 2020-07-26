namespace FastHls.Models
{
    public struct PreloadHint
    {
        public HintType Type { get; set; }
        public string Path { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
    }
}
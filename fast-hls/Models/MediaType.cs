namespace FastHls.Models
{
    public class MediaType
    {
        private readonly string _value;

        private MediaType(string value) => _value = value;

        public static readonly MediaType AUDIO = "AUDIO";
        public static readonly MediaType VIDEO = "VIDEO";
        public static readonly MediaType SUBTITLES = "SUBTITLES";
        public static readonly MediaType CLOSEDCAPTIONS = "CLOSED-CAPTIONS";

        public static implicit operator MediaType(string s) => new MediaType(s);
        public override string ToString() => _value;
    }
}
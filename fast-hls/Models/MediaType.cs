namespace FastHls.Models
{
    public class MediaType
    {
        private readonly string value;

        private MediaType(string value) => this.value = value;

        public static readonly MediaType AUDIO = "AUDIO";
        public static readonly MediaType VIDEO = "VIDEO";
        public static readonly MediaType SUBTITLES = "SUBTITLES";
        public static readonly MediaType CLOSEDCAPTIONS = "CLOSED-CAPTIONS";

        public static implicit operator MediaType(string s) => new MediaType(s);
        public override string ToString() => value;
    }
}
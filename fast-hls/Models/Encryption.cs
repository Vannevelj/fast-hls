namespace FastHls.Models
{
    public class Encryption
    {
        private readonly string value;

        private Encryption(string value) => this.value = value;

        public static readonly Encryption AES128 = "AES-128";
        public static readonly Encryption SAMPLEAES = "SAMPLE=AES";

        public static implicit operator Encryption(string s) => new Encryption(s);
        public override string ToString() => value;
    }
}
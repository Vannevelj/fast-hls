namespace FastHls.Models
{
    public class EncryptionKind
    {
        private readonly string _value;

        private EncryptionKind(string value) => this._value = value;

        public static readonly EncryptionKind AES128 = "AES-128";
        public static readonly EncryptionKind SAMPLEAES = "SAMPLE-AES";

        public static implicit operator EncryptionKind(string s) => new EncryptionKind(s);
        public override string ToString() => _value;
    }
}
namespace FastHls.Models
{
    public struct Encryption
    {
        public EncryptionKind EncryptionKind {get;set;}
        public string? Uri {get;set;}
        public string? IV {get;set;}
        public string? KeyFormat {get;set;}
        public int[]? KeyFormatVersions {get;set;}
    }
}
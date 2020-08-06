using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class EncryptionTests
    {
        [Theory]
        [InlineData(EncryptionKind.AES128, "https://example.org/enc", "123ABC", "abc", @"#EXT-X-KEY:METHOD=AES-128,URI=""https://example.org/enc"",IV=""123ABC"",KEYFORMAT=""abc"",")]
        public void WritesEncryption(EncryptionKind encryptionKind, string? uri, string? iv, string? keyformat, string expected)
        {
            var encryption = new Encryption
            {
                EncryptionKind = encryptionKind,
                Uri = uri,
                IV = iv,
                KeyFormat = keyformat
            };
            Assert.Equal(expected, encryption.Render());
        }
    }
}

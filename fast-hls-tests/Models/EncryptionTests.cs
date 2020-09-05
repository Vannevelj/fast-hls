using System.Collections.Generic;
using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class EncryptionTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { EncryptionKind.AES128, "https://example.org/enc", "123ABC", "abc", @"#EXT-X-KEY:METHOD=AES-128,URI=""https://example.org/enc"",IV=""123ABC"",KEYFORMAT=""abc""" },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void WritesEncryption(EncryptionKind encryptionKind, string? uri, string? iv, string? keyformat, string expected)
        {
            var encryption = new Encryption
            {
                EncryptionKind = encryptionKind,
                Uri = uri,
                IV = iv,
                KeyFormat = keyformat
            };
            AssertStreamContentEqual(expected, encryption);
        }
    }
}

using System.Collections.Generic;
using FastHls.Models;
using Xunit;
using static FastHlsTests.AssertExtensions;

namespace FastHlsTests.Models
{
    public class MediaTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { MediaType.CLOSEDCAPTIONS, "audio-hi", "Dutch", "dutch.m3u8", null, null, false, false, false, null, null, @"#EXT-X-MEDIA:TYPE=CLOSED-CAPTIONS,GROUP-ID=""audio-hi"",NAME=""Dutch"",URI=""dutch.m3u8""" },
                new object[] { MediaType.AUDIO, "audio-hi", "Dutch", "dutch.m3u8", "nl-BE", "nl-NL", true, true, false, null, new string[] { "public.accessibility.describes-video" }, @"#EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID=""audio-hi"",NAME=""Dutch"",LANGUAGE=""nl-BE"",ASSOC-LANGUAGE=""nl-NL"",DEFAULT=YES,AUTOSELECT=YES,CHARACTERISTICS=""public.accessibility.describes-video"",URI=""dutch.m3u8""" },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void WritesMedia(MediaType mediaType, string groupId, string name, string? uri, string? language, string? assocLanguage, bool isDefault, bool autoSelect, bool forced, string? instreamId, string[]? characteristics, string expected)
        {
            var media = new Media
            {
                MediaType = mediaType,
                GroupId = groupId,
                Name = name,
                Uri = uri,
                Language = language,
                AssocLanguage = assocLanguage,
                IsDefault = isDefault,
                AutoSelect = autoSelect,
                Forced = forced,
                InstreamId = instreamId,
                Characteristics = characteristics
            };
            AssertStreamContentEqual(expected, media);
        }
    }
}

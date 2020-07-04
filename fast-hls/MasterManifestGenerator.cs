using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Abstractions;
using FastHls.Models;

namespace FastHls
{
    public class MasterManifestGenerator : AbstractManifestGenerator
    {
        public MasterManifestGenerator() : base() { }

        public MasterManifestGenerator(Stream output, bool continuousPersistence) : base(output, continuousPersistence) { }

        public async Task Start(int version, bool independentSegments = true)
        {
            var text = $@"#EXTM3U
#EXT-X-VERSION:{version}\r\n";

            if (independentSegments)
            {
                text += $"#EXT-X-INDEPENDENT-SEGMENTS\r\n";
            }

            await Append(text);
        }

        public async Task AddMedia(
            MediaType mediaType,
            string groupId,
            string name,
            string uri = null,
            string language = null,
            string assocLanguage = null,
            bool isDefault = false,
            bool autoselect = false,
            bool forced = false,
            string instreamId = null,
            string[] characteristics = null) // TODO: turn characteristics this into an enum
        {
            var text = $"#EXT-X-MEDIA:TYPE={mediaType},GROUP-ID=\"{groupId}\",NAME=\"{name}\"";

            if (language != null)
            {
                text += $",LANGUAGE=\"{language}\"";
            }

            if (assocLanguage != null)
            {
                text += $",ASSOC-LANGUAGE=\"{assocLanguage}\"";
            }

            if (isDefault)
            {
                text += ",DEFAULT=YES";
            }

            if (autoselect)
            {
                text += ",AUTOSELECT=YES";
            }

            if (forced)
            {
                text += ",FORCED=YES";
            }

            if (instreamId != null)
            {
                text += $",INSTREAM-ID=\"{instreamId}\"";
            }

            if (characteristics != null)
            {
                text += $",CHARACTERISTICS=\"{string.Join(",", characteristics)}\"";
            }

            if (uri != null)
            {
                text += $",URI=\"{uri}\"";
            }

            await Append(text);
        }

        public async Task AddVariantStream(
            string uri,
            int bandwidth,
            int? averageBandwidth = null,
            string[] codecs = null,
            Resolution? resolution = null,
            string audio = null,
            string video = null,
            string subtitles = null,
            string closedCaptions = null)
        {
            var text = $"#EXT-X-STREAM-INF:BANDWIDTH={bandwidth}";

            if (averageBandwidth.HasValue)
            {
                text += $",AVERAGE-BANDWIDTH={averageBandwidth.Value}";
            }

            if (codecs != null)
            {
                text += $",CODECS=\"{string.Join(",", codecs)}\"";
            }

            if (resolution.HasValue)
            {
                text += $",RESOLUTION={resolution.Value.Width}x{resolution.Value.Height}";
            }

            if (audio != null)
            {
                text += $",AUDIO=\"{audio}\"";
            }

            if (video != null)
            {
                text += $",VIDEO=\"{video}\"";
            }

            if (subtitles != null)
            {
                text += $",SUBTITLES=\"{subtitles}\"";
            }

            if (closedCaptions != null)
            {
                text += $",CLOSED-CAPTIONS=\"{closedCaptions}\"";
            }

            text += $"\r\n{uri}\r\n";
            await Append(text);
        }

        public async Task AddIFrameVariantStream(
            string uri,
            int bandwidth,
            int? averageBandwidth = null,
            string[] codecs = null,
            Resolution? resolution = null,
            string video = null)
        {
            var text = $"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH={bandwidth},URI=\"{uri}\"";

            if (averageBandwidth.HasValue)
            {
                text += $",AVERAGE-BANDWIDTH={averageBandwidth.Value}";
            }

            if (codecs != null)
            {
                text += $",CODECS=\"{string.Join(",", codecs)}\"";
            }

            if (resolution.HasValue)
            {
                text += $",RESOLUTION={resolution.Value.Width}x{resolution.Value.Height}";
            }

            if (video != null)
            {
                text += $",VIDEO=\"{video}\"";
            }

            await Append(text);
        }

        public async Task AddSessionData(
            string dataId,
            string value = null,
            string uri = null,
            string language = null)
        {
            var text = $"#EXT-X-SESSION-DATA:DATA-ID=\"{dataId}\"";

            if (value != null)
            {
                text += $",VALUE=\"{value}\"";
            }

            if (uri != null)
            {
                text += $",URI=\"{uri}\"";
            }

            if (language != null)
            {
                text += $",LANGUAGE=\"{language}\"";
            }

            await Append(text);
        }
    }
}
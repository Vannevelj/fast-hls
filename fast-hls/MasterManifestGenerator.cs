using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Abstractions;
using FastHls.Extensions;
using FastHls.Models;

namespace FastHls
{
    public class MasterManifestGenerator : AbstractManifestGenerator
    {
        public MasterManifestGenerator() : base() { }

        public MasterManifestGenerator(Stream output, bool continuousPersistence) : base(output, continuousPersistence) { }

        public void Start(int version, bool independentSegments = true)
        {
            AppendLine("#EXTM3U");
            AppendLine($"#EXT-X-VERSION:{version}");

            if (independentSegments)
            {
                AppendLine($"#EXT-X-INDEPENDENT-SEGMENTS");
            }
        }

        public void AddMedia(
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
            var builder = _stringBuilderPool.Get();

            builder.Append($"#EXT-X-MEDIA:TYPE={mediaType},GROUP-ID=\"{groupId}\",NAME=\"{name}\"");

            if (language != null)
            {
                builder.Append($",LANGUAGE=\"{language}\"");
            }

            if (assocLanguage != null)
            {
                builder.Append($",ASSOC-LANGUAGE=\"{assocLanguage}\"");
            }

            if (isDefault)
            {
                builder.Append(",DEFAULT=YES");
            }

            if (autoselect)
            {
                builder.Append(",AUTOSELECT=YES");
            }

            if (forced)
            {
                builder.Append(",FORCED=YES");
            }

            if (instreamId != null)
            {
                builder.Append($",INSTREAM-ID=\"{instreamId}\"");
            }

            if (characteristics != null)
            {
                builder.Append($",CHARACTERISTICS=\"{string.Join(",", characteristics)}\"");
            }

            if (uri != null)
            {
                builder.Append($",URI=\"{uri}\"");
            }

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddVariantStream(
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
            var builder = _stringBuilderPool.Get();

            builder.Append($"#EXT-X-STREAM-INF:BANDWIDTH={bandwidth}");

            if (averageBandwidth.HasValue)
            {
                builder.Append($",AVERAGE-BANDWIDTH={averageBandwidth.Value}");
            }

            if (codecs != null)
            {
                builder.Append($",CODECS=\"{string.Join(",", codecs)}\"");
            }

            if (resolution.HasValue)
            {
                builder.Append($",RESOLUTION={resolution.Value.Width}x{resolution.Value.Height}");
            }

            if (audio != null)
            {
                builder.Append($",AUDIO=\"{audio}\"");
            }

            if (video != null)
            {
                builder.Append($",VIDEO=\"{video}\"");
            }

            if (subtitles != null)
            {
                builder.Append($",SUBTITLES=\"{subtitles}\"");
            }

            if (closedCaptions != null)
            {
                builder.Append($",CLOSED-CAPTIONS=\"{closedCaptions}\"");
            }

            builder.AppendNormalizedNewline();
            builder.AppendNormalizedline(uri);
            Append(builder.ToString());
        }

        public void AddIFrameVariantStream(
            string uri,
            int bandwidth,
            int? averageBandwidth = null,
            string[] codecs = null,
            Resolution? resolution = null,
            string video = null)
        {
            var builder = _stringBuilderPool.Get();

            builder.Append($"#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH={bandwidth},URI=\"{uri}\"");

            if (averageBandwidth.HasValue)
            {
                builder.Append($",AVERAGE-BANDWIDTH={averageBandwidth.Value}");
            }

            if (codecs != null)
            {
                builder.Append($",CODECS=\"{string.Join(",", codecs)}\"");
            }

            if (resolution.HasValue)
            {
                builder.Append($",RESOLUTION={resolution.Value.Width}x{resolution.Value.Height}");
            }

            if (video != null)
            {
                builder.Append($",VIDEO=\"{video}\"");
            }

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddSessionData(
            string dataId,
            string value = null,
            string uri = null,
            string language = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-SESSION-DATA:DATA-ID=\"{dataId}\"");

            if (value != null)
            {
                builder.Append($",VALUE=\"{value}\"");
            }

            if (uri != null)
            {
                builder.Append($",URI=\"{uri}\"");
            }

            if (language != null)
            {
                builder.Append($",LANGUAGE=\"{language}\"");
            }

            builder.AppendNormalizedNewline();
            Append(builder.ToString());
        }
    }
}
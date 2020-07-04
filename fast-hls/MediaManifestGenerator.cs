using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Abstractions;
using FastHls.Extensions;
using FastHls.Models;

namespace FastHls
{
    public class MediaManifestGenerator : AbstractManifestGenerator
    {
        public MediaManifestGenerator() : base() {}

        public MediaManifestGenerator(Stream output, bool continuousPersistence) : base(output, continuousPersistence) {}

        public void Start(PlaylistType playlistType, int version, double targetDuration, int? discontinuitySequence = null)
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-PLAYLIST-TYPE:{playlistType}");
            AppendLine($"#EXT-X-TARGETDURATION:{targetDuration}");
            AppendLine($"#EXT-X-VERSION:{version}");
            AppendLine($"#EXT-X-MEDIA-SEQUENCE:0");

            if (discontinuitySequence.HasValue)
            {
                AppendLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{discontinuitySequence.Value}");
            }
        }

        public void AddStartTag(double offsetInSeconds, bool isPrecise = false)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-START:TIME-OFFSET={offsetInSeconds}");

            if (isPrecise)
            {
                builder.Append(",PRECISE=YES");
            }

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddEncryption(Encryption encryption, string uri = null, string iv = null, string keyformat = null, int[] keyformatVersions = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-KEY:METHOD={encryption}");

            if (uri != null)
            {
                builder.Append($",URI=\"{uri}\"");
            }

            if (iv != null)
            {
                builder.Append($",IV=\"{iv}\"");
            }

            if (keyformat != null)
            {
                builder.Append($",KEYFORMAT=\"{keyformat}\"");
            }

            if (keyformatVersions != null)
            {
                builder.Append($",KEYFORMATVERSIONS=\"{string.Join("/", keyformatVersions)}\"");
            }

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddMediaFile(string path, double duration)
        {
            AppendLine($"#EXTINF:{duration:F1},");
            AppendLine(path);
        }

        public void InsertDiscontinuity()
        {
            AppendLine("#EXT-X-DISCONTINUITY");
        }

        public void AddByteRange(int length, int? offset = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-BYTERANGE:{length}");

            if (offset.HasValue)
            {
                builder.Append($"@{offset}");
            }

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddMap(string uri, int? length = null, int? offset = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-MAP:URI=\"{uri}\"");

            if (length.HasValue && !offset.HasValue)
            {
                builder.Append($",BYTERANGE=\"{length}\"");
            }

            if (length.HasValue && offset.HasValue)
            {
                builder.Append($",BYTERANGE=\"{length}@{offset}\"");
            }

            builder.AppendNormalizedNewline();
            Append(builder.ToString());
        }

        public override async Task Finish() 
        {
            AppendLine("#EXT-X-ENDLIST");

            await base.Finish();
        }
    }
}

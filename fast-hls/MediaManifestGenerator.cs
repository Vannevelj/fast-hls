using FastHls.Abstractions;
using FastHls.Extensions;
using FastHls.Models;

namespace FastHls
{
    public class MediaManifestGenerator : AbstractManifestGenerator
    {
        public MediaManifestGenerator() : base() { }

        public MediaManifestGenerator(Stream output, bool continuousPersistence) : base(output, continuousPersistence) { }

        public void Start(PlaylistType playlistType, int version, double targetDuration, int? discontinuitySequence = null, ServerControl? serverControl = null, double? partDuration = null)
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-PLAYLIST-TYPE:{playlistType}");
            if (serverControl.HasValue)
            {
                AppendLine(serverControl.Value.ToString());
            }
            AppendLine($"#EXT-X-TARGETDURATION:{targetDuration}");
            AppendLine($"#EXT-X-VERSION:{version}");
            AppendLine($"#EXT-X-MEDIA-SEQUENCE:0");

            if (discontinuitySequence.HasValue)
            {
                AppendLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{discontinuitySequence.Value}");
            }

            if (partDuration.HasValue)
            {
                AppendLine($"#EXT-X-PART-INF:PART-TARGET={partDuration.Value}");
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

        public void AddEncryption(EncryptionKind encryption, string uri = null, string iv = null, string keyformat = null, int[] keyformatVersions = null)
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

        public void AddPartialFile(string path, double duration, bool isIndependent = false, ByteRange? byteRange = null, bool hasGap = false)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-PART:DURATION={duration},URI=\"{path}\"");

            if (isIndependent)
            {
                builder.Append($",INDEPENDENT={(isIndependent ? "YES" : "NO")}");
            }

            if (byteRange.HasValue)
            {
                builder.Append($",BYTERANGE={byteRange.Value}");
            }

            if (hasGap)
            {
                builder.Append($",GAP={(hasGap ? "YES" : "NO")}");
            }

            builder.AppendNormalizedNewline();
            Append(builder.ToString());
        }

        public void InsertDiscontinuity()
        {
            AppendLine("#EXT-X-DISCONTINUITY");
        }

        public void AddByteRange(ByteRange byteRange)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-BYTERANGE:{byteRange}");

            builder.AppendNormalizedNewline();

            Append(builder.ToString());
        }

        public void AddMap(string uri, ByteRange? byteRange = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-MAP:URI=\"{uri}\"");

            if (byteRange.HasValue)
            {
                builder.Append($",BYTERANGE=\"{byteRange.Value}\"");
            }

            builder.AppendNormalizedNewline();
            Append(builder.ToString());
        }

        public void AddPreloadHint(HintType type, string path, int? start = null, int? length = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-PRELOAD-HINT:TYPE={type},URI=\"{path}\"");

            if (start.HasValue)
            {
                builder.Append($",BYTERANGE-START=\"{start.Value}\"");
            }

            if (length.HasValue)
            {
                builder.Append($",BYTERANGE-LENGTH=\"{length.Value}\"");
            }

            builder.AppendNormalizedNewline();
            Append(builder.ToString());
        }

        public void AddRenditionReport(string path, int lastMsn, int? lastPart = null)
        {
            var builder = _stringBuilderPool.Get();
            builder.Append($"#EXT-X-RENDITION-REPORT:URI=\"{path}\",LAST-MSN={lastMsn}");

            if (lastPart.HasValue)
            {
                builder.Append($",LAST-PART={lastPart.Value}");
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

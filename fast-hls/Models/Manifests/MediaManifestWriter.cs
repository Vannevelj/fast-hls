using System.IO;
using System.Threading.Tasks;
using FastHls.Abstractions;

namespace FastHls.Models.Manifests
{
    public class MediaManifestWriter : AbstractManifestWriter
    {
        private readonly MediaManifest _manifest;
        private int? _lastMediaSequence = null;

        public MediaManifestWriter(MediaManifest manifest, Stream output) : base(output)
        {
            _manifest = manifest;
        }

        public async Task Render()
        {
            await RenderHeader();
            await RenderTimeline();

            // or, RenderDifference()
        }

        private async Task RenderHeader()
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-PLAYLIST-TYPE:{_manifest.PlaylistType}");
            if (_manifest.ServerControl.HasValue)
            {
                AppendLine(_manifest.ServerControl.Value.Render());
            }
            AppendLine($"#EXT-X-TARGETDURATION:{_manifest.TargetDuration}");
            AppendLine($"#EXT-X-VERSION:{_manifest.Version}");
            AppendLine($"#EXT-X-MEDIA-SEQUENCE:{_manifest.MediaSequence}");

            if (_manifest.DiscontinuitySequence.HasValue)
            {
                AppendLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{_manifest.DiscontinuitySequence.Value}");
            }

            if (_manifest.PartDuration.HasValue)
            {
                AppendLine($"#EXT-X-PART-INF:PART-TARGET={_manifest.PartDuration.Value}");
            }

            if (_manifest.Start.HasValue)
            {
                Append(_manifest.Start.Value.Render());
            }

            if (_manifest.Encryption.HasValue)
            {
                Append(_manifest.Encryption.Value.Render());
            }

            if (_manifest.Map.HasValue)
            {
                Append(_manifest.Map.Value.Render());
            }
        }

        private async Task RenderTimeline()
        {
            foreach (var file in _manifest.Timeline)
            {
                Append(file.Render());
            }

            foreach (var report in _manifest.RenditionReports)
            {
                Append(report.Render());
            }

            if (_manifest.PlaylistType == PlaylistType.VOD)
            {
                AppendLine(new EndList().Render());
            }
        }
    }
}

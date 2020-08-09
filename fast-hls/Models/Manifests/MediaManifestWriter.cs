using System.IO;
using System.Threading.Tasks;
using FastHls.Abstractions;

namespace FastHls.Models.Manifests
{
    public class MediaManifestWriter : AbstractManifestWriter
    {
        private readonly MediaManifest _manifest;

        public MediaManifestWriter(MediaManifest manifest, Stream output) : base(output)
        {
            _manifest = manifest;
        }

        public async Task Render()
        {
            RenderHeader();
            RenderTimeline();

            await WriteToOutput();

            // or, RenderDifference()
        }

        private void RenderHeader()
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

            if (_manifest.DiscontinuitySequence > 0)
            {
                AppendLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{_manifest.DiscontinuitySequence}");
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

        private void RenderTimeline()
        {
            var discontinuityCounter = 0;
            var mediaCounter = 0;

            foreach (var item in _manifest.Timeline)
            {
                if (_manifest.DiscontinuitySequence > discontinuityCounter && item is Discontinuity)
                {
                    discontinuityCounter++;
                    continue;
                }
                else if (_manifest.MediaSequence > mediaCounter && item is MediaFile)
                {
                    mediaCounter++;
                    continue;
                }
                Append(item.Render());
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

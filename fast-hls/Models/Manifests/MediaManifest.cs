using System.Collections.Generic;
using FastHls.Abstractions;
using FastHls.Models.Interfaces;

namespace FastHls.Models.Manifests
{
    public class MediaManifest : AbstractManifestGenerator
    {
        public int Version { get; }
        public PlaylistType PlaylistType { get; }

        public ServerControl? ServerControl { get; set; }
        public double TargetDuration { get; set; }
        public int MediaSequence { get; set; }
        public int? DiscontinuitySequence { get; set; }
        public double? PartDuration { get; set; }
        public Start? Start { get; set; }
        public Map? Map { get; set; }
        public Encryption? Encryption { get; set; }
        public PreloadHint PartPreloadHint { get; set; }
        public PreloadHint MapPreloadHint { get; set; }
        public List<RenditionReport> RenditionReports { get; set; } = new List<RenditionReport>();
        public List<ITimelineItem> Timeline { get; set; } = new List<ITimelineItem>();

        public MediaManifest(int version, PlaylistType playlistType)
        {
            Version = version;
            PlaylistType = playlistType;
        }

        public void Render()
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-PLAYLIST-TYPE:{PlaylistType}");
            if (ServerControl.HasValue)
            {
                AppendLine(ServerControl.Value.Render());
            }
            AppendLine($"#EXT-X-TARGETDURATION:{TargetDuration}");
            AppendLine($"#EXT-X-VERSION:{Version}");
            AppendLine($"#EXT-X-MEDIA-SEQUENCE:0");

            if (DiscontinuitySequence.HasValue)
            {
                AppendLine($"#EXT-X-DISCONTINUITY-SEQUENCE:{DiscontinuitySequence.Value}");
            }

            if (PartDuration.HasValue)
            {
                AppendLine($"#EXT-X-PART-INF:PART-TARGET={PartDuration.Value}");
            }

            if (Start.HasValue)
            {
                Append(Start.Value.Render());
            }

            if (Encryption.HasValue)
            {
                Append(Encryption.Value.Render());
            }

            if (Map.HasValue)
            {
                Append(Map.Value.Render());
            }

            foreach (var file in Timeline)
            {
                Append(file.Render());
            }

            foreach (var report in RenditionReports)
            {
                Append(report.Render());
            }

            if (PlaylistType == PlaylistType.VOD)
            {
                AppendLine("#EXT-X-ENDLIST");
            }
        }
    }
}
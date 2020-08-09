using System;
using System.Collections.Generic;
using FastHls.Models.Interfaces;

namespace FastHls.Models.Manifests
{
    public class MediaManifest
    {
        public int Version { get; }
        public PlaylistType PlaylistType { get; }
        public double TargetDuration { get; }

        public ServerControl? ServerControl { get; }
        public int MediaSequence { get; private set; }
        public int DiscontinuitySequence { get; private set; }
        public double? PartDuration { get; }
        public Start? Start { get; }
        public Map? Map { get; }
        public Encryption? Encryption { get; }
        public PreloadHint PartPreloadHint { get; }
        public PreloadHint MapPreloadHint { get; }
        public List<RenditionReport> RenditionReports { get; } = new List<RenditionReport>();
        internal List<ITimelineItem> Timeline { get; } = new List<ITimelineItem>();

        public MediaManifest(
            int version,
            PlaylistType playlistType,
            double targetDuration,
            ServerControl? serverControl = null,
            double? partDuration = null,
            Start? start = null,
            Map? map = null,
            Encryption? encryption = null)
        {
            Version = version;
            PlaylistType = playlistType;
            TargetDuration = targetDuration;
            ServerControl = serverControl;
            PartDuration = partDuration;
            Start = start;
            Map = map;
            Encryption = encryption;
        }

        public void Add(ITimelineItem timelineItem) => Timeline.Add(timelineItem);

        public void AddAndIncrementSequence(ITimelineItem timelineItem)
        {
            Add(timelineItem);
            switch (timelineItem)
            {
                case MediaFile _:
                    MediaSequence++;
                    break;
                case Discontinuity _:
                    DiscontinuitySequence = DiscontinuitySequence + 1;
                    break;
                default:
                    throw new ArgumentException($"Unsupported value: {timelineItem}");
            }
        }
    }
}
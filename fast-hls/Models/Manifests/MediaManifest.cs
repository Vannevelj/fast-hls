namespace FastHls.Models.Manifests
{
    public class MediaManifest
    {
        public PlaylistType PlaylistType { get; set; }
        public ServerControl? ServerControl { get; set; }
        public double TargetDuration { get; set; }
        public int Version { get; set; }
        public int MediaSequence { get; set; }
        public int? DiscontinuitySequence { get; set; }
        public double? PartDuration { get; set; }
        public Start? Start { get; set; }
        public PreloadHint PartPreloadHint { get; set; }
        public PreloadHint MapPreloadHint { get; set; }
        public List<ITimelineItem> Timeline { get; set; }
    }
}
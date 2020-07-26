using System;

namespace FastHls.Models.Manifests
{
    public struct Start
    {
        public TimeSpan Offset { get; set; }
        public bool? IsPrecise { get; set; }
    }
}
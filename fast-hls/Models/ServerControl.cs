using System.IO;
using FastHls.Extensions;
using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct ServerControl : IManifestItem
    {
        public bool CanBlockReload { get; set; }
        public double? CanSkipUntil { get; set; }
        public double? HoldBack { get; set; }
        public double? PartHoldBack { get; set; }

        public void Render(StreamWriter writer)
        {
            writer.Write($"#EXT-X-SERVER-CONTROL:CAN-BLOCK-RELOAD={( CanBlockReload ? "YES" : "NO" )}");
            if (CanSkipUntil.HasValue)
            {
                writer.Write($",CAN-SKIP-UNTIL={CanSkipUntil.Value}");
            }

            if (HoldBack.HasValue)
            {
                writer.Write($",HOLD-BACK={HoldBack.Value}");
            }

            if (PartHoldBack.HasValue)
            {
                writer.Write($",PART-HOLD-BACK={PartHoldBack.Value}");
            }

            writer.AppendNormalizedNewline();
        }
    }
}
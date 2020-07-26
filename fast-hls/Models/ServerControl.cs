namespace FastHls.Models
{
    public struct ServerControl
    {
        public bool CanBlockReload { get; set; }
        public double? CanSkipUntil { get; set; }
        public double? HoldBack { get; set; }
        public double? PartHoldBack { get; set; }

        public override string ToString()
        {
            var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
            var sb = pool.Get();

            sb.Append($"#EXT-X-SERVER-CONTROL:CAN-BLOCK-RELOAD={(CanBlockReload ? "YES" : "NO")}");
            if (CanSkipUntil.HasValue)
            {
                sb.Append($",CAN-SKIP-UNTIL={CanSkipUntil.Value}");
            }

            if (HoldBack.HasValue)
            {
                sb.Append($",HOLD-BACK={HoldBack.Value}");
            }

            if (PartHoldBack.HasValue)
            {
                sb.Append($",PART-HOLD-BACK={PartHoldBack.Value}");
            }

            return sb.ToString();
        }
    }
}
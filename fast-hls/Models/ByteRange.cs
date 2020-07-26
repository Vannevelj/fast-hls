using FastHls.Models.Interfaces;

namespace FastHls.Models
{
    public struct ByteRange
    {
        public int Length { get; set; }
        public int? Offset { get; set; }

        public ByteRange(int length, int? offset = null)
        {
            Length = length;
            Offset = offset;
        }

        public override string ToString() => Offset.HasValue ? $"{Length}@{Offset.Value}" : $"{Length}";
    }
}
namespace FastHls.Models
{
    public struct Resolution : IEquatable<Resolution>
    {
        public short Height { get; }
        public short Width { get; }

        public Resolution(short height, short width)
        {
            Height = height;
            Width = width;
        }

        public bool Equals(Resolution other) => other.Height == Height && other.Width == Width;
    }
}
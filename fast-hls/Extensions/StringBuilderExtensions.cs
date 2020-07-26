using System.Text;

namespace FastHls.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void AppendNormalizedNewline(this StringBuilder builder)
        {
            builder.Append("\r\n");
        }

        public static void AppendNormalizedline(this StringBuilder builder, string text)
        {
            builder.Append($"{text}\r\n");
        }
    }
}
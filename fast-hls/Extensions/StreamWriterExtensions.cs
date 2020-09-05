using System.IO;

namespace FastHls.Extensions
{
    public static class StreamWriterExtensions
    {
        public static void AppendNormalizedNewline(this StreamWriter writer) => writer.Write("\r\n");

        public static void AppendNormalizedline(this StreamWriter writer, string text) => writer.Write($"{text}\r\n");
    }
}
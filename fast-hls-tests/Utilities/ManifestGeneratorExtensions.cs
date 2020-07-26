namespace FastHlsTests
{
    internal static class ManifestGeneratorExtensions
    {
        public static async Task AssertGeneratedContent(this AbstractManifestGenerator generator, string content)
        {
            content = content.Replace("\r\n", "\n").Replace("\n", "\r\n");
            var outputStream = new MemoryStream();
            await generator.WriteToStream(outputStream);
            outputStream.Position = 0;
            var output = await new StreamReader(outputStream).ReadToEndAsync();

            Assert.Equal(content, output);
        }
    }
}
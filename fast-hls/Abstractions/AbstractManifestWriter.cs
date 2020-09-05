using System.IO;
using System.Threading.Tasks;
using FastHls.Extensions;

namespace FastHls.Abstractions
{
    public abstract class AbstractManifestWriter
    {
        private readonly Stream _output;
        protected readonly StreamWriter Writer;

        public AbstractManifestWriter(Stream output)
        {
            _output = output;
            Writer = new StreamWriter(output);
        }

        protected void Append(string text) => Writer.Write(text);

        protected void AppendLine(string text) => Writer.AppendNormalizedline(text);

        protected async ValueTask WriteToOutput() => await Writer.FlushAsync();

        public virtual async Task Finish()
        {
            if (_output != null)
            {
                await _output.DisposeAsync();
            }
        }
    }
}
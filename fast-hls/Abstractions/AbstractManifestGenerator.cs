using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FastHls.Abstractions
{
    public abstract class AbstractManifestGenerator
    {
        private readonly StringBuilder _playlist = new StringBuilder();
        private readonly bool _continuousPersistence;
        private readonly Stream _output;

        public AbstractManifestGenerator() : this(null, false) { }

        public AbstractManifestGenerator(Stream output, bool continuousPersistence)
        {
            _output = output;
            _continuousPersistence = continuousPersistence;
        }

        protected void Append(string text) {
            _playlist.Append(text);
        }

        public async ValueTask WriteToStream(Stream output)
        {
            await output.WriteAsync(Encoding.UTF8.GetBytes(_playlist.ToString()));
        }

        protected async ValueTask WriteContinuously(string text)
        {
            if (_continuousPersistence && _output != null)
            {
                await _output.WriteAsync(Encoding.UTF8.GetBytes(text));
            }
        }

        public virtual async Task Finish()
        {
            if (_output != null)
            {
                await _output.DisposeAsync();
            }
            //_playlist.Clear(); // Removes Gen1 allocations in some cases but increases overall allocs
        }
    }
}
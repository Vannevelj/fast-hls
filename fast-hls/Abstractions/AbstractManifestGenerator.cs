using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Extensions;
using Microsoft.Extensions.ObjectPool;

namespace FastHls.Abstractions
{
    public abstract class AbstractManifestGenerator
    {
        private readonly DefaultObjectPoolProvider _objectPoolProvider;
        protected readonly ObjectPool<StringBuilder> _stringBuilderPool;

        private readonly StringBuilder _playlist;
        private readonly bool _continuousPersistence;
        private readonly Stream _output;

        public AbstractManifestGenerator() : this(null, false) { }

        public AbstractManifestGenerator(Stream output, bool continuousPersistence)
        {
            _output = output;
            _continuousPersistence = continuousPersistence;

            _objectPoolProvider = new DefaultObjectPoolProvider();
            _stringBuilderPool = _objectPoolProvider.CreateStringBuilderPool();
            _playlist = _stringBuilderPool.Get();
        }

        protected void Append(string text) {
            _playlist.Append(text);
            // await WriteContinuously(text);
        }

        protected void AppendLine(string text) {
            _playlist.AppendNormalizedline(text);
        }

        public async ValueTask WriteToStream(Stream output)
        {
            await output.WriteAsync(Encoding.UTF8.GetBytes(_playlist.ToString()));
        }

        private async ValueTask WriteContinuously(string text)
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
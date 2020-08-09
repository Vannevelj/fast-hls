using System.IO;
using System.Text;
using System.Threading.Tasks;
using FastHls.Extensions;
using Microsoft.Extensions.ObjectPool;

namespace FastHls.Abstractions
{
    public abstract class AbstractManifestWriter
    {
        private readonly DefaultObjectPoolProvider _objectPoolProvider;
        private readonly ObjectPool<StringBuilder> _stringBuilderPool;

        private readonly StringBuilder _playlist;
        private readonly Stream _output;

        public AbstractManifestWriter(Stream output)
        {
            _output = output;

            _objectPoolProvider = new DefaultObjectPoolProvider();
            _stringBuilderPool = _objectPoolProvider.CreateStringBuilderPool();
            _playlist = _stringBuilderPool.Get();
        }

        protected void Append(string text)
        {
            _playlist.Append(text);
            // await WriteContinuously(text);
        }

        protected void AppendLine(string text)
        {
            _playlist.AppendNormalizedline(text);
        }

        protected async ValueTask WriteToOutput()
        {
            await _output.WriteAsync(Encoding.UTF8.GetBytes(_playlist.ToString()));
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
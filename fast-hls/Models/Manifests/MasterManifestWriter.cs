using System.IO;
using System.Threading.Tasks;
using FastHls.Abstractions;

namespace FastHls.Models.Manifests
{
    public class MasterManifestWriter : AbstractManifestWriter
    {
        private readonly MasterManifest _masterManifest;

        public MasterManifestWriter(MasterManifest masterManifest, Stream output): base(output)
        {
            _masterManifest = masterManifest;
        }

        public async Task Render()
        {
            AppendLine($"#EXTM3U");
            AppendLine($"#EXT-X-VERSION:{_masterManifest.Version}");

            if (_masterManifest.HasIndependentSegments.HasValue && _masterManifest.HasIndependentSegments.Value)
            {
                AppendLine($"#EXT-X-INDEPENDENT-SEGMENTS");
            }

            foreach (var media in _masterManifest.Media)
            {
                media.Render(Writer);
            }

            foreach (var variantStream in _masterManifest.VariantStreams)
            {
                variantStream.Render(Writer);
            }

            foreach (var iframeVariantStream in _masterManifest.IFrameVariantStreams)
            {
                iframeVariantStream.Render(Writer);
            }

            foreach (var sessionData in _masterManifest.SessionData)
            {
                sessionData.Render(Writer);
            }

            await WriteToOutput();
        }
    }
}

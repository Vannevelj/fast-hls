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
                Append(media.Render());
            }

            foreach (var variantStream in _masterManifest.VariantStreams)
            {
                Append(variantStream.Render());
            }

            foreach (var iframeVariantStream in _masterManifest.IFrameVariantStreams)
            {
                Append(iframeVariantStream.Render());
            }

            foreach (var sessionData in _masterManifest.SessionData)
            {
                Append(sessionData.Render());
            }

            await WriteToOutput();
        }
    }
}

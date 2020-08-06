using FastHls.Models;
using Xunit;

namespace FastHlsTests.Models
{
    public class ServerControlTests
    {
        [Theory]
        [InlineData(true, 3, 2, 1, "#EXT-X-SERVER-CONTROL:CAN-BLOCK-RELOAD=YES,CAN-SKIP-UNTIL=3,HOLD-BACK=2,PART-HOLD-BACK=1")]
        public void WritesServerControl(bool canBlockReload, double? canSkipUntil, double? holdBack, double? partHoldBack, string expected)
        {
            var serverControl = new ServerControl
            {
                CanBlockReload = canBlockReload,
                CanSkipUntil = canSkipUntil,
                HoldBack = holdBack,
                PartHoldBack = partHoldBack
            };
            Assert.Equal(expected, serverControl.Render());
        }
    }
}

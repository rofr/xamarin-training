using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Tests
{
    public class MockBluetoothServices : IBluetoothServices
    {
        public Task<List<string>> ScanNearbyDevices()
        {
            return Task.FromResult(new List<string>()
            {
                "{CFD33AFF-7C17-4456-872C-89D75E4A8F05}",
                "{3A44AC14-FA16-4D1E-95EF-A18598BFF170}"
            });
        }
    }
}
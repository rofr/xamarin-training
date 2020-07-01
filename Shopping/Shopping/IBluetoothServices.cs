using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping
{
    public interface IBluetoothServices
    {
        Task<List<string>> ScanNearbyDevices();
    }
}
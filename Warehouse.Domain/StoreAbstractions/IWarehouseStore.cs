using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Domain.StoreAbstractions
{
    public interface IWarehouseStore
    {
        Task<Warehouse> SearchWarehouseAsync(string id);
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();
    }
}

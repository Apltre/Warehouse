using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Domain.StoreAbstractions
{
    public interface IRouteStore
    {
        Task<IEnumerable<Route>> GetRoutesByWarehouseAsync(Warehouse warehouse);
    }
}

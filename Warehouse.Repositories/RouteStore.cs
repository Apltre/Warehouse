using Microsoft.EntityFrameworkCore;
using Warehouse.LegacyData;
using Warehouse.Domain;
using Warehouse.Domain.StoreAbstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Common;

namespace Warehouse.Repositories
{
    public class RouteStore : IRouteStore
    {
        public RouteStore(LegacyWarehouseContext legacyWarehouseContext)
        {
            LegacyWarehouseContext = legacyWarehouseContext;
        }

        protected LegacyWarehouseContext LegacyWarehouseContext { get; }

        public async Task<IEnumerable<Route>> GetRoutesByWarehouseAsync(Domain.Warehouse warehouse)
        {
            var key = LegacyKey.Parse(warehouse.Id);
            var query = from r in this.LegacyWarehouseContext.RouteCells
                         where r.StorageCell.StorageKey1 == key.Key1 && r.StorageCell.StorageKey2 == key.Key2
                         group r by new { r.RouteKey1, r.RouteKey2, r.Route.RouteNumber }
                         into grouped
                         select new Route($"{grouped.Key.RouteKey1}-{grouped.Key.RouteKey2}", warehouse, grouped.Key.RouteNumber);
            return await query.ToListAsync();
        }
    }
}

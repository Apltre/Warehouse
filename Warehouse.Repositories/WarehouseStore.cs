using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Domain.StoreAbstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.Repositories
{
    public class WarehouseStore : IWarehouseStore

    {
        public WarehouseStore(WarehouseContext warehouseContext)
        {
            WarehouseContext = warehouseContext;
        }

        protected WarehouseContext WarehouseContext { get; }

        public async Task<Domain.Warehouse> SearchWarehouseAsync(string id)
        {
            return await this.WarehouseContext.Warehouses.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Warehouse>> GetWarehousesAsync()
        {
            return await this.WarehouseContext.Warehouses.ToListAsync();
        }
    }
}
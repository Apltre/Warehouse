using Warehouse.LegacyData;
using Warehouse.Data;
using Warehouse.Domain.StoreAbstractions;
using System.Threading.Tasks;

namespace Warehouse.Repositories
{
    public class UnitOfWork
    {
        public UnitOfWork(LegacyWarehouseContext legacyWarehouseContext, WarehouseContext warehouseContext, IRouteStore routeStore, IWarehouseStore warehouseStore, ITerminalStore terminalStore)
        {
            LegacyWarehouseContext = legacyWarehouseContext;
            WarehouseContext = warehouseContext;
            RouteStore = routeStore;
            WarehouseStore = warehouseStore;
            TerminalStore = terminalStore;
        }

        public LegacyWarehouseContext LegacyWarehouseContext { get; }
        public WarehouseContext WarehouseContext { get; }
        public IRouteStore RouteStore { get; }
        public IWarehouseStore WarehouseStore { get; }
        public ITerminalStore TerminalStore { get; }

        public async Task CommitAsync() {
            await this.LegacyWarehouseContext.SaveChangesAsync();
            await this.WarehouseContext.SaveChangesAsync();
        }
    }
}

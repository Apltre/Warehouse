using System.Collections.Generic;

namespace Warehouse.Protocol.Models
{
    public class WarehousesList
    {
        public IEnumerable<Warehouse> Warehouses { get; set; }
    }
}
using System.Collections.Generic;

namespace Warehouse.LegacyData.Entity
{
    public class StorageCell : LegacyTable
    {
        public int StorageKey1 { get; set; }
        public int StorageKey2 { get; set; }
        public List<RouteCell> RouteCells { get; set; }
        public int? CellKey1 { get; set; }
        public int? CellKey2 { get; set; }
    }
}
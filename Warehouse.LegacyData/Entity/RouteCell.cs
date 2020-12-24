namespace Warehouse.LegacyData.Entity
{
    public class RouteCell : LegacyTable
    {
        public int RouteKey1 { get; set; }
        public int RouteKey2 { get; set; }
        public int StorageCellKey1 { get; set; }
        public int StorageCellKey2 { get; set; }
        public Route Route { get; set; }
        public StorageCell StorageCell { get; set; }
    }
}
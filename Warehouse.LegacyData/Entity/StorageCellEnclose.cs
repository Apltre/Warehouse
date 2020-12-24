namespace Warehouse.LegacyData.Entity
{
    public class StorageCellEnclose : LegacyTable
    {
        public int StorageCellKey1 { get; set; }
        public int StorageCellKey2 { get; set; }
        public int EncloseKey1 { get; set; }
        public int EncloseKey2 { get; set; }
        public StorageCell StorageCell { get; set; }
    }
}
using Warehouse.LegacyData.Enums;

namespace Warehouse.LegacyData.Entity
{
    public class DeliveryPoint : LegacyTable
    {
        public int StorageKey1 { get; set; }
        public int StorageKey2 { get; set; }
        public DeliveryPointType Type { get; set; }
    }
}
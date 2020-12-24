using Warehouse.LegacyData.Enums;

namespace Warehouse.LegacyData.Entity
{
    public class ItineraryInvoice
    {
        public int Key1 { get; set; }
        public int Key2 { get; set; }
        public int EncloseKey1 { get; set; }
        public int EncloseKey2 { get; set; }
        public int Priority { get; set; }
        public ScanState ScanState { get; set; }
        public string DeliveryPointNumber  { get; set; }

        public Enclose Enclose { get; set; }
        public StorageCellEnclose StorageCellEnclose { get; set; }
    }
}
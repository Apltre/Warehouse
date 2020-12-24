using Warehouse.LegacyData.Enums;

namespace Warehouse.Queries.Filters
{
    public class CellItinararyFilter
    {
        public string StorageCellId { get; set; }
        public ScanState? TakeType { get; set; }
    }
}

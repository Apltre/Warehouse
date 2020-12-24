using System.Collections.Generic;

namespace Warehouse.LegacyData.Entity
{
    public class Route : LegacyTable
    {
        public string RouteNumber { get; set; }
        public List<RouteCell> RouteCells { get; set; }
    }
}
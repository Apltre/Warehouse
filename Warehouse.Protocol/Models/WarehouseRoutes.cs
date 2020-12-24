using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Protocol.Models
{
    public class WarehouseRoutes
    {
        public int Count => this.Routes.Count();
        public IEnumerable<Route> Routes { get; set; } = new List<Route>();
    }
}

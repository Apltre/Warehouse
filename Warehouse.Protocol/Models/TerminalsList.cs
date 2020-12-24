using System.Collections.Generic;

namespace Warehouse.Protocol.Models
{
    public class TerminalsList
    {
        public IEnumerable<TerminalWithoutWarehouse> Terminals { get; set; }
    }
}

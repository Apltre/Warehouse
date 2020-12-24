using System.Collections.Generic;

namespace Warehouse.Protocol.Models
{
    public class CellInfo
    {
        public string Id { get; set; }
        public ItineraryStatus Status { get; set; }
        public UnscannedItinenaryEnclosesList Encloses { get; set; }

        public class UnscannedItinenaryEnclosesList
        {
            public int Count { get; set; }
            public IEnumerable<UnscannedItinenaryEnclose> Elements { get; set; } = new List<UnscannedItinenaryEnclose>();
        }

        public class UnscannedItinenaryEnclose
        {
            public string Id { get; set; }
            public string InvoiceId { get; set; }
            public EncloseClient Client { get; set; }
            public bool Priority { get;set; }
            public EncloseDimensions Dimensions { get; set; }
        }

        public class ItineraryStatus
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

        public class EncloseClient 
        {
            public string Name { get; set; }
        }

        public class EncloseDimensions
        {
            public double? Width { get; set; }
            public double? Length { get; set; }
            public double? Depth { get; set; }
            public string Text { get; set; }
        }
    }
}


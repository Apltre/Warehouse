using System.Collections.Generic;

namespace Warehouse.Queries.Models
{
    public class CellInfo
    {
        public string StorageCellId { get; set; }
        public Status ItineraryStatusForCell => this.UnscannedEnclosesCount > 0 ? Status.Partially : Status.Fully;
        public int UnscannedEnclosesCount => this.UnscannedItinenaryEncloses.Count;
        public List<UnscannedItinenaryEnclose> UnscannedItinenaryEncloses { get; set; } = new List<UnscannedItinenaryEnclose>();

        public class UnscannedItinenaryEnclose
        {
            public string BarCode { get; set; }
            public string InvoiceNumber { get; set; }
            public string Client { get; set; }
            public bool Priority { get; set; }
            public double? Width { get; set; }
            public double? Length { get; set; }
            public double? Height { get; set; }
        }

        public class StatusObject
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

        public enum Status
        {
            Partially,
            Fully
        }
    }
}


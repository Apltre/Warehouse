namespace Warehouse.LegacyData.Entity
{
    public class Enclose : LegacyTable
    {
        public string Barcode { get; set; }

        public int InvoiceKey1 { get; set; }

        public int InvoiceKey2 { get; set; }

        public Invoice Invoice { get; set; }

        public double? Width { get; set; }

        public double? Length { get; set; }

        public double? Height { get; set; }

        public int StorageKey1 { get; set; }

        public int StorageKey2 { get; set; } 

        public DeliveryPoint DeliveryPoint { get; set; }
    }
}

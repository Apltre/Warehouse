namespace Warehouse.LegacyData.Entity
{
    public class Invoice : LegacyTable
    {
        public int ContractKey1 { get; set; }
        public int ContractKey2 { get; set; }

        public string Number { get; set; }
        public Contract Contract { get; set; }
    }
}

namespace Warehouse.LegacyData.Entity
{
    public class Contract : LegacyTable
    {
        public int ClientKey1 { get; set; }
        public int ClientKey2 { get; set; }
        public Client Client { get; set; }
    }
}

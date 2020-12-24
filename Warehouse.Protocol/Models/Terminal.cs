
namespace Warehouse.Protocol.Models
{
    public class Terminal
    {
        public string Id { get; set; }
        public Warehouse Warehouse { get; set; }
        public bool IsActive { get; set; }
    }
}

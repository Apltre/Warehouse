using Warehouse.Common;

namespace Warehouse.LegacyData.Entity
{
    public class LegacyTable
    {
        public int Key1 { get;set; }
        public int Key2 { get; set; }

        public LegacyKey Key
        {
            get => new LegacyKey(this.Key1, this.Key2);
            set
            {
                this.Key1 = value.Key1;
                this.Key2 = value.Key2;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class DeliveryPointConfiguration : LegacyTableConfiguration<DeliveryPoint>
    {
        public override void Configure(EntityTypeBuilder<DeliveryPoint> model)
        {
            base.Configure(model);
            model.ToTable("DeliveryPointInfo");
            model.Property(d => d.StorageKey1).HasColumnName("storage_key1");
            model.Property(d => d.StorageKey2).HasColumnName("storage_key2");
            model.Property(d => d.Type).HasColumnName("point_type");
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;
using Warehouse.LegacyData.Mappings;

namespace Agents.Data.Mappings.EntityFrameworkCoreConfigurations
{
    public class StorageCellEncloseConfiguration : LegacyTableConfiguration<StorageCellEnclose>
    {
        public override void Configure(EntityTypeBuilder<StorageCellEnclose> model)
        {
            base.Configure(model);

            model.Property(k => k.StorageCellKey1).HasColumnName("storage_cell_key1");
            model.Property(k => k.StorageCellKey2).HasColumnName("storage_cell_key2");
            model.Property(k => k.EncloseKey1).HasColumnName("enclose_key1");
            model.Property(k => k.EncloseKey2).HasColumnName("enclose_key2");
            model.HasOne(k => k.StorageCell)
                .WithMany()
                .HasForeignKey(k => new { k.StorageCellKey1, k.StorageCellKey2 })
                .HasPrincipalKey(k => new { k.Key1, k.Key2 });
        }
    }
}
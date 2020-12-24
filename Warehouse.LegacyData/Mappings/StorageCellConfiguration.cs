using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;
using Warehouse.LegacyData.Mappings;

namespace Agents.Data.Mappings.EntityFrameworkCoreConfigurations
{
    public class StorageCellConfiguration : LegacyTableConfiguration<StorageCell>
    {
        public override void Configure(EntityTypeBuilder<StorageCell> model)
        {
            base.Configure(model);

            model.Property(k => k.StorageKey1).HasColumnName("storage_key1");
            model.Property(k => k.StorageKey2).HasColumnName("storage_key2");
            model.Property(k => k.CellKey1).HasColumnName("cell_key1");
            model.Property(k => k.CellKey2).HasColumnName("cell_key2");

            model.HasMany(k => k.RouteCells)
                .WithOne(r => r.StorageCell)
                .HasPrincipalKey(k => new { k.Key1, k.Key2 })
                .HasForeignKey(r => new { r.StorageCellKey1, r.StorageCellKey2 });
        }
    }
}
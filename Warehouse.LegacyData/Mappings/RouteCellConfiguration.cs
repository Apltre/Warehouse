using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class RouteCellConfiguration : LegacyTableConfiguration<RouteCell>
    {
        public override void Configure(EntityTypeBuilder<RouteCell> model)
        {
            base.Configure(model);
            model.Property(r => r.RouteKey1).HasColumnName("route_key1");
            model.Property(r => r.RouteKey2).HasColumnName("route_key2");
            model.Property(r => r.StorageCellKey1).HasColumnName("storage_cell_key1");
            model.Property(r => r.StorageCellKey2).HasColumnName("storage_cell_key2");
        }
    }
}

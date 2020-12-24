using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{

    public class RouteConfiguration : LegacyTableConfiguration<Route>
    {
        public override void Configure(EntityTypeBuilder<Route> model)
        {
            base.Configure(model);
            model.HasMany(r => r.RouteCells)
                .WithOne(rc => rc.Route)
                .HasPrincipalKey(r => new { r.Key1, r.Key2 })
                .HasForeignKey(rc => new { rc.RouteKey1, rc.RouteKey2 });
        }
    }
}

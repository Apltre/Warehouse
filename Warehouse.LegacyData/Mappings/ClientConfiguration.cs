using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class ClientConfiguration : LegacyTableConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> model)
        {
            base.Configure(model);
            model.Property(m => m.Name).HasColumnName("company_name");
        }
    }
}
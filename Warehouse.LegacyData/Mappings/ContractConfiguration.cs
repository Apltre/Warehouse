using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class ContractConfiguration : LegacyTableConfiguration<Contract>
    {
        public override void Configure(EntityTypeBuilder<Contract> model)
        {
            base.Configure(model);
            model.Property(m => m.ClientKey1).HasColumnName("client_key1");
            model.Property(m => m.ClientKey2).HasColumnName("client_key2");
            model.HasOne(c => c.Client)
               .WithMany()
               .HasForeignKey(c => new { c.ClientKey1, c.ClientKey2 })
               .HasPrincipalKey(c => new { c.Key1, c.Key2});
        }
    }
}

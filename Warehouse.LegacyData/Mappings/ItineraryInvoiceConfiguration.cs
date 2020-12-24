using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class ItineraryInvoiceConfiguration : IEntityTypeConfiguration<ItineraryInvoice>
    {
        public void Configure(EntityTypeBuilder<ItineraryInvoice> model)
        {           
            model.HasKey(m => new { m.Key1, m.Key2 });
            model.Property(m => m.Key1).HasColumnName("key1");
            model.Property(m => m.Key2).HasColumnName("key2");
            model.Property(m => m.DeliveryPointNumber).HasColumnName("point_number");
            model.Property(m => m.EncloseKey1).HasColumnName("enclose_key1");
            model.Property(m => m.EncloseKey2).HasColumnName("enclose_key2");
            model.HasOne(m => m.StorageCellEnclose)
                .WithOne()
                .HasPrincipalKey<ItineraryInvoice>(m => new { m.EncloseKey1, m.EncloseKey2 })
                .HasForeignKey<StorageCellEnclose>(k => new { k.EncloseKey1, k.EncloseKey2 });

            model.HasOne(m => m.Enclose)
                .WithMany()
                .HasForeignKey(m => new { m.EncloseKey1, m.EncloseKey2 })
                .HasPrincipalKey(e => new { e.Key1, e.Key2 });
        }
    }
}

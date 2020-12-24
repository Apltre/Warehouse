using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class EncloseConfiguration : LegacyTableConfiguration<Enclose>
    {
        public override void Configure(EntityTypeBuilder<Enclose> model)
        {
            base.Configure(model);
            model.Property(e => e.InvoiceKey1).HasColumnName("invoice_key1");
            model.Property(e => e.InvoiceKey2).HasColumnName("invoice_key2");

            model.Property(e => e.Width);
            model.Property(e => e.Length);
            model.Property(e => e.Height);
            model.Property(e => e.StorageKey1).HasColumnName("storage_key1");
            model.Property(e => e.StorageKey2).HasColumnName("storage_key2");

            model.HasOne(e => e.Invoice)
                .WithMany()
                .HasForeignKey(e => new { e.InvoiceKey1, e.InvoiceKey2 })
                .HasPrincipalKey(i => new { i.Key1, i.Key2 });

            model.HasOne(e => e.DeliveryPoint)
                .WithMany()
                .HasForeignKey(e => new { e.StorageKey1, e.StorageKey2 })
                .HasPrincipalKey(d => new { d.StorageKey1, d.StorageKey2 });
        }
    }
}

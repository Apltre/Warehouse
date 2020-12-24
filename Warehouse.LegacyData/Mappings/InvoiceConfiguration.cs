using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class InvoiceConfiguration : LegacyTableConfiguration<Invoice>
    {
        public override void Configure(EntityTypeBuilder<Invoice> model)
        {
            base.Configure(model);

            model.Property(e => e.ContractKey1).HasColumnName("contaract_key1");
            model.Property(e => e.ContractKey2).HasColumnName("contract_key2");

            model.Property(e => e.Number)
                .HasColumnName("invoice_number")
                .HasConversion(p => p, p => p.TrimEnd());

            model.HasOne(i => i.Contract)
               .WithMany()
               .HasForeignKey(i => new { i.ContractKey1, i.ContractKey2 })
               .HasPrincipalKey(c => new { c.Key1, c.Key2 });
        }
    }
}
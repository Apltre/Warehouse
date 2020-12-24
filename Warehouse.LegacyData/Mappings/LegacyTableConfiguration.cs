using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.LegacyData.Entity;

namespace Warehouse.LegacyData.Mappings
{
    public class LegacyTableConfiguration<T> : IEntityTypeConfiguration<T>
    where T : LegacyTable
    {
        public virtual void Configure(EntityTypeBuilder<T> model)
        {
            model.Ignore(m => m.Key);
            model.HasKey(t => new { t.Key1, t.Key2 });
        }
    }
}

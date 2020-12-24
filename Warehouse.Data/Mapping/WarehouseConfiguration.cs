using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Data.Entities
{
	public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Warehouse>
	{
		public void Configure(EntityTypeBuilder<Domain.Warehouse> model)
		{
			model.HasKey(w => w.Id);
			model.Property(w => w.CreatedOn)
				.HasDefaultValueSql("NOW()");
		}
	}
}

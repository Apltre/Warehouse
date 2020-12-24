using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain;

namespace Warehouse.Data.Entities
{
	public class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
	{
		public void Configure(EntityTypeBuilder<Terminal> model)
		{
			model.HasKey(t => t.Id);
			model.Property(t => t.CreatedOn)
				.HasDefaultValueSql("NOW()");
			model.HasOne(t => t.Warehouse);
		}
	}
}

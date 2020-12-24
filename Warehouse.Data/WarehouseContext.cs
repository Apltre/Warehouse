using Microsoft.EntityFrameworkCore;
using Warehouse.Data.Entities;
using Warehouse.Domain;

namespace Warehouse.Data
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Domain.Warehouse> Warehouses { get; set; }
		public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
			modelBuilder.ApplyConfiguration(new TerminalConfiguration());
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Agents.Data.Mappings.EntityFrameworkCoreConfigurations;
using Warehouse.LegacyData.Entity;
using Warehouse.LegacyData.Mappings;

namespace Warehouse.LegacyData
{
    public class LegacyWarehouseContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteCell> RouteCells { get; set; }
        public DbSet<StorageCell> StorageCells { get; set; }
        public DbSet<ItineraryInvoice> ItineraryInvoices { get; set; }
        public DbSet<StorageCellEnclose> StorageCellEncloses { get; set; }
        public DbSet<Enclose> Encloses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DeliveryPoint> DeliveryPoints { get; set; }

        public LegacyWarehouseContext(DbContextOptions<LegacyWarehouseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new RouteCellConfiguration());
            modelBuilder.ApplyConfiguration(new StorageCellConfiguration());
            modelBuilder.ApplyConfiguration(new StorageCellEncloseConfiguration());
            modelBuilder.ApplyConfiguration(new ItineraryInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new EncloseConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryPointConfiguration());
        }
    }
}
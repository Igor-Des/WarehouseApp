using Microsoft.EntityFrameworkCore;
using WarehouseApp.Models;

namespace WarehouseApp.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TypeComponent> TypeComponents { get; set; }
        public DbSet<Component> Components { get; set; }
    }
}
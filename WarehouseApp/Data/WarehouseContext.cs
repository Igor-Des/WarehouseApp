using Microsoft.EntityFrameworkCore;
using WarehouseApp.Models;

namespace WarehouseApp.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options)
        {

        }

        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<TypeComponent> TypeComponent { get; set; }
        public DbSet<Component> Component { get; set; }
    }
}

{
}
}

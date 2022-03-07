using final_pharmacy.DbHandler.DbModel;
using Microsoft.EntityFrameworkCore;

namespace final_pharmacy.DbHandler.DbServices
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DataFile.db");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AuthLog> AuthLogs { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreDemo
{
    public class CustomerDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Customer>()
                .ToTable("tblCustomer","efcore");
            modelBuilder.Entity<Customer>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(x => x.Name).HasMaxLength(50);
        }
    }
}
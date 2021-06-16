using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker.Entries()
                .Where(c => c.Entity is BaseEntity && (c.State == EntityState.Added || c.State == EntityState.Modified));
            foreach (var item in entries)
            {
                ((BaseEntity)item.Entity).LastModified = DateTime.Now;
                if (item.State == EntityState.Added)
                {
                    ((BaseEntity)item.Entity).CreatedDate = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}

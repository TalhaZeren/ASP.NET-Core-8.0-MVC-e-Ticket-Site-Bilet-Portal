using BiletPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int> 
    {
        protected readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));


        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public  DbSet<Slider> Slider { get; set; }
        public DbSet<AppUser> AppUser { get; set; } 
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<SelectSeat> SelectSeat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SelectSeat>()
                .HasOne(seat => seat.Products)
                .WithMany()
                .HasForeignKey(seat => seat.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

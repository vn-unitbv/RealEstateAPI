using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .UseSqlServer("Server=LAPTOP-GLKGU87O\\SQLEXPRESS;Database=RealEstateAPI;Trusted_Connection=True;TrustServerCertificate=true;");
            //.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Announcement>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RealEstate>(
                dob =>
                {
                    dob.ToTable("Announcements");
                });

            modelBuilder.Entity<Announcement>(
                ob =>
                {
                    ob.ToTable("Announcements");
                    ob.HasOne(o => o.RealEstate).WithOne()
                        .HasForeignKey<RealEstate>(o => o.Id);
                    ob.Navigation(o => o.RealEstate).IsRequired();
                });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
	public class AppDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.AddJsonFile("appsettings.Development.json")
				.Build();
			optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
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
					dob.Property(o => o.Type)
						.HasConversion(new EnumToStringConverter<RealEstateType>());
				});

			modelBuilder.Entity<Announcement>(
				ob =>
				{
					ob.ToTable("Announcements");
					ob.HasOne(o => o.RealEstate).WithOne()
						.HasForeignKey<RealEstate>(o => o.Id);
					ob.Navigation(o => o.RealEstate).IsRequired();
					ob.Property(o => o.TransactionType)
						.HasConversion(new EnumToStringConverter<TransactionType>());
				});

			modelBuilder.Entity<User>()
				.Property(o => o.Role)
					.HasConversion(new EnumToStringConverter<UserRole>());
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Announcement> Announcements { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}

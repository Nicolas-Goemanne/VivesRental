using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VivesRental.Enums;
using VivesRental.Model;
using VivesRental.Repository.Extensions;
using Microsoft.AspNetCore.Identity;

namespace VivesRental.Repository.Core
{
    public class VivesRentalDbContext : IdentityDbContext<IdentityUser>
    {
        public VivesRentalDbContext(DbContextOptions<VivesRentalDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<ArticleReservation> ArticleReservations => Set<ArticleReservation>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RemovePluralizingTableNameConvention();
        }

        public void SeedData()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), FirstName = "Nicolas", LastName = "Goemanne", Email = "nicolas.test@example.com", PhoneNumber = "1234567890" },
                new Customer { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "converge@example.com", PhoneNumber = "0987654321" }
            };

            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Rode schop", Description = "Sneeuwschop voor de parking te ontruimen", Manufacturer = "Makita", Publisher = "Publisher 1", RentalExpiresAfterDays = 30 },
                new Product { Id = Guid.NewGuid(), Name = "Groene schop", Description = "Tuinwerk schop", Manufacturer = "Bosch", Publisher = "Publisher 2", RentalExpiresAfterDays = 60 }
            };

            var articles = new List<Article>
            {
                new Article { Id = Guid.NewGuid(), ProductId = products[0].Id, Status = ArticleStatus.Normal },
                new Article { Id = Guid.NewGuid(), ProductId = products[0].Id, Status = ArticleStatus.Normal },
                new Article { Id = Guid.NewGuid(), ProductId = products[0].Id, Status = ArticleStatus.Normal },
                new Article { Id = Guid.NewGuid(), ProductId = products[1].Id, Status = ArticleStatus.Normal },
                new Article { Id = Guid.NewGuid(), ProductId = products[1].Id, Status = ArticleStatus.Normal }
            };

            Customers.AddRange(customers);
            Products.AddRange(products);
            Articles.AddRange(articles);

            SaveChanges();
        }
    }

    public class VivesRentalDbContextFactory : IDesignTimeDbContextFactory<VivesRentalDbContext>
    {
        public VivesRentalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VivesRentalDbContext>();
            optionsBuilder.UseSqlServer("Server=HYZOKAN\\VIVES;Database=VivesRentalDb;User ID=sa;Password=Vives2023!;TrustServerCertificate=True;", b => b.MigrationsAssembly("VivesRental.Repository"));

            return new VivesRentalDbContext(optionsBuilder.Options);
        }
    }
}




















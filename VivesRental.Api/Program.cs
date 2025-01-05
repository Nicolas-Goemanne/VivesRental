using Microsoft.EntityFrameworkCore;
using VivesRental.Repository.Core;
using VivesRental.Sdk;
using VivesRental.Services.Abstractions;
using VivesRental.Services;
using VivesRental.Enums;
using VivesRental.Model;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "VivesRental API", Version = "v1" });
});

builder.Services.AddDbContext<VivesRentalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderLineService, OrderLineService>();
builder.Services.AddScoped<IArticleReservationService, ArticleReservationService>();

builder.Services.AddScoped<ProductSdk>();
builder.Services.AddScoped<ArticleSdk>();
builder.Services.AddScoped<CustomerSdk>();
builder.Services.AddScoped<OrderSdk>();
builder.Services.AddScoped<OrderLineSdk>();
builder.Services.AddScoped<ArticleReservationSdk>();

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<VivesRentalDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VivesRental API V1");
        c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root (http://localhost:<port>/)
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VivesRentalDbContext>();
    SeedData(dbContext);
}

app.Run();

void SeedData(VivesRentalDbContext context)
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

    context.Customers.AddRange(customers);
    context.Products.AddRange(products);
    context.Articles.AddRange(articles);

    context.SaveChanges();
}





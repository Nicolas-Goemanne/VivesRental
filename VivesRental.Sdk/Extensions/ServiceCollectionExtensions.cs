using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Sdk.Handlers;

namespace VivesRental.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddScoped<AuthorizationHandler>();

            services.AddHttpClient("VivesRentalApi", options =>
            {
                options.BaseAddress = new Uri(apiUrl);
            }).AddHttpMessageHandler<AuthorizationHandler>();

            services.AddScoped<IdentitySdk>();
            services.AddScoped<CustomerSdk>();
            services.AddScoped<OrderSdk>();
            services.AddScoped<ArticleSdk>();
            services.AddScoped<ProductSdk>();
            services.AddScoped<ArticleReservationSdk>();
            services.AddScoped<OrderLineSdk>();

            return services;
        }
    }
}

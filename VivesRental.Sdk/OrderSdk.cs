using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OrderResult?> Get(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/Order/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<List<OrderResult>> Find(OrderFilter? filter)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync("api/Order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderResult>>() ?? new List<OrderResult>();
        }

        public async Task<OrderResult?> CreateAsync(Guid customerId)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsync($"api/Order?customerId={customerId}", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<bool> Return(Guid orderId, DateTime returnedAt)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync($"api/Order/{orderId}/Return", returnedAt);
            return response.IsSuccessStatusCode;
        }
    }
}














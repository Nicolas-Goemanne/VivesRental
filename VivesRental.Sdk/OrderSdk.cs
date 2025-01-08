using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderSdk
    {
        private readonly HttpClient _httpClient;

        public OrderSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/orders/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<List<OrderResult>> Find(OrderFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderResult>>();
        }

        public async Task<OrderResult?> Create(Guid customerId)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/orders?customerId={customerId}", customerId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderResult>();
        }

        public async Task<bool> Return(Guid id, DateTime returnedAt)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}/return", returnedAt);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
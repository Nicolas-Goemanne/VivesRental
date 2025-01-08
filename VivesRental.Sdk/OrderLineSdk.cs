using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderLineSdk
    {
        private readonly HttpClient _httpClient;

        public OrderLineSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderLineResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/orderlines/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderLineResult>();
        }

        public async Task<List<OrderLineResult>> Find(OrderLineFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orderlines/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrderLineResult>>();
        }

        public async Task<OrderLineResult?> Create(OrderLineRequest entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orderlines", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderLineResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/orderlines/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}


















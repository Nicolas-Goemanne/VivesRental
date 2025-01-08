using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ProductSdk
    {
        private readonly HttpClient _httpClient;

        public ProductSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<List<ProductResult>> Find(ProductFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProductResult>>();
        }

        public async Task<ProductResult?> Create(ProductRequest entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<ProductResult?> Edit(Guid id, ProductRequest entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}


















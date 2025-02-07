using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ProductSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ProductResult?> Get(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/Product/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<List<ProductResult>> Find(ProductFilter? filter)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var queryString = GenerateQueryString(filter);
            var response = await httpClient.GetAsync($"api/Product{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProductResult>>() ?? new List<ProductResult>();
        }

        public async Task<List<ProductResult>> FindAllIncludingUnavailableArticles()
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");

            // Haal alle producten op
            var response = await httpClient.GetAsync("api/Product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProductResult>>() ?? new List<ProductResult>();
        }

        public async Task<ProductResult?> Create(ProductRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync("api/Product", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<ProductResult?> Edit(Guid id, ProductRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PutAsJsonAsync($"api/Product/{id}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.DeleteAsync($"api/Product/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> GenerateArticles(Guid productId, int amount)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync($"api/Product/{productId}/generate-articles?amount={amount}", new { });
            return response.IsSuccessStatusCode;
        }

        private string GenerateQueryString(ProductFilter? filter)
        {
            if (filter == null)
            {
                return string.Empty;
            }

            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (filter.AvailableFromDateTime.HasValue)
            {
                queryString["AvailableFromDateTime"] = filter.AvailableFromDateTime.Value.ToString("o");
            }

            if (filter.AvailableUntilDateTime.HasValue)
            {
                queryString["AvailableUntilDateTime"] = filter.AvailableUntilDateTime.Value.ToString("o");
            }

            return queryString.ToString() != string.Empty ? $"?{queryString}" : string.Empty;
        }
    }
}
































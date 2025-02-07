using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class CustomerSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CustomerResult?> Get(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/Customer/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<List<CustomerResult>> Find(CustomerFilter? filter)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var queryString = filter != null ? $"?Search={filter.Search}" : string.Empty;
            var response = await httpClient.GetAsync($"api/Customer{queryString}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CustomerResult>>();
        }

        public async Task<CustomerResult?> Create(CustomerRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync("api/Customer", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<CustomerResult?> Edit(Guid id, CustomerRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PutAsJsonAsync($"api/Customer/{id}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.DeleteAsync($"api/Customer/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}










using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class CustomerSdk
    {
        private readonly HttpClient _httpClient;

        public CustomerSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/customers/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<List<CustomerResult>> Find(CustomerFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CustomerResult>>();
        }

        public async Task<CustomerResult?> Create(CustomerRequest entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<CustomerResult?> Edit(Guid id, CustomerRequest entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{id}", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
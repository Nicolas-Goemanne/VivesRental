using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Enums;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleSdk
    {
        private readonly HttpClient _httpClient;

        public ArticleSdk(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArticleResult?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/articles/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<List<ArticleResult>> Find(ArticleFilter? filter)
        {
            var response = await _httpClient.PostAsJsonAsync("api/articles/find", filter);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ArticleResult>>();
        }

        public async Task<ArticleResult?> Create(ArticleRequest entity)
        {
            var response = await _httpClient.PostAsJsonAsync("api/articles", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleResult>();
        }

        public async Task<bool> UpdateStatus(Guid articleId, ArticleStatus status)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/articles/{articleId}/status", status);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Remove(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/articles/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}


















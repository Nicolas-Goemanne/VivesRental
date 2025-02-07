using System.Net.Http;
using System.Net.Http.Json;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleReservationSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ArticleReservationSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ArticleReservationResult?> Get(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/ArticleReservation/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }

        public async Task<List<ArticleReservationResult>> Find(ArticleReservationFilter? filter = null)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var query = filter != null
                ? $"?articleId={filter.ArticleId}&customerId={filter.CustomerId}"
                : string.Empty;

            var response = await httpClient.GetAsync($"api/ArticleReservation{query}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ArticleReservationResult>>() ?? new List<ArticleReservationResult>();
        }

        public async Task<ArticleResult?> FindFirstReservableArticle(Guid productId)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.GetAsync($"api/Article?productId={productId}");
            response.EnsureSuccessStatusCode();
            var articles = await response.Content.ReadFromJsonAsync<List<ArticleResult>>() ?? new List<ArticleResult>();

            // Retourneer het eerste artikel met status Normal
            return articles.FirstOrDefault(a => a.Status == VivesRental.Enums.ArticleStatus.Normal);
        }

        public async Task<ArticleReservationResult?> Create(ArticleReservationRequest entity)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.PostAsJsonAsync("api/ArticleReservation", entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
        }

        public async Task<bool> Remove(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesRentalApi");
            var response = await httpClient.DeleteAsync($"api/ArticleReservation/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}























